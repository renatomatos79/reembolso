using System;
using System.Globalization;

namespace HackathonReembolso.Framework.Helpers
{
    /// <summary>
    /// Classe desenvolvida para geração e validação de tokens entre aplicações
    /// </summary>
    public static class HelperToken
    {
        public static string GenerateSystemToken(string systemCode)
        {
            return systemCode + DateTime.Now.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
        }
    }
}
