using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMALI
{
    public partial class VALIDATE : System.Web.UI.Page
    {
        /// <summary>
        /// 验证通知程序签名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["trade_sign"]) && !string.IsNullOrEmpty(Request.QueryString["app_sign"]) && !string.IsNullOrEmpty(Request.QueryString["trade_no"]))
                {
                    string[] ms = JMP.TOOL.DESEncrypt.Decrypt(Request.QueryString["trade_sign"].ToString(), "hyx").Split(',');

                    if (ms.Length == 3)
                    {
                        if (Convert.ToDateTime(ms[0]) > System.DateTime.Now.AddMinutes(-3))
                        {
                            if (ms[1] == Request.QueryString["trade_no"].ToString() && ms[2] == Request.QueryString["app_sign"].ToString())
                            {
                                Response.Write("SUCCESS");
                            }
                            else
                            {
                                Response.Write("FAIL");
                            }
                        }
                        else
                        {
                            Response.Write("FAIL");
                        }
                    }
                    else
                    {
                        Response.Write("FAIL");
                    }
                }
                else
                {
                    Response.Write("FAIL");
                }
            }
            catch { Response.Write("FAIL"); }
        }
    }
}