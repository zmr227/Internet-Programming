using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Storyphase.Models;

namespace Storyphase.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        // add table to DB
        public DbSet<StoryTypes> StoryTypes { get; set; }
        public DbSet<SpecialTags> SpecialTags { get; set; }
        public DbSet<PrivacyTags> PrivacyTags { get; set; }

        public DbSet<StoryBlocks> StoryBlocks { get; set; }
        public DbSet<Stories> Stories { get; set; }
        
        public DbSet<Comments> Comments { get; set; }

        public DbSet<StoriesAddToFavorite> StoriesAddToFavorites { get; set; }

        public DbSet<ApplicationUsers> ApplicationUsers { get; set; }
        
    }
}
