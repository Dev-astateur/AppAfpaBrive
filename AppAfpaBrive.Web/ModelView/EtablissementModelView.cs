using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView
{
    public partial class EtablissementModelView
    {
        public EtablissementModelView()
        {
            CollaborateurAfpas = new HashSet<CollaborateurAfpa>();
            Evenements = new HashSet<Evenement>();
            InverseIdEtablissementRattachementNavigation = new HashSet<Etablissement>();
            OffreFormations = new HashSet<OffreFormation>();
        }
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "L'Id de l'établissement est requis")]
        public string IdEtablissement { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "L'Id de l'établissement de rattachement est requis")]
        public string IdEtablissementRattachement { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le Nom de l'établissement est requis")]
        public string NomEtablissement { get; set; }
        public string MailEtablissement { get; set; }
        public string TelEtablissement { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "L'adresse est requise")]
        public string Ligne1Adresse { get; set; }
        public string Ligne2Adresse { get; set; }
        public string Ligne3Adresse { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le code postal est requis")]
        [StringLength(10, ErrorMessage = "Le Libelle Court Formation ne peut pas etre plus long que 10 caracteres")]
        public string CodePostal { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La Ville est requise")]
        public string Ville { get; set; }

        public virtual Etablissement IdEtablissementRattachementNavigation { get; set; }
        public virtual ICollection<CollaborateurAfpa> CollaborateurAfpas { get; set; }
        public virtual ICollection<Evenement> Evenements { get; set; }
        public virtual ICollection<Etablissement> InverseIdEtablissementRattachementNavigation { get; set; }
        public virtual ICollection<OffreFormation> OffreFormations { get; set; }

        public Etablissement GetEtablissement()
        {
            return new Etablissement()
            {
                IdEtablissement = this.IdEtablissement,
                IdEtablissementRattachement = this.IdEtablissementRattachement,
                NomEtablissement = this.NomEtablissement,
                MailEtablissement= this.MailEtablissement,
                TelEtablissement = this.TelEtablissement,
                Ligne1Adresse = this.Ligne1Adresse,
                Ligne2Adresse=this.Ligne2Adresse,
                Ligne3Adresse=this.Ligne3Adresse,
                CodePostal= this.CodePostal,
                Ville= this.Ville,
            };
        }
    }
}
