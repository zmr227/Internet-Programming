using System;
using Microsoft.EntityFrameworkCore;

namespace Storyphase.Models
{
    public class StoryphaseContext : DbContext
    {
        public StoryphaseContext(DbContextOptions<StoryphaseContext> options)
            : base(options)
        {
        }
        public DbSet<Storyphase.Models.Story> Story { get; set; }
    }
}
