using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Infra.CrossCutting.Identity.Model
{
    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Codigo")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Lembrar-me Nesta Navegador")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }
}