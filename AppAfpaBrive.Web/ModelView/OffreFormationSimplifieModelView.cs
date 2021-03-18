using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.BOL;

namespace AppAfpaBrive.Web.ModelView
{
    public class OffreFormationSimplifieModelView
    {
        public OffreFormationSimplifieModelView(OffreFormation offre)
        {
            IdOffreFormation = offre.IdOffreFormation;
            MatriculeCollaborateurAfpa = offre.MatriculeCollaborateurAfpa;
            LibelleOffreFormation = offre.LibelleOffreFormation;
            LibelleReduitOffreFormation = offre.LibelleReduitOffreFormation;
            
        }
        public int IdOffreFormation { get; set; }
        public string MatriculeCollaborateurAfpa { get; set; }
        public string LibelleOffreFormation { get; set; }
        public string LibelleReduitOffreFormation { get; set; }
    }
}
