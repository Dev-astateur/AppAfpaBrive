using System;
using System.Collections.Generic;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class ProduitFormation
    {
        public ProduitFormation()
        {
            OffreFormations = new HashSet<OffreFormation>();
            ProduitFormationAppellationRomes = new HashSet<ProduitFormationAppellationRome>();
        }

        public int CodeProduitFormation { get; set; }
        public string NiveauFormation { get; set; }
        public string LibelleProduitFormation { get; set; }
        public string LibelleCourtFormation { get; set; }
        public bool FormationContinue { get; set; }
        public bool FormationDiplomante { get; set; }

        public virtual ICollection<OffreFormation> OffreFormations { get; set; }
        public virtual ICollection<ProduitFormationAppellationRome> ProduitFormationAppellationRomes { get; set; }
    }
}
