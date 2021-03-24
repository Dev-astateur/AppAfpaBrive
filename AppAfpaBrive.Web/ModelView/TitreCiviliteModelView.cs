using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView
{
    public class TitreCiviliteModelView
    {
        public TitreCiviliteModelView(TitreCivilite titreCivilite)
        {
            CodeTitreCivilite = titreCivilite.CodeTitreCivilite;
            TitreCiviliteAbrege = titreCivilite.TitreCiviliteAbrege;
            TitreCiviliteComplet = titreCivilite.TitreCiviliteComplet;
            
            Beneficiaires = new HashSet<BeneficiaireModelView>();
            //CollaborateurAfpas = new HashSet<CollaborateurAfpaModelView>();
            Professionnels = new HashSet<ProfessionnelModelView>();
        }

        public int CodeTitreCivilite { get; set; }
        public string TitreCiviliteAbrege { get; set; }
        public string TitreCiviliteComplet { get; set; }

        public virtual ICollection<BeneficiaireModelView> Beneficiaires { get; set; }
        //public virtual ICollection<CollaborateurAfpa> CollaborateurAfpas { get; set; }
        public virtual ICollection<ProfessionnelModelView> Professionnels { get; set; }
    }
}
