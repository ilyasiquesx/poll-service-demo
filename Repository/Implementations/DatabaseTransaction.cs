using Microsoft.EntityFrameworkCore.Storage;
using Repository.Abstractions;
using Repository.Context;

namespace Repository.Implementations
{
    public class DatabaseTransaction : ITransaction
    {
        private readonly ApplicationContext _context;

        public DatabaseTransaction(ApplicationContext context)
        {
            _context = context;
        }
        
        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }
    }
}