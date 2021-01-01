using Microsoft.EntityFrameworkCore.Storage;

namespace Repository.Abstractions
{
    public interface ITransaction
    {
        IDbContextTransaction BeginTransaction();
    }
}