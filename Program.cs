using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kryptering_Forberedlese.Asymmetrisk;
using Kryptering_Forberedlese.Symmetrisk;
using Kryptering_Forberedlese.Homemade_Cryptography;

namespace Kryptering_Forberedlese
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(VigenereKoden.Encryption.Encrypt("zbc", "zbc")+"\n");
            //AsyTest.RSATest();
            //SymTest.AESTest();

            //byte[] a = new byte[4] {0xD4, 0xBF, 0x5D, 0x30};

            //byte[] b = CryptographyDataMethods.MixColumnByteArray(a);

            //foreach (byte b1 in b)
            //{
            //    Console.Write(b1.ToString("X2")+" ");
            //}

            //Homemade_Cryptography.KeyEncryptor.Encrypt("abchtgdquilhygtf");
            byte[][,] key = KeyEncryptor.Encrypt("1234567887654321");

            byte[] input = CryptographyDataMethods.TextToHex("abcdabcdabcdabcd");
            byte[][,] text = CryptographyDataMethods.ByteArrayToBlocks(TextPadding.AddPadding(input, 16));

            string AAAAAAAAAAAAAA = TextCryptography.Encrypt(text, key);

            Console.WriteLine(AAAAAAAAAAAAAA.TrimStart());

            Console.ReadKey(true);

            // https://stackoverflow.com/questions/13572253/what-kind-of-padding-should-aes-use
            // https://kavaliro.com/wp-content/uploads/2014/03/AES.pdf

            // http://www.infosecwriters.com/text_resources/pdf/AESbyExample.pdf
            // http://etutorials.org/Networking/802.11+security.+wi-fi+protected+access+and+802.11i/Appendixes/Appendix+A.+Overview+of+the+AES+Block+Cipher/Steps+in+the+AES+Encryption+Process/
        }
    }
}
