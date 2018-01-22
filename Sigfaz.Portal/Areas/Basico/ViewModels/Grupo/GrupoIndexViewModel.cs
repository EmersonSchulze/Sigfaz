using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Portal.Areas.Basico.ViewModels.Grupo
{
    public class GrupoIndexViewModel
    {
        [Key]
        public int GrupoId { get; set; }

        [Required(ErrorMessage = "Preencha o nome do grupo")]
        [MaxLength(200)]
        [DisplayName("Grupo")]
        public string Descricao { get; set; }

       
    }
}