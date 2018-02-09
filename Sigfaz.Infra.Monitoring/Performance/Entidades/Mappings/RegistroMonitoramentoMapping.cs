using Sigfaz.Infra.Data.NHibernate.Mapping;
using FluentNHibernate.Mapping;
using Porto.Saude.Infra.Monitoring;

namespace Sigfaz.Infra.Monitoring.Performance.Entidades.Mappings
{
    public class RegistroMonitoramentoMapping : ClassMap<RegistroMonitoramento>
    {
        public RegistroMonitoramentoMapping() 
        {
    
            Table("SIS_MONITOR");
                    
            Id(x => x.Handle, "HANDLE").SelectBestIdentityGenerationStrategy("SEQ_SISMONITOR");
               
            Map(x => x.Fim, "FIM");

            Map(x => x.Inicio, "INICIO");
            Map(x => x.Inteiro1, "INTEIRO1");
            Map(x => x.Inteiro2, "INTEIRO2");

            Map(x => x.Osuser, "OSUSER").Length(80);

            Map(x => x.Procpai, "PROCPAI").Length(250);

            Map(x => x.Programa, "PROGRAMA").Length(250);
            Map(x => x.Sequencia, "SEQUENCIA");

            Map(x => x.String1, "STRING1").Length(250);

            Map(x => x.String2, "STRING2").Length(250);

            Map(x => x.Time, "TIME1");
            
        }
    }
}