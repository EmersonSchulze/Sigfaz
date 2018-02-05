using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sigfaz.Infra.Monitoring.Processos.Entidades;
using Porto.Saude.ComponentModel.Extensions;
using System.Collections.Generic;
using Porto.Saude.ComponentModel;
using Porto.Saude.ComponentModel.DataAnnotations;
using Porto.Saude.Infra.Data;

namespace Porto.Saude.Infra.Monitoring
{
    /// <summary>
    /// Mensagens do Processo (SIS_PROCESSO_MENSAGENS)
    /// </summary>
    [Description("Mensagens do Processo")]
    public partial class MensagemRegistroProcesso : Entidade     {
        [DataHora(PrecisaoData.AnoMesDia, PrecisaoHora.HoraMinutoSegundo)]
        [Required]
        [Display(Name = "Data/Hora", Description = "")]
        public virtual DateTime Datahora { get; set; }

        [StringLength(4000)]
        [Required]
        [Display(Name = "Mensagem", Description = "")]
        public virtual string Mensagem { get; set; }

        [Lookup(TipoComplexo = typeof(RegistroProcesso), CamposPesquisa = "Descricao")]
        [Required]
        [Display(Name = "Processo", Description = "")]
        public virtual long? HandleProcesso { get; set; }

        public virtual RegistroProcesso Processo { get; set; }
    }
}