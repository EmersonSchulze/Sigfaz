using System;
using System.Reflection;
using System.Transactions;
using Sigfaz.ComponentModel;
using Sigfaz.Infra.Data.NHibernate;
using Sigfaz.Infra.Data.Repositorio;
using Sigfaz.Infra.Monitoring.Processos.Entidades;
using log4net;
using Porto.Saude.Infra.Monitoring;
using Porto.Saude.Infra.Monitoring.Processos.Procedure;

namespace Sigfaz.Infra.Monitoring.Processos
{
    internal class MonitoramentoProcesso : IMonitoramentoProcesso
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(MonitoramentoProcesso));
        
        private static readonly bool logDebug = logger.IsDebugEnabled;
        
        public SituacaoMonitoramento SituacaoAtual { internal set; get; }
        
        public string Descricao { internal set; get; }
        
        public string MensagemAtual { internal set; get; }
        
        private long progressoAtual;
        
        public long ProgressoAtual 
        {
            get
            {
                return progressoAtual;
            }
            internal set
            {
                progressoAtual = value;
                if (progressoAtual < 0)
                    progressoAtual = 0;
                if (progressoAtual > progressoMaximo)
                    progressoAtual = progressoMaximo;
            }
        }

        private long progressoMaximo;
        
        public long ProgressoMaximo
        {
            get
            {
                return progressoMaximo;
            }
            internal set
            {
                progressoMaximo = (value <= 0) ? 1 : value;
            }
        }

        public long HandleRotina { internal set; get; }
        
        public string TabelaRotina { internal set; get; }
        
        public long HandleRegistro { internal set; get; }
        
        public long UsuarioExecucao { internal set; get; }
        
        public string HostExecucao { internal set; get; }
        
        public long UsuarioAbortar { internal set; get; }
        
        public string HostAbortar { internal set; get; }
        
        public string OcorrenciaFinal { internal set; get; }
        
        private string OcorrenciaConcatenar;
        
        public string LocalChamada { internal set; get; }
        
        public string ArquivoLogs { internal set; get; }
        
        public bool InstanciaAutomatica { internal set; get; }
        
        public string ChaveCorrelacao { internal set; get; }
        
        public long HandleProcessoPai { internal set; get; }
        
        public IMonitoramentoProcesso MonitoramentoPai { get; internal set; }
        
        public bool ProgressoAutomatico { internal set; get; }

        public bool GerouCriticas { internal set; get; }

        public bool PegarOcorrenciasNaTabelaInformada { internal set; get; }

        internal MonitoramentoProcesso(long handleUsuario, MethodBase localChamada)
            :this(String.Empty, String.Empty, 0, handleUsuario, localChamada, false, false)
        {
        }

        internal MonitoramentoProcesso(string descricao, string mensagemAtual, long maximoProgresso, long handleUsuario, MethodBase localChamada, bool instanciaAutomatica, bool progressoAutomatico, bool pegarOcorrenciasNaTabelaInformada = true) :
            this(descricao, mensagemAtual, maximoProgresso, handleUsuario, 0, String.Empty, localChamada, instanciaAutomatica, progressoAutomatico, pegarOcorrenciasNaTabelaInformada)
        {
        }

        internal MonitoramentoProcesso(string descricao, string mensagemAtual, long progressoMaximo,
            long handleUsuario, long handleRotina, string tabelaRotina, MethodBase localChamada, bool instanciaAutomatica, bool progressoAutomatico, bool pegarOcorrenciasNaTabelaInformada = true)
        {
            this.GerouCriticas = false;
            this.TabelaRotina = tabelaRotina;
            this.HandleRotina = handleRotina;
            this.PegarOcorrenciasNaTabelaInformada = pegarOcorrenciasNaTabelaInformada;
            this.ProgressoMaximo = progressoMaximo;
            this.MensagemAtual = mensagemAtual;
            this.Descricao = descricao;
            this.MonitoramentoPai = MonitoramentoProcessoHelper.Current;
            this.LocalChamada = MonitoramentoProcessoHelper.FormatarLocalChamada(localChamada);
            this.InstanciaAutomatica = instanciaAutomatica;
            this.HandleRegistro = 0;
            this.OcorrenciaConcatenar = String.Empty;
            this.ArquivoLogs = null;
            this.ProgressoAutomatico = progressoAutomatico;
            if (ContextoAcessoHelper.Current != null)
                this.ChaveCorrelacao = ContextoAcessoHelper.Current.ChaveCorrelacao;
            if (this.MonitoramentoPai != null)
                this.HandleProcessoPai = MonitoramentoPai.HandleRegistro;

            ProgressoAtual = 0;
            UsuarioExecucao = handleUsuario;
            HostExecucao = Environment.MachineName;
            UsuarioAbortar = 0;
            HostAbortar = String.Empty;
            OcorrenciaFinal = String.Empty;

            MonitoramentoProcessoHelper.ChecarTimeout();
        }        

        private SituacaoMonitoramento CallProcedure(SituacaoMonitoramento SituacaoSolicitada)
        {
            BSMonitoramento resultado = new BSMonitoramento();

            string ocorrenciasGravar = OcorrenciaFinal ?? String.Empty;
            var concatenarOcorrencias = false;
            if (!String.IsNullOrEmpty(OcorrenciaConcatenar))
            {
                concatenarOcorrencias = true;
                ocorrenciasGravar = OcorrenciaConcatenar;
                OcorrenciaConcatenar = String.Empty;
            }

            var splitOcorrencias = ocorrenciasGravar.Split(3950);
            if (splitOcorrencias.Count > 0)
            {
                foreach (string ocorrencia in splitOcorrencias)
                {
                    resultado = InternalExecuteProcedure(SituacaoSolicitada, concatenarOcorrencias, ocorrencia);
                    concatenarOcorrencias = true;
                }
            }
            else
                resultado = InternalExecuteProcedure(SituacaoSolicitada, concatenarOcorrencias, string.Empty);

            if (SituacaoSolicitada == SituacaoMonitoramento.Iniciada)
                HandleRegistro = resultado.Handle ?? 0;

            return String.IsNullOrEmpty(resultado.Situacao)
                       ? SituacaoMonitoramento.Nenhuma
                       : (SituacaoMonitoramento)Int32.Parse(resultado.Situacao);
        }

        private BSMonitoramento InternalExecuteProcedure(SituacaoMonitoramento SituacaoSolicitada, bool concatenarOcorrencias, string ocorrencia)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                using (var session = SessionHelper.SessionFactory.OpenSession())
                {
                    var procedureInvoker = new ProcedureInvoker(session);
                    var bsMonitoramento = BSMonitoramento.Executar(procedureInvoker,
                                                logger,
                                                (long?)SituacaoSolicitada,
                                                HandleRegistro,
                                                Descricao,
                                                UsuarioExecucao,
                                                HostExecucao,
                                                ProgressoAtual,
                                                ProgressoMaximo,
                                                HostAbortar,
                                                UsuarioAbortar,
                                                MensagemAtual,
                                                ocorrencia,
                                                HandleRotina,
                                                TabelaRotina,
                                                LocalChamada,
                                                ArquivoLogs,
                                                "-",
                                                concatenarOcorrencias ? 1 : 0,
                                                ChaveCorrelacao,
                                                HandleProcessoPai,
                                                BooleanHelper.getBuilderBooleanToString(ProgressoAutomatico),
                                                BooleanHelper.getBuilderBooleanToString(GerouCriticas));
                    scope.Complete();
                    return bsMonitoramento;
                }
            }
        }

        private void TratarConcatenacaoOcorrencias(string ocorrenciasPendentes, bool concatenarOcorrencias)
        {
            this.OcorrenciaConcatenar = String.Empty;
            if (!String.IsNullOrEmpty(ocorrenciasPendentes))
            {
                if ((concatenarOcorrencias) && (!String.IsNullOrEmpty(OcorrenciaFinal)))
                {
                    this.OcorrenciaFinal += String.Format("{0}{1}",
                        this.OcorrenciaFinal.EndsWith("\n") ? String.Empty : "\n", ocorrenciasPendentes);
                    this.OcorrenciaConcatenar = String.Format("{0}{1}",
                        this.OcorrenciaFinal.EndsWith("\n") ? String.Empty : "\n", ocorrenciasPendentes);
                }
                else
                    this.OcorrenciaFinal = ocorrenciasPendentes;
            }
        }

        internal void InicializarDados(string descricaoProcesso, string mensagemAtual, long progressoMaximo, long? handleRotina, string tabelaRotina)
        {
            this.Descricao = descricaoProcesso;
            this.MensagemAtual = mensagemAtual;
            this.ProgressoMaximo = progressoMaximo;
            this.InstanciaAutomatica = false;
            if (handleRotina.HasValue)
            {
                this.HandleRotina = handleRotina.Value;
                this.TabelaRotina = tabelaRotina;
            }
            this.SituacaoAtual = CallProcedure(SituacaoMonitoramento.Iniciada);
            ContextoAcessoHelper.Current.IdMonitoramento = this.HandleRegistro;
        }

        /// <summary>
        /// Atualiza a posição de progresso do monitoramento, atualizando a mensagem atual exibida no monitor de tarefas.
        /// </summary>
        /// <param name="mensagemAtual">Nova mensagem atual</param>
        /// <param name="posicaoAtual">Nova posição de progresso, com relação ao máximo inicializado</param>
        /// <param name="OcorrenciaParcial">Ocorrências de processamento até o momento</param>
        /// <param name="ConcatenarOcorrencias">Indica se a ocorrência parcial fornecida deve ser concatenada às ocorrências fornecidas anteriormente</param>
        public void AtualizarProgresso(string mensagemAtual, long posicaoAtual, string OcorrenciaParcial = null, bool ConcatenarOcorrencias = false)
        {
            AtualizarProgresso(mensagemAtual, posicaoAtual, ProgressoMaximo, OcorrenciaParcial, ConcatenarOcorrencias);
        }

        /// <summary>
        /// Atualiza a posição e o máximo de progresso do monitoramento, atualizando a mensagem atual exibida no monitor de tarefas.
        /// </summary>
        /// <param name="mensagemAtual">Nova mensagem atual</param>
        /// <param name="posicaoAtual">Nova posição de progresso</param>
        /// <param name="MaximoProgresso">Novo máximo de progresso</param>
        /// <param name="OcorrenciaParcial">Ocorrências de processamento até o momento</param>
        /// <param name="ConcatenarOcorrencias">Indica se a ocorrência parcial fornecida deve ser concatenada às ocorrências fornecidas anteriormente</param>
        public void AtualizarProgresso(string mensagemAtual, long posicaoAtual, long MaximoProgresso, string OcorrenciaParcial = null, bool ConcatenarOcorrencias = false)
        {
            try
            {
                this.ProgressoAtual = posicaoAtual;
                this.MensagemAtual = mensagemAtual;
                this.ProgressoMaximo = MaximoProgresso;
                TratarConcatenacaoOcorrencias(OcorrenciaParcial, ConcatenarOcorrencias);
                this.SituacaoAtual = CallProcedure(SituacaoMonitoramento.AtualizarProgresso);
                if (this.SituacaoAtual == SituacaoMonitoramento.AbortarSolicitado)
                    RecarregarDoBanco();
            }
            catch (Exception ex)
            {
                logger.Warn("Erro ao Atualizar Progresso", ex);
            }
        }

        /// <summary>
        /// Informa a conclusão do processo com sucesso, fornecendo as ocorrências finais do processo.
        /// </summary>
        /// <param name="mensagemAtual">Nova mensagem atual</param>
        /// <param name="ocorrencias">Texto completo das ocorrências do processo</param>
        /// <param name="ConcatenarOcorrencias">Indica se a ocorrência parcial fornecida deve ser concatenada às ocorrências fornecidas anteriormente</param>
        public void ConcluirComSucesso(string mensagemAtual, string ocorrencias, bool ConcatenarOcorrencias = false)
        {
            try
            {
                this.ProgressoAtual = this.ProgressoMaximo;
                this.MensagemAtual = mensagemAtual;
                TratarConcatenacaoOcorrencias(ocorrencias, ConcatenarOcorrencias);
                this.SituacaoAtual = CallProcedure(SituacaoMonitoramento.FinalizadaComSucesso);
                if (ProgressoAutomatico || InstanciaAutomatica)
                    Dispose();
            }
            catch (Exception ex)
            {
                logger.Warn("Erro ao Concluir Com Sucesso", ex);
            }
        }

        /// <summary>
        /// Informa a interrupção do processo com erros
        /// </summary>
        /// <param name="ocorrencias">Texto completo das ocorrências do processo</param>
        /// <param name="ConcatenarOcorrencias">Indica se a ocorrência parcial fornecida deve ser concatenada às ocorrências fornecidas anteriormente</param>
        public void AbortarComErro(string ocorrencias, bool ConcatenarOcorrencias = false)
        {
            AbortarComErro(string.Empty, ocorrencias);
        }

        /// <summary>
        /// Informa a interrupção do processo com erros
        /// </summary>
        /// <param name="mensagemAtual">Nova mensagem atual</param>
        /// <param name="ocorrencias">Texto completo das ocorrências do processo</param>
        /// <param name="ConcatenarOcorrencias">Indica se a ocorrência parcial fornecida deve ser concatenada às ocorrências fornecidas anteriormente</param>
        public void AbortarComErro(string mensagemAtual, string ocorrencias, bool ConcatenarOcorrencias = false)
        {
            try
            {
                if (String.IsNullOrEmpty(mensagemAtual))
                    mensagemAtual = (UsuarioAbortar == 0) ? "Processo abortado devido a erros!" : "Processo abortado pelo usuário!";
                this.MensagemAtual = mensagemAtual;
                TratarConcatenacaoOcorrencias(ocorrencias, ConcatenarOcorrencias);
                this.SituacaoAtual = CallProcedure(SituacaoMonitoramento.FinalizadaComErro);
                if (ProgressoAutomatico || InstanciaAutomatica)
                    Dispose();
            }
            catch (Exception ex)
            {
                logger.Warn("Erro ao Abortar Com Erro", ex);
            }
        }

        /// <summary>
        /// Recarrega os dados do monitoramento a partir do registro do banco. Útil quando alguma procedure ou outra dll altera o monitoramento.
        /// </summary>
        /// <param name="repositorio">Instância do repositório para a SIS_PROCESSO</param>
        [Obsolete("Utilizar a sobrecarga sem parâmetros")]
        public void RecarregarDoBanco(IRepositorio<RegistroProcesso> repositorio)
        {
            RecarregarDoBanco();
        }

        /// <summary>
        /// Recarrega os dados do monitoramento a partir do registro do banco. Útil quando alguma procedure ou outra dll altera o monitoramento.
        /// </summary>
        /// <param name="repositorio">Instância do repositório para a SIS_PROCESSO</param>
        public void RecarregarDoBanco()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                using (var session = SessionHelper.SessionFactory.OpenSession())
                {
                    if (HandleRegistro == 0)
                    {
                        throw new ApolloBusinessException("Handle do registro de monitoramento ainda não determinado!");
                    }

                    var registro = session.Get<RegistroProcesso>(HandleRegistro);

                    if (registro == null)
                    {
                        throw new ApolloBusinessException(String.Format("Não encontrado registro de monitoramento com handle {0}!", HandleRegistro));
                    }

                    session.Refresh(registro);
                    session.Evict(registro);
                    registro.PegarOcorrenciasNaTabelaInformada = PegarOcorrenciasNaTabelaInformada;
                    this.ProgressoMaximo = registro.Maximo ?? 0;
                    this.ProgressoAtual = registro.Posicao ?? 0;
                    this.UsuarioExecucao = registro.Usuario ?? 0;
                    this.HostExecucao = registro.Host;
                    this.UsuarioAbortar = registro.Abortusuario ?? 0;
                    this.HostAbortar = registro.Aborthost;
                    this.HandleRotina = registro.Rotinaorigem ?? 0;
                    this.TabelaRotina = registro.Tabelaorigem;
                    this.Descricao = registro.Descricao;
                    this.MensagemAtual = registro.Mensagem;
                    this.OcorrenciaFinal = registro.CarregarOcorrencias(); // tratar
                    this.LocalChamada = registro.LocalDeChamada;
                    this.ArquivoLogs = registro.CaminhoDownloadLogs;
                    this.ProgressoAutomatico = registro.ProgressoAutomatico ?? false;
                    this.SituacaoAtual = (SituacaoMonitoramento)Int32.Parse(registro.Situacao);
                    this.ChaveCorrelacao = registro.ChaveCorrelacao;
                    this.HandleProcessoPai = registro.HandleProcessoPai ?? 0;
                    this.GerouCriticas = registro.GerouCriticas == true;
                    ContextoAcessoHelper.Current.IdMonitoramento = this.HandleRegistro;
                    scope.Complete();
                }
            }
        }

        public void Dispose()
        {
            RemoverDoContexto();
        }

        private void RemoverDoContexto()
        {
            MonitoramentoProcessoHelper.RemoverMonitoramentoContexto(this);
        }

        /// <summary>
        /// Realiza a associação de um dado com o monitoramento
        /// </summary>
        /// <param name="dadoAssociar">Instância do dado que está sendo associado</param>
        public void AssociarDado(Entidade dadoAssociar)
        {
            if (dadoAssociar == null)
                return;

            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                using (var session = SessionHelper.SessionFactory.OpenSession())
                {
                        var procedureInvoker = new ProcedureInvoker(session);
                        BSMonitoramentoAssociarDado.Executar(procedureInvoker, logger, HandleRegistro, dadoAssociar.GetType().ToString(), dadoAssociar.Handle);
                }
                scope.Complete();
            }
        }

        public void AbortarSolicitadoPeloUsuario(long handleUsuario)
        {
            try
            {
                this.MensagemAtual = "Usuário solicitou que o processo seja abortado!";
                this.HostAbortar = Environment.MachineName;
                this.UsuarioAbortar = handleUsuario;
                this.SituacaoAtual = CallProcedure(SituacaoMonitoramento.AbortarSolicitado);
                if (ProgressoAutomatico || InstanciaAutomatica)
                    Dispose();
            }
            catch (Exception ex)
            {
                logger.Warn("Erro ao marcar processo para ser abortado pelo usuário", ex);
            }
        }

        public void SinalizarCritica()
        {
            GerouCriticas = true;
            InternalExecuteProcedure(SituacaoMonitoramento.AtualizarProgresso, false, null);
        }

        public override bool Equals(object obj)
        {
            var temp = obj as MonitoramentoProcesso;

            if (temp == null)
                return false;

            return this.HandleRegistro == temp.HandleRegistro;
        }

        public override int GetHashCode()
        {
            return this.HandleRegistro.GetHashCode();
        }
    }
}
