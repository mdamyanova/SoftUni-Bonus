namespace SoftUni.WebServer.Mvc.Routers
{
    using Common;
    using Exceptions;
    using Helpers;
    using Http.Enums;
    using Http.Interfaces;
    using Http.Responses;
    using Server.Interfaces;
    using System;
    using System.Collections.Generic;

    public class ResourceRouter : IHttpRequestHandler
    {
        public static readonly string ContentDirectory =
            $"{AppDomain.CurrentDomain.BaseDirectory}/Content/";

        private static readonly Dictionary<string, string> ExtensionsToMIMETypes = 
            new Dictionary<string, string>()
        {
            ["js"] = "application/javascript",
            ["css"] = "text/css",
            ["html"] = "text/html",
            ["htm"] = "text/html",
            ["jpg"] = "image/jpeg",
            ["jpeg"] = "image/jpeg",
            ["jpe"] = "image/jpeg",
            ["bmp"] = "image/bmp",
            ["gif"] = "image/gif",
            ["svg"] = "image/svg+xml",
            ["tif"] = "image/tiff",
            ["tiff"] = "image/tiff",
            ["ico"] = "image/x-icon",
        };

        public IHttpResponse Handle(IHttpRequest request)
        {
            try
            {
                var resourcePath = ResourceRouterHelper.GetResourcePath(request);
                var folderPath = ResourceRouterHelper.GetFolderPath(request);

                if (resourcePath.Length > 2)
                {
                    folderPath = resourcePath[2];
                }

                var extension = ResourceRouterHelper.GetExtension(folderPath);

                if (!ExtensionsToMIMETypes.ContainsKey(extension))
                {
                    throw new FileTypeNotSupportedException();
                }

                var fileContent = this.ReadFileContent(folderPath);

                return new FileResponse(HttpStatusCode.Ok, fileContent, 
                    ExtensionsToMIMETypes[extension]);
            }
            catch (Exception)
            {
                return new NotFoundResponse();
            }
        }

        private string ReadFileContent(string fileFullName)
        {
            return StringExtensions.ReadAllText(ContentDirectory + fileFullName);
        }
    }
}