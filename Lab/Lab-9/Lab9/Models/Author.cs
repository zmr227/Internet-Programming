using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Lab9.Models
{
    public class Author
    {
        public Author()
        {
            Id = -1;
            UserName = "undefined";
            FirstName = "undefined";
            LastName = "undefined";
            BirthDate = "xx/xx/xxxx";
            Email = "xxx@xx.com";
        }

        public int Id { get; set; }
        public String UserName { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String BirthDate { get; set; }
        public String Email { get; set; }

    }

    public class AuthorList : IEnumerable<Author>
    {
        public List<Author> Authors { get; set; } = new List<Author>();
        private static AuthorList instance { get; set; } = null;

        public int size()
        {
            return Authors.Count;
        }

        public void add(Author user)
        {
            Authors.Add(user);
        }

        public bool delete(int id)
        {
            if (0 <= id && id < size())
            {
                Authors.RemoveAt(id);
                return true;
            }
            return false;
        }

        public Author this[int i]
        {
            get { return Authors[i]; }
            set { Authors[i] = value; }
        }

        //----< both of these functions are needed to implement IEnumerable >----

        public IEnumerator<Author> GetEnumerator()
        {
            return Authors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        //----< here's where persistance across views is managed >---

        public static AuthorList getInstance()
        {
            if (instance == null)
            {
                instance = new AuthorList();
            }
            return instance;
        }
        //----< constructor generates "getting started" list of courses >----

        private AuthorList()
        {
            var user = new Author
            {
                Id = 1,
                UserName = "Jane123",
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane123@gmail.com",
                BirthDate = "08/02/1988"
            };
            Authors.Add(user);
            user = new Author
            {
                Id = 2,
                UserName = "John30",
                FirstName = "John",
                LastName = "Doe",
                Email = "john30@gmail.com",
                BirthDate = "30/08/1979"
            };
            Authors.Add(user);
            user = new Author
            {
                Id = 3,
                UserName = "Lara_Croft",
                FirstName = "Lara",
                LastName = "Croft",
                Email = "lara_croft@outlook.com",
                BirthDate = "25/10/1996"
            };
            Authors.Add(user);
        }
    }
}

