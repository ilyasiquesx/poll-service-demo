using System;
using MediatR;

namespace Service.Commands.Votes
{
    public class AddVoteCommand : IRequest
    {
        public string UserId { get; set; }
        public Guid TopicId { get; set; }
        public Guid OptionId { get; set; }
    }
}