/************聚米支付平台__邮件帮助类************/
//描述：
//功能：发送邮件
//开发者：谭玉科
//开发时间: 2016.04.29
/************聚米支付平台__邮件帮助类************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace JMP.TOOL
{
    /// <summary>
    /// 类名：MailHelper
    /// 功能：发送邮件
    /// 详细：发送邮件
    /// 修改日期：2016.04.29
    /// </summary>
    public class MailHelper
    {
        /// <summary>
        /// 发送方法
        /// </summary>
        /// <param name="from">发送人邮件地址</param>
        /// <param name="fromname">发送人显示名称</param>
        /// <param name="to">发送给谁（邮件地址）</param>
        /// <param name="subject">标题</param>
        /// <param name="body">内容</param>
        /// <param name="username">邮件登录名</param>
        /// <param name="password">邮件密码</param>
        /// <param name="server">邮件服务器 smtp服务器地址</param>
        /// <param   name= "IsHtml "> 是否是HTML格式的邮件 </param>
        /// <returns>send ok</returns>
        public static bool SendMail(string from, string fromname, string to, string subject, string body, string server, string username, string password, bool IsHtml)
        {
            //邮件发送类
            MailMessage mail = new MailMessage();
            try
            {
                //是谁发送的邮件
                mail.From = new MailAddress(from, fromname);
                //发送给谁
                mail.To.Add(to);
                //标题
                mail.Subject = subject;
                //内容编码
                mail.BodyEncoding = Encoding.Default;
                //发送优先级
                mail.Priority = MailPriority.High;
                //邮件内容
                mail.Body = body;
                //是否HTML形式发送
                mail.IsBodyHtml = IsHtml;
                //邮件服务器和端口
                SmtpClient smtp = new SmtpClient(server, 25);
                smtp.UseDefaultCredentials = true;
                //指定发送方式
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //发件人身份验证,否则163   发不了
                smtp.UseDefaultCredentials = true;
                //指定登录名和密码
                smtp.Credentials = new System.Net.NetworkCredential(username, password);
                //超时时间
                smtp.EnableSsl = false;
                smtp.Timeout = 10000;
                smtp.Send(mail);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                mail.Dispose();
            }
        }

        //读取指定URL地址的HTML，用来以后发送网页用
        public static string ScreenScrapeHtml(string url)
        {
            //读取stream并且对于中文页面防止乱码
            StreamReader reader = new StreamReader(System.Net.WebRequest.Create(url).GetResponse().GetResponseStream(), System.Text.Encoding.UTF8);
            string str = reader.ReadToEnd();
            reader.Close();
            return str;
        }

        //发送plaintxt
        public static bool SendText(string from, string fromname, string to, string subject, string body, string server, string username, string password)
        {
            return SendMail(from, fromname, to, subject, body, server, username, password, false);
        }

        //发送HTML内容
        public static bool SendHtml(string from, string fromname, string to, string subject, string body, string server, string username, string password)
        {
            return SendMail(from, fromname, to, subject, body, server, username, password, true);
        }

        //发送制定网页
        public static bool SendWebUrl(string from, string fromname, string to, string subject, string server, string username, string password, string url)
        {
            //发送制定网页
            return SendHtml(from, fromname, to, subject, ScreenScrapeHtml(url), server, username, password);

        }

    }
}
