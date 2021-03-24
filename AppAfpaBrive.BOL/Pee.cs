﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class Pee : EntityBase
    {
        public Pee()
        {
            PeriodePees = new HashSet<PeriodePee>();
        }

        public decimal IdPee { get; set; }
        public string MatriculeBeneficiaire { get; set; }
        public int IdTuteur { get; set; }
        public int IdResponsableJuridique { get; set; }
        public int IdEntreprise { get; set; }
        public int IdOffreFormation { get; set; }
        public string IdEtablissement { get; set; }
        public int EtatPee { get; set; }
        public string Remarque { get; set; }

        public virtual OffreFormation Id { get; set; }
        
        public virtual Professionnel IdResponsableJuridiqueNavigation { get; set; }
        public virtual Entreprise IdEntrepriseNavigation { get; set; }
        public virtual Professionnel IdTuteurNavigation { get; set; }
        public virtual Beneficiaire MatriculeBeneficiaireNavigation { get; set; }
        public virtual ICollection<PeriodePee> PeriodePees { get; set; }
        public virtual ICollection<PeeDocument> PeeDocument { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Pee pee &&
                   IdPee == pee.IdPee;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdPee);
        }
    }
}
