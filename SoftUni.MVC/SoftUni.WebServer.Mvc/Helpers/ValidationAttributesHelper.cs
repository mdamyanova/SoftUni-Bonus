namespace SoftUni.WebServer.Mvc.Helpers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    public static class ValidationAttributesHelper
    {
       public static IEnumerable<ValidationAttribute> GetValidationAttributes(
           PropertyInfo property)
            => property
            .GetCustomAttributes(true)
            .Where(ca => ca is ValidationAttribute)
            .Cast<ValidationAttribute>();

        public static ValidationResult GetValidationResult(
            ValidationAttribute validationAttribute, 
            object propertyValue, 
            object bindingModel)
            => validationAttribute.GetValidationResult(
                propertyValue, new ValidationContext(bindingModel));
    }
}