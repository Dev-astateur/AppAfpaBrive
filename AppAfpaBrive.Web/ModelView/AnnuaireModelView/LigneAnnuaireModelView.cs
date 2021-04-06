using AppAfpaBrive.BOL.AnnuaireSocial;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView.AnnuaireModelView
{
    public class LigneAnnuaireModelView
    {

        public LigneAnnuaireModelView()
        {
            CategorieLigneAnnuaires = new HashSet<CategorieLigneAnnuaire>();
            ContactLigneAnnuaires = new HashSet<ContactLigneAnnuaire>();
        }

        [Key]
        public int IdLigneAnnuaire { get; set; }

        [Required(ErrorMessage="Merci d'entrer un public concerné")]
        public string PublicConcerne { get; set; }

        [Required(ErrorMessage = "Merci d'entrer une description abrégée des services proposées")]
        public string ServiceAbrege { get; set; }

        [Required(ErrorMessage = "Merci d'entrer une description des services proposées")]
        public string Service { get; set; }
        public string Conditions { get; set; }

        public int IdStructure { get; set; }
        public virtual Structure Structure { get; set; }

        public virtual ICollection<CategorieLigneAnnuaire> CategorieLigneAnnuaires { get; set; }
        public virtual ICollection<ContactLigneAnnuaire> ContactLigneAnnuaires { get; set; }

        public LigneAnnuaire GetLigneAnnuaire()
        {
            return new LigneAnnuaire
            {
                IdLigneAnnuaire = this.IdLigneAnnuaire,
                PublicConcerne = this.PublicConcerne,
                ServiceAbrege = this.ServiceAbrege,
                Service = this.Service,
                Conditions = this.Conditions,
                IdStructure = this.IdStructure,
                Structure = this.Structure,
                CategorieLigneAnnuaires = this.CategorieLigneAnnuaires,
                ContactLigneAnnuaires = this.ContactLigneAnnuaires
            };
        }
        


    }
}
