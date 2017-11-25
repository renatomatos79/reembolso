using System;

namespace HackathonReembolso.Framework.Helpers
{
    public static class HelperConstants
    {
        #region .: ConnectionStrings :.

        public static string ConnectionStringRbac
        {
            get { return HelperSettings.ReadString("ConnectionStrings.Rbac"); }
        }

        public static string ConnectionStringReports
        {
            get { return HelperSettings.ReadString("ConnectionStrings.Reports"); }
        }

        public static string ConnectionStringGsnet
        {
            get { return HelperSettings.ReadString("ConnectionStrings.Gsnet"); }
        }

        public static string ConnectionStringLog
        {
            get { return HelperSettings.ReadString("ConnectionStrings.Log"); }
        }

        public static string ConnectionStringStudio
        {
            get { return HelperSettings.ReadString("ConnectionStrings.Studio"); }
        }

        public static string ConnectionStringForms
        {
            get { return HelperSettings.ReadString("ConnectionStrings.Forms"); }
        }

        public static string ConnectionStringSuprimentos
        {
            get { return HelperSettings.ReadString("ConnectionStrings.Suprimentos"); }
        }

        public static string ConnectionStringSaltkey
        {
            get { return HelperSettings.ReadString("ConnectionStrings.Saltkey"); }
        }

        #endregion

        #region .: Application :.

        public static string ApplicationName
        {
            get { return HelperSettings.ReadString("Application.Name"); }
        }

        public static string ApplicationProviders
        {
            get { return HelperSettings.ReadString("Application.Providers"); }
        }

        public static string ApplicationRecaptchaSiteKey
        {
            get { return HelperSettings.ReadString("Application.Recaptcha.SiteKey"); }
        }

        public static string ApplicationRecaptchaSecretKey
        {
            get { return HelperSettings.ReadString("Application.Recaptcha.SecretKey"); }
        }

        public static string ApplicationRecaptchaVerifyURL
        {
            get { return HelperSettings.ReadString("Application.Recaptcha.VerifyURL"); }
        }

        #endregion

        #region .: System :.

        public static string SystemCode
        {
            get { return HelperSettings.ReadString("System.Code"); }
        }

        #endregion

        #region .: CDN :.

        public static string Cdn
        {
            get { return HelperSettings.ReadString("Cdn"); }
        }

        public static string Template
        {
            get { return HelperConstants.Cdn + "/template/" + HelperSettings.ReadString("Template") + "/assets"; }
        }

        public static string Logos
        {
            get { return HelperConstants.Cdn + "/logos" ; }
        }

        #endregion

        #region .: LOG :.

        public static bool LogEnabled
        {
            get { return HelperSettings.ReadBoolean("Log.Enabled"); }
        }
        public static string LogType
        {
            get { return HelperSettings.ReadString("Log.Type"); }
        }
        public static string LogFilePath
        {
            get
            {
                var path = HelperSettings.ReadString("Log.File.Path");
                // se existir a palavra chave {BaseDirectory}, a substitui pelo current diretório
                if (path.Contains("{BaseDirectory}"))
                {
                    path = path.Replace("{BaseDirectory}", AppDomain.CurrentDomain.BaseDirectory);
                }
                // se existir a palavra chave {CurrentDirectory}, a substitui pelo current diretório
                else if (path.Contains("{CurrentDirectory}"))
                {
                    path = path.Replace("{CurrentDirectory}", System.Environment.CurrentDirectory);
                }
                return path;
            }
        }
        public static string LogMailSmtp
        {
            get { return HelperSettings.ReadString("Log.Mail.Smtp"); }
        }
        public static string LogMailFrom
        {
            get { return HelperSettings.ReadString("Log.Mail.From"); }
        }
        public static string LogMailTo
        {
            get { return HelperSettings.ReadString("Log.Mail.To"); }
        }
        public static string LogMailSubject
        {
            get { return HelperSettings.ReadString("Log.Mail.Subject"); }
        }
        public static int LogMailSmtpPort
        {
            get
            {
                var port = HelperSettings.ReadInteger("Log.Mail.SmtpPort");
                return port == 0 ? 25 : port;
            }
        }
        public static string LogDatabaseConnection
        {
            get { return HelperSettings.ReadString("Log.Database.Connection"); }
        }
        public static string LogDatabaseApplicationName
        {
            get { return HelperSettings.ReadString("Log.Database.ApplicationName"); }
        }
        public static string LogApplicationName
        {
            get { return HelperSettings.ReadString("Log.EventView.ApplicationName"); }
        }

        #endregion 

        #region .: URL's de Proxy :.

        public static string UrlRbac
        {
            get { return HelperSettings.ReadString("Url.Rbac"); }
        }

        public static string UrlGsnet
        {
            get { return HelperSettings.ReadString("Url.Gsnet"); }
        }

        public static string UrlLogin
        {
            get { return HelperSettings.ReadString("Url.Login"); }
        }

        public static string UrlLoginCallback
        {
            get { return HelperSettings.ReadString("Url.Login.Callback"); }
        }

        public static string UrlLoginSaltKey 
        {
            get { return HelperSettings.ReadString("Url.Login.SaltKey"); }
        }

        public static string UrlReports
        {
            get { return HelperSettings.ReadString("Url.Reports"); }
        }

        public static string UrlStudio
        {
            get { return HelperSettings.ReadString("Url.Studio"); }
        }

        public static string UrlForms
        {
            get { return HelperSettings.ReadString("Url.Forms"); }
        }
        public static string UrlSuprimentos
        {
            get { return HelperSettings.ReadString("Url.Suprimentos"); }
        }

        public static string UrlRbacSite
        {
            get
            {
                return HelperSettings.ReadString("Url.Rbac.Site");
            }
        }

        #endregion
    }
}
