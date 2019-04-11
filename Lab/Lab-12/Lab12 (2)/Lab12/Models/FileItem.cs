/////////////////////////////////////////////////////////////
// FileItem.cs - Web Api Model                             //
//                                                         //
// Jim Fawcett, CSE686 - Internet Programming, Spring 2019 //
/////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12.Models
{
  public class FileItem
  {
    public int FileItemId { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
  }
}
