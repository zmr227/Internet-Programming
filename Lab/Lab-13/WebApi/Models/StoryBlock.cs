///////////////////////////////////////////////////////////////
// FileItem.cs - model for FileItems database                //
//                                                           //
// Jim Fawcett, CSE686 - Internet Programming, Spring 2019   //
///////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class StoryBlock
    {
        public long StoryBlockId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        
    }
}
