namespace SoftUni.WebServer.Mvc.Helpers
{
    using Attributes.Security;
    using Common;
    using Controllers;
    using Http.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class ControllerRouterHelper
    {
        public static string[] GetInvocationParameters(IHttpRequest request)
            => request.Path.Split(Constants.InvocationParametersSplit, 
                StringSplitOptions.RemoveEmptyEntries);

        public static string GetControllerName(string invocationParameter)
            => invocationParameter.CapitalizeFirstLetter() + 
            MvcContext.Get.ControllerSuffix;

        public static string GetActionName(string invocationParameter)
            => invocationParameter.CapitalizeFirstLetter();

        public static AuthorizeAttribute GetAuthorizeAttribute(MethodInfo action)
            => action
            .GetCustomAttributes()
            .Where(attr => attr is AuthorizeAttribute)
            .Cast<AuthorizeAttribute>()
            .FirstOrDefault();

        public static string GetControllerTypeName(string controllerName)
            => string.Format(
                Constants.ControllerTypeNameFormat,
                MvcContext.Get.AssemblyName,
                MvcContext.Get.ControllersFolder,
                controllerName);

        public static Type GetControllerType(string controllerTypeName)
           => Type.GetType(controllerTypeName);

        public static IEnumerable<MethodInfo> GetMethods(Controller controller, string actionName)
            => controller
            .GetType()
            .GetMethods()
            .Where(methodInfo => methodInfo.Name.ToLower() == actionName.ToLower());
    }
}