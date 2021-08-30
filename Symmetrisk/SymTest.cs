using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kryptering_Forberedlese.Symmetrisk
{
    class SymTest
    {
        public static void AESTest()
        {
            //the key has to be either 16, 24 or 32 digits to work, since
            //16 = 128
            //24 = 192
            //32 = 256
            string key = "b14ca5898a4e4133bbce2ea2";

            Console.WriteLine("Please enter a string for encryption");
            string str = Console.ReadLine();
            string encryptedString = AesCryptography.EncryptString(key, str);
            Console.WriteLine($"encrypted string = {encryptedString}");

            string decryptedString = AesCryptography.DecryptString(key, encryptedString);
            Console.WriteLine($"decrypted string = {decryptedString}");

            Console.ReadKey();
        }
    }
}
