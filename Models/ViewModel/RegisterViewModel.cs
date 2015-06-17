using System.ComponentModel.DataAnnotations;

namespace Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "用户名不能为空")]
        [StringLength(10, MinimumLength = 2)]
        [Display(Name = "用户名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密码长度需要在6~20之间")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Required(ErrorMessage = "确认密码不能为空")]
        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
