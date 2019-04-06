using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Storyphase.Models
{
    public class StoryTypes
    {
        // auto identified as primary key
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
