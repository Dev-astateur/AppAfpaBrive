using System;
using System.Collections.Generic;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class EntrepriseProfessionnel
    {
        public int IdEntreprise { get; set; }
        public int IdProfessionnel { get; set; }
        public string AdresseMailPro { get; set; }
        public string TelephonePro { get; set; }
        public bool? Actif { get; set; }
        public string Fonction { get; set; }
        public virtual Entreprise IdEntrepriseNavigation { get; set; }
        public virtual Professionnel IdProfessionnelNavigation { get; set; }
    }
}
