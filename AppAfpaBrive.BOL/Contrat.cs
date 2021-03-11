using System;
using System.Collections.Generic;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class Contrat
    {
        public Contrat()
        {
            DestinataireEnquetes = new HashSet<DestinataireEnquete>();
        }

        public int IdContrat { get; set; }
        public int IdEntreprise { get; set; }
        public string MatriculeBeneficiaire { get; set; }
        public int? CodeAppellation { get; set; }
        public DateTime DateEntreeFonction { get; set; }
        public DateTime? DateSortieFonction { get; set; }
        public int TypeContrat { get; set; }
        public int DureeContratMois { get; set; }
        public bool EnLienMetierFormation { get; set; }
        public string LibelleFonction { get; set; }

        public virtual Entreprise IdEntrepriseNavigation { get; set; }
        public virtual Beneficiaire MatriculeBeneficiaireNavigation { get; set; }
        public virtual TypeContrat TypeContratNavigation { get; set; }
        public virtual ICollection<DestinataireEnquete> DestinataireEnquetes { get; set; }
    }
}
