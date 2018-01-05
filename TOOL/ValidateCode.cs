/************聚米支付平台__生成验证码************/
//描述：
//功能：生成验证码
//开发者：谭玉科
//开发时间: 2016.03.14
/************聚米支付平台__生成验证码************/
using System;
using System.IO;
using System.Web.UI;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Web;

namespace JMP.TOOL
{
    /// <summary>
    /// 类名：ValidateCode
    /// 功能：生成验证码
    /// 详细：生成验证码
    /// 修改日期：2016.03.14
    /// </summary>
    public class ValidateCode
    {
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="CodeCount">验证码长度</param>
        /// <returns></returns>
        public string GetRandomCode(int CodeCount)
        {
            //string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,i,J,K,M,N,P,Q,R,S,T,U,W,X,Y,Z";
            string allChar = "0,1,2,3,4,5,6,7,8,9";
            string[] allCharArray = allChar.Split(',');
            string RandomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < CodeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(temp * i * ((int)DateTime.Now.Ticks));
                }

                //int t = rand.Next(33);
                int t = rand.Next(10);
                while (temp == t)
                {
                    // t = rand.Next(33);
                    t = rand.Next(10);
                }

                temp = t;
                RandomCode += allCharArray[t];
            }

            return RandomCode;
        }

        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="checkCode">验证码</param>
        /// <returns></returns>
        public byte[] CreateValidateGraphic(string checkCode)
        {
            int iwidth = (int)(checkCode.Length * 14);
            Bitmap image = new System.Drawing.Bitmap(iwidth, 20);
            Graphics g = Graphics.FromImage(image);

            try
            {
                Font f = new System.Drawing.Font("Arial ", 10);
                Brush b = new System.Drawing.SolidBrush(Color.Black);
                Brush r = new System.Drawing.SolidBrush(Color.FromArgb(166, 8, 8));

                g.Clear(System.Drawing.ColorTranslator.FromHtml("#929292"));//背景色

                char[] ch = checkCode.ToCharArray();
                for (int i = 0; i < ch.Length; i++)
                {
                    //if (ch[i] >= '0' && ch[i] <= '9')
                    //{
                    g.DrawString(ch[i].ToString(), f, r, 3 + (i * 12), 3);//数字用红色显示
                    //}
                    //else
                    //{
                    //    g.DrawString(ch[i].ToString(), f, b, 3 + (i * 12), 3);//字母用黑色显示
                    //}
                }

                MemoryStream ms = new MemoryStream();
                image.Save(ms, ImageFormat.Jpeg);
                //输出图片流
                return ms.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="checkCode">验证码</param>
        /// <returns></returns>
        public byte[] CreateValidateGraphic(string checkCode, int height = 20)
        {
            int iwidth = (int)(checkCode.Length * 14);
            Bitmap image = new System.Drawing.Bitmap(iwidth, height);
            Graphics g = Graphics.FromImage(image);

            try
            {
                Font f = new System.Drawing.Font("Arial ", 14);
                Brush b = new System.Drawing.SolidBrush(Color.Black);
                Brush r = new System.Drawing.SolidBrush(Color.FromArgb(166, 8, 8));

                g.Clear(System.Drawing.ColorTranslator.FromHtml("#929292"));//背景色

                char[] ch = checkCode.ToCharArray();
                for (int i = 0; i < ch.Length; i++)
                {
                    //if (ch[i] >= '0' && ch[i] <= '9')
                    //{
                    g.DrawString(ch[i].ToString(), f, r, 3 + (i * 12), (float)((height - 14 * 1.5) / 2));//数字用红色显示
                    //}
                    //else
                    //{
                    //    g.DrawString(ch[i].ToString(), f, b, 3 + (i * 12), 3);//字母用黑色显示
                    //}
                }

                MemoryStream ms = new MemoryStream();
                image.Save(ms, ImageFormat.Jpeg);
                //输出图片流
                return ms.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        /// <summary>
        /// 获取随机码
        /// </summary>
        /// <param name="CodeCount">随机码长度</param>
        /// <returns></returns>
        public static string GetRandCode(int CodeCount)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,m,n,p,q,r,s,t,u,w,x,y,z,!,@,#,$,%,&";
            string[] allCharArray = allChar.Split(',');
            string RandomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < CodeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(temp * i * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(allCharArray.Length);
                while (temp == t)
                {
                    t = rand.Next(allCharArray.Length);
                }

                temp = t;
                RandomCode += allCharArray[t];
            }

            return RandomCode;
        }


        #region 生成验证图片
        /// <summary>
        /// 生成验证图片
        /// </summary>
        /// <param name="checkCode">验证字符</param>
        public byte[] CreateImageTwo(string checkCode, int height)
        {
            byte[] imgByte = new byte[0];
            int codeW = (int)((checkCode.Length + 1) * 16); ;
            int codeH = height;
            int fontSize = 20;
            //string chkCode = string.Empty;
            string chkCode = checkCode;
            //颜色列表，用于验证码、噪线、噪点 
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
            //字体列表，用于验证码 
            string font =  "Times New Roman";
            //验证码的字符集，去掉了一些容易混淆的字符 
            //char[] character = { '2', '3', '4', '5', '6', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };

            Random rnd = new Random();
           
            //创建画布
            Bitmap bmp = new Bitmap(codeW, codeH);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //画噪线 
            //for (int i = 0; i < 1; i++)
            //{
            //    int x1 = rnd.Next(codeW);
            //    int y1 = rnd.Next(codeH);
            //    int x2 = rnd.Next(codeW);
            //    int y2 = rnd.Next(codeH);
            //    Color clr = color[rnd.Next(color.Length)];
            //    g.DrawLine(new Pen(clr), x1, y1, x2, y2);
            //}
            //画验证码字符串 
            for (int i = 0; i < chkCode.Length; i++)
            {
                Font ft = new Font(font, fontSize);
                Color clr = color[rnd.Next(color.Length)];
              
                g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 18 + 2, (float)(height - 16) / 2);
            }
            //画噪点 
            for (int i = 0; i < 80; i++)
            {
                int x = rnd.Next(bmp.Width);
                int y = rnd.Next(bmp.Height);
                Color clr = color[rnd.Next(color.Length)];
                bmp.SetPixel(x, y, clr);
            }

            //清除该页输出缓存，设置该页无缓存 
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
            HttpContext.Current.Response.Expires = 0;
            HttpContext.Current.Response.CacheControl = "no-cache";
            HttpContext.Current.Response.AppendHeader("Pragma", "No-Cache");
            //将验证码图片写入内存流，并将其以 "image/Png" 格式输出 
            MemoryStream ms = new MemoryStream();
            try
            {
                bmp.Save(ms, ImageFormat.Png);
                imgByte = ms.ToArray();
            }
            finally
            {
                //显式释放资源 
                bmp.Dispose();
                g.Dispose();
            }
            return imgByte;
        }

        #endregion


    }
}
