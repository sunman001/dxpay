using JMP.TOOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WxPayAPI;

namespace JMALI
{
    public partial class PAYNOTICE : System.Web.UI.Page
    {
        /// <summary>
        /// 微信官网通知接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ResultNotify resultNotify = new ResultNotify(this);
                int pid = int.Parse(Request.QueryString["pid"].ToString());
                resultNotify.ProcessNotify(pid);
            }
            catch (Exception ex)
            {

                Response.Write("非法访问");
            }

        }
    }
}