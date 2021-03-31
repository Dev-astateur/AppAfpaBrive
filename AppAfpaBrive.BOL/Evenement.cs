using System;
using System.Collections.Generic;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class Evenement
    {
        public Evenement()
        {
            EvenementDocuments = new HashSet<EvenementDocument>();
        }

        public decimal IdEvent { get; set; }
        public string IdCategorieEvent { get; set; }
        public DateTime DateEvent { get; set; }
        public string IdEtablissement { get; set; }
        public string DétailsEvent { get; set; }
        public virtual CategorieEvenement IdCategorieEventNavigation { get; set; }
        public virtual Etablissement IdEtablissementNavigation { get; set; }
        public virtual ICollection<EvenementDocument> EvenementDocuments { get; set; }
        
        public Guid IdGroupe { get; set; }

        
    }
}
