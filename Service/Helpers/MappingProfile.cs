using System;
using AutoMapper;
using Domain.Models;
using Service.Commands.Topics;
using Service.Commands.Votes;
using Service.ViewModels.Options;
using Service.ViewModels.Topics;

namespace Service.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Topic, GetTopicModel>();
            CreateMap<CreateTopicCommand, Topic>()
                .ForMember(t => t.CreatedBy,
                    opt => opt.MapFrom(ctc => ctc.UserId))
                .ForMember(t => t.Id, 
                    opt => 
                        opt.MapFrom(s => Guid.NewGuid()));
            CreateMap<AddOptionModel, Option>()
                .ForMember(o => o.Id, 
                    opt => 
                        opt.MapFrom(s => Guid.NewGuid()));
            CreateMap<AddVoteCommand, Vote>();
            CreateMap<Option, PickOptionModel>();
        }
    }
}