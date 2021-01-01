using System;
using MediatR;
using Service.ViewModels.Topics;

namespace Service.Queries.Topics
{
    public class GetTopicByIdQuery : IRequest<GetTopicDetailsModel>
    {
        public Guid Id { get; set; }
        
        public string UserId { get; set; }
    }
}