using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AppAfpaBrive.Web.ModelView
{
    public class OffreFormationModelView
    {
        public OffreFormationModelView()
        {
            BeneficiaireOffreFormations = new HashSet<BeneficiaireOffreFormation>();
            //CampagneMails = new HashSet<CampagneMail>();
            Pees = new HashSet<PeeModelView>();
        }

        public OffreFormationModelView(OffreFormation offreFormation)
        {
            if (offreFormation is not null)
            {
                IdOffreFormation = offreFormation.IdOffreFormation;
                IdEtablissement = offreFormation.IdEtablissement;
                MatriculeCollaborateurAfpa = offreFormation.MatriculeCollaborateurAfpa;
                CodeProduitFormation = offreFormation.CodeProduitFormation;
                LibelleOffreFormation = offreFormation.LibelleOffreFormation;
                LibelleReduitOffreFormation = offreFormation.LibelleReduitOffreFormation;
                DateDebutOffreFormation = offreFormation.DateDebutOffreFormation;
                DateFinOffreFormation = offreFormation.DateFinOffreFormation;
                CodeProduitFormation = offreFormation.CodeProduitFormation;
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

        //public virtual ProduitFormation CodeProduitFormationNavigation { get; set; }
        //public virtual Etablissement IdEtablissementNavigation { get; set; }
        //public virtual CollaborateurAfpa MatriculeCollaborateurAfpaNavigation { get; set; }
        public virtual ICollection<BeneficiaireOffreFormation> BeneficiaireOffreFormations { get; set; }
        //public virtual ICollection<CampagneMail> CampagneMails { get; set; }
        public virtual ICollection<PeeModelView> Pees { get; set; }
        public ICollection<OffreFormationModelView> OffreFormations { get; set; }

        public void AlimenterListeOffreFormations(ICollection<OffreFormation> offre)
        {
            foreach (OffreFormation item in offre)
            {
                OffreFormations.Add(new OffreFormationModelView (item));
            }
        }
    }

}




