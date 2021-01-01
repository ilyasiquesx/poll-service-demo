using System;

namespace Service.ViewModels.Topics
{
    public class GetTopicModel 
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }

        public string CreatedBy { get; set; }
        
        public int VotesCount { get; set; }
    }
}