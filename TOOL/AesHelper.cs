/***********聚米支付平台__Aes加解密************/
//描述：Aes加解密
//功能：Aes加解密
//开发者：秦际攀
//开发时间: 2016.03.08
/************聚米支付平台__Aes加解密************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace JMP.TOOL
{
    public class AesHelper
    {
        /// <summary>
        ///  AES 加密
        /// </summary>
        /// <param name="str">需要加密的字符窜</param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        public static string AesEncrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);

            System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = System.Security.Cryptography.CipherMode.ECB,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            };

            System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        ///  AES 解密
        /// </summary>
        /// <param name="str">需要解密的字符串</param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        public static string AesDecrypt(string str, string key)
        {
            Byte[] resultArray = null;
            try
            {
                if (string.IsNullOrEmpty(str)) return null;
                Byte[] toEncryptArray = Convert.FromBase64String(str);

                System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
                {
                    Key = Encoding.UTF8.GetBytes(key),
                    Mode = System.Security.Cryptography.CipherMode.ECB,
                    Padding = System.Security.Cryptography.PaddingMode.PKCS7
                };

                System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateDecryptor();

                resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            }
            catch (Exception)
            {
                return "9999";
                //throw;
            }
            return Encoding.UTF8.GetString(resultArray);
        }
    }
}
