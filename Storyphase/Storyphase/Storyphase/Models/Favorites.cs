using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Storyphase.Models
{
    public class Favorites
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public int StoryId { get; set; }

        [ForeignKey("StoryId")]
        public virtual Stories Stories { get; set; }
    }
}
