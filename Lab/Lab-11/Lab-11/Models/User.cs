using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab11.Models
{
    public class User
    {
        // user ID from AspNetUser table.
        public string UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public UserStatus Status { get; set; }

        public virtual ICollection<Story> Stories { get; set; }
    }

    public enum UserStatus
    {
        Submitted,
        Approved,
        Rejected
    }
}
