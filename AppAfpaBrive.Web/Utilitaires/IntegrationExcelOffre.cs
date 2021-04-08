using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Utilitaires
{
    public class IntegrationExcelOffre
    {
        private readonly IConfiguration _config;
        private readonly AFPANADbContext _context;
        public IntegrationExcelOffre(IConfiguration config, AFPANADbContext context)
        {
            _config = config;
            _context = context;
        }


        public void IntegrerDonnees(string matriculeCollaborateurAfpa, int codeProduitFormation, string filePath)
        {
            var configuration = _config.GetSection("ImportDataOffreBeneficiaires").Get<ImportDataOffreBeneficiaires>();
            //"N° établissement,N° offre,Libellé de l'offre,N° Client Stagiaire,
            //Nom usuel,Prénom,Date de Naissance,Téléphone 1,Email,
            //Date de début résa,Date de fin résa,
            //Date d’entrée en formation,Date de sortie de formation"
            string[] colonnes = configuration.ListeColonnes.Split(',');
            int?[] positions = new int?[colonnes.Length];
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                if (reader.Read()) // Lecture première ligne Entêtes
                {
                    // Vérification de la présence des colonnes nécessaires.
                    // Si une absente, Exception et arrêt intégration
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string colName = reader.GetString(i).Trim();
                        int pos = Array.IndexOf(colonnes, colName);
                        if (pos != -1)
                        {
                            positions[pos] = i;
                        }


                    }
                    // Une colonne n'a pas été trouvée -> arrêt du traitement
                    if (positions.Any(position => position == null))
                    {
                        throw new ApplicationException("Des colonnes manquent au niveau du fichier. Extraction abandonnée");
                    }
                    TraiterDonnees(reader, positions, matriculeCollaborateurAfpa, codeProduitFormation);
                }
                // Fichier vide Exception
                else
                {
                    throw new ApplicationException("Fichier Vide. Extraction abandonnée");
                }


            }
        }


        private void TraiterDonnees(IExcelDataReader reader, int?[] positions, string matriculeCollaborateurAfpa, int codeProduitFormation)
        {
            bool offreTraitee = false;
            DateTime dateDebOffre ;
            DateTime dateFinOffre ;
            string idEtab = string.Empty;
            int idOffre = 0;
            string libOffre;
            OffreFormation offreFormation = null;
            Beneficiaire beneficiaire;
            BeneficiaireOffreFormation beneficiaireOffre;
            while (reader.Read())
            {
                //"N° établissement,N° offre,Libellé de l'offre,N° Client Stagiaire,
                //Nom usuel,Prénom,Date de Naissance,Téléphone 1,Email,
                //Date de début résa,Date de fin résa
                //Date d’entrée en formation,Date de sortie de formation"
                if (!offreTraitee)
                {
                    idEtab = reader.GetString(positions[0].Value);
                    idOffre = (int)reader.GetDouble(positions[1].Value);
                    libOffre = reader.GetString(positions[2].Value);
                    dateDebOffre = DateTime.Parse(reader.GetString(positions[9].Value));
                    dateFinOffre = DateTime.Parse(reader.GetString(positions[10].Value));
                    offreFormation = _context.OffreFormations.Find(new object[] { idOffre, idEtab });
                    if (offreFormation == null)
                    {
                        offreFormation = new OffreFormation()
                        {
                            IdEtablissement = idEtab,
                            IdOffreFormation = idOffre,
                            Etat = EntityPOCOState.Added
                        };
                    }
                    else
                    {
                        offreFormation.Etat = EntityPOCOState.Modified;
                    }
                    offreFormation.LibelleOffreFormation = libOffre;
                    offreFormation.DateFinOffreFormation = dateFinOffre;
                    offreFormation.DateDebutOffreFormation = dateDebOffre;
                    offreFormation.MatriculeCollaborateurAfpa = matriculeCollaborateurAfpa;
                    offreFormation.CodeProduitFormation = codeProduitFormation;
                    offreTraitee = true;
                }

                string numeroClient;
                string nomUsuel;
                string prenom;
                DateTime? dateNaissance;
                string telephone1;
                string mail;
                DateTime dateEntree;
                DateTime? dateSortie;
                //"N° établissement,N° offre,Libellé de l'offre,N° Client Stagiaire,
                //Nom usuel,Prénom,Date de Naissance,Téléphone 1,Email,
                //Date de début résa,Date de fin résa
                //Date d’entrée en formation,Date de sortie de formation"

                // Uniquement les stagiaires entrés en formation
                // Date Entrée différent du symbole -
                if (reader.GetString(positions[11].Value) != "-")
                {
                    numeroClient = reader.GetString(positions[3].Value);
                    nomUsuel = reader.GetString(positions[4].Value);
                    prenom = reader.GetString(positions[5].Value);
                    dateNaissance = DateTime.Parse(reader.GetString(positions[6].Value));
                    telephone1 = reader.GetString(positions[7].Value);
                    mail = reader.GetString(positions[8].Value);
                    dateEntree = DateTime.Parse(reader.GetString(positions[11].Value));
                    if(reader.GetString(positions[12].Value) != "-")
                    {
                        dateSortie = DateTime.Parse(reader.GetString(positions[12].Value));
                    }
                    else
                    {
                        dateSortie = null;
                    }
                    //dateSortie = (reader.GetString(positions[12].Value) != "-")
                    //    ? DateTime.Parse(reader.GetString(positions[12].Value))
                    //    : null;

                    beneficiaire = _context.Beneficiaires.Find(new object[] { numeroClient });
                    // beneficiaire = _context.Beneficiaires.FirstOrDefault(c => c.MatriculeBeneficiaire == numeroClient);
                    if (beneficiaire == null)
                    {
                        beneficiaire = new Beneficiaire()
                        {
                            MatriculeBeneficiaire = numeroClient,
                            Etat = EntityPOCOState.Added
                        };

                    }
                    else
                    {
                        beneficiaire.Etat = EntityPOCOState.Modified;
                    }
                    beneficiaire.NomBeneficiaire = nomUsuel;
                    beneficiaire.PrenomBeneficiaire = prenom;
                    beneficiaire.DateNaissanceBeneficiaire = dateNaissance.Value;
                    beneficiaire.TelBeneficiaire = telephone1;
                    beneficiaire.MailBeneficiaire = mail;
                     

                    _context.Entry<Beneficiaire>(beneficiaire).State = (EntityState)beneficiaire.Etat;
                    beneficiaireOffre = _context.BeneficiaireOffreFormations.Find(new object[] { numeroClient, idOffre, idEtab });
                    if (beneficiaireOffre == null)
                    {
                        beneficiaireOffre = new BeneficiaireOffreFormation()
                        {
                            Idetablissement = idEtab,
                            IdOffreFormation = idOffre,
                            MatriculeBeneficiaire = numeroClient,
                            Suppleant = false,
                            Delegue = false,
                            Etat = EntityPOCOState.Added
                        };
                    }
                    else
                    {
                        beneficiaireOffre.Etat = EntityPOCOState.Modified;
                    }
                    beneficiaireOffre.DateEntreeBeneficiaire = dateEntree;
                    beneficiaireOffre.DateSortieBeneficiaire = dateSortie;
                    _context.Entry<BeneficiaireOffreFormation>(beneficiaireOffre).State = (EntityState)beneficiaire.Etat;
                }
            }
            if (offreFormation != null)
            {
                _context.Entry<OffreFormation>(offreFormation).State = (EntityState)offreFormation.Etat;
                _context.SaveChanges();
            }

        }


    }

}

