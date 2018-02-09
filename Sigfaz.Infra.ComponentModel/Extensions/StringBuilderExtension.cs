using System;
using System.Text;

namespace Sigfaz.Infra.ComponentModel.Extensions
{
    public static class StringBuilderExtension
    {
        public static void AddOcorrencias(this StringBuilder sb, string mensagem)
        {
            sb.AppendLine(String.Format("[{0:dd/MM/yyyy HH:mm:ss}] {1}", DateTime.Now, mensagem));
        }
    }
}
