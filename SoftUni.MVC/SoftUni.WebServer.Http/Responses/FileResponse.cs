namespace SoftUni.WebServer.Http.Responses
{
    using Enums;
    using Http.Exceptions;
    using Http.Headers;

    public class FileResponse : ContentResponse
    {
        public FileResponse(HttpStatusCode statusCode, string fileContent, string mimeType)
            : base(statusCode, fileContent)
        {
            EnsureValidStatusCode(statusCode);

            this.Headers.Add(new HttpHeader(HttpHeader.ContentLength, this.Content.Length.ToString()));
            this.Headers.Add(new HttpHeader(HttpHeader.ContentDisposition, "attachment"));
            this.Headers.Add(new HttpHeader(HttpHeader.ContentType, mimeType));
        }

        private static void EnsureValidStatusCode(HttpStatusCode statusCode)
        {
            int statusCodeNumber = (int)statusCode;
            bool isValidResponseCode = statusCodeNumber >= 200 && statusCodeNumber < 300;

            if (!isValidResponseCode)
            {
                throw new InvalidResponseException("File responses need a OK-type status code.");
            }
        }
    }
}