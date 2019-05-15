namespace SoftUni.WebServer.Mvc.Routers
{
    using Attributes.Security;
    using Common;
    using Controllers;
    using Exceptions;
    using Extensions;
    using Helpers;
    using Http.Enums;
    using Http.Interfaces;
    using Http.Responses;
    using Interfaces;
    using Server.Interfaces;
    using System;
    using System.Reflection;
    using Validation;

    public class ControllerRouter : IHttpRequestHandler
    {
        public IHttpResponse Handle(IHttpRequest request)
        {
            var getParams = request.UrlParameters;
            var postParams = request.FormData;
            var requestMethod = request.Method.ToString();

            var controllerName = string.Empty;
            var actionName = string.Empty;

            if (request.Path == Constants.BasePath)
            {
                return new RedirectResponse(Constants.DefaultHomePath);
            }
            else
            {
                var invocationParameters = ControllerRouterHelper
                    .GetInvocationParameters(request);

                if (invocationParameters.Length != 2)
                {
                    throw new InvalidOperationException(Constants.InvalidUrlMessage);
                }

                controllerName = ControllerRouterHelper.GetControllerName(invocationParameters[0]);
                actionName = ControllerRouterHelper.GetActionName(invocationParameters[1]);
            }

            var controller = ControllerRouterExtensions.GetController(controllerName, request);

            try
            {
                var action = ControllerRouterExtensions.GetMethod(
                    requestMethod, controller, actionName);

                if (action == null)
                {
                    return new NotFoundResponse();
                }

                var authAttribute = ControllerRouterHelper.GetAuthorizeAttribute(action);

                if (authAttribute != null && !controller.User.IsAuthenticated)
                {
                    return authAttribute.GetResponse(Constants.UserNotAuthorizedMessage);
                }

                var parameterValidator = new ParameterValidator();
                var actionParameters = ParametersExtensions.MapActionParameters(
                    action, getParams, postParams, parameterValidator);

                controller.ParameterValidator = parameterValidator;

                return this.PrepareResponse(controller, action, actionParameters);
            }
            catch (UnauthorizedException e)
            {
                return new AuthorizeAttribute().GetResponse(e.Message);
            }
        }

        private IHttpResponse PrepareResponse(
            Controller controller, MethodInfo action, object[] parameters)
        {
            controller.OnActionExecuting(action);

            var actionResult = (IActionResult)action.Invoke(controller, parameters);
            var invocationResult = actionResult.Invoke();

            controller.OnActionExecuted(action, invocationResult);

            if (actionResult is IViewable)
            {
                return new ContentResponse(HttpStatusCode.Ok, invocationResult);
            }
            else if (actionResult is IRedirectable)
            {
                return new RedirectResponse(invocationResult);
            }
            else
            {
                throw new ViewResultNotSupportedException();
            }
        }
    }
}