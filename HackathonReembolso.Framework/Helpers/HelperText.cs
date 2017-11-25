using System;
using System.Globalization;
using System.Text;

namespace HackathonReembolso.Framework.Helpers
{
    public class HelperText
    {
        public static bool IsNullOrEmptyOrWhiteSpace(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return true;
            }
            if (string.IsNullOrWhiteSpace(str))
            {
                return true;
            }
            return false;
        }

        // Validada com PEX : 05/02/2014
        public static String TrataTexto(String strTexto)
        {
            if (string.IsNullOrEmpty(strTexto))
            {
                return string.Empty;
            }

            strTexto = RemoveAcentos(strTexto);
            strTexto = RemoveSpecialChars(strTexto);
            return strTexto;
        }

        // Validada com PEX : 05/02/2014
        public static string RemoveAcentos(string strTexto)
        {
            if (string.IsNullOrEmpty(strTexto))
                return string.Empty;

            strTexto = strTexto.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (char c in strTexto.ToCharArray())
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);

            return sb.ToString();
        }

        // Validada com PEX : 05/02/2014
        public static string RemoveSpecialChars(String str)
        {
            #region Pex

            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            #endregion Pex

            String C_acentos = "\"ÁÀÉÈÍÌÓÒÚÙÂÃÕÔÊÎÛáéíóúàèìòùãâêîôûçÁÈôÇáèÒçÂËòâëØÑÀÐøñàðÕÅõÝåÍÖýÃíöãÎÄîÚ<äÌú>ÆìÛ&æÏûïÙ®Éù©éÓÜÞÊóüþêÔº.";
            String S_acentos = "_AAEEIIOOUUAAOOEIUaeiouaeiouaaeioucAEoCaeOcAEoaeoNADonaoOAoYAIOyAioaUAiU_aIu_EiU_eIuiUrEUceOUpEoupeO__";

            for (int i = 0; i < C_acentos.Length; i++)
                str = str.Replace(C_acentos[i].ToString(), S_acentos[i].ToString()).Trim();

            // remove \0 = null
            str = str.Replace("\0", string.Empty);

            return str;
        }

        //# Validado pelo Pex: #12/02/2012#
        /// <summary>
        /// Remove os caracteres especiais non-Ascii
        /// </summary>
        /// <param name="str"></param>
        /// <returns>String sem caracteres especiais</returns>
        public static string RemoveNonAscii(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            StringBuilder strb = new StringBuilder();
            foreach (char c in str)
            {
                if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || char.IsPunctuation(c))
                {
                    strb.Append(c);
                }
            }
            return strb.ToString();
        }


        // Validada com PEX : 05/02/2014
        public static string ExtractNumbers(string original)
        {
            // remove caracteres especiais da string
            var novaString = TrataTexto(original);
            // retira somente os números
            return System.Text.RegularExpressions.Regex.Replace(novaString, "[^0-9]+", string.Empty);
        }

        public static string SepararTextoPorMaiusculo(string text)
        {
            string output = "";

            foreach (char letter in text)
            {
                if (Char.IsUpper(letter) && output.Length > 0)
                    output += " " + letter;
                else
                    output += letter;
            }

            return output;
        }

        public static bool Compare(string text1, string text2)
        {
            text1 = text1 ?? "";
            text2 = text2 ?? "";
            return text1.Trim().Equals(text2.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        public static bool Contains(string text1, string text2)
        {
            text1 = text1 ?? "";
            text2 = text2 ?? "";
            return text1.IndexOf(text2, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static string CnabCompletar(string texto,int totalCaracteres,bool eNumero)
        {
            texto = TrataTexto(texto);

            var substituirCom = eNumero ? '0' : ' ';

            if (eNumero)
                texto = ExtractNumbers(texto);

            if(string.IsNullOrEmpty(texto))
                return new string(substituirCom, totalCaracteres);

            if (texto.Length > totalCaracteres)
                return texto.Substring(0, totalCaracteres);

            var resultado = new StringBuilder().Insert(0, substituirCom.ToString(), totalCaracteres - texto.Length);

            if (eNumero)
                return resultado + texto;

            return texto + resultado;
        }

        public static string FormatarCnpj(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj))
                return string.Empty;

            return Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
        }

        public static string FormatarCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return string.Empty;

            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }
    }
}
