using System;

namespace TOOL.Message.AudioMessage.ChuangLan
{
    /// <summary>
    /// 创蓝语音消息发送类
    /// </summary>
    public class ChuangLanAudioMessageSender : IMessageSender
    {
        private readonly RequestPayload _chuangLanRequest;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="chuangLanRequest">创蓝请求参数实体类</param>
        public ChuangLanAudioMessageSender(RequestPayload chuangLanRequest)
        {
            _chuangLanRequest = chuangLanRequest;
            _response=new ResponseModel();
        }

        private ResponseModel _response;
        /// <summary>
        /// 响应结果对象
        /// </summary>
        public ResponseModel Response { get { return _response; } }

        /// <summary>
        /// 向创蓝发送[发送短信]的请求
        /// </summary>
        /// <returns></returns>
        public bool Send()
        {
            var util = new Util();
            if (_chuangLanRequest == null)
            {
                throw new Exception("创蓝语音消息参数对象为空");
            }
            if (_chuangLanRequest.teltemp <= 0)
            {
                _response.Msg = "创蓝语音消息模板ID不正确";
                _response.Success = false;
                return false;
            }
            _response = util.DoRequest(_chuangLanRequest);
            //解析创蓝返回的消息
            try
            {
                //var model = Parser(response);
                if (!_response.Success)
                {
                    throw new Exception(string.Format("创蓝短信接口错误,状态码:{0},错误信息:{1}",
                        _response.Code, _response.Msg));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("创蓝短信接口错误,原因:{0}", ex));
            }
            return _response.Success;
        }
    }
}