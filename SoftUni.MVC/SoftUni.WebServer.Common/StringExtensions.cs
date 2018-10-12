namespace SoftUni.WebServer.Common
{
    using System.IO;

    public static class StringExtensions
    {
        public static string CapitalizeFirstLetter(this string param)
            => param[0].ToString().ToUpper() + param.Substring(1);

        public static bool FileExists(string path)
            => File.Exists(path);

        public static string ReadAllText(string path)
            => File.ReadAllText(path);
    }
}