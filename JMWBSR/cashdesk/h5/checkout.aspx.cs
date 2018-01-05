using System;
using System.Linq;
using JMWBSR.Extensions;
using JMWBSR.Models;

namespace JMWBSR.cashdesk.h5
{
    public partial class checkout : System.Web.UI.Page
    {
        public H5ViewModel H5ViewModel;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Request.QueryString["p"] == null)
            {
                Response.Redirect("~/error.aspx");
            }
            var para = Request.QueryString["p"];
            var payParameters = JMP.TOOL.JsonHelper.Deserialize<CheckoutJsonModel>(para);
            var paymodes = payParameters.spotpaytype.Split(',').Select(x => Convert.ToInt32(x)).ToList();
            var postBackPara = RemoveUnnecessaryProperty(para);


            H5ViewModel = new H5ViewModel
            {
                Price = payParameters.price,
                code = payParameters.code,
                goodsname = payParameters.goodsname,
                Parameter = postBackPara
            };
            var lst = PaymentModeLoader.PaymentModeSource.Where(x => paymodes.Any(p => p == x.PayType)).ToList();
            foreach (var t in lst)
            {
                t.Price = H5ViewModel.Price;
                t.Enabled = t.PaymentEnableDetect(IsWx, IsMobile);
            }
            H5ViewModel.PaymentModes = lst.OrderByDescending(x => x.Enabled).ThenBy(x => x.Category).ToList();
        }

        public bool IsWx
        {
            get
            {
                var userAgent = Request.UserAgent;
                return userAgent != null && userAgent.ToLower().Contains("micromessenger");
            }
        }

        private bool IsMobile
        {
            get { return Request.Browser.IsMobileDevice; }
        }

        /// <summary>
        /// 删除JSON中不必要的属性
        /// </summary>
        /// <param name="json">原始JSON字符串</param>
        /// <returns></returns>
        private string RemoveUnnecessaryProperty(string json)
        {
            var postBackParaModel = JMP.TOOL.JsonHelper.Deserialize<CheckoutPostBackJsonModel>(json);
            return JMP.TOOL.JsonHelper.Serialize(postBackParaModel);
        }
    }
}