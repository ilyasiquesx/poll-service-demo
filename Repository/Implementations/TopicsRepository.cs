using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Repository.Abstractions;
using Repository.Context;

namespace Repository.Implementations
{
    public class TopicsRepository : AbstractRepository, ITopicsRepository
    {
        public TopicsRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }

        public IEnumerable<Topic> GetAll()
        {
            return ApplicationContext.Topics;
        }

        public Topic GetById(Guid id)
        {
            return ApplicationContext.Topics.FirstOrDefault(t => t.Id == id);
        }

        public Task UpdateAsync(Topic model)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(Topic model)
        {
            ApplicationContext.Topics.Add(model);
            await SaveChangesAsync();
        }

        public Task DeleteAsync(Topic model)
        {
            throw new NotImplementedException();
        }

        public bool DoesTopicExist(Guid id)
        {
            return ApplicationContext.Topics.FirstOrDefault(t => t.Id == id) != null;
        }

        public IEnumerable<Topic> GetYoungestTwenty()
        {
            return ApplicationContext.Topics.OrderByDescending(t => t.CreatedAt).Take(20);
        }
    }
}