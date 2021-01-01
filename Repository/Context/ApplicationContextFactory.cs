using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Repository.Context
{
    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationContext>();
            builder.UseNpgsql("Host=localhost;Port=5432;Database=voting-service;Username=postgres;Password=mysecretpassword");
            return new ApplicationContext(builder.Options);
        }
    }
}