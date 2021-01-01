using System;
using System.Collections.Generic;
using Service.ViewModels.Options;

namespace Service.ViewModels.Topics
{
    public class GetTopicDetailsModel : GetTopicModel
    {
        public bool HadUserVote { get; set; }

        public IEnumerable<PickOptionModel> PickOptionModels { get; set; }

        public Guid TopicId { get; set; }
        
        public Guid OptionId { get; set; }

        public IEnumerable<UserVotedModel> UsersVoted { get; set; }
        
        public string SerializedData { get; set; }
    }
}