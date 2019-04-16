using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Storyphase.Models
{
    public class Stories
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
       
        [Display(Name = "Story Type")]
        public int StoryTypeId { get; set; }

        [Display(Name = "Category")]
        [ForeignKey("StoryTypeId")]
        public virtual StoryTypes StoryTypes { get; set; }

        [Display(Name = "Special Tag")]
        public int SpecialTagId { get; set; }

        [Display(Name = "Special Tag")]
        [ForeignKey("SpecialTagId")]
        public virtual SpecialTags SpecialTags { get; set; }

        [Display(Name = "Privacy Tag")]
        public int PrivacyTagId { get; set; }

        [Display(Name = "Privacy")]
        [ForeignKey("PrivacyTagId")]
        public virtual PrivacyTags PrivacyTags { get; set; }
    }

    public class StoryBlocks
    {
        public long Id { get; set; }
        [Required]
        public string Content { get; set; }
        public byte[] Image { get; set; }

        public long? StoryId { get; set; }
        [ForeignKey("StoriesId")]
        public Stories Story { get; set; }
    }

    public class Comments
    {
        public long Id { get; set; }

        [Required]
        public string Content { get; set; }

        public long? StoryId { get; set; }
        [ForeignKey("StoriesId")]
        public Stories Story { get; set; }
    }
}
