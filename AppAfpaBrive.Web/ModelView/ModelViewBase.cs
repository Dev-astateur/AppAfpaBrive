using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView
{
    public abstract class ModelViewBase : IDataErrorInfo, IEntityPOCOState
    {
        protected bool _isValid = true;
        public bool IsValid
        {
            get
            {
                return isValid();
            }
        }

        public string this[string property]
        {
            get
            {
                var propertyDescriptor = TypeDescriptor.GetProperties(this)[property];
                if (propertyDescriptor == null)
                { return string.Empty; }

                var results = new List<ValidationResult>();
                bool result = Validator.TryValidateProperty(
                                            propertyDescriptor.GetValue(this),
                                            new ValidationContext(this, null, null)
                                            { MemberName = property },
                                            results);
                if (!result)
                { return results.First().ErrorMessage; }
                else
                {
                    return string.Empty;
                }

            }
        }

        public string Error
        {
            get
            {
                var results = new List<ValidationResult>();
                _isValid = Validator.TryValidateObject(this,
                    new ValidationContext(this, null, null), results, true);
                if (!_isValid)
                {
                    return string.Join("\n", results.Select(x => x.ErrorMessage));
                }
                else
                {
                    return null;
                }
            }
        }


        [NotMapped]
        public EntityPOCOState Etat { get; set; } = EntityPOCOState.Unchanged;

        private bool isValid()
        {

            return Validator.TryValidateObject(this,
                    new ValidationContext(this, null, null), null, true);
        }
    }
}
