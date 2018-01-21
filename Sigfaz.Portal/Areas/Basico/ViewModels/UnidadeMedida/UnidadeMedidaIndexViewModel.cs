using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Portal.Areas.Basico.ViewModels.UnidadeMedida
{
    public class UnidadeMedidaIndexViewModel
    {
        [Key]
        public int UnidadeMedidaId { get; set; }

        [MaxLength(200)]
        [DisplayName("Unidade de Medida")]
        public string Descricao { get; set; }

        [MaxLength(2)]
        [MinLength(2, ErrorMessage = "Informa a sigla da unidade de medida")]
        [DisplayName("Sigla")]
        public string Sigla { get; set; }

    }
}