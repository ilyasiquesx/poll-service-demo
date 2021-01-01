using System;

namespace Service.ViewModels.Topics
{
    public class UserVotedModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public DateTime VotedAt { get; set; }
    }
}