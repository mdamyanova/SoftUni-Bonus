namespace SoftUni.WebServer.Mvc.Extensions
{
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;
    using Validation;

    public static class ValidationAttributesExtensions
    {
        public static void AddModelErrors(ParameterValidator parameterValidator, 
            PropertyInfo property, ValidationResult validationResult, object bindingModel)
        {
            parameterValidator.AddModelError(
                property.Name, 
                validationResult
                .ErrorMessage
                .Replace(bindingModel.GetType().Name, property.Name));
        }
    }
}