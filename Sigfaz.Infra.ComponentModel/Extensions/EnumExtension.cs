using System;
using System.ComponentModel;

namespace Sigfaz.Infra.ComponentModel.Extensions
{
    public static class EnumExtension
    {
        public static string ObterDescricao(this Enum enumerador)
        {
            return ObterDescricao(enumerador, "Sem Descrição");
        }

        public static string ObterDescricao(this Enum enumerador, string retornoEnumSemDescricao)
        {
            var fieldInfo = enumerador.GetType().GetField(enumerador.ToString());
            var atributos = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return atributos.Length > 0 ? atributos[0].Description ?? retornoEnumSemDescricao : enumerador.ToString();
        }
     }
}
