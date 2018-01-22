using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Portal.Areas.Basico.ViewModels.ItemManutencao
{
    public class ItemManutencaoIndexViewModel
    {
        [Key]
        public int ItemManutencaoId { get; set; }

        [Required(ErrorMessage = "Preencha o nome do item da manutenção")]
        [MaxLength(200)]
        [DisplayName("Item da Manutenção")]
        public string Descricao { get; set; }

    }
}