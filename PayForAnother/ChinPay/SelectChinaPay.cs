using PayForAnother.Logger;
using PayForAnother.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayForAnother.ChinPay
{
    public class SelectChinaPay
    {
        private static readonly ILogWriter Logger = LogWriterManager.GetPayForAnotherLogger;

        /// <summary>
        /// 查询代付信息
        /// </summary>
        /// <param name="payparamter"></param>
        /// <returns></returns>
        public bool SubmitSelectChinaPay(SelectChinaPayParameter payparamter)
        {
            bool flag = false;
            JMP.BLL.jmp_BankPlaymoney bll = new JMP.BLL.jmp_BankPlaymoney();
            try
            {

                string respStr = ChinaPayForAnother.SelectChinaPay(payparamter.merDate, payparamter.merSeqId, payparamter.PrivateKey, payparamter.merId);

                if (!string.IsNullOrEmpty(respStr))
                {
                    //截取数据验签
                    string responseCode = respStr.Substring(0, 3);

                    int dex = respStr.LastIndexOf("|");// dex为chkValue的索引位置
                    string plain = respStr.Substring(0, dex + 1);//获取待验签的字符串
                    string cpChkValue = respStr.Substring(dex + 1);//获取ChinaPay 返回的签名值

                    //验签方法
                    string result = Utils.checkData(plain, cpChkValue, payparamter.p_PublicKey);//验签

                    if (result != "0")
                    {
                        flag = false;

                        Logger.PayForAnotherLog("单笔查询接口验签失败", "交易流水号：" + payparamter.merSeqId + "返回参数：" + respStr);
                    }
                    else
                    {
                        #region 验签成功
                        switch (responseCode)
                        {

                            //成功
                            case "000":

                                string[] s = plain.Split('|');

                                int start = ChinaPayForAnother.VerificationStatCX(s[14]);

                                switch (start)
                                {
                                    case 1:

                                        //修改银行打款对接表相关信息
                                        flag = bll.UpdateBankPayTradeno(1, payparamter.merSeqId, s[5]);
                                        if (!flag)
                                        {
                                            Logger.PayForAnotherLog("修改银行打款对接表状态失败", "交易流水号：" + payparamter.merSeqId + "交易状态:" + 1 + "代付返回参数：" + respStr);
                                        }

                                        break;
                                    case 2:

                                        //修改银行打款对接表相关信息
                                        flag = bll.UpdateBankPayTradeno(2, payparamter.merSeqId, "");

                                        if (!flag)
                                        {
                                            Logger.PayForAnotherLog("修改银行打款对接表状态失败", "交易流水号：" + payparamter.merSeqId + "交易状态:" + 2 + "代付返回参数：" + respStr);
                                        }

                                        break;
                                    case -1:

                                        //修改银行打款对接表相关信息
                                        flag = bll.UpdateBankPayTradeno(-1, payparamter.merSeqId, "");
                                        if (!flag)
                                        {
                                            Logger.PayForAnotherLog("修改银行打款对接表状态失败", "交易流水号：" + payparamter.merSeqId + "交易状态:" + -1 + "代付返回参数：" + respStr);
                                        }

                                        Logger.PayForAnotherLog("代付提现失败返回参数", "交易流水号：" + payparamter.merSeqId + "代付返回参数：" + respStr);

                                        break;
                                }

                                break;
                            //无记录(交易失败)
                            case "001":

                                //修改银行打款对接表相关信息
                                flag = bll.UpdateBankPayTradeno(-1, payparamter.merSeqId, "");

                                Logger.PayForAnotherLog("单笔查询接口查询无记录", "交易流水号：" + payparamter.merSeqId + "返回参数：" + respStr);

                                if (!flag)
                                {
                                    Logger.PayForAnotherLog("单笔查询接口查询无记录", "交易流水号：" + payparamter.merSeqId + "交易状态:" + -1 + "返回参数：" + respStr);

                                }

                                break;
                            //查询出错
                            case "002":

                                Logger.PayForAnotherLog("单笔查询接口查询出错", "交易流水号：" + payparamter.merSeqId + "返回参数：" + respStr);
                                break;
                        }
                        #endregion

                    }
                }
                else
                {
                    //修改银行打款对接表相关信息
                    flag = bll.UpdateBankPayTradeno(3, payparamter.merSeqId, "");
                    if (!flag)
                    {
                        Logger.PayForAnotherLog("修改银行打款对接表状态失败", "交易流水号：" + payparamter.merSeqId + "交易状态:" + 3 + "代付返回参数：" + respStr);
                    }

                    Logger.PayForAnotherLog("查询接口返回数据为空！！！", "交易流水号：" + payparamter.merSeqId + "交易状态:" + 3 + "代付返回参数：" + respStr);
                }

            }
            catch (Exception ex)
            {
                //修改银行打款对接表相关信息
                flag = bll.UpdateBankPayTradeno(3, payparamter.merSeqId, "");
                if (!flag)
                {
                    Logger.PayForAnotherLog("修改银行打款对接表状态失败", "交易流水号：" + payparamter.merSeqId + "交易状态:" + 3);
                }

                Logger.PayForAnotherLog("代付查询接口异常", "异常信息：" + ex.Message);
                throw;
            }

            return flag;
        }
    }
}
