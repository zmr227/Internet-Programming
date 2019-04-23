using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class StoryBlocks
    {
        public long StoryBlocksId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public int? StoriesId { get; set; }

        public virtual Stories Stories { get; set; }
    }
}
