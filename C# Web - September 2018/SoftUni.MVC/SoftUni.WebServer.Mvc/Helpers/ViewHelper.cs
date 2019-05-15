namespace SoftUni.WebServer.Mvc.Helpers
{
    using Common;

    public static class ViewHelper
    {
        public static string GetErrorPath()
            => GetViewPath(Constants.ErrorViewName, Constants.HtmlExtension);

        public static string GetViewPath(string viewName, string extension)
            => string.Format(
                Constants.ViewPathFormat,
                MvcContext.Get.ViewsFolder,
                viewName,
                extension);

        public static string GetFullHtml(string layoutHtml, string templateHtml)
            => layoutHtml.Replace(Constants.ContentPlaceholder, templateHtml);
    }
}