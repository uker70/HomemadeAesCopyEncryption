using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kryptering_Forberedlese.Homemade_Cryptography
{
    class CryptographyDataMethods
    {
        private static readonly char[] _sBoxIndex = new char[16]
        {
            '0',  '1',  '2',  '3',  '4',  '5',  '6',  '7',  '8',  '9',  'A',  'B',  'C',  'D',  'E',  'F'
        };
        private static readonly string[,] _sBox = new string[16, 16]
        {
            {"63", "7C", "77", "7B", "F2", "6B", "6F", "C5", "30", "01", "67", "2B", "FE", "D7", "AB", "76"},
            {"CA", "82", "C9", "7D", "FA", "59", "47", "F0", "AD", "D4", "A2", "AF", "9C", "A4", "72", "C0"},
            {"B7", "FD", "93", "26", "36", "3F", "F7", "CC", "34", "A5", "E5", "F1", "71", "D8", "31", "15"},
            {"04", "C7", "23", "C3", "18", "96", "05", "9A", "07", "12", "80", "E2", "EB", "27", "B2", "75"},
            {"09", "83", "2C", "1A", "1B", "6E", "5A", "A0", "52", "3B", "D6", "B3", "29", "E3", "2F", "84"},
            {"53", "D1", "00", "ED", "20", "FC", "B1", "5B", "6A", "CB", "BE", "39", "4A", "4C", "58", "CF"},
            {"D0", "EF", "AA", "FB", "43", "4D", "33", "85", "45", "F9", "02", "7F", "50", "3C", "9F", "A8"},
            {"51", "A3", "40", "8F", "92", "9D", "38", "F5", "BC", "B6", "DA", "21", "10", "FF", "F3", "D2"},
            {"CD", "0C", "13", "EC", "5F", "97", "44", "17", "C4", "A7", "7E", "3D", "64", "5D", "19", "73"},
            {"60", "81", "4F", "DC", "22", "2A", "90", "88", "46", "EE", "B8", "14", "DE", "5E", "0B", "DB"},
            {"E0", "32", "3A", "0A", "49", "06", "24", "5C", "C2", "D3", "AC", "62", "91", "95", "E4", "79"},
            {"E7", "C8", "37", "6D", "8D", "D5", "4E", "A9", "6C", "56", "F4", "EA", "65", "7A", "AE", "08"},
            {"BA", "78", "25", "2E", "1C", "A6", "B4", "C6", "E8", "DD", "74", "1F", "4B", "BD", "8B", "8A"},
            {"70", "3E", "B5", "66", "48", "03", "F6", "0E", "61", "35", "57", "B9", "86", "C1", "1D", "9E"},
            {"E1", "F8", "98", "11", "69", "D9", "8E", "94", "9B", "1E", "87", "E9", "CE", "55", "28", "DF"},
            {"8C", "A1", "89", "0D", "BF", "E6", "42", "68", "41", "91", "2D", "0F", "B0", "54", "BB", "16"}
        };
        private static readonly string[,] _inverseSBox = new string[16, 16]
        {
            {"52", "09", "6A", "D5", "30", "36", "A5", "38", "BF", "40", "A3", "9E", "81", "F3", "D7", "FB"},
            {"7C", "E3", "39", "82", "9B", "2F", "FF", "87", "34", "8E", "43", "44", "C4", "DE", "E9", "CB"},
            {"54", "7B", "94", "32", "A6", "C2", "23", "3D", "EE", "4C", "95", "0B", "42", "FA", "C3", "4E"},
            {"08", "2E", "A1", "66", "28", "D9", "24", "B2", "76", "5B", "A2", "49", "6D", "8B", "D1", "25"},
            {"72", "F8", "F6", "64", "86", "68", "98", "16", "D4", "A4", "5C", "CC", "5D", "65", "B6", "92"},
            {"6C", "70", "48", "50", "FD", "ED", "B9", "DA", "5E", "15", "46", "57", "A7", "8D", "9D", "84"},
            {"90", "D8", "AB", "00", "8C", "BC", "D3", "0A", "F7", "E4", "58", "05", "B8", "B3", "45", "06"},
            {"D0", "2C", "1E", "8F", "CA", "3F", "0F", "02", "C1", "AF", "BD", "03", "01", "13", "8A", "6B"},
            {"3A", "91", "11", "41", "4F", "67", "DC", "EA", "97", "F2", "CF", "CE", "F0", "B4", "E6", "73"},
            {"96", "AC", "74", "22", "E7", "AD", "35", "85", "2E", "F9", "37", "E8", "1C", "75", "DF", "6E"},
            {"47", "F1", "1A", "71", "1D", "29", "C5", "89", "6F", "B7", "62", "0E", "AA", "18", "BE", "1B"},
            {"FC", "56", "3E", "4B", "C6", "D2", "79", "20", "9A", "DB", "C0", "FE", "78", "CD", "5A", "F4"},
            {"1F", "DD", "A8", "33", "88", "07", "C7", "31", "B1", "12", "10", "59", "27", "80", "EC", "5F"},
            {"60", "51", "7F", "A9", "19", "B5", "4A", "0D", "2D", "E5", "7A", "9F", "93", "C9", "9C", "EF"},
            {"A0", "E0", "3B", "4D", "AE", "2A", "F5", "B0", "C8", "EB", "BB", "3C", "83", "53", "99", "61"},
            {"17", "2B", "04", "7E", "BA", "77", "D6", "26", "E1", "69", "14", "63", "55", "21", "0C", "7D"}
        };

        public static byte[] TextToHex(string input)
        {
            byte[] returnBytes = new byte[input.Length];

            for (int inputCounter = 0; inputCounter < input.Length; inputCounter++)
            {
                returnBytes[inputCounter] = Convert.ToByte(input[inputCounter]);
            }

            return returnBytes;
        }

        //converts key to 2d HEX array (block)
        public static byte[,] ArrayTo2DArray(byte[] input)
        {
            byte[,] keyBlockBuffer = new byte[4, 4];
            int selectedByteCounter = 0;
            for (int columnCounter = 0; columnCounter < 4; columnCounter++)
            {
                for (int rowCounter = 0; rowCounter < 4; rowCounter++)
                {
                    keyBlockBuffer[rowCounter, columnCounter] = input[selectedByteCounter];
                    selectedByteCounter++;
                }
            }

            return keyBlockBuffer;
        }

        //moves the values in the array around
        public static byte[,] Shuffle2DArray(byte[,] array)
        {
            byte[] byteBuffer = new byte[4];
            for (int rowCounter = 0; rowCounter < 4; rowCounter++)
            {
                for (int selected = 0; selected < 4; selected++)
                {
                    byteBuffer[selected] = array[rowCounter, selected];
                }

                int selectedByteCounter = rowCounter;
                for (int columnCounter = 0; columnCounter < 4; columnCounter++)
                {
                    array[rowCounter, columnCounter] = byteBuffer[selectedByteCounter];
                    if (selectedByteCounter != 3)
                    {
                        selectedByteCounter++;
                    }
                    else
                    {
                        selectedByteCounter = 0;
                    }
                }
            }

            return array;
        }

        //runs the 2d key byte array through the S-Box "value changer" i guess you can call it
        public static byte[,] SBoxConvert(byte[,] array)
        {
            byte[,] byteBuffer = new byte[4, 4];
            for (int rowCounter = 0; rowCounter < array.GetLength(0); rowCounter++)
            {
                for (int columnCounter = 0; columnCounter < array.GetLength(1); columnCounter++)
                {
                    int row = 20;
                    int column = 20;
                    for (int index = 0; index < _sBoxIndex.Length; index++)
                    {
                        if (_sBoxIndex[index] == array[rowCounter, columnCounter].ToString("X2")[0])
                        {
                            row = index;
                        }

                        if (_sBoxIndex[index] == array[rowCounter, columnCounter].ToString("X2")[1])
                        {
                            column = index;
                        }

                        if (column != 20 && row != 20)
                        {
                            break;
                        }
                    }
                    byteBuffer[rowCounter, columnCounter] = (byte)Convert.ToInt32(_sBox[row, column], 16);
                }
            }

            return byteBuffer;
        }

        //adds a round constant of the HEX 01 (0000 0001) to the input value, through XOR operation
        public static byte AddRoundConstant(byte inputByte)
        {
            return Convert.ToByte(inputByte ^ 0x01);
        }

        //run XOR on all rows in a 2d byte array
        public static byte[,] XOR2DArray(byte[,] arrayOne, byte[,] arrayTwo)
        {
            byte[] byteBuffer = new byte[4];
            byte[,] returnArray = new byte[4, 4];

            for (int rowCounter = 0; rowCounter < 4; rowCounter++)
            {
                byteBuffer = XORByteArrays(
                    new byte[4]
                    {
                        arrayOne[rowCounter, 0], arrayOne[rowCounter, 1], arrayOne[rowCounter, 2],
                        arrayOne[rowCounter, 3]
                    },
                    new byte[4]
                    {
                        arrayTwo[rowCounter, 0], arrayTwo[rowCounter, 1], arrayTwo[rowCounter, 2],
                        arrayTwo[rowCounter, 3]
                    });

                for (int columnCounter = 0; columnCounter < 4; columnCounter++)
                {
                    returnArray[rowCounter, columnCounter] = byteBuffer[columnCounter];
                }
            }

            return returnArray;
        }

        //gets 2 byte arrays and runs XOR on them and returns result
        public static byte[] XORByteArrays(byte[] arrayOne, byte[] arrayTwo)
        {
            byte[] returnArray = new byte[4];

            for (int byteCounter = 0; byteCounter < 4; byteCounter++)
            {
                returnArray[byteCounter] = Convert.ToByte(arrayOne[byteCounter] ^ arrayTwo[byteCounter]);
            }

            return returnArray;
        }

        //splits a byte array into blocks of 2d byte arrays
        public static byte[][,] ByteArrayToBlocks(byte[] input)
        {
            byte[][,] textBlocks = new byte[input.Length / 16][,];
            byte[] buffer = new byte[16];

            for (int textCounter = 0; textCounter < textBlocks.Length; textCounter++)
            {
                int bufferCounter = 0;
                for (int inputCounter = textCounter * 16; inputCounter < textCounter * 16 + 16; inputCounter++)
                {
                    buffer[bufferCounter] = input[inputCounter];
                    bufferCounter++;
                }

                textBlocks[textCounter] = CryptographyDataMethods.ArrayTo2DArray(buffer);
            }

            return textBlocks;
        }

        //runs the mix column operation on a byte array
        //google AES mix column for explanation
        private static byte[,] _mixColumnBytes = new byte[4, 4]
        {
            {0x02, 0x03, 0x01, 0x01},
            {0x01, 0x02, 0x03, 0x01},
            {0x01, 0x01, 0x02, 0x03},
            {0x03, 0x01, 0x01, 0x02}
        };
        private static byte _galosFieldRepresentation = 0x1b;
        public static byte[] MixColumnByteArray(byte[] inputArray)
        {
            byte[] returnArray = new byte[4];

            for (int roundCounter = 0; roundCounter < 4; roundCounter++)
            {
                byte[] buffer = new byte[4];

                for (int numberCounter = 0; numberCounter < 4; numberCounter++)
                {
                    bool useGalos = false;
                    string bufferString = "";
                    switch (_mixColumnBytes[roundCounter, numberCounter])
                    {
                        case 0x01:
                            buffer[numberCounter] = inputArray[numberCounter];
                            break;

                        case 0x02:
                            bufferString = Convert.ToString(inputArray[numberCounter], 2).PadLeft(8, '0') + '0';

                            if (bufferString[0] == '1')
                            {
                                useGalos = true;
                            }
                            bufferString = bufferString.Remove(0, 1);

                            if (useGalos)
                            {
                                buffer[numberCounter] = Convert.ToByte(Convert.ToByte(bufferString, 2) ^ _galosFieldRepresentation);
                            }
                            else
                            {
                                buffer[numberCounter] = Convert.ToByte(bufferString, 2);
                            }
                            break;

                        case 0x03:
                            bufferString = Convert.ToString(inputArray[numberCounter], 2).PadLeft(8, '0') + '0';

                            if (bufferString[0] == '1')
                            {
                                useGalos = true;
                            }
                            bufferString = bufferString.Remove(0, 1);

                            if (useGalos)
                            {
                                buffer[numberCounter] = Convert.ToByte(
                                    (Convert.ToByte(bufferString, 2) ^ _galosFieldRepresentation) ^ inputArray[numberCounter]);
                            }
                            else
                            {
                                buffer[numberCounter] = Convert.ToByte(Convert.ToByte(bufferString, 2) ^ inputArray[numberCounter]);
                            }
                            break;
                    }

                    returnArray[roundCounter] = Convert.ToByte((buffer[0] ^ buffer[1]) ^ (buffer[2] ^ buffer[3]));
                }
            }

            return returnArray;
        }
    }
}
