using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OpenXmlHelpers.Word;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Layers
{
    public class ImpressionFicheSuivi /*: Controller*/
    {
        private string _pathFile = null;

        private readonly AFPANADbContext _dbContext;
        private readonly IConfiguration _config;

        private readonly IHostEnvironment _env;

        public ImpressionFicheSuivi(AFPANADbContext context, IConfiguration config, IHostEnvironment env)
        {
            _dbContext = context;
            _config = config;
            _env = env;

        }
        public string PathFile { get; private set; }
        public string FolderFile { get => Path.Combine(_env.ContentRootPath, "ModelesOffice"); }

        public Pee GetDataBeneficiairePeeById(int id)
        {

            return _dbContext.Pees.Include(P => P.MatriculeBeneficiaireNavigation)
                .ThenInclude(S => S.CodeTitreCiviliteNavigation)
                .Include(pee => pee.IdResponsableJuridiqueNavigation)
                .ThenInclude(T => T.TitreCiviliteNavigation)
                .Include(t => t.IdTuteurNavigation)
                .ThenInclude(T => T.TitreCiviliteNavigation)
                .Include(Offre => Offre.Id)
                .Include(P => P.IdEntrepriseNavigation)
                .Include(E => E.IdResponsableJuridiqueNavigation.TitreCiviliteNavigation)
                .FirstOrDefault(pee => pee.IdPee == id);
        }
        public OffreFormation GetEntrepriseProfessionnel(int id)
        {
            var pee = GetDataBeneficiairePeeById(id);
            return _dbContext.OffreFormations.Include(O => O.MatriculeCollaborateurAfpaNavigation)
                .Where(C => C.IdOffreFormation == pee.IdOffreFormation && C.IdEtablissement == pee.IdEtablissement).FirstOrDefault();
        }
        public EntrepriseProfessionnel GetEntrepriseProfessionnelData(int id)
        {
            var pee = GetDataBeneficiairePeeById(id);
            return _dbContext.EntrepriseProfessionnels
                .FirstOrDefault(F => F.IdProfessionnel == pee.IdResponsableJuridique);
        }
        public PeriodePee GetPeriodePeeByPeeId(int id)
        {
            return _dbContext.PeriodePees.FirstOrDefault(p => p.IdPee == id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<string> pathFileModelFollowUpDocumentPee(int value, int id)
        {
            List<string> listPath = new List<string>();
            var pee = GetDataBeneficiairePeeById(id);
            if (value == 1)
            {
                string PathFileEnvoie = Path.Combine(FolderFile, "2-Lettre_Envoi_Convention.docx");
                string PathFileConvention = pee.MatriculeBeneficiaireNavigation.CodeTitreCiviliteNavigation.TitreCiviliteComplet == "Monsieur" ? 
                    Path.Combine(FolderFile, "1-ConventionPE_M.docx") : Path.Combine(FolderFile, "1-ConventionPE_F.docx");
                listPath.Add(PathFileEnvoie);
                listPath.Add(PathFileConvention);
                
            }

            switch (value)
            {

                case 2:
                    listPath.Add(Path.Combine(FolderFile, "3-Lettre_Retour_Signee.docx"));
                    break;
                case 3:
                    listPath.Add(Path.Combine(FolderFile, "4-Lettre_Accompagnement_Tuteur.docx"));
                    break;
                default:
                    break;

            }
            return listPath;
        }
        /// <summary>
        /// Get WordprocessingDocument Document 
        /// </summary>
        /// <param name="pee"></param>
        /// <param name="Collaborateur"></param>
        /// <param name="poste"></param>
        /// <param name="Periodepee"></param>
        /// <param name="docPath"></param>
        /// <returns></returns>
        public WordprocessingDocument GetModifiedDocument(Pee pee, OffreFormation Collaborateur, EntrepriseProfessionnel poste, PeriodePee Periodepee, string docPath)
        {
            
            WordprocessingDocument doc;
            using (doc = WordprocessingDocument.Open(docPath, true))
            {

                var mergeFields = doc.GetMergeFields().ToList();
                mergeFields.WhereNameIs("Entreprise").ReplaceWithText(pee.IdEntrepriseNavigation.RaisonSociale);
                mergeFields.WhereNameIs("Adresse1").ReplaceWithText(pee.IdEntrepriseNavigation.Ligne1Adresse);
                mergeFields.WhereNameIs("Adresse2").ReplaceWithText(pee.IdEntrepriseNavigation.Ligne2Adresse );
                mergeFields.WhereNameIs("Adresse3").ReplaceWithText(pee.IdEntrepriseNavigation.Ligne3Adresse);
                mergeFields.WhereNameIs("Code_Postal").ReplaceWithText(pee.IdEntrepriseNavigation.CodePostal);
                mergeFields.WhereNameIs("Commune").ReplaceWithText(pee.IdEntrepriseNavigation.Ville);
                mergeFields.WhereNameIs("TITRE_REP").ReplaceWithText(pee.IdResponsableJuridiqueNavigation.TitreCiviliteNavigation.TitreCiviliteComplet);
                mergeFields.WhereNameIs("Représentant").ReplaceWithText(pee.IdResponsableJuridiqueNavigation.NomProfessionnel + " " + pee.IdResponsableJuridiqueNavigation.PrenomProfessionnel);
                mergeFields.WhereNameIs("Fonction_Représentant").ReplaceWithText(poste.Fonction);
                mergeFields.WhereNameIs("Formation1").ReplaceWithText(pee.Id.LibelleOffreFormation);
                mergeFields.WhereNameIs("Titre_Formateur").ReplaceWithText(Collaborateur.MatriculeCollaborateurAfpaNavigation.CodeTitreCiviliteNavigation.TitreCiviliteComplet);
                mergeFields.WhereNameIs("Formateur").ReplaceWithText(Collaborateur.MatriculeCollaborateurAfpaNavigation.NomCollaborateur + " " + Collaborateur.MatriculeCollaborateurAfpaNavigation.PrenomCollaborateur);
                mergeFields.WhereNameIs("TélFormateur").ReplaceWithText(Collaborateur.MatriculeCollaborateurAfpaNavigation.TelCollaborateurAfpa);
                mergeFields.WhereNameIs("Début_stage").ReplaceWithText(Periodepee.DateDebutPeriodePee.ToString("dd-MM-yyyy"));
                mergeFields.WhereNameIs("Fin_stage").ReplaceWithText(Periodepee.DateFinPeriodePee.ToString("dd-MM-yyyy"));
                mergeFields.WhereNameIs("Titre_Stagiaire").ReplaceWithText(pee.MatriculeBeneficiaireNavigation.CodeTitreCiviliteNavigation.TitreCiviliteComplet);
                mergeFields.WhereNameIs("Prénom_Stagiaire").ReplaceWithText(pee.MatriculeBeneficiaireNavigation.PrenomBeneficiaire);
                mergeFields.WhereNameIs("NOM_Stagiaire").ReplaceWithText(pee.MatriculeBeneficiaireNavigation.NomBeneficiaire);
                mergeFields.WhereNameIs("Titre_Tuteur_1").ReplaceWithText(pee.IdTuteurNavigation.TitreCiviliteNavigation.TitreCiviliteComplet);
                mergeFields.WhereNameIs("Tuteur1").ReplaceWithText($"{pee.IdTuteurNavigation.PrenomProfessionnel} {pee.IdTuteurNavigation.NomProfessionnel}");

                doc.MainDocumentPart.Document.Save();
            }
            return doc;
        }
        /// <summary>
        /// Get Path new file 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>

        public List<string> GetPathFile(int id, int value)
        {
            List<string> ListPathDoc = new List<string>();
            var pee = GetDataBeneficiairePeeById(id);
            List<string> pathModele = pathFileModelFollowUpDocumentPee(value, id);
            var poste = GetEntrepriseProfessionnelData(id);
            var Collaborateur = GetEntrepriseProfessionnel(id);
            var Periodepee = GetPeriodePeeByPeeId(id);
            string PathDoc = null;
            if (pathModele.Count() == 1)
            {
                for (int i = 0; i < pathModele.Count(); i++)
                {
                    PathDoc = pathModele[i];
                    string newNameFile = PathDoc.Replace(".docx", $"_{(DateTime.Now - DateTime.MinValue).TotalMilliseconds}.docx");
                    string docPath = @$"{newNameFile}";
                    System.IO.File.Copy($"{PathDoc}", docPath, true);
                    GetModifiedDocument(pee, Collaborateur, poste, Periodepee, docPath);
                    ListPathDoc.Add(docPath);
                }
            }
            else
            {
                for (int i = 0; i < pathModele.Count(); i++)
                {
                    PathDoc = pathModele[i];
                    string newNameFile = PathDoc.Replace(".docx", $"_{(DateTime.Now - DateTime.MinValue).TotalMilliseconds}.docx");
                    string docPath = @$"{newNameFile}";
                    System.IO.File.Copy($"{PathDoc}", docPath, true);
                    GetModifiedDocument(pee, Collaborateur, poste, Periodepee, docPath);
                    ListPathDoc.Add(docPath);
                }
            }

            return ListPathDoc;
        }


        ///code Romain
        ///
        public BeneficiaireOffreFormation GetDemandeurAbsence(string matricule)
        {
            return _dbContext.BeneficiaireOffreFormations
                .Include(b => b.MatriculeBeneficiaireNavigation)
                .Include(b => b.Id).FirstOrDefault();
        }
        public string RequestForSuchLeave(string matricule)
        {
            var benenificiaire = GetDemandeurAbsence(matricule);
            string FloderPath = Path.Combine(_env.ContentRootPath, "ModelesOffice\\C6-2.03.Autorisation_d_absence .docx");
            string newNameFile = FloderPath.Replace(".docx", $"_{(DateTime.Now - DateTime.MinValue).TotalMilliseconds}.docx");
            string docPath = @$"{newNameFile}";
            System.IO.File.Copy($"{FloderPath}", docPath, true);
            using (WordprocessingDocument doc = WordprocessingDocument.Open(docPath, true))
            {
                var mergeFields = doc.GetMergeFields().ToList();
                mergeFields.WhereNameIs("NomBeneficiaire").ReplaceWithText(benenificiaire.MatriculeBeneficiaireNavigation.NomBeneficiaire);
                mergeFields.WhereNameIs("PrenomBeneficiaire").ReplaceWithText(benenificiaire.MatriculeBeneficiaireNavigation.PrenomBeneficiaire);
                mergeFields.WhereNameIs("LibelleOffreFormation").ReplaceWithText(benenificiaire.Id.LibelleOffreFormation);
                doc.Save();
            }
            return docPath;
        }
    }
}
