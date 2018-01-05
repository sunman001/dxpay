using System;
using System.Security.Cryptography;
using System.Text;

namespace JMP.TOOL
{
    public class MD5
    {
        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="ConvertString"></param>
        /// <returns></returns>
        public static string GetStrMd5(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)));
            t2 = t2.Replace("-", "");
            return t2;
        }
        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt16(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(password)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }
        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Md5Encrypt32(string str)
        {
            string cl = str;
            string pwd = "";
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                pwd = pwd + s[i].ToString("X");

            }
            return pwd;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="convertString">需要加密的字符串</param>
        /// <param name="isBase32">是否为32位加密</param>
        /// <returns></returns>
        public static string md5strGet(string convertString, bool isBase32 = false)
        {
            var md5 = new MD5CryptoServiceProvider();
            var data = Encoding.UTF8.GetBytes(convertString);
            var encs = md5.ComputeHash(data);
            return isBase32 == true ? BitConverter.ToString(encs).Replace("-", "") : BitConverter.ToString(encs, 4, 8).Replace("-", "");
        }
    }
}
