﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAfpaBrive.BOL.AnnuaireSocial
{
    public partial class Contact
    {

        public Contact()
        {
            ContactStructures = new HashSet<ContactStructure>();
            ContactLigneAnnuaires = new HashSet<ContactLigneAnnuaire>();
        }

        public int IdContact { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Mail { get; set; }
        public string Telephone { get; set; }

        public ICollection<ContactStructure> ContactStructures { get; set; }

        public ICollection<ContactLigneAnnuaire> ContactLigneAnnuaires { get; set; }
    }
}