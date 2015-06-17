using System.ComponentModel.DataAnnotations;

namespace Models.ViewModel
{
    public class ResetPasswordViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [System.Web.Mvc.Remote("ExistMail","Member",ErrorMessage ="This mail addredd does not exist!")]
        public string Username { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "ValidCode")]
        public string Code { get; set; }
    }
}
