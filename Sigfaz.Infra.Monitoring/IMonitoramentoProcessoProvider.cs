using Porto.Saude.Infra.Monitoring;

namespace Sigfaz.Infra.Monitoring
{
    public interface IMonitoramentoProcessoProvider
    {
        IMonitoramentoProcesso Current { get; set; }
    }
}
