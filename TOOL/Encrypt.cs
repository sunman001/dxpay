using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace JMP.TOOL
{
    /// <summary>
    /// 加密/解密类。
    /// </summary>
    public class Encrypt
    {
        public Encrypt() { }

        #region ========加密========

        /// <summary>
        /// 默认加密
        /// </summary>
        /// <param name="Text">加密参数</param>
        /// <returns></returns>
        public static string IndexEncrypt(string Text)
        {
            string KEY = "HUYUXI_@_&_$_jumipay";
            return DESEncrypt(AESEncrypt(Text, KEY), KEY);
        }

        /// <summary>
        /// 带秘钥加密
        /// </summary>
        /// <param name="Text">加密参数</param>
        /// <param name="KEY">加密秘钥</param>
        /// <returns></returns>
        public static string IndexEncrypts(string Text, string KEY)
        {
            return DESEncrypt(AESEncrypt(Text, KEY), KEY);
        }
        static string MD5Hash(string input)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            if (sb.Length < 9) { sb.Append("GH,3HN5.5K87$&*gf152VBNGHN"); }
            return sb.ToString();
        }

        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        static string DESEncrypt(string Text, string sKey)
        {

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(MD5Hash(sKey).Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(MD5Hash(sKey).Substring(8, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        /// <summary>  
        /// AES加密 
        /// </summary>  
        /// <param name="plainBytes">被加密的明文</param>  
        /// <param name="key">密钥</param>  
        /// <returns>密文</returns>  
        static string AESEncrypt(String Data, String Key)
        {
            MemoryStream mStream = new MemoryStream();
            RijndaelManaged aes = new RijndaelManaged();

            byte[] plainBytes = Encoding.UTF8.GetBytes(Data);
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 128;
            aes.Key = ASCIIEncoding.ASCII.GetBytes(MD5Hash(Key).Substring(4, 24));
            CryptoStream cryptoStream = new CryptoStream(mStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
            try
            {
                cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                cryptoStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            finally
            {
                cryptoStream.Close();
                mStream.Close();
                aes.Clear();
            }
        }

        #endregion

        #region ========解密========


        /// <summary>
        /// 默认解密
        /// </summary>
        /// <param name="Text">解密参数</param>
        /// <returns></returns>
        public static string IndexDecrypt(string Text)
        {
            string KEY = "HUYUXI_@_&_$_jumipay";
            return AESDecrypt(DESDecrypt(Text, KEY), KEY);
        }
        /// <summary>
        /// 带参数解密
        /// </summary>
        /// <param name="Text">解密参数</param>
        /// <param name="KEY">秘钥</param>
        /// <returns></returns>
        public static string IndexDecrypts(string Text, string KEY)
        {
            return AESDecrypt(DESDecrypt(Text, KEY), KEY);
        }
        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        static string DESDecrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(MD5Hash(sKey).Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(MD5Hash(sKey).Substring(8, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }





        /// <summary>  
        /// AES解密  
        /// </summary>  
        /// <param name="encryptedBytes">被加密的明文</param>  
        /// <param name="key">密钥</param>  
        /// <returns>明文</returns>  
        static string AESDecrypt(String Data, String Key)
        {
            Byte[] encryptedBytes = Convert.FromBase64String(Data);
            MemoryStream mStream = new MemoryStream(encryptedBytes);
            //mStream.Write( encryptedBytes, 0, encryptedBytes.Length );  
            //mStream.Seek( 0, SeekOrigin.Begin );  
            RijndaelManaged aes = new RijndaelManaged();
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 128;
            aes.Key = ASCIIEncoding.ASCII.GetBytes(MD5Hash(Key).Substring(4, 24));
            CryptoStream cryptoStream = new CryptoStream(mStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
            try
            {
                byte[] tmp = new byte[encryptedBytes.Length + 32];
                int len = cryptoStream.Read(tmp, 0, encryptedBytes.Length + 32);
                byte[] ret = new byte[len];
                Array.Copy(tmp, 0, ret, 0, len);
                return Encoding.UTF8.GetString(ret);
            }
            finally
            {
                cryptoStream.Close();
                mStream.Close();
                aes.Clear();
            }
        }

        #endregion


    }
}
