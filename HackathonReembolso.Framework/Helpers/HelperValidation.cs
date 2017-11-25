using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace HackathonReembolso.Framework.Helpers
{
    public class HelperValidation
    {
        /// <summary>
        /// LessThan : se a data for menor do que
        /// HigherThan : se a data for maior do que
        /// </summary>
        public enum DateTypeValidation { LessThan, HigherThan }

        // Validada com PEX : 05/02/2014
        public static bool ValidarCPF(string cpf)
        {
            #region Pex

            if (string.IsNullOrEmpty(cpf))
            {
                return false;
            }

            // verifica se a string tem mais que 11 caracteres
            cpf = cpf.Replace(".", "").Replace("-", "").Replace("_", "");
            if (cpf.Length != 11)
            {
                return false;
            }

            // apos remover os caracteres invalidos, verifica se a nova string ainda tem mais que 11 caracteres
            cpf = HelperText.ExtractNumbers(cpf);
            if (cpf.Length != 11)
            {
                return false;
            }

            #endregion Pex

            string s = cpf;
            string c = s.Substring(0, 9);
            string dv = s.Substring(9, 2);
            int d1 = 0;
            for (int i = 0; i < 9; i++)
            {
                d1 += Int32.Parse(c.Substring(i, 1)) * (10 - i);
            }

            if (d1 == 0) return false;
            d1 = 11 - (d1 % 11);
            if (d1 > 9) d1 = 0;
            if (Int32.Parse(dv.Substring(0, 1)) != d1)
            {
                return false;
            }
            d1 *= 2;
            for (int i = 0; i < 9; i++)
            {
                d1 += Int32.Parse(c.Substring(i, 1)) * (11 - i);
            }
            d1 = 11 - (d1 % 11);
            if (d1 > 9) d1 = 0;
            if (Int32.Parse(dv.Substring(1, 1)) != d1)
            {
                return false;
            }

            return true;
        }

        // Validada com PEX : 05/02/2014
        public static bool ValidarCNPJ(string cnpj)
        {
            #region Pex

            if (string.IsNullOrEmpty(cnpj))
            {
                return false;
            }

            // apos remover os caracteres invalidos, verifica se a nova string ainda tem mais que 11 caracteres
            cnpj = HelperText.ExtractNumbers(cnpj);
            if (cnpj.Length != 14)
            {
                return false;
            }

            #endregion Pex

            string s = cnpj;
            string c = s.Substring(0, 12);
            string dv = s.Substring(12, 2);

            int d1 = 0;
            for (int i = 0; i < 12; i++)
            {
                d1 += Int32.Parse(c.Substring(11 - i, 1)) * (2 + (i % 8));
            }
            if (0 == d1) return false;

            d1 = 11 - (d1 % 11);
            if (d1 > 9) d1 = 0;
            if (Int32.Parse(dv.Substring(0, 1)) != d1)
            {
                return false;
            }
            d1 *= 2;
            for (int i = 0; i < 12; i++)
            {
                d1 += Int32.Parse(c.Substring(11 - i, 1)) * (2 + ((i + 1) % 8));
            }
            d1 = 11 - (d1 % 11);
            if (d1 > 9) d1 = 0;
            if (Int32.Parse(dv.Substring(1, 1)) != d1)
            {
                return false;
            }
            return true;
        }

        // Validada com PEX : 05/02/2014
        public static bool ValidarDataNascimento(DateTime data, int idade, DateTypeValidation type)
        {
            #region Pex

            if (!IsValidDate(data))
            {
                return false;
            }

            if (idade <= 0)
            {
                return false;
            }

            #endregion

            int idadeCalculada = CalcularIdade(data);

            if (type == DateTypeValidation.HigherThan)
            {
                return (idadeCalculada >= idade);
            }
            else
            {
                return (idadeCalculada <= idade);
            }
        }

        public static bool ValidarIP(string ip)
        {
            IPAddress address;

            if (IPAddress.TryParse(ip, out address))
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    return true;
                }
            }

            return false;
        }

        public static int CalcularIdade(DateTime DataNascimento)
        {
            #region Pex

            if (DataNascimento == null) return 0;
            if (DataNascimento == DateTime.MinValue) return 0;
            if (DataNascimento == DateTime.MaxValue) return 0;

            #endregion

            int anos = DateTime.Now.Year - DataNascimento.Year;
            if (DateTime.Now.Month < DataNascimento.Month || (DateTime.Now.Month == DataNascimento.Month && DateTime.Now.Day < DataNascimento.Day))
            {
                anos--;
            }
            return anos;
        }

        // Validada com PEX : 05/02/2014
        public static bool IsValidEmail(string email)
        {
            #region Pex Validation

            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            #endregion Pex Validation

            return new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(email);
        }

        public static bool IsTimeInRange(string time, string start, string end)
        {
            return IsTimeInRange(HelperConvert.ToTimeSpan(time), HelperConvert.ToTimeSpan(start), HelperConvert.ToTimeSpan(end));
        }

        public static bool IsCurrentTimeInRange(string start, string end)
        {
            return IsTimeInRange(DateTime.Now.TimeOfDay, HelperConvert.ToTimeSpan(start), HelperConvert.ToTimeSpan(end));
        }

        public static bool IsTimeInRange(TimeSpan time, TimeSpan start, TimeSpan end)
        {
            return (time >= start) && (time <= end);
        }

        public static bool IsDateTime(string s)
        {
            DateTime result;
            if (DateTime.TryParse(s, out result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidDate(DateTime? date)
        {
            if (!date.HasValue)
            {
                return false;
            }
            return IsMinOrMaxDate(date.Value);
        }

        public static bool IsMinOrMaxDate(DateTime date)
        {
            if ((date.Day == DateTime.MinValue.Day) && (date.Month == DateTime.MinValue.Month) && (date.Year == DateTime.MinValue.Year))
            {
                return false;
            }
            else if ((date.Day == DateTime.MaxValue.Day) && (date.Month == DateTime.MaxValue.Month) && (date.Year == DateTime.MaxValue.Year))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsValidDate(string date)
        {
            return IsValidDate(HelperConvert.ToDateTime(date));
        }

        // Validado com PEX no dia 12/02/2014
        public static bool IsValidGuid(string guid)
        {
            return !string.IsNullOrEmpty(guid) && !HelperConvert.ToGuid(guid).Equals(Guid.Empty);
        }

        // Pex OK
        public static bool FilePathHasInvalidChars(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return true;
            }
            
            if (string.IsNullOrWhiteSpace(path))
            {
                return true;
            }

            if (path.IndexOfAny(System.IO.Path.GetInvalidPathChars()) >= 0)
            {
                return true;
            }

            return false;
        }

        // Pex OK!
        public static bool IsValidDir(string dir)
        {
            if (FilePathHasInvalidChars(dir))
            {
                return false;
            }

            if (!System.IO.Directory.Exists(dir))
            {
                return false;
            }

            return true;
        }

        public static bool IsValidEan13(string eanBarcode)
        {
            return IsValidEan(eanBarcode, 13);
        }

        public static bool IsValidEan12(string eanBarcode)
        {
            return IsValidEan(eanBarcode, 12);
        }

        public static bool IsValidEan14(string eanBarcode)
        {
            return IsValidEan(eanBarcode, 14);
        }

        public static bool IsValidEan8(string eanBarcode)
        {
            return IsValidEan(eanBarcode, 8);
        }

        private static bool IsValidEan(string eanBarcode, int length)
        {
            if (eanBarcode.Length != length) return false;
            var allDigits = eanBarcode.Select(c => int.Parse(c.ToString(CultureInfo.InvariantCulture))).ToArray();
            var s = length % 2 == 0 ? 3 : 1;
            var s2 = s == 3 ? 1 : 3;
            return allDigits.Last() == (10 - (allDigits.Take(length - 1).Select((c, ci) => c * (ci % 2 == 0 ? s : s2)).Sum() % 10)) % 10;
        }
    }
}