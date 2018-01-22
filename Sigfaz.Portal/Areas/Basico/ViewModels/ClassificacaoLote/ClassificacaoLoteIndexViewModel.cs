using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Portal.Areas.Basico.ViewModels.ClassificacaoLote
{
    public class ClassificacaoLoteIndexViewModel
    {
        [Key]
        public int ClassificacaoLoteId { get; set; }

        [Required(ErrorMessage = "Preencha a descricao da Classificacao do lote")]
        [MaxLength(200)]
        [DisplayName("Classificação do Lote")]
        public string Descricao { get; set; }

        [DisplayName("Faixa de Idade em mesês")]
        public int Periodo { get; set; }

       
    }
}