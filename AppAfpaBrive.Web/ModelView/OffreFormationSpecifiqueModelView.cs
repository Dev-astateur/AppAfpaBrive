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
                MatriculeCollaborateurAfpa = offre.MatriculeCollaborateurAfpa;
                LibelleOffreFormation = offre.LibelleOffreFormation;
                OffreFormations = new HashSet<OffreFormationModelView>();
              
            }
        }
        public int IdOffreFormation { get; set; }
        public string MatriculeCollaborateurAfpa { get; set; }
        public int CodeProduitFormation { get; set; }
        public string LibelleOffreFormation { get; set; }
 
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
