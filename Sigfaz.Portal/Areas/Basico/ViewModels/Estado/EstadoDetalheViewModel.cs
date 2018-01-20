using System.ComponentModel;

namespace Sigfaz.Portal.Areas.Basico.ViewModels.Estado
{
    public class EstadoDetalheViewModel
    {
        [DisplayName("Estado")]
        public string Nome { get; set; }

        [DisplayName("UF")]
        public string Sigla { get; set; }

        [DisplayName("Código IBGE")]
        public int CodigoIbge { get; set; }

    }
}