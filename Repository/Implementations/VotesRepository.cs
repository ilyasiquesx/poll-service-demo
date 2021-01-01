using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Repository.Abstractions;
using Repository.Context;

namespace Repository.Implementations
{
    public class VotesRepository : AbstractRepository, IVotesRepository
    {
        public VotesRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }

        public IEnumerable<Vote> GetAll()
        {
            return ApplicationContext.Votes;
        }

        public Vote GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Vote model)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(Vote model)
        {
            ApplicationContext.Votes.Add(model);
            await SaveChangesAsync();
        }

        public Task DeleteAsync(Vote model)
        {
            throw new NotImplementedException();
        }

        public int GetCountOfVotesForOption(Guid id)
        {
            return ApplicationContext.Votes.Count(v => v.OptionId == id);
        }

        public int GetCountOfVotesForTopic(Guid id)
        {
            return ApplicationContext.Votes.Count(v => v.TopicId == id);
        }

        public bool HadUserVote(Guid topicId, string userId)
        {
            var usersVote = ApplicationContext.Votes.FirstOrDefault(v => v.TopicId == topicId && v.UserId == userId);
            return usersVote != null;
        }

        public IEnumerable<Vote> LastFiveVotesForTopic(Guid topicId)
        {
            return ApplicationContext.Votes.Where(v => v.TopicId == topicId)
                .OrderByDescending(v => v.CreatedAt).Take(5);
        }
    }
}