using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppAfpaBrive.BOL
{
    public partial class InsertionsTroisMois : IInsertion
    {

        public InsertionsTroisMois()
        {

        }
        [Required]
        [ForeignKey("IdEtablissement")]
        public string IdEtablissement { get; set; }
        [Required]
        [ForeignKey("IdOffreFormation")]
        public int IdOffreFormation { get; set; }
        [Required]
        public bool EnLienAvecFormation { get; set; }
        [Required]
        public int Annee { get; set; }
        [Required]
        public int TotalReponse { get; set; }
        [Required]
        public int Cdi { get; set; }
        [Required]
        public int Cdd { get; set; }
        [Required]
        public int Alternance { get; set; }
        [Required]
        public int SansEmploie { get; set; }
        [Required]
        public int Autres { get; set; }


        public virtual Etablissement IdEtablissementNavigation { get; set; }
        public virtual OffreFormation IdOffreFormationNavigation { get; set; }

    }
}
