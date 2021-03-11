using System;
using System.Collections.Generic;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class DestinataireEnquete
    {
        public Guid IdSoumissionnaire { get; set; }
        public int IdPlanificationCampagneMail { get; set; }
        public string MatriculeBeneficiaire { get; set; }
        public int IdOffreFormation { get; set; }
        public string IdEtablissement { get; set; }
        public DateTime DateEcheanceEnquete { get; set; }
        public DateTime? DateRealisationEnquete { get; set; }
        //public int EtatTraitementQuestionnaire { get; set; }
        public bool? Agrege { get; set; }
        //public int? EtapeQuestionnaire { get; set; }
        public DateTime? DateRelance1 { get; set; }
        public DateTime? DateRelance2 { get; set; }
        public int? IdContrat { get; set; }
        public bool? EnEmploi { get; set; }

        public virtual Contrat IdContratNavigation { get; set; }
        public virtual PlanificationCampagneMail IdPlanificationCampagneMailNavigation { get; set; }
        public virtual Beneficiaire MatriculeBeneficiaireNavigation { get; set; }
    }
}
