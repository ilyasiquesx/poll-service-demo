using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Repository.Abstractions;
using Service.Queries.Topics;
using Service.ViewModels.Topics;

namespace Service.Handlers.Topics
{
    public class GetAllTopicsHandler : IRequestHandler<GetAllTopicsQuery, TopicIndexModel>
    {
        private readonly IVotesRepository _votesRepository;
        private readonly ITopicsRepository _topicsRepository;
        private readonly UserManager<User> _userManager;

        public GetAllTopicsHandler(ITopicsRepository topicsRepository,
            IVotesRepository votesRepository,
            UserManager<User> userManager)
        {
            _topicsRepository = topicsRepository;
            _votesRepository = votesRepository;
            _userManager = userManager;
        }

        public Task<TopicIndexModel> Handle(GetAllTopicsQuery request, CancellationToken cancellationToken)
        {
            var topics = _topicsRepository.GetYoungestTwenty().ToList();
            var getTopicModels =  topics.Select(t => new GetTopicModel
            {
                Id = t.Id,
                Title = t.Title,
                CreatedBy = _userManager.FindByIdAsync(t.CreatedBy).Result.UserName,
                VotesCount = _votesRepository.GetCountOfVotesForTopic(t.Id)
            });
            
            var topicIndexModel = new TopicIndexModel
            {
                GetTopicModels = getTopicModels
            };

            return Task.FromResult(topicIndexModel);
        }
    }
}