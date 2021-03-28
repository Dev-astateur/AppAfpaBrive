using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Utilitaires
{
    public class MessagePee
    {
        public string Sujet { get; set; }
        public string Positive { get; set; }
        public string Negative { get; set; }
        public string Message { get; set; }

        public string GetText ( int choix )
        {
            return choix == 0 ? Negative : Positive;
        }
    }
}
