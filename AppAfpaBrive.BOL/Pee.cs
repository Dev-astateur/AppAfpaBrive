using System;
using System.Collections.Generic;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class Pee
    {
        public Pee()
        {
            PeriodePees = new HashSet<PeriodePee>();
        }

        public decimal IdPee { get; set; }
        public string MatriculeBeneficiaire { get; set; }
        public int IdTuteur { get; set; }
        public int IdResponsableJuridique { get; set; }
        public int IdEntreprise { get; set; }
        public int IdOffreFormation { get; set; }
        public string IdEtablissement { get; set; }

        public virtual OffreFormation Id { get; set; }
        public virtual Professionnel IdResponsableJuridiqueNavigation { get; set; }
        public virtual Professionnel IdTuteurNavigation { get; set; }
        public virtual Beneficiaire MatriculeBeneficiaireNavigation { get; set; }
        public virtual ICollection<PeriodePee> PeriodePees { get; set; }
    }
}
