using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storyphase.Models.ViewModels
{
    public class StoriesViewModel
    {
        public Stories Stories { get; set; }
        public IList<StoryBlocks> StoryBlocks { get; set; }
        public IList<Comments> Comments { get; set; }

        public IEnumerable<StoryTypes> StoryTypes { get; set; }
        public IEnumerable<SpecialTags> SpecialTags { get; set; }
        public IEnumerable<PrivacyTags> PrivacyTags { get; set; }
    }
}
