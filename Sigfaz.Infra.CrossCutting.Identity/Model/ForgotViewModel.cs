using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Infra.Cross.Cutting.Identity.Model
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}