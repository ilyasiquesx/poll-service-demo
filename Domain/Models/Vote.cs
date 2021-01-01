using System;
using Domain.Abstractions;

namespace Domain.Models
{
    public class Vote : BaseEntity, IAuditedEntity
    {
        public Guid TopicId { get; set; }

        public Topic Topic { get; set; }

        public Guid OptionId { get; set; }

        public Option Option { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; }
    }
}