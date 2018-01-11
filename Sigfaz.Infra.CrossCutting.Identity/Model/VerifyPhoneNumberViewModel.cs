using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Infra.Cross.Cutting.Identity.Model
{
    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Código de Área")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Número do Telefone")]
        public string PhoneNumber { get; set; }
    }
}