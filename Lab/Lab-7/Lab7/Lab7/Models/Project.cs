using System;
namespace Lab7.Models
{
    public class Project
    {
        public int Id { get; set; } = -1;
        public string Name { get; set; }
        public string Language { get; set; }
        public string Technology { get; set; }
        public string Purpose { get; set; }
        public string Notes { get; set; }

        public Project()
        {
        }
    }
}
