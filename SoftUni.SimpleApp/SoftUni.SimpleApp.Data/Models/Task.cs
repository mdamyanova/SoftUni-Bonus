namespace SoftUni.SimpleApp.Data.Models
{
    public class Task
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int JournalId { get; set; }

        public Journal Journal { get; set; }
    }
}