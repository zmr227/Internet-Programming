using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab9.Models
{
    public class Story
    {
        public Story()
        {
            Id = -1;
            Title = "undefined";
            Author = "unknown";
            AuthorId = -1;
            Privacy = "public"; // private, public ...
            Description = "undefined";
            Category = "undefined";
        }

        public int Id { get; set; }
        public String Title { get; set; }
        public String Author { get; set; }
        public int AuthorId { get; set; }
        public String Privacy { get; set; }
        public String Description { get; set; }
        public String Category { get; set; }
    }

    public class StoryList : IEnumerable<Story>
    {
        public List<Story> Stories { get; set; } = new List<Story>();
        private static StoryList instance { get; set; } = null;

        public int size()
        {
            return Stories.Count;
        }

        public void add(Story sty)
        {
            Stories.Add(sty);
        }

        public bool delete(int id)
        {
            if (0 <= id && id < size())
            {
                Stories.RemoveAt(id);
                return true;
            }
            return false;
        }

        public Story this[int i]
        {
            get { return Stories[i]; }
            set { Stories[i] = value; }
        }

        //----< both of these functions are needed to implement IEnumerable >----

        public IEnumerator<Story> GetEnumerator()
        {
            return Stories.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        //----< here's where persistance across views is managed >---

        public static StoryList getInstance()
        {
            if (instance == null)
            {
                instance = new StoryList();
            }
            return instance;
        }
        //----< constructor generates "getting started" list of courses >----

        private StoryList()
        {
            var sty = new Story
            {
                Id = 1,
                Title = "A Tour of the Underwater World",
                Category = "Science Fiction",
                Author = "Jane123",
                AuthorId = 1,
                Description = "Story 1"
            };
            Stories.Add(sty);
            sty = new Story
            {
                Id = 2,
                Title = "Sleeping Beauty",
                Category = "Fairytale",
                Author = "John30",
                AuthorId = 2,
                Description = "Story 2"
            };
            Stories.Add(sty);
            sty = new Story
            {
                Id = 3,
                Title = "Tomb Raider",
                Category = "Action & Adventure Fiction",
                Author = "Lara_Croft",
                AuthorId = 3,
                Description = "Story 3"
            };
            Stories.Add(sty);
        }
    }
}
