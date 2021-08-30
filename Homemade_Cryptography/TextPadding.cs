using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kryptering_Forberedlese.Homemade_Cryptography
{
    class TextPadding
    {
        public static byte[] AddPadding(byte[] text, int byteSize)
        {
            byte[] returnText = null;
            int missingBytes = text.Length % byteSize;

            if (missingBytes != 0)
            {
                returnText = new byte[text.Length+(byteSize-missingBytes)];
            }
            else
            {
                returnText = new byte[text.Length + byteSize];
            }

            for (int textCounter = 0; textCounter < returnText.Length; textCounter++)
            {
                if (text.Length > textCounter)
                {
                    returnText[textCounter] = text[textCounter];
                }
                else if (text.Length == textCounter)
                {
                    returnText[textCounter] = 0x80;
                }
                else
                {
                    returnText[textCounter] = 0x00;
                }
            }

            return returnText;
        }

        public static byte[] RemovePadding(byte[] text)
        {
            byte[] returnText = null;

            for (int findPaddingCounter = 0; findPaddingCounter < text.Length; findPaddingCounter++)
            {
                if (text[findPaddingCounter] == 0x80)
                {
                    if (text.Length == findPaddingCounter + 1 || text[findPaddingCounter + 1] == 0x00)
                    {
                        returnText = new byte[findPaddingCounter];
                        for (int textCounter = 0; textCounter < returnText.Length; textCounter++)
                        {
                            returnText[textCounter] = text[textCounter];
                        }
                        break;
                    }
                }
            }

            return returnText;
        }
    }
}
