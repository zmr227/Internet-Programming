using System;
using System.ComponentModel.DataAnnotations;

namespace Storyphase.Models
{
    public class Story
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
        public string Genre { get; set; }
    }
}
