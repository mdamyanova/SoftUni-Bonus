namespace SoftUni.WebServer.Mvc.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Validation;

    public static class ParametersExtensions
    {
        public static object[] MapActionParameters(
            MethodInfo method,
            IDictionary<string, string> getParams,
            IDictionary<string, string> postParams,
            ParameterValidator parameterValidator)
        {
            var parameterDescriptions = method.GetParameters();
            var parameters = new object[parameterDescriptions.Length];

            for (int index = 0; index < parameterDescriptions.Length; index++)
            {
                var param = parameterDescriptions[index];

                if (param.ParameterType.IsPrimitive || param.ParameterType == typeof(string))
                {
                    // GET request -> primitive types only
                    parameters[index] = ProcessPrimitiveParameter(getParams, param);
                }
                else
                {
                    // POST request -> models
                    parameters[index] = ProcessBindingModelParameters(
                        postParams, param, parameterValidator);
                }
            }

            return parameters;
        }

        private static object ProcessPrimitiveParameter(
            IDictionary<string, string> getParams, 
            ParameterInfo param)
        {
            object value = getParams[param.Name];

            return Convert.ChangeType(value, param.ParameterType);
        }

        private static object ProcessBindingModelParameters(
            IDictionary<string, string> postParams, 
            ParameterInfo param,
            ParameterValidator parameterValidator)
        {
            var modelType = param.ParameterType;
            var modelInstance = Activator.CreateInstance(modelType);
            var modelProperties = modelType.GetProperties();

            foreach (var property in modelProperties)
            {
                try
                {
                    var value = postParams[property.Name];
                    property.SetValue(modelInstance, 
                        Convert.ChangeType(value, property.PropertyType));
                }
                catch
                {
                    parameterValidator.AddModelError(property.Name, 
                        $"The {property.Name} field could not be mapped.");
                }
            }

            return Convert.ChangeType(modelInstance, modelType);
        } 
    }
}