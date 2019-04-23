using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class SpecialTags
    {
        public SpecialTags()
        {
            Stories = new HashSet<Stories>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Stories> Stories { get; set; }
    }
}
