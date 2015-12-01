using System;
using System.Security;
using System.Text;
using LogService;

// http://www.codeproject.com/Articles/4086/Converting-Hexadecimal-String-to-from-Byte-Array-i
namespace eZustellSendPOC.Services
{
    /// <summary>
    /// Summary description for ExtensionMethods.
    /// </summary>
    public static class ExtensionMethods
    {
        public static int GetByteCount(string hexString)
        {
            int numHexChars = 0;
            char c;
            // remove all none A-F, 0-9, characters
            for (int i = 0; i < hexString.Length; i++)
            {
                c = hexString[i];
                if (IsHexDigit(c))
                    numHexChars++;
            }
            // if odd number of characters, discard last character
            if (numHexChars % 2 != 0)
            {
                numHexChars--;
            }
            return numHexChars / 2; // 2 characters per byte
        }
        public static byte[] GetBytes(this string hexString)
        {
            int discarded;
            return GetBytes(hexString, out discarded);
        }
        /// <summary>
        /// Creates a byte array from the hexadecimal string. Each two characters are combined
        /// to create one byte. First two hexadecimal characters become first byte in returned array.
        /// Non-hexadecimal characters are ignored. 
        /// </summary>
        /// <param name="hexString">string to convert to byte array</param>
        /// <param name="discarded">number of characters in string ignored</param>
        /// <returns>byte array, in the same left-to-right order as the hexString</returns>
        public static byte[] GetBytes(this string hexString, out int discarded)
        {
            discarded = 0;
            string newString = "";
            char c;
            // remove all none A-F, 0-9, characters
            for (int i = 0; i < hexString.Length; i++)
            {
                c = hexString[i];
                if (IsHexDigit(c))
                    newString += c;
                else
                    discarded++;
            }
            // if odd number of characters, discard last character
            if (newString.Length % 2 != 0)
            {
                discarded++;
                newString = newString.Substring(0, newString.Length - 1);
            }

            int byteLength = newString.Length / 2;
            byte[] bytes = new byte[byteLength];
            string hex;
            int j = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                hex = new String(new Char[] { newString[j], newString[j + 1] });
                bytes[i] = HexToByte(hex);
                j = j + 2;
            }
            return bytes;
        }
        public static string GetHexString(this byte[] bytes)
        {
            string hexString = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                hexString += bytes[i].ToString("X2");
            }
            return hexString;
        }
        /// <summary>
        /// Determines if given string is in proper hexadecimal string format
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static bool InHexFormat(string hexString)
        {
            bool hexFormat = true;

            foreach (char digit in hexString)
            {
                if (!IsHexDigit(digit))
                {
                    hexFormat = false;
                    break;
                }
            }
            return hexFormat;
        }

        /// <summary>
        /// Returns true is c is a hexadecimal digit (A-F, a-f, 0-9)
        /// </summary>
        /// <param name="c">Character to test</param>
        /// <returns>true if hex digit, false if not</returns>
        public static bool IsHexDigit(Char c)
        {
            int numChar;
            int numA = Convert.ToInt32('A');
            int num1 = Convert.ToInt32('0');
            c = Char.ToUpper(c);
            numChar = Convert.ToInt32(c);
            if (numChar >= numA && numChar < (numA + 6))
                return true;
            if (numChar >= num1 && numChar < (num1 + 10))
                return true;
            return false;
        }
        /// <summary>
        /// Converts 1 or 2 character string into equivalant byte value
        /// </summary>
        /// <param name="hex">1 or 2 character string</param>
        /// <returns>byte</returns>
        private static byte HexToByte(string hex)
        {
            if (hex.Length > 2 || hex.Length <= 0)
                throw new ArgumentException("hex must be 1 or 2 characters in length");
            byte newByte = byte.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            return newByte;
        }
        /// <summary>
        /// Konvertiert "normalen" String zu Secure String
        /// </summary>
        /// <param name="input">"normaler" String</param>
        /// <returns>SecureString</returns>
        public static SecureString ToSecureString(this string input)
        {
            SecureString secure = new SecureString();
            foreach (char c in input)
            {
                secure.AppendChar(c);
            }
            secure.MakeReadOnly();
            return secure;
        }

        /// <summary>
        /// Konvertiert einen SecureString in einen "normalen" String
        /// </summary>
        /// <param name="input">SecureString</param>
        /// <returns>"Normaler" String</returns>
        public static string ToInsecureString(this SecureString input)
        {
            if (input == null)
            {
                return null;
            }
            string returnValue = string.Empty;
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);
            try
            {
                returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
            return returnValue;
        }

        public static string EncryptString(this string input, byte[] entropy)
        {
            SecureString secStr = input.ToSecureString();
            string encStr = secStr.EncryptSecureString(entropy);
            return encStr;
        }

        // static byte[] entropy = System.Text.Encoding.Unicode.GetBytes("Salt Is Not A Password");
        /// <summary>
        /// Verschlüsselt den angegebenen String
        /// </summary>
        /// <param name="input">Inputstring, e.g. Passwort</param>
        /// <returns>Base64 kodierten verschlüsselten Input String</returns>
        public static string EncryptSecureString(this SecureString input, byte[] entropy)
        {
            byte[] encryptedData = System.Security.Cryptography.ProtectedData.Protect(
                System.Text.Encoding.Unicode.GetBytes(ToInsecureString(input)),
                entropy,
                System.Security.Cryptography.DataProtectionScope.LocalMachine);
            return Convert.ToBase64String(encryptedData);
        }

        public static string DecryptString(this string input, byte[] entropy)
        {
            SecureString secStr = input.DecryptSecureString(entropy);
            string insec = secStr.ToInsecureString();
            return insec;
        }

        /// <summary>
        /// Entschlüsselt den angegebenen String
        /// </summary>
        /// <param name="encryptedData">Mit EncryptString verschlüsselter String</param>
        /// <returns>Entschlüsselten SecureString</returns>
        public static SecureString DecryptSecureString(this string encryptedData, byte[] entropy)
        {
            try
            {
                byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
            Convert.FromBase64String(encryptedData),
            entropy,
            System.Security.Cryptography.DataProtectionScope.LocalMachine);
                return ToSecureString(System.Text.Encoding.Unicode.GetString(decryptedData));
            }
            catch (Exception ex)
            {
               Log.Error(ex,CallerInfo.Create(),"Decrypt failed.");
                return null;
            }
        }
    }
}
