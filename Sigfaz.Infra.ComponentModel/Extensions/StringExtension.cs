using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Sigfaz.Infra.ComponentModel.Extensions
{
    public static class StringExtension
    {
        public static string RemoveEspacoInicioMeioFim(this string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return string.Empty;
            nome = nome.Trim();
            nome = Regex.Replace(nome, "[ ]{2,}", " ", RegexOptions.Compiled);
            return nome;
        }

        /// <summary>
        /// Checar se o nome possui alguma abreviatura no meio que não se encaixe em: DO, DA, DE, E, Y.
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        public static bool ContemAbreviaturaInvalida(this string nome)
        {
            var nomes = nome.Split(new[] { ' ' });

            var permite = new List<string> { "DO", "DA", "DE", "E", "Y" };

            var nomeMeio = nomes.Skip(1).SkipLast().Where(x => x.Length <= 2);

            return nomeMeio.Any() && !nomeMeio.Intersect(permite).Any();
        }

       public static bool PossuiMaisDeUmNomeComLetrasEspeciaisNoPrimeiroNome(this string nome)
        {
           var nomes = nome.Split(new[] { ' ' });

            List<string> permite = new List<string> { "D", "I", "O", "U", "Y", "Í", "Ì", "Ò", "Ó", "Õ", "Ô", "Ö", "Ú", "Ù", "Ü" };

            var unicaLetraPrimeiroNome = nomes.Take(1).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(unicaLetraPrimeiroNome) && unicaLetraPrimeiroNome.Length > 1)
                return false;

            return !permite.Contains(unicaLetraPrimeiroNome);
        }

        public static bool PossuiNomeESobrenome(this string nome)
        {
            return nome.Contains(" ");
        }

        public static bool UltimoNomeComLetrasEspeciais(this string nome)
        {
            var nomes = nome.Split(new[] { ' ' });

            if (nomes.Last().Count() > 1)
                return false;

            return !Regex.IsMatch(nomes.Last(), "[IOUYÍÌÒÓÕÔÖÚÙÜ]$");
        }

        public static bool ContemTresLetrasRepetidasSeguidas(this string nome)
        {
            return (nome.Substring(2, 1).Equals(nome.Substring(0, 1)) && (nome.Substring(2, 1).Equals(nome.Substring(1, 1))));
        }

        public static bool SomenteLetras(this string nome)
        {
            return !(Regex.IsMatch(nome, "[^a-zA-ZáàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]", RegexOptions.Compiled));
        }

        public static string CapitalizeFirstLetter(this string s)
        {
            if (String.IsNullOrEmpty(s))
                return s;
            if (s.Length == 1)
                return s.ToUpper();
            return s.Remove(1).ToUpper() + s.Substring(1);
        }

        public static DateTime FromJsonString(this string s)
        {
            return DateTime.ParseExact(s, "dd/MM/yyyy HH:mm:ss", new DateTimeFormatInfo());
        }

        public static String GetOnlyNumbers(this string s)
        {
            if (!String.IsNullOrEmpty(s))
                return new String(s.Where(Char.IsDigit).ToArray());
            else
                return s;
        }

        public static String SpaceOnCapitals(this String s)
        {
            if (String.IsNullOrEmpty(s))
                return s;
            var resultString = String.Empty;
            var ultimaMaiuscula = false;
            for (int c = 0; c < s.Length; c++)
            {
                if ((Char.IsUpper(s[c])) && (!(String.IsNullOrEmpty(resultString))))
                {
                    if ((!ultimaMaiuscula) || // Se a anterior não foi maiúscula ou
                        ((c + 1 < s.Length) && (Char.IsLower(s[c + 1])))) // se a próxima é minúscula (para o caso de abreviações)
                        resultString += " ";
                }

                resultString += s[c];
                ultimaMaiuscula = Char.IsUpper(s[c]);
            }

            return resultString;
        }

        public static String ToWSCFBlueEnumDescrition(this String s)
        {
            s = s.RemoverAcentos();
            s = s.Replace(" ", String.Empty);
            s = s.Replace("(", String.Empty);
            s = s.Replace(")", String.Empty);
            s = s.Replace("/", String.Empty);
            s = s.Replace("\\", String.Empty);
            s = s.Replace(".", String.Empty);
            s = s.Replace(",", String.Empty);
            s = s.Replace("-", String.Empty);
            s = s.Replace(":", String.Empty);
            s = s.Replace(";", String.Empty);
            s = s.Replace("ª", String.Empty);
            return s;
        }

        public static string RemoverAcentos(this string texto)
        {
            if (texto != null)
            {
                var s = texto.Normalize(NormalizationForm.FormD);

                var sb = new StringBuilder();

                for (int k = 0; k < s.Length; k++)
                {
                    UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(s[k]);
                    if (uc != UnicodeCategory.NonSpacingMark)
                    {
                        sb.Append(s[k]);
                    }
                }

                return sb.ToString();
            }
            else
            {
                return null;
            }
        }

        public static string RemoverCaracteresEspeciais(this string texto)
        {
            return Regex.Replace(texto, "[^0-9a-zA-Z._\\s]+?", "");
        }

        public static string TrocarCaracteresEspeciais(this string texto)
        {
            texto = texto.RemoverAcentos();

            texto = texto.Replace("º", "o.");
            texto = texto.Replace("ª", "a.");
            texto = texto.Replace("''", "");
            texto = texto.Replace("¨", "");
            texto = texto.Replace("´", "");
            texto = texto.Replace("`", "");
            texto = texto.Replace("^", "");
            texto = texto.Replace("~", "");
            texto = texto.Replace("<", "");
            texto = texto.Replace(">", "");
            texto = texto.Replace("°", "");
            texto = texto.Replace("€", "");
            texto = texto.Replace("ç°", "C");
            texto = texto.Replace("#10", "");
            texto = texto.Replace("#13", "");
            texto = texto.Replace("¢", "O");
            texto = texto.Replace('¡', 'I');
            texto = texto.Replace('&', 'E');
            texto = texto.Replace('¡', '!'); //Inverted exclamation
            texto = texto.Replace('¢', 'c'); //Cent sign
            texto = texto.Replace('£', 'l'); //Pound sterling
            texto = texto.Replace('¤', 'o'); //General currency sign
            texto = texto.Replace('¥', 'y'); //Yen sign
            texto = texto.Replace('¦', '|'); //Broken vertical bar
            texto = texto.Replace('§', 's'); //Section sign
            texto = texto.Replace('¨', '"'); //Umlaut (dieresis)
            texto = texto.Replace('©', 'c'); //Copyright
            texto = texto.Replace('ª', '-'); //Feminine ordinal
            texto = texto.Replace('«', '<'); //Left angle quote, guillemotleft
            texto = texto.Replace('¬', '-'); //Not sign
            texto = texto.Replace('­', '-'); //Soft hyphen
            texto = texto.Replace('®', 'r'); //Registered trademark
            texto = texto.Replace('¯', '^'); //Macron accent
            texto = texto.Replace('°', '^'); //Degree sign
            texto = texto.Replace('±', '+'); //Plus or minus
            texto = texto.Replace('²', '^'); //Superscript two
            texto = texto.Replace('³', '^'); //Superscript three
            texto = texto.Replace("´", "\\"); //Acute accent
            texto = texto.Replace('µ', '/'); //Micro sign
            texto = texto.Replace('¶', 'P'); //Paragraph sign
            texto = texto.Replace('·', '^'); //Middle dot
            texto = texto.Replace('¸', ','); //Cedilla
            texto = texto.Replace('¹', '^'); //Superscript one
            texto = texto.Replace('º', '_'); //Masculine ordinal
            texto = texto.Replace('»', '>'); //Right angle quote, guillemotright
            texto = texto.Replace('¼', '1'); //Fraction one-fourth
            texto = texto.Replace('½', '1'); //Fraction one-half
            texto = texto.Replace('¾', '3'); //Fraction three-fourths
            texto = texto.Replace('¿', '?'); //Inverted question mark
            texto = texto.Replace('Ð', 'D'); //Capital Eth, Icelandic
            texto = texto.Replace('Ñ', 'N'); //Capital N, tilde
            texto = texto.Replace('Ø', 'O'); //Capital O, slash
            texto = texto.Replace('Þ', 'P'); //Capital THORN, Icelandic
            texto = texto.Replace('ß', 's'); //Small sharp s, German (sz ligature)
            texto = texto.Replace('ä', 'a'); //Small a, dieresis or umlaut mark
            texto = texto.Replace('å', 'a'); //Small a, ring
            texto = texto.Replace('æ', 'a'); //Small ae dipthong (ligature)
            texto = texto.Replace('ð', 'd'); //Small eth, Icelandic
            texto = texto.Replace('ñ', 'n'); //Small n, tilde
            texto = texto.Replace('÷', '-'); //Division sign
            texto = texto.Replace('ø', 'o'); //Small o, slash
            texto = texto.Replace('þ', 'p'); //Small thorn, Icelandic
            texto = texto.Replace('ÿ', 'y'); //Small y, dieresis or umlaut mark

            texto = Regex.Replace(texto, "[!#$%&'()*+,-./:=;?@[\\]_`{|}~]", "");
            texto = texto.Replace("\"", "");
            texto = texto.Replace("\\", "");
            texto = texto.Replace("^", "");

            return texto;
        }

        public static string TiraPonto(this string texto)
        {
            return Regex.Replace(texto, "[.,]", "");
        }

        public static string TiraParenteses(this string texto)
        {
            var retorno = Regex.Replace(texto, "[(]", "");
            return Regex.Replace(retorno, "[)]", "");
        }

        public static string TiraParentesesEsquerdo(this string texto)
        {
            return Regex.Replace(texto, "[(]", "");
        }

        public static string TiraParentesesDireito(this string texto)
        {
            return Regex.Replace(texto, "[)]", "");
        }

        public static bool ContemSomenteNumeros(this string texto)
        {
            return texto.Where(c => char.IsNumber(c)).Count() == texto.Count();
        }

        public static string Iniciais(this string texto)
        {
            string iniciais = "";
            string[] textoArray = texto.Split(' ');
            foreach (var item in textoArray)
            {
                if (!String.IsNullOrEmpty(item))
                    iniciais += item[0];
            }

            return iniciais;
        }

        public static string NullIfEmpty(this string texto)
        {
            return String.IsNullOrEmpty(texto) ? null : texto;
        }

        /// <summary>
        /// Formata e alinha caracteres de uma string.
        /// </summary>
        /// <param name="valor">Valor a ser formatado</param>
        /// <param name="alinha">D ou E (D - Direita coloca o completa do lado direito   E - Esquerda o inverso</param>
        /// <param name="completa">Caracter para completar a direita ou a esquerda</param>
        /// <param name="quantiade">Qtde que irá formatar o campo</param>
        public static string FormatarAlinharCaracterStr(this string valor, char alinha, char completa, int quantiade)
        {
            while (valor.Length < quantiade)
            {
                if (alinha == 'D')
                    valor = completa + valor;
                else
                    valor = valor + completa;
            }

            return valor;
        }

        /// <summary>
        /// Converte todas as primeiras letas das palavras no nome em maiúscula.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static string PrimeirasMaiuscula(this string texto, bool ignorarPalavrasLigacao = false)
        {
            if (!string.IsNullOrEmpty(texto))
            {
                var palavrasLigacao = new List<String>() { "a", "à", "ante", "ao", "aos", "após", "as", "às", "até", "com", "contra", "da", "das", "de", "de:", "desde", 
                                                           "do", "dos", "e", "em", "entre", "mais", "na", "não", "nas", "no", "nos", "o", "os", "ou", "para", "pela", 
                                                           "per", "perante", "por", "que", "sem", "ser", "sob", "sobre", "trás", "um", "uma", "umas", "uns", "e/ou" };

                var palavrasASeremSubstituidas = new List<String>() { "IOF", "ADA","Break-Even", "SAP", "InfraRules", "CRM", "PEG", "PEGs", "RH", "IRRF", "GCDE", "GRS", "RPC", "Agravo/CPT", "SLA", 
                                                                      "OPME", "CPT", "IR", "Co-Participação", "SUSEP", "TXT", "PF", "PFs", "WEB", "PIS", "NF", "CEP", 
                                                                      "CNPJ", "CPF", "RG", "DP", "DV", "GAM", "EMS", "DDD", "UF", "IMC", 
                                                                      "D/C", "S/A", "CPF/CNPJ", "INSS", "ISS", "CBO", "CNAE", "BI","CID", "CNU", "LOGs", "DV", "SCPA", 
                                                                      "SIB","GCDE","DIRF", "RPS", "SMS", "JIT","THM", "TMO", "HM", "SADT", "ICMS", "OPM" };

                var caracteresParaDeixarMaiusculaDepois = new List<string>() { "/", "\\" };

                var retorno = string.Empty;

                var array = texto.ToLower().Split(' ');

                CultureInfo cul = new CultureInfo("pt-BR");

                for (int i = 0; i < array.Length; i++)
                {
                    var palavraArray = array[i];
                    var existeParentesesEsquerdo = array[i].IndexOf('(');
                    var existeParentesesDireito = array[i].IndexOf(')');
                    array[i] = array[i].TiraParenteses();

                    if (ignorarPalavrasLigacao)
                    {
                        var palavraSemParenteses = array[i].TiraParenteses().ToUpper();
                        palavraArray = palavraSemParenteses.ToLower();

                        if (palavrasASeremSubstituidas.Exists(x => x.ToUpper().Equals(palavraSemParenteses)))
                        {
                            palavraArray = palavrasASeremSubstituidas.Find(x => x.ToUpper().Equals(palavraSemParenteses));
                        }
                        else if (palavraSemParenteses.EhLetrasComNumero() || palavraSemParenteses.EhNumeroRomano())
                        {
                            palavraArray = palavraSemParenteses;
                        }
                        else
                        {
                            if ((i == 0) || (!palavrasLigacao.Any(y => y.ToUpper().Equals(palavraSemParenteses.ToUpper()))))
                                palavraArray = (palavraSemParenteses.Count() > 1 ? palavraSemParenteses.Substring(0, 1).ToUpper() + palavraSemParenteses.Substring(1).ToLower() : cul.TextInfo.ToTitleCase(palavraSemParenteses));
                        }

                        var palavraAux = palavraSemParenteses.ToLower();
                        var temCaracter = false;
                        foreach (var caracter in caracteresParaDeixarMaiusculaDepois)
                        {
                            var palavraExit = "";
                            if (palavraAux.Contains(caracter) && !palavrasLigacao.Any(y => y.ToUpper().Equals(palavraAux.ToUpper())))
                            {
                                for (var x = 0; x < palavraAux.Count(); x++)
                                {
                                    var palavra = (String.IsNullOrWhiteSpace(palavraAux[x].ToString())) ? palavraAux[x].ToString() : palavraAux[x].ToString().ToUpper();
                                    palavraExit += (x == 0) ? Convert.ToChar(palavra) : palavraAux[x];
                                    if (palavraAux[x].ToString() == caracter.ToString())
                                    {
                                        if (palavraAux.Last() != palavraAux[x])
                                            palavraExit += (String.IsNullOrWhiteSpace(palavraAux[x + 1].ToString())) ? palavraAux[x + 1].ToString() : palavraAux[x + 1].ToString().ToUpper();
                                        x++;
                                    }
                                }
                                temCaracter = true;
                            }
                            if (palavraExit != "")
                                palavraAux = palavraExit;
                        }
                        if (temCaracter)
                            palavraArray = palavraAux;
                    }
                    else
                        palavraArray = cul.TextInfo.ToTitleCase(array[i]);

                    if (existeParentesesEsquerdo >= 0)
                        palavraArray = palavraArray.Insert(existeParentesesEsquerdo, "(");

                    if (existeParentesesDireito >= 0)
                        palavraArray = palavraArray.Insert(existeParentesesDireito, ")");

                    retorno += String.Format("{0}", palavraArray);

                    if (i != array.Length)
                        retorno += " ";
                }

                return retorno.TrimEnd();
            }

            return texto;
        }

        public static Boolean EhLetrasComNumero(this string t)
        {
            var numeros = t.Where(c => char.IsNumber(c)).Count();
            return numeros > 0 && numeros != t.Count();
        }

        public static Boolean EhNumeroRomano(this string t)
        {
            string p1 = @"\bx{0,3}(i{1,3}|i[vx]|vi{0,3})\b";
            return Regex.IsMatch(t, p1, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Converte o valor string para o padrão utilizado pelo Builder para colunas Z_ indexadas
        /// </summary>
        /// <param name="value">Valor da coluna original</param>
        /// <returns>Valor padronizado para coluna indexada</returns>
        public static string ToZIndexColumnValue(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return value;
            return value.ToUpperInvariant().RemoverAcentos();
        }

        public static long? ToNullableLong(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;
            var x = long.Parse(value);
            return (long?)x;
        }

        public static Int32? ToNullableInt32(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;
            var x = Int32.Parse(value);
            return (Int32?)x;
        }

        public static Int64? ToNullableInt64(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;
            var x = Int64.Parse(value);
            return (Int64?)x;
        }

        public static Double? ToNullableDouble(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;
            var x = Double.Parse(value);
            return (Double?)x;
        }

        public static DateTime? ToNullableDateTime(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;
            var x = DateTime.Parse(value);
            return (DateTime?)x;
        }

        public static DateTime? ToNullableDateTimeExact(this string value, string format, IFormatProvider formatProvider)
        {
            if (String.IsNullOrEmpty(value))
                return null;
            var x = DateTime.ParseExact(value, format, formatProvider);
            return (DateTime?)x;
        }

        public static bool? ToNullablebool(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;
            var x = bool.Parse(value);
            return (bool?)x;
        }

        /// <summary>
        /// Extrai o número localizado no início da string 
        /// </summary>
        /// <returns>
        /// Número localizado no início da string
        /// <example>"4514 string".PrefixedLong(): 4514</example>
        /// </returns>
        public static long? PrefixedLong(this string value)
        {
            if (value == null)
                return null;
            var match = Regex.Match(value, @"^\d+").Value;
            if (string.IsNullOrEmpty(match))
                return null;
            return match.ToNullableLong();
        }

        [Obsolete("Utilize ConvertRtfToHtml")]
        public static string ConvertFromRtfToHtml(this string RTF)
        {
            List<FromRtfToHtml> list = new List<FromRtfToHtml>();
            list.Add(new FromRtfToHtml() { From = @"\cf1", To = @"<span style='color:red'>" });
            list.Add(new FromRtfToHtml() { From = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1046{\fonttbl{\f0\fswiss\fcharset0 Courier New;}{\f1\fnil\fcharset2 Symbol;}}", To = @"" });
            list.Add(new FromRtfToHtml() { From = @"{\colortbl ;\red255\green0\blue0;\red0\green0\blue255;\red0\green128\blue0;}", To = @"" });
            list.Add(new FromRtfToHtml() { From = @"\par", To = @"<br/>" });
            list.Add(new FromRtfToHtml() { From = @"\cf0", To = @"</span>" });
            list.Add(new FromRtfToHtml() { From = @"\fs18", To = @"" });
            list.Add(new FromRtfToHtml() { From = @"\fs20", To = @"" });
            list.Add(new FromRtfToHtml() { From = @"\fs40", To = @"" });
            list.Add(new FromRtfToHtml() { From = @"\cf2", To = @"<span style='color:blue'>" });
            list.Add(new FromRtfToHtml() { From = @"\cf3", To = @"<span style='color:green'>" });
            list.Add(new FromRtfToHtml() { From = @"\b0", To = @"</strong>" });
            list.Add(new FromRtfToHtml() { From = @"\b", To = @"<strong>" });
            list.Add(new FromRtfToHtml() { From = "\r\n", To = @"<br/>" });

            foreach (var item in list)
            {
                RTF = RTF.Replace(item.From, item.To);
            }

            return RTF;
        }

        /// <summary>
        /// Método para completar uma string com um determinado caracter
        /// </summary>
        /// <param name="texto">Texto que receberá o novo caracter</param>
        /// <param name="tamanho">Tamanho total do retorno</param>
        /// <param name="alinha">D ou E (D - Direita coloca o completa do lado direito   E - Esquerda o inverso</param>
        /// <param name="completa">Qual caracter será adicionado ao texto</param>
        /// <returns>Retorna o novo texto</returns>
        public static string Completa(this string texto, int tamanho, char alinha, char completa)
        {
            var auxTamamho = texto.TrimEnd().Length;
            if (auxTamamho > tamanho)
                return texto;

            return texto.FormatarAlinharCaracterStr(alinha, completa, (auxTamamho - tamanho));
        }

        /// <summary>
        /// Método que ao se passar um tamanho (length) maior que o tamanho real da string, 
        /// simplesmente ignora o parâmetro e pega o valor o tamanho da string. Caso contrário,
        /// o método funciona igual o substring.
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubStringMaxLength(this string texto, int startIndex, int length)
        {
            if (string.IsNullOrEmpty(texto))
                return texto;
            else if (length > texto.Length - startIndex)
                return texto.Substring(startIndex, texto.Length - startIndex);
            else
                return texto.Substring(startIndex, length);
        }

        /// <summary>
        /// Faz a quebra do texto em porções com o tamanho indicado
        /// </summary>
        /// <param name="str">Texto</param>
        /// <param name="chunkSize">Tamanho de cada porção</param>
        /// <returns>Lista com as porções</returns>
        public static List<string> Split(this string str, int chunkSize)
        {
            return Enumerable.Range(0, (int)Math.Ceiling((double)str.Length / chunkSize))
                .Select(i => (i * chunkSize + chunkSize < str.Length) ? str.Substring(i * chunkSize, chunkSize) : str.Substring(i * chunkSize)).ToList();
        }

        public static bool IsValidXml(this string str)
        {
            if (String.IsNullOrEmpty(str))
                return false;
            try
            {
                var xDoc = new XmlDocument();
                xDoc.LoadXml(str);
                xDoc = null;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Faz a quebra das linhas
        /// </summary>
        /// <param name="str">Texto a quebrar em linhas</param>
        /// <returns>Lista com as linhas</returns>
        public static List<string> GetLines(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return new List<string>();

            return str.Replace("\r\n", "¬").Replace("\r", "¬").Replace("\n", "¬").Split('¬').ToList();
        }

        /// <summary>
        /// Pega o valor em um XML pelo nome da tag
        /// </summary>
        /// <param name="texto"> this </param>
        /// <param name="tag">texto de descrição do tag que se deseja pegar o valor</param>
        /// <param name="comTag">padrão false apenas o valor, passa true se deseja que retorne com a tag Ex: "</param>
        /// <returns>
        /// <example>comTag = false; entrada: <xml><preco>123,5</preco></xml>; saida: 123,5</example>
        /// <example>comTag = true; entrada: <xml><preco>123,5</preco></xml>; saida: <preco>123,5</preco></example>
        /// </returns>
        public static string GetValueInXML(this string texto, string tag, bool comTag = false)
        {
            if (!string.IsNullOrEmpty(texto))
            {
                string tagInicial = string.Format("<{0}>", tag);
                string tagFinal = string.Format("</{0}>", tag);
                int tagFimLength = tagFinal.Length;

                var inicio = texto.IndexOf(tagInicial);
                var fim = texto.IndexOf(tagFinal);

                if (inicio < 0 || fim < 0)
                    return null;

                inicio = inicio + (comTag ? 0 : tagInicial.Length);
                fim = fim + (comTag ? tagFimLength : 0);

                string xml = texto.Substring(inicio, fim - inicio);
                return xml;
            }

            return null;
        }

        private static bool NonoDigito(string celular)
        {
            return celular.Length == 11;
        }

        private static bool Prefixo4Digitos(string celular)
        {
            return celular.Length == 10;
        }

        public static string GetTelefoneDDD(this string texto)
        {
            var celular = texto.GetOnlyNumbers().Trim();
            return celular.Substring(0, 2);

        }

        public static string GetTelefonePrefixo(this string texto)
        {
            var celular = texto.GetOnlyNumbers().Trim();

            if (Prefixo4Digitos(celular))
                return celular.Substring(2, 4);

            if (NonoDigito(celular))
                return celular.Substring(2, 5);

            return String.Empty;
        }

        public static string GetTelefoneSufixo(this string texto)
        {
            var celular = texto.GetOnlyNumbers().Trim();

            if (Prefixo4Digitos(celular))
                return celular.Substring(6, 4);

            if (NonoDigito(celular))
                return celular.Substring(7, 4);

            return String.Empty;
        }
    }

    class FromRtfToHtml
    {
        public string From { get; set; }
        public string To { get; set; }
    }

    class ConvertRtfHtmlThreadData
    {
        public string RtfText { get; set; }
        public string HtmlText { get; set; }
    }
}
