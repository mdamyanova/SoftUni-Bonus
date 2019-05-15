namespace SoftUni.WebServer.Mvc.Controllers
{
    using ActionResults;
    using Extensions;
    using Helpers;
    using Http.Interfaces;
    using Interfaces;
    using Models;
    using Security;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Validation;
    using Views;

    public abstract class Controller
    {
        protected Controller()
        {
            this.ViewData = new ViewData();
            this.User = new Authentication();
        }

        public ViewData ViewData { get; protected set; }

        public Authentication User { get; set; }

        public IHttpRequest Request { get; set; }

        public ParameterValidator ParameterValidator { get; set; }

        #region Lifecycle methods
        public virtual void OnControllerCreated()
        {
        }

        public virtual void OnAuthentication()
        {
        }

        public virtual void OnActionExecuting(MethodInfo action)
        {
        }

        public virtual void OnActionExecuted(MethodInfo action, string invocationResult)
        {
        }
        #endregion

        protected internal void SetAuthentication()
        {
            var userId = AuthenticationHelper.GetSessionUserId(this.Request);
            var username = AuthenticationHelper.GetSessionUsername(this.Request);
            var roles = AuthenticationHelper.GetSessionUserRoles(this.Request);

            if (roles == null)
            {
                roles = new string[0];
            }

            if (userId != null && username != null)
            {
                this.User = new Authentication(userId, username, roles);
            }
            else
            {
                this.User = new Authentication();
            }
        }

        protected IViewable View([CallerMemberName] string caller = "")
        {
            var controllerName = ControllerHelper.GetControllerName(this);

            var fullyQualifiedName = ControllerHelper
                .GetViewFullQualifiedName(controllerName, caller);

            var view = new View(fullyQualifiedName, this.ViewData.Data);

            return new ViewResult(view);
        }

        protected IRedirectable RedirectToAction(string redirectUrl)
            => new RedirectResult(redirectUrl);

        protected bool IsValidModel(object bindingModel)
        {
            var properties = ControllerHelper
                .GetBindingModelProperties(bindingModel);
            var errorFound = false;

            foreach (var property in properties)
            {
                var validationAttributes = ValidationAttributesHelper
                    .GetValidationAttributes(property);

                if (!validationAttributes.Any())
                {
                    continue;
                }

                foreach (var validationAttribute in validationAttributes)
                {
                    var propertyValue = ControllerHelper
                        .GetPropertyValue(property, bindingModel);

                    var validationResult = ValidationAttributesHelper
                        .GetValidationResult(
                        validationAttribute, propertyValue, bindingModel);

                    if (validationResult != ValidationResult.Success)
                    {
                        ValidationAttributesExtensions.AddModelErrors(this.ParameterValidator,
                           property, validationResult, bindingModel);

                        errorFound = true;
                    }
                }
            }

            return !errorFound;
        }

        protected void SignIn(string username, string userId, string role)
        {
            AuthenticationExtensions.SignIn(
                username, userId, new List<string> { role }, this.Request);

            this.SetAuthentication();
        }

        protected void SignIn(string username, int userId, string role)
        {
            AuthenticationExtensions.SignIn(
                username, userId, new List<string> { role }, this.Request);

            this.SetAuthentication();
        }

        protected void SignOut()
        {
            AuthenticationExtensions.SignOut(this.Request);
        }
    }
}