using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kryptering_Forberedlese.Asymmetrisk
{
    class RsaCryptography
    {
        public static byte[] EncryptString(string publicKey, string text)
        {
            byte[] textBytes = Encoding.Unicode.GetBytes(text);
            byte[] encryptedTextBytes;

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);
                encryptedTextBytes = rsa.Encrypt(textBytes, false);
            }

            return encryptedTextBytes;
        }

        public static string DecryptString(string privateKey, byte[] text)
        {
            byte[] decryptedTextBytes;

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey);
                decryptedTextBytes = rsa.Decrypt(text, false);
            }

            return Encoding.Unicode.GetString(decryptedTextBytes);
        }
    }
}
