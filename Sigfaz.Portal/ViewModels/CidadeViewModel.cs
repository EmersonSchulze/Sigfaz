using Sigfaz.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sigfaz.Portal.ViewModels
{
    public class CidadeViewModel
    {
        [Key]
        public int CidadeId { get; set; }

        [Required(ErrorMessage = "Preencha o nome da cidade")]
        [MaxLength(200)]
        [DisplayName("Cidade")]
        public string Nome { get; set; }

        public int EstadoId { get; set; }

        public virtual Estado Estado { get; set; }
    }
}