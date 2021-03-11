using System;
using System.Collections.Generic;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class TypeContrat
    {
        public TypeContrat()
        {
            Contrats = new HashSet<Contrat>();
        }

        public int IdTypeContrat { get; set; }
        public string DesignationTypeContrat { get; set; }

        public virtual ICollection<Contrat> Contrats { get; set; }
    }
}
