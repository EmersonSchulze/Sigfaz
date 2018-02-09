using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Infra.CrossCutting.Identity.Model
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Numero Telefone")]
        public string Number { get; set; }
    }
}