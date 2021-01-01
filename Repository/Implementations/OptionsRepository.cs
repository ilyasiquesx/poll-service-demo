using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Repository.Abstractions;
using Repository.Context;

namespace Repository.Implementations
{
    public class OptionsRepository : AbstractRepository, IOptionsRepository
    {
        public OptionsRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }

        public IEnumerable<Option> GetAll()
        {
            return ApplicationContext.Options;
        }

        public Option GetById(Guid id)
        {
           return ApplicationContext.Options.FirstOrDefault(o => o.Id == id);
        }

        public async Task UpdateAsync(Option model)
        {
            await SaveChangesAsync();
        }

        public async Task CreateAsync(Option model)
        {
            await SaveChangesAsync();
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Option model)
        {
            await SaveChangesAsync();
            throw new NotImplementedException();
        }

        public async Task CreateRangeAsync(IEnumerable<Option> options)
        {
            ApplicationContext.Options.AddRange(options);
            await SaveChangesAsync();
        }

        public IEnumerable<Option> GetOptionsForTopic(Guid id)
        {
            return ApplicationContext.Options.Where(o => o.TopicId == id);
        }

        public bool DoesOptionExist(Guid id)
        {
            return ApplicationContext.Options.FirstOrDefault(o => o.Id == id) != null;
        }
    }
}