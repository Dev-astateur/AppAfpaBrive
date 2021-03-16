using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppAfpaBrive.BOL
{
    class InsertionTroisMois
    {
        [Key]
        public string IdEtablissement { get; set; }
        [Key]
        public int IdOffreFormation { get; set; }
        [Key]
        public int Annee { get; set; }
        public int TotalReponse { get; set; }
        public int Cdi { get; set; }
        public int Cdd { get; set; }
        public int Alternance { get; set; }
        public int SansEmploie { get; set; }
        public int Autres { get; set; }

        public virtual Etablissement IdEtablissementNavivation { get; set; }
        public virtual ProduitFormation CodeProduitFormationNavigation { get; set; }

    }
}
