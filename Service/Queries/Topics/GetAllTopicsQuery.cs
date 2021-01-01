using MediatR;
using Service.ViewModels.Topics;

namespace Service.Queries.Topics
{
    public class GetAllTopicsQuery : IRequest<TopicIndexModel>
    {
    }
}