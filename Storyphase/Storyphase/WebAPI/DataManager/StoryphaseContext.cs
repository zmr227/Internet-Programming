using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAPI.Models
{
    public partial class StoryphaseContext : DbContext
    {
        public StoryphaseContext()
        {
        }

        public StoryphaseContext(DbContextOptions<StoryphaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<PrivacyTags> PrivacyTags { get; set; }
        public virtual DbSet<SpecialTags> SpecialTags { get; set; }
        public virtual DbSet<Stories> Stories { get; set; }
        public virtual DbSet<StoriesAddToFavorites> StoriesAddToFavorites { get; set; }
        public virtual DbSet<StoryBlocks> StoryBlocks { get; set; }
        public virtual DbSet<StoryTypes> StoryTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Storyphase;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Discriminator).IsRequired();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasIndex(e => e.StoriesId);

                entity.Property(e => e.Content).IsRequired();

                entity.HasOne(d => d.Stories)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.StoriesId);
            });

            modelBuilder.Entity<PrivacyTags>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<SpecialTags>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Stories>(entity =>
            {
                entity.HasIndex(e => e.PrivacyTagId);

                entity.HasIndex(e => e.SpecialTagId);

                entity.HasIndex(e => e.StoryTypeId);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.PrivacyTag)
                    .WithMany(p => p.Stories)
                    .HasForeignKey(d => d.PrivacyTagId);

                entity.HasOne(d => d.SpecialTag)
                    .WithMany(p => p.Stories)
                    .HasForeignKey(d => d.SpecialTagId);

                entity.HasOne(d => d.StoryType)
                    .WithMany(p => p.Stories)
                    .HasForeignKey(d => d.StoryTypeId);
            });

            modelBuilder.Entity<StoriesAddToFavorites>(entity =>
            {
                entity.HasIndex(e => e.StoryId);

                entity.HasOne(d => d.Story)
                    .WithMany(p => p.StoriesAddToFavorites)
                    .HasForeignKey(d => d.StoryId);
            });

            modelBuilder.Entity<StoryBlocks>(entity =>
            {
                entity.HasIndex(e => e.StoriesId);

                entity.HasOne(d => d.Stories)
                    .WithMany(p => p.StoryBlocks)
                    .HasForeignKey(d => d.StoriesId);
            });

            modelBuilder.Entity<StoryTypes>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });
        }
    }
}
