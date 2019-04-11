/////////////////////////////////////////////////////////////
// FilesContext.cs - Web Api Context for InMemory DB       //
//                                                         //
// Jim Fawcett, CSE686 - Internet Programming, Spring 2019 //
/////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lab12.Models
{
  public class FileContext : DbContext
  {
    public FileContext(DbContextOptions<FileContext> options) 
      : base(options) { }

    public DbSet<FileItem> FileItems { get; set; }
  }
}
