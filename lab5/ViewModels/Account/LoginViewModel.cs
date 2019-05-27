using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace lab5.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required (ErrorMessage = "Не указано имя")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Длина строки должна быть от 4")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required (ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
