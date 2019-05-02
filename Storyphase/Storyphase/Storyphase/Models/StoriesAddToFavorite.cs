using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Storyphase.Models
{
    public class StoriesAddToFavorite
    {
        public int Id { get; set; }
        public int StoryId { get; set; }

        [ForeignKey("StoryId")]
        public virtual Stories Stories { get; set; }

        public string UserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual ApplicationUsers Users { get; set; }
    }
}
