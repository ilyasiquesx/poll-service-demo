using System;
using System.Collections.Generic;
using Domain.Models;

namespace Repository.Abstractions
{
    public interface ITopicsRepository : IRepository<Topic>
    {
        bool DoesTopicExist(Guid id);
        IEnumerable<Topic> GetYoungestTwenty();
    }
}