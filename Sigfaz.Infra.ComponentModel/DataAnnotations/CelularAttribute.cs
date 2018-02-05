using System;
using System.ComponentModel.DataAnnotations;
using Sigfaz.Infra.ComponentModel.Telefone;

namespace Sigfaz.Infra.ComponentModel.DataAnnotations
{
    public class CelularAttribute : ValidationAttribute
    {
        private bool PermiteNulo { get; set; }
        private int QuantidadeDigitosDdd { get; set; }       

        public CelularAttribute(bool permiteNulo = true, int quantidadeDigitosDdd = 2)
        {
            this.PermiteNulo = permiteNulo;
            this.QuantidadeDigitosDdd = quantidadeDigitosDdd;
        }

        public override bool IsValid(object value)
        {
            try
            {
                var text = Convert.ToString(value);
                return VerificaCelular(text);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool VerificaCelular(string text)
        {
            return Celular.Validar(text, PermiteNulo, QuantidadeDigitosDdd);
        }
    }
}
