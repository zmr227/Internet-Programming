using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Comments
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public int? StoriesId { get; set; }

        public virtual Stories Stories { get; set; }
    }
}
