using System;
using System.ComponentModel;
using Sigfaz.ComponentModel;

namespace Sigfaz.Infra.Monitoring.Processos.Entidades
{
    /// <summary>
    /// Processos do Sistema (SIS_PROCESSO)
    /// </summary>
    [Description("Processos do Sistema")]
    public partial class RegistroProcesso : Entidade     {
        public virtual DateTime? Abortdatahora { get; set; }

        public virtual string Aborthost { get; set; }

        public virtual long? Abortusuario { get; set; }

        public virtual string Descricao { get; set; }

        public virtual DateTime? Fim { get; set; }

        public virtual string Host { get; set; }

        public virtual DateTime? Inicio { get; set; }

        public virtual string LocalDeChamada { get; set; }

        public virtual long? Maximo { get; set; }

        public virtual string Mensagem { get; set; }

        public virtual string OcorrenciaAntiga { get; set; }

        public virtual long? Posicao { get; set; }

        public virtual long? Rotinaorigem { get; set; }

        public virtual string Situacao { get; set; }

        public virtual string Tabelaorigem { get; set; }

        public virtual long? Usuario { get; set; }

        public virtual string CaminhoDownloadLogs { get; set; }

        public virtual long? HandleProcessoPai { get; set; }

        public virtual string ChaveCorrelacao { get; set; }

        public virtual string Z_ChaveCorrelacao { get; set; }

        public virtual bool? ProgressoAutomatico { get; set; }

        public virtual bool? GerouCriticas { get; set; }
    }
}