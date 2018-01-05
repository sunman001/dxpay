using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

namespace TOOL.Message.TextMessage.ChuangLan
{
    public class Util
    {
        public static string PostUrl = ConfigurationManager.AppSettings["CHUANGLAN.SEND"];
        public ResponseModel DoRequest(ChuangLanRequest sender)
        {
            var responseModel = new ResponseModel();
            try
            {
                var postStrTpl = "account={0}&pswd={1}&mobile={2}&msg={3}&needstatus=true&extno={4}";

                var encoding = new UTF8Encoding();
                var postData =
                    encoding.GetBytes(string.Format(postStrTpl, sender.Account, sender.Password, sender.Mobile,
                        sender.Content, sender.ExtNo));

                var myRequest = (HttpWebRequest)WebRequest.Create(PostUrl);
                myRequest.Method = "POST";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentLength = postData.Length;

                var newStream = myRequest.GetRequestStream();
                // Send the data.
                newStream.Write(postData, 0, postData.Length);
                newStream.Flush();
                newStream.Close();

                var myResponse = (HttpWebResponse)myRequest.GetResponse();
                responseModel.Status = myResponse.StatusCode;
                if (myResponse.StatusCode == HttpStatusCode.OK)
                {
                    var reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                    responseModel.Content = reader.ReadToEnd();
                    responseModel.Success = true;
                }
                else
                {
                    //访问失败
                }
                return responseModel;
            }
            catch (Exception ex)
            {
                responseModel.ErrorMessage = ex.ToString();
                return responseModel;
            }
        }

    }
}
