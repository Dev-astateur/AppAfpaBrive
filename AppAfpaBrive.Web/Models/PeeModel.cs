using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.BOL;
namespace AppAfpaBrive.Web.Models
{
    public class PeeModel
    {
        public string MatriculeBeneficiaire { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int IdPee { get; set; }
        public List<Pee> Pees { get; set; }
    }
}
