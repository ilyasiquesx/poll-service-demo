using System;
using System.Collections.Generic;
using Domain.Models;

namespace Repository.Abstractions
{
    public interface IVotesRepository : IRepository<Vote>
    {
        int GetCountOfVotesForOption(Guid id);
        int GetCountOfVotesForTopic(Guid id);
        bool HadUserVote(Guid topicId, string userId);
        IEnumerable<Vote> LastFiveVotesForTopic(Guid topicId);
    }
}