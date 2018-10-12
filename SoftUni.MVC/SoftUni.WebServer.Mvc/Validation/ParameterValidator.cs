namespace SoftUni.WebServer.Mvc.Validation
{
    using System.Collections.Generic;

    public class ParameterValidator
    {
        private readonly Dictionary<string, IList<string>> modelErrors;

        public ParameterValidator()
        {
            this.modelErrors = new Dictionary<string, IList<string>>();
        }

        public IReadOnlyDictionary<string, IList<string>> ModelErrors => this.modelErrors;

        public void AddModelError(string parameterName, string message)
        {
            if (!this.modelErrors.ContainsKey(parameterName))
            {
                this.modelErrors[parameterName] = new List<string>();
            }

            this.modelErrors[parameterName].Add(message);
        }
    }
}