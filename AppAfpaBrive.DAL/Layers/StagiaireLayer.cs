using AppAfpaBrive.BOL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAfpaBrive.DAL.Layers
{

    public class StagiaireLayer
    {
        private readonly AFPANADbContext _context;
        public StagiaireLayer(AFPANADbContext context)
        {
            _context = context;
        }

        //Methode pour aller chercher tous les stagiaires
        public List<Beneficiaire> GetAllStagiaires()
        {
            return _context.Beneficiaires.ToList();
        }

        //Methode pour chercher les stagiaires par matricule
        public Beneficiaire FinByMatricule(string matricule)
        {
            return _context.Beneficiaires.Find(new object[] { matricule });
        }

        //Methode pour chercher les stagiaires par offre de formation
        //public ICollection<Beneficiaire> GetBeneficiaireParIdOffreDeFormation(int id)
        //{
        //    return _context.BeneficiaireOffreFormations
        //        .Where(x => x.IdOffreFormation == id)
        //        .Include(x => x.MatriculeBeneficiaireNavigation)
        //        .Select(x => x.MatriculeBeneficiaireNavigation).ToList();
        //}

        public ICollection<Beneficiaire> GetBeneficiaireParLibelleOffreDeFormation(string libelle)
        {
            return _context.OffreFormations
                    .Where(x => x.LibelleOffreFormation == libelle)
                    .Join(_context.BeneficiaireOffreFormations
                    , p => p.IdOffreFormation
                    , b => b.IdOffreFormation
                    , (p, b) => new
                    {
                        matricule = b.MatriculeBeneficiaire
                    })
                    .Join(_context.Beneficiaires
                    , be => be.matricule
                    , b => b.MatriculeBeneficiaire
                    , (be, b) => new Beneficiaire
                    {
                        MatriculeBeneficiaire = b.MatriculeBeneficiaire,
                        CodeTitreCivilite = b.CodeTitreCivilite,
                        NomBeneficiaire = b.NomBeneficiaire,
                        PrenomBeneficiaire = b.PrenomBeneficiaire,
                        DateNaissanceBeneficiaire = b.DateNaissanceBeneficiaire,
                        MailBeneficiaire = b.MailBeneficiaire,
                        TelBeneficiaire = b.TelBeneficiaire,
                        Ligne1Adresse = b.Ligne1Adresse,
                        Ligne2Adresse = b.Ligne2Adresse,
                        Ligne3Adresse = b.Ligne3Adresse,
                        CodePostal = b.CodePostal,
                        Ville = b.Ville,
                        UserId = b.UserId,
                        IdPays2 = b.IdPays2,
                        PathPhoto = b.PathPhoto,
                        MailingAutorise = b.MailingAutorise
                    })
                    .ToList();
        }
    }
}
