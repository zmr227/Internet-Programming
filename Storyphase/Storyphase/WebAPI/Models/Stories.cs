using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Stories
    {
        public Stories()
        {
            Comments = new HashSet<Comments>();
            StoriesAddToFavorites = new HashSet<StoriesAddToFavorites>();
            StoryBlocks = new HashSet<StoryBlocks>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateTimeString { get; set; }
        public int StoryTypeId { get; set; }
        public int SpecialTagId { get; set; }
        public int PrivacyTagId { get; set; }

        public virtual PrivacyTags PrivacyTag { get; set; }
        public virtual SpecialTags SpecialTag { get; set; }
        public virtual StoryTypes StoryType { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<StoriesAddToFavorites> StoriesAddToFavorites { get; set; }
        public virtual ICollection<StoryBlocks> StoryBlocks { get; set; }
    }
}
