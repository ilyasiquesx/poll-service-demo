using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Abstractions
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        Task UpdateAsync(T model);
        Task CreateAsync(T model);
        Task DeleteAsync(T model);
    }
}