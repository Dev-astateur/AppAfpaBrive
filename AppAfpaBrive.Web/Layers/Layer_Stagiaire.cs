using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.ModelView;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Layers
{

    public class Layer_Stagiaire
    {
        private readonly AFPANADbContext _context;
        public Layer_Stagiaire(AFPANADbContext context)
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

        public async Task<ICollection<Beneficiaire>> GetBeneficiaireParLibelleOffreDeFormationAsync(string libelle)
        {
            return await _context.OffreFormations
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
                    .ToListAsync();
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
        
        public BeneficiaireModelView UpdateBeneficiaire(BeneficiaireModelView beneficiaire)
        {
            Beneficiaire benef = new Beneficiaire()
            {
                MatriculeBeneficiaire = beneficiaire.MatriculeBeneficiaire,
                CodeTitreCivilite = beneficiaire.CodeTitreCivilite,
                NomBeneficiaire = beneficiaire.NomBeneficiaire,
                PrenomBeneficiaire = beneficiaire.PrenomBeneficiaire,
                DateNaissanceBeneficiaire = beneficiaire.DateNaissanceBeneficiaire,
                MailBeneficiaire = beneficiaire.MailBeneficiaire,
                TelBeneficiaire = beneficiaire.TelBeneficiaire,
                Ligne1Adresse = beneficiaire.Ligne1Adresse,
                Ligne2Adresse = beneficiaire.Ligne2Adresse,
                Ligne3Adresse = beneficiaire.Ligne3Adresse,
                CodePostal = beneficiaire.CodePostal,
                Ville = beneficiaire.Ville,
                UserId = beneficiaire.UserId,
                IdPays2 = beneficiaire.IdPays2,
                PathPhoto = beneficiaire.PathPhoto,
                MailingAutorise = beneficiaire.MailingAutorise

            };
            _context.Beneficiaires.Update(benef);
            _context.SaveChanges();
            return beneficiaire;
        }

    }
}
