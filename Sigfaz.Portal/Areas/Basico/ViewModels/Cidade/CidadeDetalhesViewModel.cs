﻿using System.ComponentModel;

namespace Sigfaz.Portal.Areas.Basico.ViewModels.Cidade
{
    public class CidadeDetalheViewModel
    {
        [DisplayName("Cidade")]
        public string Nome { get; set; }

        [DisplayName("Estado")]
        public virtual Dominio.Entidades.Estado Estado { get; set; }
    }
}