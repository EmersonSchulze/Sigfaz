using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NHibernate;
using NHibernate.Exceptions;
using NHibernate.Type;
using Porto.Saude.Infra.Monitoring;

namespace Sigfaz.Infra.Monitoring.Processos.Entidades
{
    public class UserDto
    {
        public string Apelido;
        public string Nome;
    }

    public partial class RegistroProcesso : IValidatableObject
    {
        public RegistroProcesso()
        {
            PegarOcorrenciasNaTabelaInformada = true;
        }


        [Display(Name = "Abortado Por", Description = "Usuário que abortou o processo")]
        public virtual string DescricaoAbortUsuario
        {
            get
            {
                if ((Abortusuario ?? 0) == 0)
                    return String.Empty;
                var userDto = GetUserDto(Abortusuario.Value);
                return String.Format("{0} - {1}", userDto.Apelido, userDto.Nome);
            }
        }

        private UserDto GetUserDto(long handleUsuario)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                using (var session = SessionHelper.SessionFactory.OpenSession())
                {
                    Dictionary<String, IType> aliasCriteria = new Dictionary<string, IType>();

                    ISQLQuery criteria = session.CreateSQLQuery("SELECT Apelido, Nome FROM Z_GRUPOUSUARIOS WHERE HANDLE = :HANDLE");
                    criteria.SetParameter("HANDLE", handleUsuario);
                    criteria.AddScalar("Apelido", NHibernateUtil.String);
                    criteria.AddScalar("Nome", NHibernateUtil.String);
                    criteria.SetResultTransformer(NHibernate.Transform.Transformers.AliasToBean(typeof(UserDto)));

                    return criteria.UniqueResult<UserDto>();
                }
            }
        }

        [Display(Name = "Usuário de Execução", Description = "Usuário que iniciou o processo")]
        public virtual string DescricaoUsuario
        {
            get
            {
                if (Usuario == null)
                    return String.Empty;
                var userDto = GetUserDto(Usuario.Value);
                return String.Format("{0} - {1}", userDto.Apelido, userDto.Nome);
            }
        }

        [StringLength(10)]
        [Display(Name = "Situação", Description = "Situação atual do processo")]
        public virtual string DescricaoSituacao
        {
            get
            {
                var descricao = String.Empty;
                switch (Situacao)
                {
                    case "1": descricao = "Iniciando";
                        break;
                    case "2": descricao = "Em execução";
                        break;
                    case "3": descricao = "Concluído com sucesso";
                        break;
                    case "4": descricao = "Abortando";
                        break;
                    case "5": descricao = (Abortusuario != null) ? "Abortado pelo usuário" : "Abortado devido a erros";
                        break;
                }
                if (GerouCriticas == true)
                    descricao += " (com críticas)";
                return descricao;
            }
        }

        [StringLength(10)]
        [Display(Name = "Duração", Description = "Duração do processo")]
        public virtual string Duracao
        {
            get
            {
                return (Inicio.HasValue) ? Inicio.Value.Duration(Fim ?? DateTimeHelper.DatabaseNow) : String.Empty;
            }
        }

        public virtual IEnumerable<MensagemRegistroProcesso> Mensagens
        {
            get
            {
                using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    using (var session = SessionHelper.SessionFactory.OpenSession())
                    {
                        var list = session.QueryOver<MensagemRegistroProcesso>()
                            .Where(x => x.HandleProcesso == Handle)
                            .OrderBy(y => y.Handle).Asc.List();
                        for (var i = 1; i <= list.Count; i++)
                            list[i - 1].Sequencia = i;
                        return list;
                    }
                }
            }
        }

        public virtual string CarregarOcorrenciaNova(ISession session)
        {
            var sql = "SELECT OCORRENCIAS FROM SIS_PROCESSO_OCORRENCIAS WHERE SISPROCESSO = :HANDLE";
            return session.CreateSQLQuery(sql).SetParameter("HANDLE", Handle).UniqueResult<String>();
        }

        [StringLength(4000)]
        [Display(Name = "Ocorrências", Description = "Ocorrências com tamanho ilimitado")]
        public virtual string OcorrenciaNova
        {
            get
            {
                using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    using (var session = SessionHelper.SessionFactory.OpenSession())
                    {
                        return CarregarOcorrenciaNova(session);
                    }
                }

            }
        }

        public virtual bool PegarOcorrenciasNaTabelaInformada { get; set; }

        public virtual string CarregarOcorrencias(ISession session = null)
        {
            if (session == null)
            {
                using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    using (session = SessionHelper.SessionFactory.OpenSession())
                    {
                        return CarregarOcorrenciasInterno(session);
                    }
                }
            }
            else
                return CarregarOcorrenciasInterno(session);
        }

        private string CarregarOcorrenciasInterno(ISession session)
        {
            var ocorrencias = String.Empty;
            if (((Rotinaorigem ?? 0) > 0) && (!String.IsNullOrEmpty(Tabelaorigem)) && (PegarOcorrenciasNaTabelaInformada))
            {
                ocorrencias = TentarPegarOcorrenciasRotina("OCORRENCIAS", session);
                if (String.IsNullOrEmpty(ocorrencias))
                    ocorrencias = TentarPegarOcorrenciasRotina("OCORRENCIA", session);
            }
            if (String.IsNullOrEmpty(ocorrencias))
            {
                var ocorrenciaNova = CarregarOcorrenciaNova(session);
                ocorrencias = String.IsNullOrEmpty(ocorrenciaNova) ? this.OcorrenciaAntiga : ocorrenciaNova;
            }
            return ocorrencias;
        }

        [StringLength(4000)]
        [Display(Name = "Ocorrências", Description = "Ocorrências do processamento")]
        public virtual string Ocorrencias
        {
            get
            {
                return CarregarOcorrencias(); //TODO: Remover isso e todos os pontos que precisam de um Session, a entidade não tem que ter conhecimento disto. Mover a lógica de carregamento para o mapeamento.
            }
        }

        [Display(Name = "% Progresso", Description = "Percentual de progresso do processamento")]
        public virtual int PorcentagemProgresso
        {
            get
            {
                double porcentagemProgresso = 0;
                if (Situacao == "3") // Se a rotina está finalizada com sucesso
                    porcentagemProgresso = 100;
                else
                {
                    double posicao = Posicao ?? 0;
                    double maximo = Maximo ?? 0;
                    if (maximo > 0)
                    {
                        var conta = (posicao / maximo) * 100;
                        porcentagemProgresso = (Math.Round(conta, 0));
                    }
                    if (porcentagemProgresso > 100)
                        porcentagemProgresso = 100;
                }
                return (int)porcentagemProgresso;
            }
        }

        private string TentarPegarOcorrenciasRotina(string campo, ISession session)
        {
            try
            {
                // Tenta pegar as ocorrências da tabela da rotina
                var sql = String.Format("SELECT {0} FROM {1} WHERE HANDLE = {2}", campo, Tabelaorigem, Rotinaorigem.Value);
                return (String)session.CreateSQLQuery(sql).UniqueResult();
            }
            catch (GenericADOException)
            {
                return string.Empty;
            }
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return Enumerable.Empty<ValidationResult>();
        }

        public override string ToString()
        {
            return this.Descricao;
        }
    }
}