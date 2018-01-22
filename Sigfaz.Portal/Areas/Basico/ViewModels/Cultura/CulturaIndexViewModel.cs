using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Portal.Areas.Basico.ViewModels.Cultura
{
    public class CulturaIndexViewModel
    {
        [Key]
        public int CulturaId { get; set; }

        [Required(ErrorMessage = "Preencha o nome da Cultura")]
        [MaxLength(200)]
        [DisplayName("Cultura")]
        public string Descricao { get; set; }

        [DisplayName("Apresentavel")]
        public string Apelido { get; set; }

       
    }
}