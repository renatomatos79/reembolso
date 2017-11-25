using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HackathonReembolso.Framework.Helpers
{
    public static class HelperSecurity
    {
        private static readonly string PasswordHash = "R3n@t*";
        private static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        public static string SaltKey(int size)
        {
            if (size <= 0)
            {
                return string.Empty;
            }

            string guid = Guid.NewGuid().ToString();
            guid = guid.Replace("-", "");
            return guid.Substring(0, size).ToUpper();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText">Textoa ser criptografado</param>
        /// <param name="SaltKey">Chave pública de criptografia</param>
        /// <returns>string</returns>
        public static string Encrypt(string plainText, string SaltKey)
        {
            #region .: PEX Validation :.

            if (HelperText.IsNullOrEmptyOrWhiteSpace(plainText))
            {
                return string.Empty;
            }

            if (HelperText.IsNullOrEmptyOrWhiteSpace(SaltKey))
            {
                return string.Empty;
            }

            if (SaltKey.Length < 8)
            {
                return string.Empty;
            }

            #endregion

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] cipherTextBytes;
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                cipherTextBytes = memoryStream.ToArray();
            }

            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string EncryptSHA512(string Str)
        {
            string SHA512Base64 = "";

            try
            {
                SHA512Managed Cod512 = new SHA512Managed();
                UTF8Encoding UTF8 = new UTF8Encoding();

                SHA512Base64 = Convert.ToBase64String(Cod512.ComputeHash(UTF8.GetBytes(Str)));
            }
            catch (Exception)
            {
                throw;
            }
            return SHA512Base64;
        }

        /// <summary>
        /// Caso a string não possa ser descriptografada uma exceção será gerada.
        /// </summary>
        /// <param name="encryptedText">Texto criptografado usando Encrypt</param>
        /// <param name="SaltKey">Chave pública de criptografia</param>
        /// <returns>string</returns>
        public static string Decrypt(string encryptedText, string SaltKey)
        {
            #region .: PEX Validation :.

            if (HelperText.IsNullOrEmptyOrWhiteSpace(encryptedText))
            {
                return string.Empty;
            }

            if (HelperText.IsNullOrEmptyOrWhiteSpace(SaltKey))
            {
                return string.Empty;
            }

            if (SaltKey.Length < 8)
            {
                return string.Empty;
            }

            #endregion

            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            int decryptedByteCount = 0;
            byte[] plainTextBytes = null;

            using (var memoryStream = new MemoryStream(cipherTextBytes))
            {
                var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                plainTextBytes = new byte[cipherTextBytes.Length];

                decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            }

            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }


    }
}
