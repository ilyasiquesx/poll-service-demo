using System.ComponentModel.DataAnnotations;

namespace Service.ViewModels.Options
{
    public class AddOptionModel
    {
        [MaxLength(32)]
        [Required(ErrorMessage = "Поле обязательно к заполнению")]
        public string Name { get; set; }
    }
}