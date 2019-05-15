namespace SoftUni.WebServer.Common
{
    using System;

    public static class Validation
    {
        public static void EnsureNotNull(object obj, string name, string message = null)
        {
            if (message == null)
            {
                message = $"{name} must not be null.";
            }

            if (obj == null)
            {
                throw new ArgumentNullException(name, message);
            }
        }

        public static void EnsureNotNullOrEmptyString(string text, string name, string message = null)
        {
            if (message == null)
            {
                message = $"{name} must not be null or empty.";
            }

            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException(message, name);
            }
        }

        public static void EnsureMinLength(string text, string name, int minLength, string message = null)
        {
            if (message == null)
            {
                message = $"{name} cannot be null or less than {minLength} symbols long.";
            }

            if (string.IsNullOrEmpty(text) || text.Length < minLength)
            {
                throw new ArgumentException(message, name);
            }
        }

        public static void EnsureEqualStrings(string first, string second, string firstName, string secondName, string message = null)
        {
            if (message == null)
            {
                message = $"The fields {firstName} and {secondName} don't match.";
            }

            if (first != second)
            {
                throw new ArgumentException(message, firstName);
            }
        }
    }
}