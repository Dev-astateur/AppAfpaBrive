﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAfpaBrive.BOL.AnnuaireSocial
{
    public partial class LigneAnnuaire
    {

        public LigneAnnuaire()
        {
            CategorieLigneAnnuaires = new HashSet<CategorieLigneAnnuaire>();
            ContactLigneAnnuaires = new HashSet<ContactLigneAnnuaire>();
        }

        [Key]
        public int IdLigneAnnuaire {get; set; }

        public string PublicConcerne { get; set; }
        public string ServiceAbrege { get; set; }
        public string Service { get; set; }
        public string Conditions { get; set; }

        public int IdStructure { get; set; }
        public virtual Structure Structure { get; set; }

        public virtual ICollection<CategorieLigneAnnuaire> CategorieLigneAnnuaires { get; set; }

        public virtual ICollection<ContactLigneAnnuaire> ContactLigneAnnuaires { get; set; }



    }
}