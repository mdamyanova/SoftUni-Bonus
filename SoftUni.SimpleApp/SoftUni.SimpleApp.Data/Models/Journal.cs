namespace SoftUni.SimpleApp.Data.Models
{
    using System.Collections.Generic;

    public class Journal
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public IEnumerable<Task> Tasks
        {
            get
            {
                return new List<Task>();
            }
        }
    }
}