using System.ComponentModel.DataAnnotations;

namespace AppAfpaBrive.Web.ModelView
{
    public class Pro_Session_ModelView
    {
        public string NomProfessionnel { get; set; }
        public int ID { get; set; }

        public string PrenomProfessionnel { get; set; }

        public int CodeTitreCiviliteProfessionnel { get; set; }

        public string AdresseMail { get; set; }

        public string NumerosTel { get; set; }
        public string Fonction { get; set; }
        public bool Create { get; set; }
    }

}
