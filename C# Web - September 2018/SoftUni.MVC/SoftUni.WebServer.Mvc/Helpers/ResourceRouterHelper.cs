namespace SoftUni.WebServer.Mvc.Helpers
{
    using Common;
    using Http.Interfaces;
    using System.Linq;

    public static class ResourceRouterHelper
    {
        public static string[] GetResourcePath(IHttpRequest request)
            => request.Path
            .Split(Constants.ResourcePathSplit, Constants.ResourcePathSplitCount);

        public static string GetFolderPath(IHttpRequest request)
            => request.Path;

        public static string GetExtension(string folderPath)
            => folderPath.Split(Constants.ExtensionPathSplit).Last();
    }
}