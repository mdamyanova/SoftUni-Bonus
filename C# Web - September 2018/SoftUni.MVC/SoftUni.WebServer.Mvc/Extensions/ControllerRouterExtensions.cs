namespace SoftUni.WebServer.Mvc.Extensions
{
    using Common;
    using Controllers;
    using Helpers;
    using Http.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class ControllerRouterExtensions
    {     
        public static Controller GetController(string controllerName, 
            IHttpRequest request)
        {          
            if (controllerName == null)
            {
                return null; 
            }

            var controller = CreateInstance(controllerName);

            if (controller != null)
            {
                controller.OnControllerCreated();
                controller.Request = request;
                controller.SetAuthentication();
                controller.OnAuthentication();
            }

            return controller;
        }

        public static MethodInfo GetMethod(
            string requestMethod, Controller controller, string actionName)
        {
            foreach (var methodInfo in GetSuitableMethods(controller, actionName))
            {
                var attributes = ControllerHelper.GetHttpMethodAttributes(methodInfo);

                if (!attributes.Any() && requestMethod.ToUpper() == Constants.GetMethodUpper)
                {
                    return methodInfo;
                }

                foreach (var attribute in attributes)
                {
                    if (attribute.IsValid(requestMethod))
                    {
                        return methodInfo;
                    }
                }
            }

            return null;
        }

        private static IEnumerable<MethodInfo> GetSuitableMethods(
            Controller controller, string actionName)
        {
            if (controller == null)
            {
                return new MethodInfo[0];
            }

            return ControllerRouterHelper.GetMethods(controller, actionName);
        }

        private static Controller CreateInstance(string controllerName)
        {
            var controllerTypeName = ControllerRouterHelper
                .GetControllerTypeName(controllerName);
            var controllerType = ControllerRouterHelper
                .GetControllerType(controllerTypeName);

            return (Controller)Activator.CreateInstance(controllerType);
        }
    }
}