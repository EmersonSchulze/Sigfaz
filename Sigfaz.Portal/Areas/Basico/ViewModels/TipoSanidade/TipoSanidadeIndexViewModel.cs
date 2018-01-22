using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Portal.Areas.Basico.ViewModels.TipoSanidade
{
    public class TipoSanidadeIndexViewModel
    {
        [Key]
        public int TipoSanidadeId { get; set; }

        [Required(ErrorMessage = "Preencha o nome da cidade")]
        [MaxLength(200)]
        [DisplayName("Sanidade")]
        public string Descricao { get; set; }

        [DisplayName("Carencia em Mesês")]
        public int CarenciaMesses { get; set; }

    }
}