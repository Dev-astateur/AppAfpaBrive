using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.BOL; 

namespace AppAfpaBrive.Web.ModelView
{
    public class RecapModelView
    {
        public Entreprise Entreprise { get; set; }
        public Contrat Contrat { get; set; }
        public DestinataireEnquete DestinataireEnquete { get; set; }

    }
}
