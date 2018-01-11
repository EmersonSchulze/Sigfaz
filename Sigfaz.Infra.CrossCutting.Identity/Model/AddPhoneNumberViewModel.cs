using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Infra.Cross.Cutting.Identity.Model
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Numero Telefone")]
        public string Number { get; set; }
    }
}