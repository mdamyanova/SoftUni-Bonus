namespace SoftUni.WebServer.Mvc.Views
{
    using Common;
    using Exceptions;
    using Helpers;
    using Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    public class View : IRenderable
    {
        private readonly string templateFullyQualifiedName;

        private readonly IDictionary<string, string> viewData;
        private readonly List<string> errorData;

        public View(string templateFullyQualifiedName,
            IDictionary<string, string> viewData)
        {
            this.templateFullyQualifiedName = templateFullyQualifiedName;
            this.viewData = viewData;
            this.errorData = new List<string>();
        }

        public string Render()
        {
            var fullHtml = this.ReadFile(this.templateFullyQualifiedName);

            if (this.viewData.Any())
            {
                foreach (var parameter in this.viewData)
                {
                    fullHtml = fullHtml.Replace(
                        $"{{{{{{{parameter.Key}}}}}}}",
                        parameter.Value);
                }

                fullHtml = fullHtml.Replace(
                    Constants.ErrorPlaceholder,
                    "Errors: " + string.Join(", ", this.errorData));
            }

            return fullHtml;
        }

        private string ReadFile(string templateFullyQualifiedName)
        {
            var layoutHtml = this.RenderLayoutHtml();

            var templateFullyQualifiedNameWithExtension =
               $"{templateFullyQualifiedName}{Constants.HtmlExtension}";

            if (!StringExtensions.FileExists(templateFullyQualifiedNameWithExtension))
            {
                var errorHtmlPath = ViewHelper.GetErrorPath();

                if (!StringExtensions.FileExists(errorHtmlPath))
                {
                    throw new ErrorViewDoesntExistException();
                }

                var errorHtml = StringExtensions.ReadAllText(errorHtmlPath);

                this.errorData.Add(Constants.RequestedViewDoesntExistsMessage);

                return errorHtml;
            }

            var templateHtml = StringExtensions.ReadAllText(
                templateFullyQualifiedNameWithExtension);

            return ViewHelper.GetFullHtml(layoutHtml, templateHtml);
        }

        private string RenderLayoutHtml()
        {
            var layoutHtmlFullyQualifiedName = ViewHelper
                .GetViewPath(Constants.BaseLayoutFileName, Constants.HtmlExtension);

            if (!StringExtensions.FileExists(layoutHtmlFullyQualifiedName))
            {
                var errorHtmlPath = ViewHelper.GetErrorPath();
                var errorHtml = StringExtensions.ReadAllText(errorHtmlPath);
                this.errorData.Add(Constants.LayoutViewDoesntExistsMessage);

                return errorHtml;
            }

            var layoutHtml = StringExtensions
                .ReadAllText(layoutHtmlFullyQualifiedName);

            return layoutHtml;
        }
    }
}