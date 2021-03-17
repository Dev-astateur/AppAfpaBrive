using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class AppelationRome
    {
        
        public int CodeAppelationRome { get; set; }
        public string LibelleAppelationRome { get; set; }
        public string CodeRome { get; set; }

        public virtual Rome CodeRomeNavigation { get; set; }
    }
}
