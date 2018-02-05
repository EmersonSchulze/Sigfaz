using System;
using Sigfaz.ComponentModel;

namespace Sigfaz.Infra.Monitoring.Performance.Entidades
{
    /// <summary>
    ///   SIS_MONITOR
    /// </summary>
    public partial class RegistroMonitoramento  : Entidade     {
        public virtual DateTime? Fim { get; set; }

        public virtual DateTime? Inicio { get; set; }

        public virtual long? Inteiro1 { get; set; }

        public virtual long? Inteiro2 { get; set; }

        public virtual string Osuser { get; set; }

        public virtual string Procpai { get; set; }

        public virtual string Programa { get; set; }

        public virtual long? Sequencia { get; set; }

        public virtual string String1 { get; set; }

        public virtual string String2 { get; set; }

        public virtual DateTime? Time { get; set; }

    }
}