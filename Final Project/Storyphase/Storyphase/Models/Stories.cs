using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Storyphase.Models
{
    public class Stories
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        [MinLength(2)]
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        
        public string Author { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        [Display(Name = "Create Time")]
        public string CreateTimeString { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH':'mm':'ss");

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

        public int BlockNumber { get; set; }

        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<StoryBlocks> StoryBlocks { get; set; }
    }

    public class StoryBlocks
    {
        public long StoryBlocksId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }

        public int Position { get; set; }

        [Display(Name = "Story")]
        public int? StoriesId { get; set; }
        [ForeignKey("StoriesId")]
        public Stories Stories { get; set; }
    }

    public class Comments
    {
        public long Id { get; set; }

        [Required]
        public string Content { get; set; }

        public int? StoriesId { get; set; }

        [ForeignKey("StoriesId")]
        public Stories Stories { get; set; }

        public string UserName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreateTime { get; set; }
    }
}
