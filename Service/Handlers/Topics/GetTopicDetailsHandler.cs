using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Repository.Abstractions;
using Service.Queries.Topics;
using Service.ViewModels.Options;
using Service.ViewModels.Topics;

namespace Service.Handlers.Topics
{
    public class GetTopicDetailsHandler : IRequestHandler<GetTopicByIdQuery, GetTopicDetailsModel>
    {
        private readonly ITopicsRepository _topicsRepository;
        private readonly IOptionsRepository _optionsRepository;
        private readonly IVotesRepository _votesRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public GetTopicDetailsHandler(ITopicsRepository topicsRepository, 
            IOptionsRepository optionsRepository, 
            IVotesRepository votesRepository, 
            UserManager<User> userManager, IMapper mapper)
        {
            _topicsRepository = topicsRepository;
            _optionsRepository = optionsRepository;
            _votesRepository = votesRepository;
            _userManager = userManager;
            _mapper = mapper;
        }
        
        public async Task<GetTopicDetailsModel> Handle(GetTopicByIdQuery request, CancellationToken cancellationToken)
        {
            var topic = _topicsRepository.GetById(request.Id);
            if (topic == null)
            {
                throw new ArgumentException($"Topic with id {request.Id} doesn't exist");
            }

            var options = _optionsRepository.GetOptionsForTopic(topic.Id).ToList();
            var pickOptionsModel = _mapper.Map<IEnumerable<PickOptionModel>>(options);
            var data = new Dictionary<string, int>();
            foreach (var option in options)
            {
                var countOfVotes = _votesRepository.GetCountOfVotesForOption(option.Id);
                data.Add(option.Name, countOfVotes);
            }
            
            var hadUserVote = _votesRepository.HadUserVote(topic.Id, request.UserId);
            var lastFiveVotes = _votesRepository.LastFiveVotesForTopic(topic.Id).ToList();
            var userVotedModels = new List<UserVotedModel>();
            foreach (var vote in lastFiveVotes)
            {
                var user = await _userManager.FindByIdAsync(vote.UserId);
                userVotedModels.Add(new UserVotedModel
                {
                    Id = user.Id,
                    Username = user.UserName,
                    VotedAt = vote.CreatedAt
                });
            }
            var result = new GetTopicDetailsModel
            {
                Title = topic.Title,
                Id = topic.Id,
                SerializedData = JsonConvert.SerializeObject(data),
                HadUserVote = hadUserVote,
                PickOptionModels = pickOptionsModel,
                UsersVoted = userVotedModels
            };

            return result;
        }
    }
}