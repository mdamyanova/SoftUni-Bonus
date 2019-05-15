namespace SoftUni.WebServer.Mvc.Models
{
    using System.Collections.Generic;

    public class ViewData
    {
        public ViewData()
        {
            this.Data = new Dictionary<string, string>();
        }
        
        public IDictionary<string, string> Data { get; }

        public string this[string key]
        {
            get => this.Data[key];
            set => this.Data[key] = value;
        }
    }
}