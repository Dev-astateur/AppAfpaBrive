using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace AppAfpaBrive.Web.Layers
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
        public Beneficiaire FinByMatricule(string id)
        {
            return _context.Beneficiaires.Where(x => x.MatriculeBeneficiaire == id).SingleOrDefault();
        }

        //Methode pour chercher les stagiaires par offre de formation
        public ICollection<Beneficiaire> GetBeneficiaireParIdOffreDeFormation(int id)
        {
            return _context.BeneficiaireOffreFormations
                .Where(x => x.IdOffreFormation == id)
                .Include(x => x.MatriculeBeneficiaireNavigation)
                .Select(x => x.MatriculeBeneficiaireNavigation).ToList();
        }


        //public PagingList<Beneficiaire> ListeBenefPagines(int page = 1, string libelle)
        //{
        //    var qry = _context.Beneficiaires.OrderBy(x => x.NomBeneficiaire);
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

        //Methodes CRUD
        public void InsertBeneficiaire(Beneficiaire beneficiaire)
        {

            _context.Beneficiaires.Add(beneficiaire);
            _context.SaveChanges();
        }
        public void RemoveBeneficiare(Beneficiaire beneficiaire)
        {
            _context.Beneficiaires.Remove(beneficiaire);
            _context.SaveChanges();
        }
        public void UpdateBeneficiaire(Beneficiaire beneficiaire)
        {
            _context.Beneficiaires.Update(beneficiaire);
            _context.SaveChanges();
        }
    }
}
