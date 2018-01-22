using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Portal.Areas.Basico.ViewModels.Raca
{
    public class RacaIndexViewModel
    {
        [Key]
        public int RacaId { get; set; }

        [Required(ErrorMessage = "Preencha o nome da Raça")]
        [MaxLength(200)]
        [DisplayName("Raca")]
        public string Descricao { get; set; }

        [MaxLength(4)]
        [DisplayName("Abreveatura da Raça")]
        public string Abreveatura { get; set; }

    }
}