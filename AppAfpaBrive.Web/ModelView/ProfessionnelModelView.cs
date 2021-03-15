using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
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
            IdProfessionnel = professionnel.IdProfessionnel;
            NomProfessionnel = professionnel.NomProfessionnel;
            PrenomProfessionnel = professionnel.PrenomProfessionnel;
            CodeTitreCiviliteProfessionnel = professionnel.CodeTitreCiviliteProfessionnel;

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
        public string NomProfessionnel { get; set; }
        public string PrenomProfessionnel { get; set; }
        public int CodeTitreCiviliteProfessionnel { get; set; }

        public virtual ICollection<PeeModelView> PeeIdResponsableJuridiqueNavigations { get; set; }
        public virtual ICollection<PeeModelView> PeeIdTuteurNavigations { get; set; }
    }
}
