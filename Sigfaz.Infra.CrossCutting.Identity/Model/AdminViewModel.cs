using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Sigfaz.Infra.CrossCutting.Identity.Model
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Sobrenome")]
        public string SobreNome { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}