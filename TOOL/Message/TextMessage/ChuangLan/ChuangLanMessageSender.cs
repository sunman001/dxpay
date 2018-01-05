using System;

namespace TOOL.Message.TextMessage.ChuangLan
{
    /// <summary>
    /// 创蓝短信发送类
    /// </summary>
    public class ChuangLanMessageSender : IMessageSender
    {
        private readonly ChuangLanRequest _chuangLanRequest;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="chuangLanRequest">创蓝请求参数实体类</param>
        public ChuangLanMessageSender(ChuangLanRequest chuangLanRequest)
        {
            _chuangLanRequest = chuangLanRequest;
        }
        /// <summary>
        /// 向创蓝发送[发送短信]的请求
        /// </summary>
        /// <returns></returns>
        public bool Send()
        {
            var util = new Util();
            var response = util.DoRequest(_chuangLanRequest);
            //解析创蓝返回的消息
            try
            {
                var model = Parser(response);
                if (!model.Success)
                {
                    throw new Exception(string.Format("创蓝短信接口错误,响应时间:{0},状态码:{1},错误信息:{2}", model.ReponseTimeString,
                        model.Code, model.Message));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("创蓝短信接口错误,原因:{0}",ex));
            }
            return response.Success;
        }

        /// <summary>
        /// 解析创蓝返回的消息
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private ChuangLanResult Parser(ResponseModel response)
        {
            var split = response.Content.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var row1 = split[0];
            var row1Split = row1.Split(',');
            var responseTime = row1Split[0];
            var code = row1Split[1];
            var model = new ChuangLanResult
            {
                Code = code,
                Message = ResponseCode.FindConnotationByStatusCode(code),
                ReponseTimeString = responseTime
            };

            return model;
        }
    }
}