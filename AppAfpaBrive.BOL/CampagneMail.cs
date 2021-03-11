using System;
using System.Collections.Generic;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class CampagneMail
    {
        public CampagneMail()
        {
            PlanificationCampagneMails = new HashSet<PlanificationCampagneMail>();
        }

        public int IdCampagneMail { get; set; }
        public DateTime DateCreation { get; set; }
        public string Description { get; set; }
        public int IdOffreFormation { get; set; }
        public string IdEtablissement { get; set; }

        public virtual OffreFormation Id { get; set; }
        public virtual ICollection<PlanificationCampagneMail> PlanificationCampagneMails { get; set; }
    }
}
