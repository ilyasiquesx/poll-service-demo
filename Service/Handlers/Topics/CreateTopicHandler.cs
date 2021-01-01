using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Repository.Abstractions;
using Service.Commands.Topics;

namespace Service.Handlers.Topics
{
    public class CreateTopicHandler : IRequestHandler<CreateTopicCommand, Guid>
    {
        private readonly IOptionsRepository _optionsRepository;
        private readonly ILogger<CreateTopicHandler> _logger;
        private readonly ITopicsRepository _topicsRepository;
        private readonly ITransaction _databaseTransaction;
        private readonly IMapper _mapper;

        public CreateTopicHandler(IMapper mapper,
            ITopicsRepository topicsRepository,
            IOptionsRepository optionsRepository,
            ITransaction databaseTransaction, ILogger<CreateTopicHandler> logger)
        {
            _mapper = mapper;
            _topicsRepository = topicsRepository;
            _optionsRepository = optionsRepository;
            _databaseTransaction = databaseTransaction;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            var topic = _mapper.Map<Topic>(request);
            var options = _mapper.Map<List<Option>>(request.AddOptionModels);

            try
            {
                var _ = options.ToDictionary(k => k.Name, v => string.Empty);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Each option should be different");
            }

            foreach (var opt in options)
            {
                opt.TopicId = topic.Id;
            }

            await using var transaction = _databaseTransaction.BeginTransaction();
            try
            {
                await _topicsRepository.CreateAsync(topic);
                await _optionsRepository.CreateRangeAsync(options);
                transaction.Commit();
                _logger.LogInformation("{Message} {TopicId} {TopicName} {CreatedBy}", "Created topic", topic.Id, topic.Title, topic.User.UserName);
                return topic.Id;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}