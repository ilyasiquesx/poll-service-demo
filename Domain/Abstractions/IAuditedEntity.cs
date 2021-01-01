using System;

namespace Domain.Abstractions
{
    public interface IAuditedEntity
    {
        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; }
    }
}