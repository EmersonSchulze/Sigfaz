using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Porto.Saude.Infra.Monitoring
{
    public enum SituacaoMonitoramento
    {
        [Description("Nenhuma")] 
        Nenhuma = 0,
        [Description("Iniciado")] 
        Iniciada = 1,
        [Description("Atualizar Progresso")]
        AtualizarProgresso = 2,
        [Description("Finalizada com Sucesso")]
        FinalizadaComSucesso = 3,
        [Description("Abortar Solicitado")]
        AbortarSolicitado = 4,
        [Description("Finalizado com Erro")]
        FinalizadaComErro = 5,
        [Description("Eliminar Registro")]
        EliminarRegistro = 6
    }
}