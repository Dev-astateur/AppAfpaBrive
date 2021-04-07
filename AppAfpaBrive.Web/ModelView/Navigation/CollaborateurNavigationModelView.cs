using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AppAfpaBrive.Web.ModelView
{
    public class CollaborateurNavigationModelView
    {
        [Display(Name = "Matricule")]
        public string MatriculeCollaborateurAfpa { get; set; }
        public string NomCollaborateur { get; set; }
        public string PrenomCollaborateur { get; set; }

        public override string ToString()
        {
            return NomCollaborateur + " " + PrenomCollaborateur;
        }
    }
}
