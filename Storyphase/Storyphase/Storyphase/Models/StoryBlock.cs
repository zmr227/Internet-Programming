///////////////////////////////////////////////////////////////
// FileItem.cs - model for FileItems database                //
//                                                           //
// Jim Fawcett, CSE686 - Internet Programming, Spring 2019   //
///////////////////////////////////////////////////////////////

using Storyphase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Storyphase.Models
{
    public class StoryBlock
    {
        public long StoryBlockId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }

        public int? StoriesId { get; set; }
        [ForeignKey("StoriesId")]
        public Stories Stories { get; set; }
    }

    
}
