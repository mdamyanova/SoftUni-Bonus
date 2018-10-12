namespace SoftUni.WebServer.Mvc
{
    public static class Constants
    {
        public const string GetMethodUpper = "GET";
        public const string PostMethodUpper = "POST";
        public const string ControllersFolder = "Controllers";
        public const string ControllerSuffix = "Controller";
        public const string ModelsFolder = "Models";
        public const string ViewsFolder = "Views";

        public const string BaseLayoutFileName = "Layout";
        public const string ErrorViewName = "Error";
        public const string ContentPlaceholder = "{{{content}}}";
        public const string ErrorPlaceholder = "{{{error}}}";
        public const string HtmlExtension = ".html";

        public const string BasePath = "/";
        public const string DefaultControllerName = "HomeController";
        public const string DefaultActionName = "Index";
        public const string DefaultHomePath = "/Home/Index";

        public const string LoginPath = "/users/login";

        public const string ViewFullQualifiedNameFormat = "{0}\\{1}\\{2}";
        public const string ControllerTypeNameFormat = "{0}.{1}.{2}, {0}";
        public const string ViewPathFormat = "{0}/{1}{2}";

        public const char InvocationParametersSplit = '/';
        public const char ResourcePathSplit = '/';
        public const char ExtensionPathSplit = '.';

        public const int ResourcePathSplitCount = 3;

        public const string FileNotFoundMessage = "File not found.";
        public const string UnauthorizedMessage = "You are not authorized to perform this operation.";
        public const string InvalidUrlMessage = "Invalid URL";
        public const string UserNotAuthorizedMessage = "The user is not authorized to perform this action.";
        public const string ViewResultNotSupportedMessage = "The view result is not supported.";
        public const string FileTypeNotSupportedMessage = "The file type is not supported.";
        public const string ErrorViewDoesntExistMessage = "Error view does not exist.";
        public const string RequestedViewDoesntExistsMessage = "Requested view does not exist.";
        public const string LayoutViewDoesntExistsMessage = "Layout view does no exist.";
    }
}