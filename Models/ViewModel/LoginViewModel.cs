using System.ComponentModel.DataAnnotations;

namespace Models.ViewModel
{
    /// <summary>
    /// loginViewModel
    /// </summary>
    public class LoginViewModel
    {
        [Required(ErrorMessage = "用户名不能为空")]
        [StringLength(10, MinimumLength = 2)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        [StringLength(20, MinimumLength = 6)]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
