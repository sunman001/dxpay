
/************聚米支付平台__数据加密类************/
//描述：根据传入的文本进行相对应的加解密
//功能：加密解密
//开发者：胡玉溪
//开发时间: 2016.03.03
/************聚米支付平台__数据加密类************/

using System;
using System.Security.Cryptography;
using System.Text;

namespace JMP.TOOL
{
    /// <summary>
    /// DES加密/解密类。
    /// </summary>
    public class DESEncrypt
    {
        public DESEncrypt()
        {

        }
        #region ========加密========

        /// <summary>
        /// 默认加密
        /// </summary>
        /// <param name="Text">加密前的文本</param>
        /// <returns>加密后的文本</returns>
        public static string Encrypt(string Text)
        {
            return Encrypt(Text, "jumipay");
        }
        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text">加密前的文本</param> 
        /// <param name="sKey">秘钥</param> 
        /// <returns>加密后的文本</returns> 
        public static string Encrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
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

        #endregion

        #region ========解密========


        /// <summary>
        /// 默认解密
        /// </summary>
        /// <param name="Text">密文</param>
        /// <returns>明文</returns>
        public static string Decrypt(string Text)
        {
            return Decrypt(Text, "jumipay");
        }
        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="Text">密文</param> 
        /// <param name="sKey">秘钥</param> 
        /// <returns>明文</returns> 
        public static string Decrypt(string Text, string sKey)
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
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        #endregion


    }
}
