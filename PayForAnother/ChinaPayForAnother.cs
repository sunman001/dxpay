using PayForAnother.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PayForAnother
{
    public class ChinaPayForAnother
    {
        #region ChinaPay 代付

        /// <summary>
        /// 代付
        /// </summary>
        /// <param name="cardNo">收款账号</param>
        /// <param name="usrName">收款人姓名</param>
        /// <param name="transAmt">金额,人民币，单位分,整数</param>
        /// <param name="prov">省份</param>
        /// <param name="city">城市</param>
        /// <param name="openBank">开户银行</param>
        /// <param name="subBank">开户行支行</param>
        /// <param name="purpose">用途</param>
        /// <param name="flags">付款标志</param>
        /// <returns></returns>
        public static string ChinaPay(ChinaPayParameter payParameter)
        {

            //在收付捷平台中开通的商户编号
            //string merId = "808080211305900";
            //商户日期
            string merDate = DateTime.Now.ToString("yyyyMMdd");
            //版本号 
            string version = "20160530";
            //付款标志
            string flag = payParameter.flags;
            //签名标志
            string signFlag = "1";
            //表示商户代付业务使用场景，（业务参数）07：互联网 08：移动端
            string termType = "07";
            //表示商户代付业务交易模式（业务参数）0：被动发起代付 1：主动发起代付
            string payMode = "1";

            //组装字符串准备验签
            string sing = payParameter.merId + merDate + payParameter.merSeqId + payParameter.cardNo + payParameter.usrName + payParameter.openBank + payParameter.prov + payParameter.city + payParameter.transAmt + payParameter.purpose + payParameter.subBank + flag + version + termType + payMode;
            //验签
            string chkValue = Utils.signData(payParameter.merId, sing, payParameter.priKeyPath);
            //签名
            string chkValues = chkValue;

            //转码
            payParameter.usrName = HttpUtility.UrlEncode(payParameter.usrName, System.Text.Encoding.GetEncoding("GBK"));
            payParameter.prov = HttpUtility.UrlEncode(payParameter.prov, System.Text.Encoding.GetEncoding("GBK"));
            payParameter.city = HttpUtility.UrlEncode(payParameter.city, System.Text.Encoding.GetEncoding("GBK"));
            payParameter.openBank = HttpUtility.UrlEncode(payParameter.openBank, System.Text.Encoding.GetEncoding("GBK"));
            payParameter.purpose = HttpUtility.UrlEncode(payParameter.purpose, System.Text.Encoding.GetEncoding("GBK"));
            payParameter.subBank = HttpUtility.UrlEncode(payParameter.subBank, System.Text.Encoding.GetEncoding("GBK"));

            //请求数据组装
            string reqStr = "merId=" + payParameter. merId + "&merDate=" + merDate + "&merSeqId=" + payParameter.merSeqId + "&cardNo=" + payParameter.cardNo + "&usrName=" + payParameter.usrName
                + "&transAmt=" + payParameter.transAmt + "&prov=" + payParameter.prov + "&city=" + payParameter.city + "&version=" + version + "&openBank=" + payParameter. openBank + "&flag=" + flag
                + "&signFlag=" + signFlag + "&purpose=" + payParameter.purpose + "&subBank=" + payParameter.subBank + "&termType=" + termType + "&payMode=" + payMode + "&chkValue=" + chkValues;

            //生产地址
            string url = "http://sfj.chinapay.com/dac/SinPayServletGBK";

            //提交请求
            string respStr = Utils.postData(reqStr, url);

            return respStr;

        }

        /// <summary>
        /// ChinaPay 代付 签名验证
        /// </summary>
        /// <param name="list">键值集合</param>
        /// <returns></returns>
        public static bool VerificationSign(System.Collections.Specialized.NameValueCollection list, string pubKeyPath)
        {
            bool Success = false;
            string urlstr = JMP.TOOL.UrlStr.GetStrNvNotKey(list, "chkValue");
            string result = Utils.checkData(urlstr, list["chkValue"], pubKeyPath);
            if (result != "0")
            {
                Success = false;

            }
            else
            {
                Success = true;
            }
            return Success;
        }


        /// <summary>
        /// 验证请求应答码
        /// </summary>
        /// <param name="list">键值集合</param>
        /// <returns></returns>
        public static bool VerificationResponseCode(System.Collections.Specialized.NameValueCollection list)
        {
            bool Success = false;
            if (list["responseCode"] == "0000")
            {
                Success = true;
            }
            else
            {
                Success = false;
            }

            return Success;
        }

        /// <summary>
        /// 验证交易状态码
        /// </summary>
        /// <param name="list">键值集合</param>
        /// <param name="respStr">代付返回结果url字符串</param>
        /// <returns>返回结果：1：交易成功，-1：交易失败，2：处理中</returns>
        public static int VerificationStat(System.Collections.Specialized.NameValueCollection list, string UrlStr)
        {
            int stat = -1;

            if (UrlStr.Contains("stat"))
            {
                switch (list["stat"])
                {
                    //交易成功
                    case "s":
                        stat = 1;
                        break;
                    //交易已接受
                    case "2":
                        stat = 2;
                        break;
                    //财务已确认
                    case "3":
                        stat = 2;
                        break;
                    //财务处理中
                    case "4":
                        stat = 2;
                        break;
                    //已发往银行
                    case "5":
                        stat = 2;
                        break;
                    //银行已退单
                    case "6":
                        stat = -1;
                        break;
                    //重汇已提交
                    case "7":
                        stat = 2;
                        break;
                    //重汇已发送
                    case "8":
                        stat = 2;
                        break;
                    //重汇已退单
                    case "9":
                        stat = -1;
                        break;
                }

            }
            else
            {
                stat = 2;
            }

            return stat;
        }

        /// <summary>
        /// 验证交易状态码
        /// </summary>
        /// <param name="list">键值集合</param>
        /// <param name="respStr">代付返回结果url字符串</param>
        /// <returns>返回结果：1：交易成功，-1：交易失败，2：处理中</returns>
        public static int VerificationStatCX(string statStr)
        {
            int stat = -1;

            switch (statStr)
            {
                //交易成功
                case "s":
                    stat = 1;
                    break;
                //交易已接受
                case "2":
                    stat = 2;
                    break;
                //财务已确认
                case "3":
                    stat = 2;
                    break;
                //财务处理中
                case "4":
                    stat = 2;
                    break;
                //已发往银行
                case "5":
                    stat = 2;
                    break;
                //银行已退单
                case "6":
                    stat = -1;
                    break;
                //重汇已提交
                case "7":
                    stat = 2;
                    break;
                //重汇已发送
                case "8":
                    stat = 2;
                    break;
                //重汇已退单
                case "9":
                    stat = -1;
                    break;
            }

            return stat;
        }


        /// <summary>
        /// 验证ChinaPay系统内部流水
        /// </summary>
        /// <param name="list">键值集合</param>
        /// <param name="UrlStr">代付返回结果url字符串</param>
        /// <returns></returns>
        public static string VerificationCpSeqId(System.Collections.Specialized.NameValueCollection list, string UrlStr)
        {
            string CpSeqId = "";

            if (UrlStr.Contains("cpSeqId"))
            {
                CpSeqId = list["cpSeqId"];
            }

            return CpSeqId;

        }



        #endregion

        #region 代付单笔查询接口

        /// <summary>
        /// 单笔查询接口
        /// </summary>
        /// <param name="merDate">商户日期</param>
        /// <param name="merSeqId">流水号</param>
        /// <param name="priKeyPath">key</param>
        /// <returns></returns>
        public static string SelectChinaPay(string merDate, string merSeqId, string priKeyPath,string merId)
        {
            //在收付捷平台中开通的商户编号
           // string merId = "808080211305900";
            //版本号
            string version = "20090501";

            //组装字符串准备验签
            string sing = merId + merDate + merSeqId + version;
            //验签
            string chkValue = Utils.signData(merId, sing, priKeyPath);
            //签名
            string chkValues = chkValue;

            //请求数据组装
            string reqStr = "merId=" + merId + "&merDate=" + merDate + "&merSeqId=" + merSeqId + "&version=" + version + "&chkValue=" + chkValue + "&signFlag=1";

            //生产地址
            string url = "http://sfj.chinapay.com/dac/SinPayQueryServletGBK";

            //提交请求
            string respStr = Utils.postData(reqStr, url);

            return respStr;
        }

        #endregion

    }
}
