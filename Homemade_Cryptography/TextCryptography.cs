using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kryptering_Forberedlese.Homemade_Cryptography;

namespace Kryptering_Forberedlese.Homemade_Cryptography
{
    class TextCryptography
    {
        public static string Encrypt(byte[][,] text, byte[][,] key)
        {
            byte[][][,] encryptedText = new byte[10][][,];

            byte[][,] initializationArray = new byte[text.Length][,];

            for (int textCounter = 0; textCounter < text.Length; textCounter++)
            {
                initializationArray[textCounter] = CryptographyDataMethods.XOR2DArray(text[textCounter], key[0]);
            }

            encryptedText[0] = initializationArray;

            for (int roundCounter = 1; roundCounter < encryptedText.Length; roundCounter++)
            {
                byte[][,] roundArray = encryptedText[roundCounter - 1];

                for (int textCounter = 0; textCounter < text.Length; textCounter++)
                {
                    roundArray[textCounter] =
                        CryptographyDataMethods.SBoxConvert(roundArray[textCounter]);

                    roundArray[textCounter] =
                        CryptographyDataMethods.Shuffle2DArray(roundArray[textCounter]);

                    //skip mix column if last round
                    if (roundCounter != text.Length-1)
                    {
                        for (int columnCounter = 0; columnCounter < 4; columnCounter++)
                        {
                            byte[] bufferArray =
                                CryptographyDataMethods.MixColumnByteArray(new byte[4]
                                {
                                    roundArray[textCounter][columnCounter, 0],
                                    roundArray[textCounter][columnCounter, 1],
                                    roundArray[textCounter][columnCounter, 2],
                                    roundArray[textCounter][columnCounter, 3]
                                });
                            for (int rowCounter = 0; rowCounter < 4; rowCounter++)
                            {
                                roundArray[textCounter][columnCounter, rowCounter] = bufferArray[rowCounter];
                            }
                        }
                    }
                    //

                    roundArray[textCounter] = CryptographyDataMethods.XOR2DArray(roundArray[textCounter], key[roundCounter]);
                }

                encryptedText[roundCounter] = roundArray;
            }
            string returnString = "";
            byte[][,] encryptedTextResult = encryptedText[encryptedText.Length - 1];

            for (int blockCounter = 0; blockCounter < encryptedTextResult.Length; blockCounter++)
            {
                for (int columnCounter = 0; columnCounter < 4; columnCounter++)
                {
                    for (int rowCounter = 0; rowCounter < 4; rowCounter++)
                    {
                        returnString +=
                            $" {encryptedTextResult[blockCounter][columnCounter, rowCounter].ToString("X2")}";
                    }
                }
            }

            return returnString;
        }
    }
}
