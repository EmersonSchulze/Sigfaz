using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sigfaz.Infra.ComponentModel.Extensions;

namespace Sigfaz.Infra.ComponentModel.Telefone
{
    public class Celular : Telefone
    {
        #region IValidatableObject Members

        public override  IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!base.EhValido || !EhValido)
                yield return new ValidationResult("Celular inválido!", new string[] { "ddd", "prefixo", "numero" });
        }

        #endregion

        public override bool EhValido
        {
            get
            {
                SomenteNumerosCampos();

                return (
                    PossuiTodosCamposVazios ||
                    (
                        EhDdd9Digitos && 
                        (ValidoParaPrefixoExcecao || (PrimeiroNumeroPrefixo9 && Possui5DigitosPrefixoE4DigitosNumero)) ||
                        !EhDdd9Digitos && Possui4DigitosEmPrefixoENumero)
                );
            }
        }
        private bool ValidoParaPrefixoExcecao
        {
            get { return EhPrefixoExcecao && Possui4DigitosEmPrefixoENumero; }
        }
        private bool EhPrefixoExcecao
        {
            get
            {
                return EhDdd9Digitos && Telefone.RegraValidacaoProvider.PrefixoExcecao(DddAsLong, Prefixo);
            }
        }

        /// <summary>
        /// Valida número de celular do tipo string.
        /// </summary>
        /// <param name="numeroCelular">Numero do celular com DDD (Padrão: 2 digitos) + prefixo + número</param>
        /// <param name="permiteNulo">Se marcado como true (Padrão: true), o parametro numeroCelular com string vazia é valido.</param>
        /// <param name="quantidadeDigitosDdd">Quantidade de digitos para DDD (Padrão: 2 digitos)</param>
        /// <returns></returns>
        public static bool Validar(string numeroCelular, bool permiteNulo = true, int quantidadeDigitosDdd = 2)
        {
            if (String.IsNullOrEmpty(numeroCelular.GetOnlyNumbers().Trim())) return permiteNulo;
            var celular = Celular.TryParse(numeroCelular, quantidadeDigitosDdd);
            return celular != null && celular.EhValido;
        }

        public static Celular TryParse(string numeroCelular, int quantidadeDigitosDdd = 2)
        {
            if (numeroCelular == null) return null;
            var text = numeroCelular.GetOnlyNumbers().Trim();
            if (String.IsNullOrEmpty(text)) return null;
            var celular = new Celular();

            if (!PossuiQuantidadeDigitosParaCelular(text, quantidadeDigitosDdd))
                return null;

            string ddd = text.Substring(0, quantidadeDigitosDdd);
            string prefixo = string.Empty;
            string numero = string.Empty;

            if (text.Length == QuantidadeDigitosComPrefixo4Digitos(quantidadeDigitosDdd))
            {
                prefixo = text.Substring(quantidadeDigitosDdd, 4);
                numero = text.Substring(quantidadeDigitosDdd + 4, 4);
            }
            if (text.Length == QuatidadeDigitosComPrefixoSaoPaulo(quantidadeDigitosDdd))
            {
                prefixo = text.Substring(quantidadeDigitosDdd, 5);
                numero = text.Substring(quantidadeDigitosDdd + 5, 4);
            }

            celular.Ddd = ddd;
            celular.Prefixo = prefixo;
            celular.Numero = numero;

            return celular;
        }

        private static bool PossuiQuantidadeDigitosParaCelular(string text, int quantidadeDigitosDdd)
        {
            return text.Length >= QuantidadeDigitosComPrefixo4Digitos(quantidadeDigitosDdd) && text.Length <= QuatidadeDigitosComPrefixoSaoPaulo(quantidadeDigitosDdd);
        }

        private static int QuantidadeDigitosComPrefixo4Digitos(int quantidadeDigitosDdd)
        {
            return quantidadeDigitosDdd + 8; 
        }

        private static int QuatidadeDigitosComPrefixoSaoPaulo(int quantidadeDigitosDdd)
        {
            return quantidadeDigitosDdd + 9;
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Celular;
            if (other == null) return false;
            return Ddd == other.Ddd && Prefixo == other.Prefixo && Numero == other.Numero;
        }
    }
}
