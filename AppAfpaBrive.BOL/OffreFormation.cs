using System;
using System.Collections.Generic;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class OffreFormation : EntityBase
    {
        public OffreFormation()
        {
            BeneficiaireOffreFormations = new HashSet<BeneficiaireOffreFormation>();
            CampagneMails = new HashSet<CampagneMail>();
            Pees = new HashSet<Pee>();
        }

        public int IdOffreFormation { get; set; }
        public string IdEtablissement { get; set; }
        public string MatriculeCollaborateurAfpa { get; set; }
        public int CodeProduitFormation { get; set; }
        public string LibelleOffreFormation { get; set; }
        public string LibelleReduitOffreFormation { get; set; }
        public DateTime DateDebutOffreFormation { get; set; }
        public DateTime DateFinOffreFormation { get; set; }

        public virtual ProduitFormation CodeProduitFormationNavigation { get; set; }
        public virtual Etablissement IdEtablissementNavigation { get; set; }
        public virtual CollaborateurAfpa MatriculeCollaborateurAfpaNavigation { get; set; }
        public virtual ICollection<BeneficiaireOffreFormation> BeneficiaireOffreFormations { get; set; }
        public virtual ICollection<CampagneMail> CampagneMails { get; set; }
        public virtual ICollection<Pee> Pees { get; set; }
    }
}
