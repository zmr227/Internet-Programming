using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Razor.Models
{
    public class Story
    {
        public int Id { get; set; }
        [Required]
        public String Title { get; set; }
        public String Author { get; set; }
        public String Description { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
