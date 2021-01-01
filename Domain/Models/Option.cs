using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Option : BaseEntity
    {
        public Guid TopicId { get; set; }
        
        public Topic Topic { get; set; }
        
        [MaxLength(32)]
        [Required]
        public string Name { get; set; }

        public ICollection<Vote> Votes { get; set; }
    }
}