using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Storyphase.Models
{
    public class ApplicationUsers : IdentityUser
    {
        [Display(Name = "Author")]
        public string Name { get; set; }

        [NotMapped]
        public bool IsAdmin { get; set; }

    }
}
