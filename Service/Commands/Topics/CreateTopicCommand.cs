using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Service.ViewModels.Options;

namespace Service.Commands.Topics
{
    public class CreateTopicCommand : IRequest<Guid>
    {
        public string UserId { get; set; }
        
        [MaxLength(32)]
        [Display(Name = "Название голосования")]
        [Required(ErrorMessage = "Поле обязательно к заполнению")]
        public string Title { get; set; }

        [Display(Name = "Набор вариантов")]
        public List<AddOptionModel> AddOptionModels { get; set; }
    }
}