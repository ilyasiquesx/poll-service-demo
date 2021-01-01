using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Repository.Abstractions;
using Service.Commands.Votes;

namespace Service.Handlers.Votes
{
    public class AddVoteCommandHandler : IRequestHandler<AddVoteCommand>
    {
        private readonly ILogger<AddVoteCommandHandler> _logger;
        private readonly IOptionsRepository _optionsRepository;
        private readonly ITopicsRepository _topicsRepository;
        private readonly IVotesRepository _votesRepository;
        private readonly IMapper _mapper;

        public AddVoteCommandHandler(IVotesRepository votesRepository, 
            IMapper mapper, 
            IOptionsRepository optionsRepository, 
            ITopicsRepository topicsRepository, 
            ILogger<AddVoteCommandHandler> logger)
        {
            _votesRepository = votesRepository;
            _mapper = mapper;
            _optionsRepository = optionsRepository;
            _topicsRepository = topicsRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(AddVoteCommand request, CancellationToken cancellationToken)
        {
            if (!_topicsRepository.DoesTopicExist(request.TopicId))
            {
                throw new ArgumentException($"Topic with id {request.TopicId} doesn't exist");
            }
            
            if (_votesRepository.HadUserVote(request.TopicId, request.UserId))
            {
                throw new ArgumentException($"User with id {request.UserId} had already voted");
            }

            if (!_optionsRepository.DoesOptionExist(request.OptionId))
            {
                throw new ArgumentException($"Option with id {request.OptionId} doesn't exist");
            }
            
            var vote = _mapper.Map<Vote>(request);
            await _votesRepository.CreateAsync(vote);
            _logger.LogInformation("{Message} {VoteId} {OptionName} {TopicName} {CreatedBy}",
                "Created vote in topic", vote.Id, vote.Option.Name, vote.Topic.Title, vote.User.UserName);
            return Unit.Value;
        }
    }
}