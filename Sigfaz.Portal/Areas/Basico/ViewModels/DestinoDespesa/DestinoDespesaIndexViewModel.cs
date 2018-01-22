using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Portal.Areas.Basico.ViewModels.DestinoDespesa
{
    public class DestinoDespesaIndexViewModel
    {
        [Key]
        public int DestinoDespesaId { get; set; }

        [Required(ErrorMessage = "Preencha o nome do destinoda despesa")]
        [MaxLength(200)]
        [DisplayName("Destino Despesa")]
        public string Descricao { get; set; }

    }
}