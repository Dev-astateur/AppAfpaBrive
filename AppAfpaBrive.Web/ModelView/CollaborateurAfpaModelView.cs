using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView
{
    public class CollaborateurAfpaModelView
    {
        public CollaborateurAfpaModelView()
        {
            OffreFormations = new HashSet<OffreFormation>();
        }

        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Le matricule du collaborateur est requis")]
        [StringLength(7, ErrorMessage = "Le matricule du collaborateur ne peut pas etre plus long que 7 caracteres")]
        public string MatriculeCollaborateurAfpa { get; set; }
        public string IdEtablissement { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le CodeTitreCivilite est requis")]
        public int CodeTitreCivilite { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le nom du Collaborateur est requis")]
        public string NomCollaborateur { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le prénom du collaborateur est requis")]
        public string PrenomCollaborateur { get; set; }

        public string MailCollaborateurAfpa { get; set; }
        public string TelCollaborateurAfpa { get; set; }
        [StringLength(3, ErrorMessage = "L'unité organisationnelle du collaborateur ne peut pas etre plus longue que 3 caracteres")]
        public string Uo { get; set; }
        public string UserId { get; set; }

        public virtual TitreCivilite CodeTitreCiviliteNavigation { get; set; }
        public virtual Etablissement IdEtablissementNavigation { get; set; }
        public virtual UniteOrganisationnelle UoNavigation { get; set; }
        public virtual ICollection<OffreFormation> OffreFormations { get; set; }

        public CollaborateurAfpa GetCollaborateur()
        {
            return new CollaborateurAfpa()
            {
                MatriculeCollaborateurAfpa = this.MatriculeCollaborateurAfpa,
                IdEtablissement = this.IdEtablissement,
                CodeTitreCivilite = this.CodeTitreCivilite,
                NomCollaborateur = this.NomCollaborateur,
                PrenomCollaborateur = this.PrenomCollaborateur,
                MailCollaborateurAfpa = this.MailCollaborateurAfpa,
                TelCollaborateurAfpa = this.TelCollaborateurAfpa,
                Uo = this.Uo,
                UserId = this.UserId,  
            };
        }
    }
}

