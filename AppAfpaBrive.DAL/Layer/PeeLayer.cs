using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.DAL;

namespace AppAfpaBrive.Web.Models.Layout
{
    public class PeeLayer
    {
        private readonly AFPANADbContext _dbContext = null;

        public PeeLayer (AFPANADbContext context) 
        {
            _dbContext = context;
        }

        public Pee TestRetour()
        {
            Pay pays = _dbContext.Pays.Find("US");

            Entreprise entreprise = new Entreprise()
            {
                IdEntreprise = 1,
                RaisonSociale = "Apple Distribution",
                NumeroSiret = "FR18539565218",
                Ligne1Adresse = "One Apple Park Way",
                Ville = "Cupertino",
                CodePostal = "CA 95014",
                TelEntreprise = "1 408 996–1010",
                MailEntreprise = "apple@apple.com",
                Idpays2 = pays.Idpays2,
                Idpays2Navigation = pays
            };

            return new Pee() { 
                IdEntreprise = entreprise.IdEntreprise,
                IdEntrepriseNavigation = entreprise
            };
        }

    }
}
