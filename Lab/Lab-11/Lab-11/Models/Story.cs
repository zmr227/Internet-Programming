using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab11.Models
{
    public class Story
    {
        public int StoryID { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String Privacy { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastModifiedTime { get; set; }

        public virtual int? UserID { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }

    public class Comment
    {
        public int CommentID { get; set; }
        public String Content { get; set; }
        public String Username { get; set; }
        public DateTime PostDate { get; set; }

        public virtual int? StoryID { get; set; }
        public virtual Story Story { get; set; }
    }
}
