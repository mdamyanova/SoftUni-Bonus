namespace SoftUni.WebServer.Mvc.Helpers
{
    using Attributes.HttpMethods;
    using Common;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class ControllerHelper
    {
        public static string GetControllerName(object controller)
            => controller.GetType()
                .Name
                .Replace(MvcContext.Get.ControllerSuffix, string.Empty);

        public static string GetViewFullQualifiedName(
            string controller,
            string action)
            => string.Format(
                Constants.ViewFullQualifiedNameFormat,
                MvcContext.Get.ViewsFolder,
                controller,
                action);

        public static PropertyInfo[] GetBindingModelProperties(object bindingModel) 
            => bindingModel.GetType().GetProperties();

        public static object GetPropertyValue(PropertyInfo property, object bindingModel)
            => property.GetValue(bindingModel);

        public static IEnumerable<HttpMethodAttribute> GetHttpMethodAttributes(
            MethodInfo methodInfo)
            => methodInfo
            .GetCustomAttributes()
            .Where(attr => attr is HttpMethodAttribute)
            .Cast<HttpMethodAttribute>();
    }
}