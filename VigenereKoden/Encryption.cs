using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kryptering_Forberedlese.VigenereKoden
{
    class Encryption
    {
        private static char[] _vigenere = "ABCDEFGHIJKLMNOPQRSTUVXYZÆØÅ ".ToLower().ToCharArray();
        public static string Encrypt(string text, string key)
        {
            string output = "";
            string changedKey = "";

            while (changedKey.Length != text.Length)
            {
                if (changedKey.Length < text.Length)
                {
                    changedKey += key;
                }
                else if(changedKey.Length > text.Length)
                {
                    changedKey = changedKey.Substring(0, text.Length);
                }
            }

            for (int i = 0; i < text.Length; i++)
            {
                int charId = Array.IndexOf(_vigenere, text[i]);
                int keyId = Array.IndexOf(_vigenere, changedKey[i]);

                int newChar = charId + keyId;
                while (_vigenere.Length-1 < newChar)
                {
                    newChar -= _vigenere.Length;
                }

                output += _vigenere[newChar];
            }

            return output;
        }
    }
}
