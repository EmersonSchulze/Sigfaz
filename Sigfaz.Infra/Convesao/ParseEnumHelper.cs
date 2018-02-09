using System;
using System.Reflection;
using log4net;

namespace Sigfaz.Infra.Convesao
{
    public static class ParseEnumHelper
    {
        /// <summary>
        /// Converte um valor de um Enum para outro Enum
        /// </summary>
        /// <typeparam name="T">Enum de entrada</typeparam>
        /// <typeparam name="M">Enum de saída</typeparam>
        /// <param name="enumDe">Valor do Enum entrada</param>
        /// <param name="mensagem">Mensagem padronizada em caso de não se encontrar um valor equivalente</param>
        /// <param name="logger">Log do sistema</param>
        /// <returns>Valor do Enum de saída</returns>
        public static TM Parse<T, TM>(T enumDe, string mensagem, ILog logger)
        {
            #region Log
            if (logger != null)
            {
                logger.DebugFormat(String.Format("Método: {0}", MethodInfo.GetCurrentMethod().Name));
                logger.DebugFormat(String.Format("Enum de entrada {0}", typeof(T).Name));
                logger.DebugFormat(String.Format("valor do Enum de entrada {0}", Enum.GetName(typeof(T), enumDe)));
                logger.DebugFormat(String.Format("Enum de saida {0}", typeof(TM).Name));
            }
            #endregion

            if ((enumDe as object) == null)
                return default(TM);

            if (String.IsNullOrEmpty(mensagem))
                mensagem = String.Format("Erro: informação: \"{0}\" é inválido", enumDe.ToString());

            try
            {
                return (TM)Enum.Parse(GetNonNullableModelType(typeof(TM)), Enum.GetName(GetNonNullableModelType(typeof(T)), enumDe), true);
            }
            catch (Exception e)
            {
                #region Log
                if (logger != null)
                {
                    logger.FatalFormat(e.Message);
                    logger.FatalFormat(e.StackTrace);
                }
                #endregion
                throw new ApplicationException(mensagem);
            }
        }

        /// <summary>
        /// Converte um valor de um Enum para outro Enum
        /// </summary>
        /// <typeparam name="T">Enum de entrada</typeparam>
        /// <typeparam name="M">Enum de saída</typeparam>
        /// <param name="enumDe">Valor do Enum entrada</param>
        /// <param name="logger">Log do sistema</param>
        /// <returns>Valor do Enum de saída</returns>
        public static TM Parse<T, TM>(T enumDe, ILog logger)
        {
            return Parse<T, TM>(enumDe, "", logger);
        }

        /// <summary>
        /// Converte um valor de um Enum para outro Enum
        /// </summary>
        /// <typeparam name="T">Enum de entrada</typeparam>
        /// <typeparam name="M">Enum de saída</typeparam>
        /// <param name="enumDe">Valor do Enum entrada</param>
        /// <returns>Valor do Enum de saída</returns>
        public static TM Parse<T, TM>(T enumDe)
        {
            return Parse<T, TM>(enumDe, "", null);
        }

        /// <summary>
        /// Converte um valor de um Enum para outro Enum
        /// </summary>
        /// <typeparam name="T">Enum de entrada</typeparam>
        /// <typeparam name="M">Enum de saída</typeparam>
        /// <param name="enumDe">Valor do Enum entrada</param>
        /// <param name="mensagem">Mensagem padronizada em caso de não se encontrar um valor equivalente</param>
        /// <returns>Valor do Enum de saída</returns>
        public static TM Parse<T, TM>(T enumDe, string mensagem)
        {
            return Parse<T, TM>(enumDe, mensagem, null);
        }

        private static Type GetNonNullableModelType(Type propertyType)
        {
            Type underlyingType = Nullable.GetUnderlyingType(propertyType);
            if (underlyingType != null)
            {
                propertyType = underlyingType;
            }
            return propertyType;
        }

        public static T GetEnumValueFromString<T>(string description, bool throwIfNotFound = false)
        {
            if (!(typeof(T).IsEnum))
                throw new ArgumentException("O tipo fornecido deve ser uma enumeração!");
            if (!String.IsNullOrEmpty(description))
            {
                foreach (T value in Enum.GetValues(typeof(T)))
                {
                    if ((value as Enum).ToString().ToLower().Contains(description.ToLower()))
                        return value;
                }
            }
            if (throwIfNotFound)
                throw new ApplicationException(String.Format("Não encontrado valor de enumeração '{0}' para descrição '{1}' fornecida!", typeof(T).Name, description));
            return default(T);
        }
    }
}
