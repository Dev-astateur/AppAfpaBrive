using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView
{
    public class PeeDocumentModelView
    {

        public PeeDocumentModelView( PeeDocument peeDocument )
        {
            IdPee = peeDocument.IdPee;
            NumOrdre = peeDocument.NumOrdre;
            PathDocument = peeDocument.PathDocument;
            idPeeNavigation = new PeeModelView(peeDocument.idPeeNavigation);
        }

        public decimal IdPee { get; set; }

        public int NumOrdre { get; set; }
        [Display(Name ="Document")]
        public string PathDocument { get; set; }

        public virtual PeeModelView idPeeNavigation { get; set; }

        public string NomDocument {
            get
            {
                string retour = PathDocument.Substring(PathDocument.LastIndexOf('\\')+1,PathDocument.Length-PathDocument.LastIndexOf('\\')-1);
                return retour;
            }
        }
    }
}
