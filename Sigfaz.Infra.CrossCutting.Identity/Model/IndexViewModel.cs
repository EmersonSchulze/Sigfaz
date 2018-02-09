using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Infra.CrossCutting.Identity.Model
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }

        [Display(Name = "Telefone")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Autenticação em 2 fatores")]
        public bool TwoFactor { get; set; }

        [Display(Name = "Lembrar-me")]
        public bool BrowserRemembered { get; set; }
    }
}