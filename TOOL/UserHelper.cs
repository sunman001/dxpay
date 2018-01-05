/************聚米支付平台__获取公共资源路径************/
//描述：
//功能：获取公共资源路径
//开发者：谭玉科
//开发时间: 2016.03.14
/************聚米支付平台__获取公共资源路径************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Web;

namespace JMP.TOOL
{
    /// <summary>
    /// 类名：UserHelper
    /// 功能：获取公共资源路径
    /// 详细：获取公共资源路径
    /// 修改日期：2016.03.14
    /// </summary>
    public class UserHelper
    {
        /// <summary>
        /// 资源站点
        /// </summary>
        public static string resSite = ConfigurationSettings.AppSettings["resSite"];

        /// <summary>
        /// 获取admin文件夹中的图片
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static HtmlString AdminImage(string path)
        {
            return new HtmlString("" + resSite + "/images/admin/" + path + "");
        }
        /// <summary>
        /// 获取images文件中的图片
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static HtmlString img(string path)
        {
            return new HtmlString("" + resSite + "/images/" + path + "");
        }
        /// <summary>
        /// 获取层叠样式表路径并添加到页面
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static HtmlString css(string path)
        {
            return new HtmlString("<link href=\"" + resSite + "/css/" + path + "\" rel=\"stylesheet\" type=\"text/css\"/>");
        }
        /// <summary>
        /// 获取Javascript路径并添加到页面
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns> 
        public static HtmlString js(string path)
        {
            return new HtmlString("<script type=\"text/javascript\" src=\"" + resSite + "/js/" + path + "?" + Guid.NewGuid() + "\"></script>");
        }

    }
}
