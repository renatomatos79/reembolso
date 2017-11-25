using System;

namespace HackathonReembolso.Framework.Helpers
{
    public class HelperSettings
    {
        public static string ReadString(string key)
        {
            try
            {
                var rd = new System.Configuration.AppSettingsReader();
                return rd.GetValue(key, typeof(String)).ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static int ReadInteger(string key)
        {
            return HelperConvert.ToInt(ReadString(key));
        }

        public static bool ReadBoolean(string key)
        {
            return HelperConvert.ToBoolean(ReadString(key));
        }
    }
}
