using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMALI
{
    public partial class GOTO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["oid"] != null)
            {
                try
                {
                    int oid = int.Parse(Request.QueryString["oid"].ToString());
                    JMP.BLL.jmp_order order = new JMP.BLL.jmp_order();
                    JMP.MDL.jmp_order morder = new JMP.MDL.jmp_order();
                    string TableName = "jmp_order";
                    morder = order.GetModelbyid(oid, TableName);
                    if (morder != null)
                    {
                        if (string.IsNullOrEmpty(morder.o_showaddress))
                        {
                            morder.o_showaddress = ConfigurationManager.AppSettings["succeed"].ToString();
                        }
                        string url = morder.o_showaddress.Contains("?") ? morder.o_showaddress + "&trade_no=" + morder.o_bizcode + "&trade_status=TRADE_SUCCESS" : morder.o_showaddress + "?trade_no=" + morder.o_bizcode + "&trade_status=TRADE_SUCCESS";
                        Response.Redirect(url);
                    }
                    else
                    {
                        Response.Write("非法访问");
                    }
                }
                catch
                {
                    Response.Write("非法访问");
                }
            }
            else
            {
                Response.Write("非法访问");
            }
        }

    }
}