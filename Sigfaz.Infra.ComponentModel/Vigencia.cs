using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Sigfaz.Infra.ComponentModel.Extensions;

namespace Sigfaz.Infra.ComponentModel
{
    public class Competencia : Vigencia
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Inicial")]
        override public DateTime DataInicial { get; set; }

        [Display(Name = "Final")]
        override public DateTime? DataFinal { get; set; }
    }

    public class Vigencia
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Inicial")]
        virtual public DateTime DataInicial { get; set; }

        [Display(Name = "Final")]
        virtual public DateTime? DataFinal { get; set; }

        #region ::: Construtores
        
        public Vigencia() { }
        
        public Vigencia(DateTime dataInicial, DateTime? dataFinal = null)
        {
            this.DataInicial = dataInicial;
            this.DataFinal = dataFinal;
        }

        #endregion

        #region ::: Verifica vigência aberta

        /// <summary>
        /// Verifica se a vigência está em aberto.
        /// </summary>
        /// <returns>Retorna "true" se a data final não estiver preenchida.</returns>
        public bool Aberta()
        {
            return this.DataFinal == null;
        }

        #endregion

        #region ::: Verifica vigência válida

        /// <summary>
        /// Verifica se a vigência é válida (data inicial é inferior à data final)
        /// </summary>
        /// <returns>Retorna "true" se a data final estiver em aberto ou se for superior ou igual à data inicial.</returns>
        public bool Valida()
        {
            return (DataFinal.HasValue) ? DataFinal.Value >= DataInicial : true;
        }

        #endregion

        #region ::: Verifica se uma data está na vigência

        /// <summary>
        /// Verifica a vigência para a data corrente
        /// </summary>
        /// <returns>Verdadeiro caso vigente para a data corrente, falso caso contrário</returns>
        public bool Vigente()
        {
            return Vigente(DateTime.Now);
        }

        /// <summary>
        /// Verifica a vigência para a data informada
        /// </summary>
        /// <param name="dataComparacao">Data relativa para comparação</param>
        /// <returns>Verdadeiro caso vigente para a data especificada, falso caso contrário</returns>
        public bool Vigente(DateTime dataComparacao)
        {
            return (DataFinal.HasValue)
                ? (DataInicial <= dataComparacao && DataFinal.Value >= dataComparacao)
                : (DataInicial <= dataComparacao);
        }

        #endregion

        #region ::: Verifica se a vigência cruza com outra

        /// <summary>
        /// Verifica se a vigência cruza com outra. ATENÇÃO: Não tem suporte via NHibernate.
        /// </summary>
        /// <param name="outra">Outra vigência para comparação</param>
        /// <returns>Retorna "true" se as vigências se cruzarem.</returns>
        public bool Cruzada(Vigencia outra)
        {
            if (this.DataFinal.HasValue)
            {
                if (outra.DataFinal.HasValue)
                    return ((this.DataInicial.Between(outra.DataInicial, outra.DataFinal.Value)) || (outra.DataInicial.Between(this.DataInicial, this.DataFinal.Value)));
                else
                    return (outra.DataInicial <= this.DataFinal.Value);
            }
            else
            {
                if (outra.DataFinal.HasValue)                    
                    return (outra.DataFinal.Value >= this.DataInicial);
                else
                    return true;
            }
        }

        /// <summary>
        /// Verifica se a vigência cruza com um intervalo aberto de tempo (quando o intervalo possui somente a data inicial).
        /// </summary>
        /// <param name="dataInicial">Data inicial do intervalo</param>
        /// <returns>Retorna "true" se as vigências se cruzarem.</returns>
        public bool Cruzada(DateTime dataInicial)
        {
            if (this.DataFinal.HasValue)
                return this.DataFinal.Value >= dataInicial;
            else
                return true;
        }

        /// <summary>
        /// Verifica se a vigência cruza com um intervalo fechado de tempo.
        /// </summary>
        /// <param name="dataInicial">Data inicial do intervalo</param>
        /// <param name="dataFinal">Data final do intervalo</param>
        /// <returns>Retorna "true" se as vigências se cruzarem.</returns>
        public bool Cruzada(DateTime dataInicial, DateTime dataFinal)
        {
            if (this.DataFinal.HasValue)
                return ((this.DataInicial.Between(dataInicial, dataFinal)) ||  (dataInicial.Between(this.DataInicial, this.DataFinal.Value)));                
            else
                return (dataFinal >= this.DataInicial);
        }

        /// <summary>
        /// Verifica se a vigência cruza com alguma das vigências da consulta.
        /// </summary>
        /// <param name="vigencias">Vigências a serem testadas</param>
        /// <returns>Retorna "true" se existir alguma vigência cruzada.</returns>
        public bool Cruzada(IQueryable<Vigencia> vigencias)
        {
            if (!vigencias.Any()) return false;

            if (this.DataFinal.HasValue)
                return vigencias.Any(outra => outra.Cruzada(this.DataInicial, this.DataFinal.Value));
            else
                return vigencias.Any(outra => outra.Cruzada(this.DataInicial));
        }

        #endregion

        #region ::: Valida seguindo todas as regras de vigência

        /// <summary>
        /// Efetua as validações de vigência retornando uma lista com as mensagens a serem mostradas para o usuário
        /// </summary>
        /// <param name="vigencias">Lista de vigência para ser validada</param>
        /// <returns>Retorna lista de mensagem para ser mostrada para o usuário</returns>
        public IEnumerable<ValidationResult> ValidarVigencia(IQueryable<Vigencia> vigencias)
        {
            if (!this.Valida())
            {
                yield return new ValidationResult("Período inválido de vigência.", new string[] { "DataInicial" });
                yield break;
            }

            if (this.DataFinal.HasValue)
            {
                if (vigencias.Any(outra => (outra.DataFinal == null) && (outra.DataInicial <= this.DataFinal)))
                {
                    yield return new ValidationResult("Existe outra vigência anterior a esta, que está em aberto.", new string[] { "DataInicial" });
                    yield break;
                }                    
            }
            else
            {
                if (vigencias.Any(outro => outro.DataFinal == null))
                {
                    yield return new ValidationResult("Existe outra vigência em aberto.", new string[] { "DataInicial" });
                    yield break;
                }

                if (vigencias.Any(outro => outro.DataInicial >= this.DataInicial))
                {
                    yield return new ValidationResult("Existe outra vigência iniciando junto ou depois da vigência especificada.", new string[] { "DataInicial" });
                    yield break;
                }
            }

            if (this.Cruzada(vigencias))
                yield return new ValidationResult("Existe outra vigência que cruza com o período especificado.", new string[] { "DataInicial" });
        }

        #endregion        

        #region ::: Métodos que não deveriam estar nesta classe

        public static DateTime UltimoDiaCompetencia(DateTime data)
        {
            return new DateTime(data.Year, data.Month, DateTime.DaysInMonth(data.Year, data.Month));
        }

        /// <summary>
        /// Diferença entre datas
        /// </summary>
        /// <param name="dataIncial"> Data Inicial </param>
        /// <param name="dataFinal"> Data Final </param>
        /// <returns>Retorna o total de dias</returns>
        public static int DiferencaDatasTotalDias(DateTime dataIncial, DateTime dataFinal)
        {
            // define maior data
            if (dataFinal > dataIncial)
            {
                DateTime dataAux = dataFinal;
                dataFinal = dataIncial;
                dataIncial = dataAux;
            }

            TimeSpan ts = dataIncial.Subtract(dataFinal);

            // Resultado em Dias
            return ts.Days;
        }

        ///<summary>
        /// Diferença entre datas
        /// </summary>
        /// <param name="dataIncial"> Data Inicial </param>
        /// <param name="dataFinal"> Data Final </param>
        /// <returns>Retorna o total de meses</returns>
        public static int DiferencaDatasTotalMeses(DateTime dataIncial, DateTime dataFinal)
        {
            // define maior data
            if (dataFinal > dataIncial)
            {
                DateTime dataAux = dataFinal;
                dataFinal = dataIncial;
                dataIncial = dataAux;
            }

            // Resultado em Meses
            double dblValue = 12 * (dataIncial.Year - dataFinal.Year) + dataIncial.Month - dataFinal.Month;
            return Convert.ToInt32(Math.Abs(dblValue));
        }

        ///<summary>
        /// Diferença entre datas
        /// </summary>
        /// <param name="dataIncial"> Data Inicial </param>
        /// <param name="dataFinal"> Data Final </param>
        /// <returns>Retorna o total de anos</returns>
        public static int DiferencaDatasTotalAnos(DateTime dataIncial, DateTime dataFinal)
        {
            // define maior data
            if (dataFinal > dataIncial)
            {
                DateTime dataAux = dataFinal;
                dataFinal = dataIncial;
                dataIncial = dataAux;
            }

            // Resultado em Anos
            return dataIncial.Year - dataFinal.Year;
        }

        #endregion

        public override string ToString()
        {
            if (this.DataFinal.HasValue)
                return String.Format("{0:dd/MM/yyyy} - {1:dd/MM/yyyy}", this.DataInicial, this.DataFinal.Value);
            else
                return String.Format("{0:dd/MM/yyyy}", this.DataInicial);
        }
    }
}
