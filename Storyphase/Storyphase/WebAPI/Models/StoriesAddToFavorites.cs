using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class StoriesAddToFavorites
    {
        public int Id { get; set; }
        public int StoryId { get; set; }

        public virtual Stories Story { get; set; }
    }
}
