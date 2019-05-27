using System;
using System.ComponentModel.DataAnnotations;

namespace lab5.ViewModels.Account
{
    public class EditUserViewModel
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Длина строки должна быть от 4")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Почта")]
        public string Email { get; set; }
    }
}
