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

        public decimal IdPee { get; set; }
        public string MatriculeBeneficiaire { get; set; }
        public int IdTuteur { get; set; }
        public int IdResponsableJuridique { get; set; }
        public int IdEntreprise { get; set; }
        public int IdOffreFormation { get; set; }
        public string IdEtablissement { get; set; }

        //public virtual EntrepriseModelView IdEntrepriseNavigation { get; set; }
        public virtual OffreFormationModelView Id { get; set; }
        //public virtual Professionnel IdResponsableJuridiqueNavigation { get; set; }
        //public virtual Professionnel IdTuteurNavigation { get; set; }
        public virtual BeneficiaireModelView MatriculeBeneficiaireNavigation { get; set; }
        //public virtual ICollection<PeriodePee> PeriodePees { get; set; }
    }
}
