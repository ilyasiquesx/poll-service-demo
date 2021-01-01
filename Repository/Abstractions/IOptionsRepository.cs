using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Repository.Abstractions
{
    public interface IOptionsRepository : IRepository<Option>
    {
        Task CreateRangeAsync(IEnumerable<Option> options);
        IEnumerable<Option> GetOptionsForTopic(Guid id);
        bool DoesOptionExist(Guid id);
    }
}