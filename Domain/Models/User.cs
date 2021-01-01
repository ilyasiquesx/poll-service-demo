using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class User : IdentityUser
    {
        public ICollection<Topic> Topics { get; set; }
        public ICollection<Vote> Votes { get; set; }
    }
}