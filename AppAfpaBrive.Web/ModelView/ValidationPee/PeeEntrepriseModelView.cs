using Microsoft.AspNetCore.Mvc.Rendering;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView.ValidationPee
{
    public class PeeEntrepriseModelView
    {
        public decimal IdPee { get; set; }
        public string MatriculeCollaborateurAfpa { get; set; }
        public EntrepriseModelView IdEntrepriseNavigation { get; set; }
    }
}
