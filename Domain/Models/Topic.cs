using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Abstractions;

namespace Domain.Models
{
    public class Topic : BaseEntity, IAuditedEntity
    {
        [MaxLength(32)]
        [Required]
        public string Title { get; set; }

        public ICollection<Option> Options { get; set; }

        public ICollection<Vote> Votes { get; set; }

        public string CreatedBy { get; set; }

        public User User { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}