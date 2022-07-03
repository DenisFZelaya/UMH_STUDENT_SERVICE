using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using NpgsqlTypes;

namespace UMH_STUDENT_SERVICE.Utils
{
    public class Security
    {
        //Variables para cifrado de la contraseña
        private static string palapaso = "UMHAPP";
        private static string valorSalt = "UMHAPP";
        private static string encrip = "SHA1";
        private static string vector = "1234567891234567";
        private static int ite = 22;
        private static int tam_clave = 256;

        //Función que se encarga de encriptar la contraseña
        public static string EncodeHash(string textoCifrar)
        {
            try
            {
                byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(vector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(valorSalt);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(textoCifrar);

                PasswordDeriveBytes password =
                    new PasswordDeriveBytes(palapaso, saltValueBytes,
                        encrip, ite);

                byte[] keyBytes = password.GetBytes(tam_clave / 8);

                RijndaelManaged symmetricKey = new RijndaelManaged();

                symmetricKey.Mode = CipherMode.CBC;

                ICryptoTransform encryptor =
                    symmetricKey.CreateEncryptor(keyBytes, InitialVectorBytes);

                MemoryStream memoryStream = new MemoryStream();

                CryptoStream cryptoStream =
                    new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

                cryptoStream.FlushFinalBlock();

                byte[] cipherTextBytes = memoryStream.ToArray();

                memoryStream.Close();
                cryptoStream.Close();

                string textoCifradoFinal = Convert.ToBase64String(cipherTextBytes);

                return textoCifradoFinal;
            }
            catch
            {
                return null;
            }
        }

        //Function que desencripta la contraseña
        public static string DecodeHash(string textoCifrado)
        {
            try
            {
                byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(vector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(valorSalt);

                byte[] cipherTextBytes = Convert.FromBase64String(textoCifrado);

                PasswordDeriveBytes password =
                    new PasswordDeriveBytes(palapaso, saltValueBytes,
                        encrip, ite);

                byte[] keyBytes = password.GetBytes(tam_clave / 8);

                RijndaelManaged symmetricKey = new RijndaelManaged();

                symmetricKey.Mode = CipherMode.CBC;

                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, InitialVectorBytes);

                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

                byte[] plainTextBytes = new byte[cipherTextBytes.Length];

                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                memoryStream.Close();
                cryptoStream.Close();

                string textoDescifradoFinal = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

                return textoDescifradoFinal;
            }
            catch
            {
                return null;
            }
        }

        public static string SanitizeString(string dirtyString)
        {
            HashSet<char> removeChars = new HashSet<char>(" ?&^$#@!()+-,:;<>’\'-_*");
            StringBuilder result = new StringBuilder(dirtyString.Length);
            foreach (char c in dirtyString)
                if (!removeChars.Contains(c)) // prevent dirty chars
                    result.Append(c);
            return result.ToString();
        }
    }
}