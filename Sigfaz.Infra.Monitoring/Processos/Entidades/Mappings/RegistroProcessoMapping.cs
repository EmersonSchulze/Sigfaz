using Sigfaz.Infra.Data.NHibernate.UserTypes;
using FluentNHibernate.Mapping;
using Porto.Saude.Infra.Monitoring;

namespace Sigfaz.Infra.Monitoring.Processos.Entidades.Mappings
{
    public class RegistroProcessoMapping : ClassMap<RegistroProcesso>
    {
        public RegistroProcessoMapping() 
        {
    
            Table("SIS_PROCESSO");

            Id(x => x.Handle, "HANDLE").GeneratedBy.Sequence("SEQ_SIS0001");

            Map(x => x.Abortdatahora, "ABORTDATAHORA");

            Map(x => x.Aborthost, "ABORTHOST").Length(50);

            Map(x => x.Abortusuario, "ABORTUSUARIO");

            Map(x => x.Descricao, "DESCRICAO").Length(100);

            Map(x => x.Fim, "FIM");

            Map(x => x.Host, "HOST").Length(50);

            Map(x => x.Inicio, "INICIO");

            Map(x => x.LocalDeChamada, "LOCALCHAMADA").Length(250);

            Map(x => x.Maximo, "MAXIMO");

            Map(x => x.Mensagem, "MENSAGEM");

            Map(x => x.OcorrenciaAntiga).Formula("NVL(OCORRENCIA2, OCORRENCIA)");

            Map(x => x.Posicao, "POSICAO");
            
            Map(x => x.Rotinaorigem, "ROTINAORIGEM");

            Map(x => x.Situacao, "SITUACAO").Length(1);

            Map(x => x.Tabelaorigem, "TABELAORIGEM").Length(30);
            
            Map(x => x.Usuario, "USUARIO");
           
            Map(x => x.CaminhoDownloadLogs, "CAMINHODOWNLOADLOGS").Length(250);

            Map(x => x.HandleProcessoPai, "PROCESSOPAI");

            Map(x => x.ChaveCorrelacao, "CHAVECORRELACAO").Length(36);

            Map(x => x.Z_ChaveCorrelacao, "Z_CHAVECORRELACAO").Length(36);

            Map(x => x.ProgressoAutomatico, "SITUACAOAUTOMATICA")
                .CustomType<BooleanHelper>();

            Map(x => x.GerouCriticas, "CRITICAS")
                .CustomType<BooleanHelper>();
        }
    }
}