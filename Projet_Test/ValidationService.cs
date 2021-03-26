using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projet_Test
{
    internal static class ValidationService
    {
        internal static IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

    }
}