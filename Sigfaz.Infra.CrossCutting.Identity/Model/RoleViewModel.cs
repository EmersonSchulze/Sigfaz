using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Infra.CrossCutting.Identity.Model
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Nome da Regra")]
        public string Name { get; set; }
    }
}