using System;
using System.Linq;
using NHibernate;
using FluentNHibernate.Mapping;
using Porto.Saude.Infra.Data;

namespace Porto.Saude.Infra.Monitoring.Mappings
{
    public class MensagemRegistroProcessoMapping : ClassMap<MensagemRegistroProcesso>
    {
        public MensagemRegistroProcessoMapping() 
        {    
            Table("SIS_PROCESSO_MENSAGENS");
                    
            Id(x => x.Handle, "HANDLE")
                .GeneratedBy.Sequence("SEQ_SIS0008");
               
            Map(x => x.Datahora, "DATAHORA");
               
            Map(x => x.Mensagem, "MENSAGEM");
            Map(x => x.HandleProcesso, "PROCESSO");
            References(x => x.Processo,"PROCESSO")
                .Not.Insert().Not.Update().Cascade.None();
        }
    }
}