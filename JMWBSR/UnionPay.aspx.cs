using JMP.TOOL;
using Pay.DinPay;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMWBSR
{
    public partial class UnionPay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params.Count > 0 && (Request.Params["UnionPay"] != null && Request.Params["UnionPay"] != ""))
                {
                    try
                    {
                        string str = "";
                        string encryption =HttpUtility.UrlDecode(Request.Params["UnionPay"].ToString());
                        string h5key = JMP.TOOL.Encrypt.IndexDecrypt(encryption);//解密缓存key
                        if (JMP.TOOL.CacheHelper.IsCache(h5key))
                        {
                            str = JMP.TOOL.CacheHelper.GetCaChe(h5key).ToString();
                            if (!string.IsNullOrEmpty(str))
                            {
                                Response.Write(str);
                            }
                            else
                            {
                                Response.Write("{\"message\":\"支付接口异常\",\"result\":102}");
                            }

                        }
                        else
                        {
                            Response.Write("{\"message\":\"支付接口异常\",\"result\":102}");
                        }
                    }
                    catch(Exception ex )
                    {

                        Response.Write("{\"message\":\"支付接口异常\",\"result\":102}");
                    }

                }
            }
        }
    }
}