using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Porto.Saude.Infra.Data;

namespace Porto.Saude.Infra.Monitoring.Performance
{
    public class MonitoramentoPerformance : IDisposable
    {
        private ILog logger;
        private IProcedureInvoker procedureInvoker;
        private string local;
        private string operation;
        private long handleSisMonitor;

        public MonitoramentoPerformance(string operation, string local, IProcedureInvoker procedureInvoker, ILog logger)
        {
            this.procedureInvoker = procedureInvoker;
            this.logger = logger;
            this.operation = operation;
            this.local = local;
            var monitor = BSSISMONITOR.Executar(procedureInvoker, logger, "I", operation, local, handleSisMonitor);
            handleSisMonitor = monitor.Sequencia ?? 0;
        }

        public void Dispose()
        {
            var monitor = BSSISMONITOR.Executar(procedureInvoker, logger, "F", operation, local, handleSisMonitor);
        }
    }
}
