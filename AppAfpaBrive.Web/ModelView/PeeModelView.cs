using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView
{
    public class PeeModelView : ModelViewBase
    {
        public PeeModelView()
        {
            //PeriodePees = new HashSet<PeriodePee>();
        }

        public PeeModelView( Pee pee )
        {
            if (pee is not null)
            {
                //PeriodePees = new HashSet<PeriodePee>();
                IdPee = pee.IdPee;
                MatriculeBeneficiaire = pee.MatriculeBeneficiaire;
                IdTuteur = pee.IdTuteur;
                IdResponsableJuridique = pee.IdResponsableJuridique;
                IdEntreprise = pee.IdEntreprise;
                IdOffreFormation = pee.IdOffreFormation;
                IdEtablissement = pee.IdEtablissement;
                Remarque = pee.Remarque;
                EtatPee = pee.EtatPee;
                Id = new OffreFormationModelView(pee.Id);
                IdEntrepriseNavigation = new EntrepriseModelView(pee.IdEntrepriseNavigation);
                MatriculeBeneficiaireNavigation = new BeneficiaireModelView(pee.MatriculeBeneficiaireNavigation);
                IdResponsableJuridiqueNavigation = new ProfessionnelModelView(pee.IdResponsableJuridiqueNavigation);
                IdTuteurNavigation = new ProfessionnelModelView(pee.IdTuteurNavigation);
            }
        }

        public decimal IdPee { get; set; }
        public string MatriculeBeneficiaire { get; set; }
        public int IdTuteur { get; set; }
        public int IdResponsableJuridique { get; set; }
        public int IdEntreprise { get; set; }
        public int IdOffreFormation { get; set; }
        public string IdEtablissement { get; set; }
        [StringLength(1024,ErrorMessage ="Les remarques ne doivent pas excéder 1024 caractères.")]
        public string Remarque { get; set; }
        public int EtatPee { get; set; }

        public virtual EntrepriseModelView IdEntrepriseNavigation { get; set; }
        public virtual OffreFormationModelView Id { get; set; }
        public virtual ProfessionnelModelView IdResponsableJuridiqueNavigation { get; set; }
        public virtual ProfessionnelModelView IdTuteurNavigation { get; set; }
        public virtual BeneficiaireModelView MatriculeBeneficiaireNavigation { get; set; }
        public virtual ICollection<PeriodePee> PeriodePees { get; set; }

        public override bool Equals(object obj)
        {
            return obj is PeeModelView pee &&
                   IdPee == pee.IdPee;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdPee);
        }

        public int GetHasCodeObject ()
        {
            return IdPee.GetHashCode()^MatriculeBeneficiaire.GetHashCode()
                ^IdTuteur.GetHashCode()^IdTuteur.GetHashCode()^IdResponsableJuridique.GetHashCode()
                ^IdEntreprise.GetHashCode()^IdOffreFormation.GetHashCode()^IdEtablissement.GetHashCode()
                ^Remarque.GetHashCode()^EtatPee.GetHashCode();
        }

        public bool ModificationBool(PeeModelView pee)
        {
            if ( this.Equals(pee) )
            {
                if ( this.GetHasCodeObject() == pee.GetHasCodeObject())
                {
                    return true;
                }
            }
            return false;
        }
    }

   
}
