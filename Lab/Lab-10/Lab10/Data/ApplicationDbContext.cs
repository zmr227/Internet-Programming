using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CoursesApp.Models;

namespace CoursesApp.Data
{
  public class ApplicationDbContext : IdentityDbContext
  {
    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
        
        public DbSet<Story> Stories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Course>().ToTable("Course");
        }

    }

}
