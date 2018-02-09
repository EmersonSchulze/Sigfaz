using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Transactions;
using Sigfaz.Infra.Data.NHibernate;
using Sigfaz.Infra.Data.Procedure;
using Sigfaz.Infra.Data.Repositorio;
using Sigfaz.Infra.Extensions;
using Sigfaz.Infra.Monitoring.Processos.Entidades;
using log4net;
using Porto.Saude.Infra.Monitoring;
using Porto.Saude.Infra.Monitoring.Processos.Procedure;

namespace Sigfaz.Infra.Monitoring.Processos
{
    public class MonitoramentoProcessoHelper
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(MonitoramentoProcesso));
        private static readonly bool logDebug = logger.IsDebugEnabled;

        private static readonly object lockObject = new object();

        private const string mensagemInicialAutomatica = "Iniciando processo...";
        private static long ultimaChecagemTimeout = 0;

        private static IMonitoramentoProcessoProvider MonitoramentoProcessoProvider;

        /// <summary>
        /// Inicializa o Helper com o provider responsável por devolver o Monitoramento Corrente
        /// </summary>
        /// <param name="provider"></param>
        public static void Init(IMonitoramentoProcessoProvider provider)
        {
            Init(provider, true);
        }

        /// <summary>
        /// Inicializa o Helper com o provider responsável por devolver o Monitoramento Corrente
        /// </summary>
        /// <param name="provider"></param>
        public static void Init(IMonitoramentoProcessoProvider provider, Boolean faultInitialized = true)
        {
            if (MonitoramentoProcessoHelper.MonitoramentoProcessoProvider != null && faultInitialized)
                throw new InvalidOperationException("MonitoramentoProcessoHelper já inicializado");

            MonitoramentoProcessoHelper.MonitoramentoProcessoProvider = provider;
        }

        /// <summary>
        /// Retorna a entidade de Monitoramento associada ao request do serviço. Presente somente para operações assíncronas.
        /// </summary>
        public static IMonitoramentoProcesso Current
        {
            get
            {
                if (MonitoramentoProcessoHelper.MonitoramentoProcessoProvider == null)
                    throw new InvalidOperationException("MonitoramentoProcessoHelper precisa ser inicializado antes");

                return MonitoramentoProcessoHelper.MonitoramentoProcessoProvider.Current;
            }
        }

        internal static MonitoramentoProcesso NewMonitoramento(
            long handleUsuario, string descricaoProcesso, string mensagem, int progressoMaximo,
            long? handleRegistroRotina, string nomeTabelaRotina, MethodBase localChamada,
            bool instanciaAutomatica = false, bool progressoAutomatico = false, bool pegarOcorrenciasNaTabelaInformada = true)
        {
            var monitoramento = (handleRegistroRotina.HasValue)
                ? new MonitoramentoProcesso(descricaoProcesso, mensagem, progressoMaximo, handleUsuario, handleRegistroRotina.Value, nomeTabelaRotina, localChamada, instanciaAutomatica, progressoAutomatico, pegarOcorrenciasNaTabelaInformada)
                : new MonitoramentoProcesso(descricaoProcesso, mensagem, progressoMaximo, handleUsuario, localChamada, instanciaAutomatica, progressoAutomatico, pegarOcorrenciasNaTabelaInformada);
            AdicionarMonitoramentoContexto(monitoramento);
            return monitoramento;
        }

        /// <summary>
        /// Inicializar um novo monitoramento de um processo, passando os dados para o monitor de tarefas.
        /// </summary>
        /// <param name="procedureInvoker">Instância de IProcedureInvoker</param>
        /// <param name="handleUsuario">Handle do usuário corrente</param>
        /// <param name="descricaoProcesso">Descrição amigável do processo, deve identificar o dado sendo processado. Ex: Liberação do PEG nº 167928</param>
        /// <param name="mensagem">Mensagem inicial exibida no monitoramento</param>
        /// <param name="progressoMaximo">Valor que indica o valor máximo de progresso do processamento</param>
        /// <param name="handleRegistroRotina">Handle do registro da rotina, deve ser usado ao monitorar o processamento de uma rotina com tabela, situação e ocorrências.</param>
        /// <param name="nomeTabelaRotina">Nome da tabela da rotina, deve ser usado ao monitorar o processamento de uma rotina com tabela, situação e ocorrências.</param>
        /// <param name="pegarOcorrenciasNaTabelaInformada">Indica se deve pegar as ocorrências diretamente na tabela informada</param>
        /// <returns>Instância da entidade responsável pelo monitoramento</returns>
        [Obsolete("Utilize o método NovoMonitoramentoProgressoManual.")]
        public static IMonitoramentoProcesso InicializarNovoMonitoramento(
            IProcedureInvoker procedureInvoker, long handleUsuario, string descricaoProcesso, string mensagem,
            int progressoMaximo, long? handleRegistroRotina, string nomeTabelaRotina, bool pegarOcorrenciasNaTabelaInformada = true)
        {
            return NovoMonitoramentoProgressoManual(handleUsuario, descricaoProcesso, mensagem, progressoMaximo, handleRegistroRotina, nomeTabelaRotina, pegarOcorrenciasNaTabelaInformada);
        }

        /// <summary>
        /// Inicializar um novo monitoramento de um processo cujo progresso será controlado manualmente, passando os dados para o monitor de tarefas.
        /// </summary>
        /// <param name="handleUsuario">Handle do usuário corrente</param>
        /// <param name="descricaoProcesso">Descrição amigável do processo, deve identificar o dado sendo processado. Ex: Liberação do PEG nº 167928</param>
        /// <param name="mensagem">Mensagem inicial exibida no monitoramento</param>
        /// <param name="progressoMaximo">Valor que indica o valor máximo de progresso do processamento</param>
        /// <param name="handleRegistroRotina">Handle do registro da rotina, deve ser usado ao monitorar o processamento de uma rotina com tabela, situação e ocorrências.</param>
        /// <param name="nomeTabelaRotina">Nome da tabela da rotina, deve ser usado ao monitorar o processamento de uma rotina com tabela, situação e ocorrências.</param>
        /// <param name="pegarOcorrenciasNaTabelaInformada">Indica se deve pegar as ocorrências diretamente na tabela informada</param>
        /// <returns>Instância da entidade responsável pelo monitoramento</returns>
        public static IMonitoramentoProcesso NovoMonitoramentoProgressoManual(long handleUsuario, string descricaoProcesso, string mensagem,
            int progressoMaximo, long? handleRegistroRotina, string nomeTabelaRotina, bool pegarOcorrenciasNaTabelaInformada = true)
        {
            if (String.IsNullOrEmpty(mensagem))
                mensagem = mensagemInicialAutomatica;
            MonitoramentoProcesso monitoramento = MonitoramentoProcessoHelper.Current as MonitoramentoProcesso;
            if ((monitoramento == null) || (!monitoramento.InstanciaAutomatica)) // Se não existe monitoramento corrente, ou se não é automático
                monitoramento = NewMonitoramento(handleUsuario, descricaoProcesso, mensagem,
                    progressoMaximo, handleRegistroRotina, nomeTabelaRotina, null, instanciaAutomatica: false, progressoAutomatico: false);
            monitoramento.InicializarDados(descricaoProcesso, mensagem, progressoMaximo, handleRegistroRotina, nomeTabelaRotina);
            return monitoramento;
        }

        /// <summary>
        /// Inicializar um novo monitoramento de um processo cujo progresso será controlado automaticamente, passando os dados para o monitor de tarefas.
        /// </summary>
        /// <param name="handleUsuario">Handle do usuário corrente</param>
        /// <param name="descricaoProcesso">Descrição amigável do processo, deve identificar o dado sendo processado. Ex: Liberação do PEG nº 167928</param>
        /// <returns>Instância da entidade responsável pelo monitoramento</returns>
        public static void NovoMonitoramentoProgressoAutomatico(long handleUsuario, string descricaoProcesso)
        {
            var mensagem = mensagemInicialAutomatica;
            MonitoramentoProcesso monitoramento = MonitoramentoProcessoHelper.Current as MonitoramentoProcesso;
            if ((monitoramento == null) || (!monitoramento.InstanciaAutomatica)) // Se não existe monitoramento corrente, ou se não é automático
                monitoramento = NewMonitoramento(handleUsuario, descricaoProcesso, mensagem,
                    0, null, null, null, instanciaAutomatica: false, progressoAutomatico: true);
            monitoramento.InicializarDados(descricaoProcesso, mensagem, 0, null, null);
        }

        /// <summary>
        /// * Não deve ser chamado manualmente! Inicializar um novo monitoramento automático instanciado pelo Proxy.
        /// </summary>
        /// <param name="handleUsuario">Handle do usuário corrente</param>
        /// <param name="descricaoProcesso">Descrição amigável do processo, deve identificar o dado sendo processado. Ex: Liberação do PEG nº 167928</param>
        /// <returns>Instância da entidade responsável pelo monitoramento</returns>
        public static IMonitoramentoProcesso MonitoramentoAutomaticoProxy(long handleUsuario, MethodBase metodoChamada)
        {
            // Não inicializa monitoramento automático caso já exista um no contexto
            if (MonitoramentoProcessoHelper.Current != null)
                return null;

            var mensagem = mensagemInicialAutomatica;
            var descricaoProcesso = FormatarDescricaoProcesso(metodoChamada);
            MonitoramentoProcesso monitoramento = MonitoramentoProcessoHelper.Current as MonitoramentoProcesso;
            if ((monitoramento == null) || (!monitoramento.InstanciaAutomatica)) // Se não existe monitoramento corrente, ou se não é automático
                monitoramento = NewMonitoramento(handleUsuario, descricaoProcesso, mensagem,
                    1, null, null, null, false, true);
            monitoramento.InicializarDados(descricaoProcesso, mensagem, 1, null, null);
            return monitoramento;
        }

        internal static void ForcarMonitoramentoContexto(IMonitoramentoProcesso monitoramento)
        {
            if (MonitoramentoProcessoHelper.Current != null)
            {
                RemoverMonitoramentoContexto(MonitoramentoProcessoHelper.Current);
                AdicionarMonitoramentoContexto(monitoramento);
            }
        }

        /// <summary>
        /// Carrega um monitoramento existente com base no handle do registro da SIS_PROCESSO
        /// </summary>
        /// <param name="ProcedureInvoker">Instância de IProcedureInvoker</param>
        /// <param name="HandleUsuario">Handle do usuário corrente</param>
        /// <param name="idProcesso">Handle do registro da SIS_PROCESSO</param>
        /// <param name="repositorioRegistroProcesso">Instância do repositório da SIS_PROCESSO</param>
        /// <returns>Instância da entidade responsável pelo monitoramento</returns>
        [Obsolete("Utilize a sobrecarga sem ProcedureInvoker.")]
        public static IMonitoramentoProcesso CarregarMonitoramentoExistente(IProcedureInvoker ProcedureInvoker, long HandleUsuario,
            long idProcesso, IRepositorio<RegistroProcesso> repositorioRegistroProcesso, bool pegarOcorrenciasNaTabelaInformada = true)
        {
            return CarregarMonitoramentoExistente(HandleUsuario, idProcesso, pegarOcorrenciasNaTabelaInformada);
        }

        /// <summary>
        /// Carrega um monitoramento existente com base no handle do registro da SIS_PROCESSO
        /// </summary>
        /// <param name="HandleUsuario">Handle do usuário corrente</param>
        /// <param name="idProcesso">Handle do registro da SIS_PROCESSO</param>
        /// <returns>Instância da entidade responsável pelo monitoramento</returns>
        public static IMonitoramentoProcesso CarregarMonitoramentoExistente(long HandleUsuario,
            long idProcesso)
        {
            return CarregarMonitoramentoExistente(HandleUsuario, idProcesso, true);
        }

        /// <summary>
        /// Carrega um monitoramento existente com base no handle do registro da SIS_PROCESSO
        /// </summary>
        /// <param name="ProcedureInvoker">Instância de IProcedureInvoker</param>
        /// <param name="HandleUsuario">Handle do usuário corrente</param>
        /// <param name="idProcesso">Handle do registro da SIS_PROCESSO</param>
        /// <param name="repositorioRegistroProcesso">Instância do repositório da SIS_PROCESSO</param>
        /// <param name="pegarOcorrenciasNaTabelaInformada">Indica se deve pegar as ocorrências diretamente na tabela informada</param>
        /// <returns>Instância da entidade responsável pelo monitoramento</returns>
        public static IMonitoramentoProcesso CarregarMonitoramentoExistente(long HandleUsuario,
            long idProcesso, bool pegarOcorrenciasNaTabelaInformada = true)
        {
            if (logDebug)
            {
                logger.DebugFormat("CarregarMonitoramentoExistente");
                logger.DebugFormat(String.Format("HandleUsuario: {0}", HandleUsuario));
                logger.DebugFormat(String.Format("idProcesso: {0}", idProcesso));
            }
            //***** ESTA GAMBIARRA FOI DESENVOLVIDA PARA SOLUCIONAR TEMPORARIAMENTE UM PROBLEMA! =D ******//
            int tentativa = 0;
            while (tentativa < 3)
            {
                try
                {
                    MonitoramentoProcesso monitoramento = new MonitoramentoProcesso(HandleUsuario, null); // TODO: Pegar local
                    monitoramento.PegarOcorrenciasNaTabelaInformada = pegarOcorrenciasNaTabelaInformada;
                    monitoramento.InstanciaAutomatica = false;
                    monitoramento.HandleRegistro = idProcesso;
                    monitoramento.RecarregarDoBanco();
                    MonitoramentoProcessoHelper.ForcarMonitoramentoContexto(monitoramento);

                    return monitoramento;
                }
                catch (Exception Ex)
                {
                    tentativa++;
                    Thread.Sleep(150);
                    if (tentativa >= 3)
                    {
                        logger.ErrorFormat(MethodInfo.GetCurrentMethod().Name);
                        logger.ErrorFormat(String.Format("HandleUsuario: {0}", HandleUsuario));
                        logger.ErrorFormat(String.Format("idProcesso: {0}", idProcesso));
                        logger.ErrorFormat(String.Format("Erro: {0}", Ex.ToString()));
                        throw new Exception("Erro ao carregar monitoramento processo: " + Ex.ToString());
                    }
                }
            }

            return null;
        }

        internal static void AdicionarMonitoramentoContexto(IMonitoramentoProcesso monitoramento)
        {
            if (MonitoramentoProcessoProvider.Current != null)
            {
                (monitoramento as MonitoramentoProcesso).MonitoramentoPai = MonitoramentoProcessoHelper.Current;
            }
            MonitoramentoProcessoProvider.Current = monitoramento;
            ContextoAcessoHelper.Current.IdMonitoramento = monitoramento.HandleRegistro;
        }

        internal static void RemoverMonitoramentoContexto(IMonitoramentoProcesso monitoramento)
        {
            if (monitoramento != null && MonitoramentoProcessoProvider.Current != null && MonitoramentoProcessoProvider.Current.Equals(monitoramento))
            {
                var temp = monitoramento as MonitoramentoProcesso;
                if (temp.MonitoramentoPai != null)
                {
                    MonitoramentoProcessoProvider.Current = temp.MonitoramentoPai;
                    ContextoAcessoHelper.Current.IdMonitoramento = temp.HandleProcessoPai;
                }
                else
                {
                    MonitoramentoProcessoProvider.Current = null;
                    ContextoAcessoHelper.Current.IdMonitoramento = 0;
                }
            }
        }

        internal static string FormatarLocalChamada(MethodBase methodBase)
        {
            if (methodBase == null)
                return null;

            return String.Format("[{0}] {1}", methodBase.ReflectedType.FullName, methodBase);
        }

        /// <summary>
        /// Formata uma descrição para o processo de acordo com o método informado.
        /// </summary>
        /// <param name="methodInfo">Informação do método de acordo com o Reflection</param>
        /// <returns></returns>
        public static string FormatarDescricaoProcesso(MethodBase methodInfo)
        {
            try
            {
                var methodNamespace = methodInfo.ReflectedType.Namespace;

                // Se chamada é do cliente, finge que é do serviço
                methodNamespace = methodNamespace.Replace("AutoGenClient", "AutoGen");
                methodNamespace = methodNamespace.Replace(".References.", ".");

                // namespace Porto.Saude.Vidas.Services.Apolices.AutoGen.AlteracaoCadastralService
                // public interface IAlteracaoCadastralService
                // alterarAdesaoResponse alterarAdesao(alterarAdesaoRequest request);
                // Deve ficar assim: [Vidas > Apolices] Alteracao Cadastral - Alterar Adesao - identificador (Se fornecido)
                var nomeAplicacao = methodNamespace.Split('.')[2]; // Vidas
                var nomeNamespace = methodNamespace.Split('.')[4]; // Apolices
                var nomeClasse = methodNamespace.Split('.').Last()
                    .Replace("Service", String.Empty).SpaceOnCapitals(); // Alteracao Cadastral
                var nomeMetodo = methodInfo.Name.Split('.').Last().CapitalizeFirstLetter().SpaceOnCapitals(); // Alterar Adesao
                return String.Format("[{0} > {1}] {2} - {3}",
                    nomeAplicacao, nomeNamespace, nomeClasse, nomeMetodo);
            }
            catch
            {
                return FormatarLocalChamada(methodInfo);
            }
        }

        internal static void CarregarMonitoramentoContexto()
        {
            if ((MonitoramentoProcessoHelper.Current == null) && (ContextoAcessoHelper.Current != null) && (ContextoAcessoHelper.Current.IdMonitoramento > 0))
            {
                var monitoramento = CarregarMonitoramentoExistente(0, ContextoAcessoHelper.Current.IdMonitoramento);
                AdicionarMonitoramentoContexto(monitoramento);
            }
        }

        internal static void ChecarTimeout()
        {
            try
            {
                lock (lockObject)
                {
                    var elapsed = new TimeSpan(DateTime.Now.Ticks - ultimaChecagemTimeout);
                    if ((elapsed.TotalMinutes > 60) || (ultimaChecagemTimeout == 0))
                    {
                        using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {
                            using (var session = SessionHelper.SessionFactory.OpenSession())
                            {
                                var procedureInvoker = new ProcedureInvoker(session);
                                BSMonitoramentoTimeout.Executar(procedureInvoker, logger);
                                ultimaChecagemTimeout = DateTime.Now.Ticks;

                                scope.Complete();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logger.WarnFormat("Erro ao checar timeout de monitoramentos: {0}", e);
            }
        }
    }
}
