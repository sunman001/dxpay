namespace Pay.DinPay
{
    public class Demo
    {
        public void Run()
        {
            //调用示例
            var formField = new FormField("2110020007", "1000201555", "0.01", "", "",
                            "http://test.dunxingpay.com/DinpayToMer_notify.aspx", "",
                            "2016-09-23 13:55:00", "图书",
                            new FormProperty(""))
            { ReturnUrl = "http://test.dunxingpay.com/callback.aspx" };
            //商家私钥
            const string merchantPrivateKey = "";
            //实例化HTML构造器
            var htmlCreator = new HtmlCreator(formField, new PrivateKeySignRsas(merchantPrivateKey));
            //生成表单字符串
            var htmlForm = htmlCreator.CreateHtmlForm();
        }
    }
}