using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView
{
    public class PeeModelView
    {
        public PeeModelView()
        {
            //PeriodePees = new HashSet<PeriodePee>();
        }

        public PeeModelView( Pee pee )
        {
            if (pee is not null)
            {
                //PeriodePees = new HashSet<PeriodePee>();
                IdPee = pee.IdPee;
                MatriculeBeneficiaire = pee.MatriculeBeneficiaire;
                IdTuteur = pee.IdTuteur;
                IdResponsableJuridique = pee.IdResponsableJuridique;
                IdEntreprise = pee.IdEntreprise;
                IdOffreFormation = pee.IdOffreFormation;
                IdEtablissement = pee.IdEtablissement;
                Remarques = "";
                Etat = pee.Etat;
                Id = new OffreFormationModelView(pee.Id);
                IdEntrepriseNavigation = new EntrepriseModelView(pee.IdEntrepriseNavigation);
                MatriculeBeneficiaireNavigation = new BeneficiaireModelView(pee.MatriculeBeneficiaireNavigation);
                IdResponsableJuridiqueNavigation = new ProfessionnelModelView(pee.IdResponsableJuridiqueNavigation);
                IdTuteurNavigation = new ProfessionnelModelView(pee.IdTuteurNavigation);
            }
        }

        public decimal IdPee { get; set; }
        public string MatriculeBeneficiaire { get; set; }
        public int IdTuteur { get; set; }
        public int IdResponsableJuridique { get; set; }
        public int IdEntreprise { get; set; }
        public int IdOffreFormation { get; set; }
        public string IdEtablissement { get; set; }
        public string Remarques { get; set; }   // il n'existe pas dans la base pour le moment à rajouter par la suite
        public int Etat { get; set; }

        public virtual EntrepriseModelView IdEntrepriseNavigation { get; set; }
        public virtual OffreFormationModelView Id { get; set; }
        public virtual ProfessionnelModelView IdResponsableJuridiqueNavigation { get; set; }
        public virtual ProfessionnelModelView IdTuteurNavigation { get; set; }
        public virtual BeneficiaireModelView MatriculeBeneficiaireNavigation { get; set; }
        //public virtual ICollection<PeriodePee> PeriodePees { get; set; }
    }
}
