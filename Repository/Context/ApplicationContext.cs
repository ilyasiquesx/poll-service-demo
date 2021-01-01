using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository.Context
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Option> Options { get; set; }

        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Topic>().HasOne(t => t.User)
                .WithMany(u => u.Topics)
                .HasForeignKey(t => t.CreatedBy);

            builder.Entity<Option>().HasOne(o => o.Topic)
                .WithMany(t => t.Options)
                .HasForeignKey(o => o.TopicId);

            builder.Entity<Vote>().HasOne(v => v.User)
                .WithMany(u => u.Votes)
                .HasForeignKey(v => v.UserId);

            builder.Entity<Vote>().HasOne(v => v.Option)
                .WithMany(o => o.Votes)
                .HasForeignKey(v => v.OptionId);

            builder.Entity<Vote>().HasOne(v => v.Topic)
                .WithMany(t => t.Votes)
                .HasForeignKey(v => v.TopicId);

            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var addedAuditedEntities = ChangeTracker.Entries<IAuditedEntity>()
                .Where(p => p.State == EntityState.Added)
                .Select(p => p.Entity);

            var modifiedAuditedEntities = ChangeTracker.Entries<IAuditedEntity>()
                .Where(p => p.State == EntityState.Modified)
                .Select(p => p.Entity);

            var now = DateTime.Now;

            foreach (var added in addedAuditedEntities)
            {
                added.CreatedAt = now;
                added.UpdatedAt = now;
            }

            foreach (var modified in modifiedAuditedEntities)
            {
                modified.UpdatedAt = now;
            }
            
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}