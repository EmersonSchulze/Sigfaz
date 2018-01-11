using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Infra.Cross.Cutting.Identity.Model
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
