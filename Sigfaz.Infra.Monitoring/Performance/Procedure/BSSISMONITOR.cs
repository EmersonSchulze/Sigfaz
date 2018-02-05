using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using log4net;
using Oracle.DataAccess.Types;
using Porto.Saude.Infra.Data;

namespace Porto.Saude.Infra.Monitoring.Performance
{
    public class BSSISMONITOR
    {
        public virtual long? Sequencia { get; set; }

        public static BSSISMONITOR Executar(IProcedureInvoker procedureInvoker, ILog logger,
            string Operacao,
            string Local,
            string ProcPai,
            long? Sequencia)
        {
            List<OracleParameter> parameters = new List<OracleParameter>();
            parameters.Clear();
            parameters.Add(procedureInvoker.AdicionarParametro("pOperacao", Operacao, ParameterDirection.Input, OracleDbType.Varchar2, 1));
            parameters.Add(procedureInvoker.AdicionarParametro("pLocal", Local, ParameterDirection.Input, OracleDbType.Varchar2, 250));
            parameters.Add(procedureInvoker.AdicionarParametro("pProcPai", ProcPai, ParameterDirection.Input, OracleDbType.Varchar2, 250));
            parameters.Add(procedureInvoker.AdicionarParametro("pSequencia", Sequencia, ParameterDirection.InputOutput, OracleDbType.Int64));

            bool logDebug = logger.IsDebugEnabled;
            if (logDebug)
            {
                logger.DebugFormat("Chamada da procedure: BSSIS_MONITOR.");
                if (parameters.Count() > 0)
                {
                    logger.DebugFormat("Parâmetros: ");
                    parameters.ForEach(p => logger.DebugFormat(String.Format("{0}: {1}", p.ParameterName, p.Value)));
                }
            }

            procedureInvoker.ExecutaProcedure("BSSIS_MONITOR", ref parameters);

            if (logDebug)
            {
                logger.DebugFormat("Procedure executada.");
                parameters.Where(p => p.Direction != ParameterDirection.Input).ToList().ForEach(p => logger.DebugFormat(String.Format("Retorno - {0}: {1}", p.ParameterName, p.Value)));
            }

            var resultado = new BSSISMONITOR();

            if (parameters.Find(x => x.ParameterName == "pSequencia").Status == OracleParameterStatus.Success)
                resultado.Sequencia = ((OracleDecimal)parameters.Find(x => x.ParameterName == "pSequencia").Value).ToInt64();
            else
                resultado.Sequencia = null;

            return resultado;
        }
    }
}
