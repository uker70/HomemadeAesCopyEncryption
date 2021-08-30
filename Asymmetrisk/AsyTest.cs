using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kryptering_Forberedlese.Asymmetrisk
{
    class AsyTest
    {
        public static void RSATest()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            string publicKey = rsa.ToXmlString(false);
            string privateKey = rsa.ToXmlString(true);

            Console.WriteLine("Please enter a string for encryption");
            string str = Console.ReadLine();
            byte[] encryptedString = RsaCryptography.EncryptString(publicKey, str);
            Console.WriteLine($"encrypted string = {Convert.ToBase64String(encryptedString)}");

            string decryptedString = RsaCryptography.DecryptString(privateKey, encryptedString);
            Console.WriteLine($"decrypted string = {decryptedString}");

            Console.ReadKey();
        }
    }
}
