using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sigfaz.Portal.ViewModels
{
    public class EstadoViewModel
    {
        [Key]
        public int EstadoId { get; set; }

        [Required(ErrorMessage ="Preencha o nome do estado")]
        [MaxLength(200)]
        [DisplayName("Estado")]
        public string Nome { get; set; }

        [MaxLength(2)]
        [MinLength(2, ErrorMessage = "Informa a sigla do estado")]
        [DisplayName("UF")]
        public string Sigla { get; set; }

        public int CodigoIbge { get; set; }

    }
}