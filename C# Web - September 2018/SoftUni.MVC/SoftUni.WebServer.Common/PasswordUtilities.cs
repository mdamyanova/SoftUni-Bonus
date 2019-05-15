namespace SoftUni.WebServer.Common
{
    using System.Linq;
    using System.Security.Cryptography;

    public class PasswordUtilities
    {
        public static string GetPasswordHash(string password)
        {
            var sha256 = SHA256.Create();
            return string.Join(
                   string.Empty,
                   sha256
                       .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))
                       .Select(b => b.ToString("x2")));
        }
    }
}