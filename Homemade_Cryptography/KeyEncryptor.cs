using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kryptering_Forberedlese.Homemade_Cryptography
{
    class KeyEncryptor
    {
        public static byte[][,] Encrypt(string key)
        {
            if (key.Length != 16)
            {
                throw new ArgumentException("Key size not 128bit", nameof(key));
            }
            byte[][,] keyRounds = new byte[10][,];
            byte[] byteBuffer = new byte[4];
            byte[,] keyBlockBuffer = new byte[4,4];
            byte[,] keyBlock; //2D array [4,4]

            keyBlock = CryptographyDataMethods.ArrayTo2DArray(CryptographyDataMethods.TextToHex(key));
            keyRounds[0] = keyBlock;

            for (int roundCounter = 1; roundCounter < 10; roundCounter++)
            {
                keyBlockBuffer = CryptographyDataMethods.Shuffle2DArray(keyBlock);
                keyBlockBuffer = CryptographyDataMethods.SBoxConvert(keyBlockBuffer);
                keyBlockBuffer[3, 0] = CryptographyDataMethods.AddRoundConstant(keyBlockBuffer[3, 0]);
                keyBlock = CryptographyDataMethods.XOR2DArray(keyBlock, keyBlockBuffer);
                keyRounds[roundCounter] = keyBlock;
            }

            //BlockWrite(keyBlock);

            int counter = 0;
            foreach (byte[,] bytes in keyRounds)
            {
                foreach (byte b in bytes)
                {
                    Console.Write($"{b.ToString("X2")} ");
                }

                Console.WriteLine($"\tround {counter}");
                counter++;
            }

            Console.WriteLine();

            return keyRounds;
        }

        private static void BlockWrite(byte[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(string.Format("{0} ", array[j, i].ToString("X2")));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }

            Console.WriteLine();
        }
    }
}
