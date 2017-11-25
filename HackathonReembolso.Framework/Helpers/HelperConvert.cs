using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace HackathonReembolso.Framework.Helpers
{
    public class HelperConvert
    {
        public static long ToLong(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                s = "0";
            }

            long result;
            if (long.TryParse(s, out result))
            {
                return result;
            }
            return 0;
        }

        public static int ToInt(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                s = "0";
            }

            int result;
            if (int.TryParse(s, out result))
            {
                return result;
            }
            return 0;
        }

        public static int ToInt(object s)
        {
            if (s == null)
            {
                return 0;
            }
            return ToInt(s.ToString());
        }

        public static Int64 ToInt64(string s)
        {
            long result = 0;
            if (!string.IsNullOrEmpty(s))
            {
                try
                {
                    result = Int64.Parse(s);
                }
                catch
                {
                    result = 0;
                }
            }
            return result;
        }

        public static Int32 ToInt32(string s)
        {
            Int32 result = 0;
            if (!string.IsNullOrEmpty(s))
            {
                try
                {
                    result = Int32.Parse(s);
                }
                catch
                {
                    result = 0;
                }
            }
            return result;
        }

        public static short ToShort(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                s = "0";
            }

            short result;
            if (short.TryParse(s, out result))
            {
                return result;
            }
            return 0;
        }

        public static DateTime ToDateTime(string s)
        {
            DateTime result;
            if (DateTime.TryParse(s, out result))
            {
                return result;
            }
            return DateTime.MinValue;
        }

        public static DateTime ToDateTimeBr(string s)
        {
            DateTime result;
            if (DateTime.TryParse(s, CultureInfo.CreateSpecificCulture("pt-BR"), DateTimeStyles.None, out result))
            {
                return result;
            }
            return DateTime.MinValue;
        }

        public static string ToDateFormatBr(string s)
        {
            return String.Format(new CultureInfo("pt-BR"), "{0:d}", s);
        }

        public static DateTime? ToNullableDateTime(string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return null;
            }
            else
            {
                return ToDateTime(s);
            }
        }

        public static TimeSpan ToTimeSpan(string s)
        {
            TimeSpan result;
            if (TimeSpan.TryParse(s, out result))
            {
                return result;
            }
            return TimeSpan.MinValue;
        }

        public static DateTime ToFirstHour(DateTime d)
        {
            int day = d.Day;
            int month = d.Month;
            int year = d.Year;
            return new DateTime(year, month, day, 0, 0, 0, 0);
        }

        public static DateTime ToLastHour(DateTime d)
        {
            int day = d.Day;
            int month = d.Month;
            int year = d.Year;
            return new DateTime(year, month, day, 23, 59, 59, 997);
        }

        public static bool IsDateTime(string s)
        {
            DateTime result;
            if (DateTime.TryParse(s, out result))
            {
                return true;
            }
            return false;
        }

        public static bool IsValidDate(DateTime date)
        {
            if ((date.Day == DateTime.MinValue.Day) && (date.Month == DateTime.MinValue.Month) &&
                (date.Year == DateTime.MinValue.Year))
            {
                return false;
            }
            return true;
        }

        public static bool IsValidDate(string date)
        {
            return IsValidDate(ToDateTime(date));
        }

        public static double ToDouble(string s)
        {
            double result;
            if (double.TryParse(s, out result))
            {
                return result;
            }
            return 0;
        }

        public static decimal ToDecimal(string s)
        {
            decimal result;
            if (decimal.TryParse(s, out result))
            {
                return result;
            }
            return 0;
        }

        public static decimal? ToNullableDecimal(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }
            else
            {
                return ToDecimal(s);
            }
        }

        public static float ToFloat(string s)
        {
            float result;
            if (float.TryParse(s, out result))
            {
                return result;
            }
            return 0;
        }

        public static string ToString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }
            return value;
        }

        public static byte[] ToByte(string value)
        {
            var bytes = new byte[value.Length*sizeof (char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        // Validado com PEX no dia 12/02/2014
        public static bool IsValiddGuid(string guid)
        {
            return !ToGuid(guid).Equals(Guid.Empty);
        }

        // Validado com PEX no dia 12/02/2014
        public static Guid ToGuid(string value)
        {
            Guid guid = Guid.Empty;
            if (Guid.TryParse(value, out guid))
            {
                return guid;
            }
            return Guid.Empty;
        }

        public static bool ToBoolean(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            else
            {
                bool result = false;
                if (bool.TryParse(value, out result))
                {
                    return result;
                }
                return result;
            }
        }

        public static bool IsInteger(PropertyInfo prop)
        {
            if (prop.PropertyType == typeof (int))
            {
                return true;
            }
            else if (prop.PropertyType == typeof(Int16))
            {
                return true;
            }
            else if (prop.PropertyType == typeof(Int32))
            {
                return true;
            }
            else if (prop.PropertyType == typeof(Int64))
            {
                return true;
            }
            return false;
        }

        public static bool IsNullableInteger(PropertyInfo prop)
        {
            if (prop.PropertyType == typeof(int?))
            {
                return true;
            }
            else if (prop.PropertyType == typeof(Int16?))
            {
                return true;
            }
            else if (prop.PropertyType == typeof(Int32?))
            {
                return true;
            }
            else if (prop.PropertyType == typeof(Int64?))
            {
                return true;
            }
            return false;
        }

        public static string ToJsonString(IEnumerable<KeyValuePair<string, object>> dict)
        {
            var entries = dict.Select(d => string.Format("\"{0}\": \"{1}\"", d.Key, (d.Value is DateTime) ? Convert.ToDateTime(d.Value).ToString("d") : d.Value.ToString()));
            return "{" + string.Join(",", entries) + "}";
        }

        public static string BooleanToFlagSN(bool boolean)
        {
            return boolean ? "S" : "N";
        }

        public static bool FlagSNToBoolean(string flag)
        {
            return (flag == "S");
        }

        public static object ToObject(string tipoDado, int precision, int escale, string value)
        {
            var codigo = tipoDado.ToUpper();
            object result = null;

            if (codigo.Contains("CHAR") || codigo.Contains("VARCHAR") || codigo.Contains("CLOB") ||
                codigo.Contains("RAW"))
            {
                if (!string.IsNullOrEmpty(value))
                    result = value;
            }
            else if (codigo.Contains("NUMBER"))
            {
                if (Regex.IsMatch(value, @"^[0-9]+$"))
                    result = ToInt(value);
                else if (precision > 0 && escale > 0)
                    result = ToDecimal(value).ToString("G", CultureInfo.InvariantCulture);
            }
            else if (codigo.Contains("LONG"))
                result = ToLong(value);
            else if (codigo.Contains("DATE") || codigo.Contains("TIMESTAMP"))
            {
                DateTime date;
                if (!string.IsNullOrEmpty(value) && DateTime.TryParse(value, out date))
                    result = ToDateTime(ToDateFormatBr(value));
            }

            return result;
        }      

        public static DateTime? ToDateTimeBr(string dateString, bool useMinDateValueOnError = false)
        {
            try
            {
                var culture = CultureInfo.CreateSpecificCulture("pt-BR");
                var styles = DateTimeStyles.None;
                DateTime dateResult;
                if (DateTime.TryParse(dateString, culture, styles, out dateResult))
                {
                    return dateResult;
                }
                return useMinDateValueOnError ? DateTime.MinValue : default(DateTime?);
            }
            catch
            {
                return useMinDateValueOnError ? DateTime.MinValue : default(DateTime?);
            }
        }

        public static DateTime? ToDateTimeBr2(string dateString, bool useMinDateValueOnError = false)
        {
            try
            {
                /* 
                    dd/MM/yyyy 
                */
                var dia = HelperConvert.ToInt(dateString.Substring(0, 2));
                var mes = HelperConvert.ToInt(dateString.Substring(3, 2));
                var ano = HelperConvert.ToInt(dateString.Substring(6, 4));

                return new DateTime(ano, mes, dia);
            }
            catch
            {
                return useMinDateValueOnError ? DateTime.MinValue : default(DateTime?);
            }
        }        
    }
}