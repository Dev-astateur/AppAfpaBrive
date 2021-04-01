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
        //private string _pathFile = null;

        private AFPANADbContext _dbContext;
       

        private IHostEnvironment _env;
        //public ImpressionFicheSuivi(AFPANADbContext context)
        //{
        //    _dbContext = context;
        //}

        public ImpressionFicheSuivi(AFPANADbContext context, IHostEnvironment env)
        {
            _dbContext = context;
            
            _env = env;

        }
        //public string PathFile { get; private set; }
        public string FolderFile { get => Path.Combine(_env.ContentRootPath, "ModelesOffice"); }

        public async Task<Pee> GetDataBeneficiairePeeById(int id)
        {

            return await _dbContext.Pees.Include(P => P.MatriculeBeneficiaireNavigation)
                .ThenInclude(S => S.CodeTitreCiviliteNavigation)
                .Include(pee => pee.IdResponsableJuridiqueNavigation)
                .ThenInclude(T => T.TitreCiviliteNavigation)
                .Include(t => t.IdTuteurNavigation)
                .ThenInclude(T => T.TitreCiviliteNavigation)
                .Include(Offre => Offre.Id)
                .Include(P => P.IdEntrepriseNavigation)
                .Include(E => E.IdResponsableJuridiqueNavigation.TitreCiviliteNavigation)
                .FirstOrDefaultAsync(pee => pee.IdPee == id);
        }
        public async Task<OffreFormation> GetEntrepriseProfessionnel(int id)
        {
            var pee = GetDataBeneficiairePeeById(id).Result;
            return await _dbContext.OffreFormations.Include(O => O.MatriculeCollaborateurAfpaNavigation)
                .Where(C => C.IdOffreFormation == pee.IdOffreFormation && C.IdEtablissement == pee.IdEtablissement).FirstOrDefaultAsync();
        }
        public async Task<EntrepriseProfessionnel> GetEntrepriseProfessionnelData(int id)
        {
            var pee =  await GetDataBeneficiairePeeById(id);
            return await _dbContext.EntrepriseProfessionnels
                .FirstOrDefaultAsync(F => F.IdProfessionnel == pee.IdResponsableJuridique);
        }
       public async Task<PeriodePee> GetPeriodePeeByPeeId(int id)
        {
            return await _dbContext.PeriodePees.FirstOrDefaultAsync(p => p.IdPee == id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<string>> pathFileModelFollowUpDocumentPee(int value, int id)
        {
            List<string> listPath = new List<string>();
            var pee = await GetDataBeneficiairePeeById(id);
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
            return  listPath;
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
        private WordprocessingDocument GetModifiedDocument(Pee pee, OffreFormation Collaborateur, EntrepriseProfessionnel poste, PeriodePee Periodepee, string docPath)
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

        public async Task<List<string>> GetPathFile(int id, int value)
        {
           List<string> ListPathDoc = new List<string>();
            var pee = await GetDataBeneficiairePeeById (id);
            List<string> pathModele = await pathFileModelFollowUpDocumentPee(value, id);
            var poste = await GetEntrepriseProfessionnelData (id);
            var Collaborateur = await GetEntrepriseProfessionnel (id);
            var Periodepee = await GetPeriodePeeByPeeId (id);
            string PathDoc = null;
            if (pathModele.Count() == 1)
            {
                for (int i = 0; i < pathModele.Count(); i++)
                {
                    PathDoc = pathModele[i];
                    string newNameFile = PathDoc.Replace(".docx", $"_{pee.MatriculeBeneficiaireNavigation.NomBeneficiaire}_{(DateTime.Now - DateTime.MinValue).TotalMilliseconds}.docx");
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
        public async Task<BeneficiaireOffreFormation> GetDemandeurAbsence(string matricule)
        {
            return await _dbContext.BeneficiaireOffreFormations
                .Include(b => b.MatriculeBeneficiaireNavigation)
                .Include(b => b.Id).FirstOrDefaultAsync();
        }
        public string RequestForSuchLeave(string matricule)
        {
            var benenificiaire = GetDemandeurAbsence(matricule).Result;
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
