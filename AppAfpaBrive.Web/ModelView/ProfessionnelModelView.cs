using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView
{
    public class ProfessionnelModelView
    {
        public ProfessionnelModelView()
        {
            PeeIdResponsableJuridiqueNavigations = new HashSet<PeeModelView>();
            PeeIdTuteurNavigations = new HashSet<PeeModelView>();
        }

        public ProfessionnelModelView( Professionnel professionnel)
        {
            if ( professionnel is not null )
            {
                IdProfessionnel = professionnel.IdProfessionnel;
                NomProfessionnel = professionnel.NomProfessionnel;
                PrenomProfessionnel = professionnel.PrenomProfessionnel;
                CodeTitreCiviliteProfessionnel = professionnel.CodeTitreCiviliteProfessionnel;
            } 
            
            //PeeIdResponsableJuridiqueNavigations = new HashSet<PeeModelView>();
            //foreach(Pee item in professionnel.PeeIdResponsableJuridiqueNavigations)
            //{
            //    PeeIdResponsableJuridiqueNavigations.Add(new PeeModelView(item));
            //}

            //PeeIdTuteurNavigations = new HashSet<PeeModelView>();
            //foreach (Pee item in professionnel.PeeIdTuteurNavigations)
            //{
            //    PeeIdResponsableJuridiqueNavigations.Add(new PeeModelView(item));
            //}
        }

        public int IdProfessionnel { get; set; }
        [Display(Name ="Nom du professionel")]
        public string NomProfessionnel { get; set; }
        [Display(Name ="Prénom")]
        public string PrenomProfessionnel { get; set; }
        public int CodeTitreCiviliteProfessionnel { get; set; }

        public virtual ICollection<PeeModelView> PeeIdResponsableJuridiqueNavigations { get; set; }
        public virtual ICollection<PeeModelView> PeeIdTuteurNavigations { get; set; }
    }
}
