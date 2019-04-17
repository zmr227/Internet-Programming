///////////////////////////////////////////////////////////////
// FilesContext.cs - context for FileItems database          //
//                                                           //
// Jim Fawcett, CSE686 - Internet Programming, Spring 2019   //
///////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
  public class FileContext : DbContext
  {
    public FileContext(DbContextOptions<FileContext> options)
      : base(options)
    {
    }
        
    //public DbSet<Stories> Stories { get; set; }
    public DbSet<StoryBlock> StoryBlocks { get; set; }
    }
}
