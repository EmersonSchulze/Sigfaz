using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Infra.CrossCutting.Identity.Model
{
    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Codigo")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Numero Telefone")]
        public string PhoneNumber { get; set; }
    }
}