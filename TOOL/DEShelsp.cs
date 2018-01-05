using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace JMP.TOOL
{
    public class DEShelsp
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Encryption(string key, string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return string.Empty;
            }

            byte[] rgbKey = Encoding.UTF8.GetBytes(key);
            byte[] rgbIV = Encoding.UTF8.GetBytes(key);

            byte[] inputByteArray = Encoding.UTF8.GetBytes(data);
            DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            dCSP.Mode = CipherMode.CBC;
            dCSP.Padding = PaddingMode.PKCS7;

            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();

            var array = mStream.ToArray();
            return string.Join(string.Empty, array.Select(c => c.ToString("X2")).ToArray());
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Decrypt(string key, string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return string.Empty;
            }

            byte[] rgbKey = Encoding.UTF8.GetBytes(key);
            byte[] rgbIV = Encoding.UTF8.GetBytes(key);

            byte[] inputByteArray = HexStringToByteArray(data);
            DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
            desc.Mode = CipherMode.CBC;
            desc.Padding = PaddingMode.PKCS7;
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, desc.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();

            return Encoding.UTF8.GetString(mStream.ToArray());
        }

        private static byte[] HexStringToByteArray(string s)
        {
            s = s.Replace("   ", " ");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }
    }
}