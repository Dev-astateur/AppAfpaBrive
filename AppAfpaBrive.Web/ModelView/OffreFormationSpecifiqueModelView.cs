using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView
{
    public class OffreFormationSpecifiqueModelView
    {
        public OffreFormationSpecifiqueModelView(OffreFormation offre)
        {
            if (offre is not null)
            {
                IdOffreFormation = offre.IdOffreFormation;
                IdEtablissement = offre.IdEtablissement;
                MatriculeCollaborateurAfpa = offre.MatriculeCollaborateurAfpa;
                CodeProduitFormation = offre.CodeProduitFormation;
                LibelleOffreFormation = offre.LibelleOffreFormation;
                LibelleReduitOffreFormation = offre.LibelleReduitOffreFormation;
                DateDebutOffreFormation = offre.DateDebutOffreFormation;
                DateFinOffreFormation = offre.DateFinOffreFormation;
                CodeProduitFormation = offre.CodeProduitFormation;
                OffreFormations = new HashSet<OffreFormationModelView>();
                BeneficiaireOffreFormations = new HashSet<BeneficiaireOffreFormation>();


            }
        }
        public int IdOffreFormation { get; set; }
        public string IdEtablissement { get; set; }
        public string MatriculeCollaborateurAfpa { get; set; }
        public int CodeProduitFormation { get; set; }
        public string LibelleOffreFormation { get; set; }
        public string LibelleReduitOffreFormation { get; set; }
        public DateTime DateDebutOffreFormation { get; set; }
        public DateTime DateFinOffreFormation { get; set; }
        public virtual ICollection<BeneficiaireOffreFormation> BeneficiaireOffreFormations { get; set; }
        public virtual ICollection<OffreFormationModelView> OffreFormations { get; set; }

       

        public void AlimenterListeOffreFormations(ICollection<OffreFormation> offre)
        {
            foreach (OffreFormation item in offre)
            {
                OffreFormations.Add(new OffreFormationModelView(item));
            }
        }
    }
}
