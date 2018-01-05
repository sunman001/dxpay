using JMP.BLL;
using JMP.TOOL;
using PayForAnother.Logger;
using PayForAnother.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace PayForAnother.ChinPay
{
    public class HandelChinPay
    {
        private static readonly ILogWriter Logger = LogWriterManager.GetPayForAnotherLogger;
        /// <summary>
        /// 处理代付接口
        /// </summary>
        /// <param name="batchnumber"></param>
        /// <returns></returns>
        public bool HandleChinaPay(HandelPay handelPay)
        {
            bool flag = false;

            JMP.BLL.jmp_BankPlaymoney bll = new JMP.BLL.jmp_BankPlaymoney();

            if (handelPay.batchnumber.Contains(","))
            {
                #region 批量循环提交

                string[] strarr = handelPay.batchnumber.Split(',');

                foreach (var item in strarr)
                {
                    if (item != "0")
                    {
                        handelPay.batchnumber = item;
                        try
                        {
                            flag = SubmitChinaPay(handelPay);
                        }
                        catch (Exception ex)
                        {
                            //修改银行打款对接表相关信息
                            flag = bll.UpdateBankPayTradestate(handelPay.batchnumber, 3, "");
                            if (!flag)
                            {
                                Logger.PayForAnotherLog("修改银行打款对接表状态失败", "批次号：" + handelPay.batchnumber + "交易状态:" + 3);
                            }

                            Logger.PayForAnotherLog("代付异常", "批次号：" + handelPay.batchnumber + "交易状态：" + 3 + "异常信息：" + ex.ToString());

                            throw;
                        }
                    }
                }

                #endregion
            }
            else
            {

                #region 单笔

                try
                {
                    flag = SubmitChinaPay(handelPay);
                }
                catch (Exception ex)
                {

                    //修改银行打款对接表相关信息
                    flag = bll.UpdateBankPayTradestate(handelPay.batchnumber, 3, "");
                    if (!flag)
                    {
                        Logger.PayForAnotherLog("修改银行打款对接表状态失败", "批次号：" + handelPay.batchnumber + "交易状态:" + 3);
                    }

                    Logger.PayForAnotherLog("代付异常", "批次号：" + handelPay.batchnumber + "交易状态：" + 3 + "异常信息：" + ex.ToString());

                    throw;
                }

                #endregion
            }


            return flag;
        }

        public bool SubmitChinaPay(HandelPay handelPay)
        {

            bool flag = false;
            JMP.BLL.jmp_userbank userBankBll = new jmp_userbank();
            JMP.MDL.jmp_userbank userBankMode = new JMP.MDL.jmp_userbank();
            JMP.BLL.jmp_BankPlaymoney bll = new JMP.BLL.jmp_BankPlaymoney();

            Random r = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));

            //商户流水号
            string merSeqId = DateTime.Now.ToString("yyMMddHHmmss") + r.Next(1111, 9999).ToString();
            //查询提现银行卡信息
            userBankMode = userBankBll.SelectUserBank_Paymoney(handelPay.batchnumber);
            //金额分
            string money = Math.Floor(userBankMode.b_money * 100).ToString();
            //用途备注
            string purpose = "商户提款";

            //备注
            string remark = "操作人：" + UserInfo.UserName + ",于时间：" + DateTime.Now + ",对批次号：" + handelPay.batchnumber + ",进行代付自动打款，";
            //修改交易编号
            flag = bll.UpdateBankPayNumber(handelPay.batchnumber, merSeqId, 2, remark);

            if (flag)
            {
                ChinaPayParameter payParameter = new ChinaPayParameter
                {
                    merId = handelPay.MerchantNumber,
                    merSeqId = merSeqId,
                    cardNo = userBankMode.u_banknumber,
                    usrName = userBankMode.u_name,
                    transAmt = money,
                    prov = userBankMode.u_province,
                    city = userBankMode.u_area,
                    openBank = userBankMode.u_bankname,
                    subBank = userBankMode.u_openbankname,
                    purpose = purpose,
                    priKeyPath = handelPay.PrivateKey,
                    flags = userBankMode.u_flag
                };
                //调用代付
                string respStr = ChinaPayForAnother.ChinaPay(payParameter);

                if (!string.IsNullOrEmpty(respStr))
                {
                    #region 代付有返回信息时

                    //转换成键值集合
                    System.Collections.Specialized.NameValueCollection nvclist = HttpUtility.ParseQueryString(respStr);

                    if (nvclist.Count > 0)
                    {
                        //验签方法
                        if (ChinaPayForAnother.VerificationSign(nvclist, handelPay.PublicKey))
                        {
                            #region

                            //验证请求应答码
                            if (ChinaPayForAnother.VerificationResponseCode(nvclist))
                            {
                                int state = ChinaPayForAnother.VerificationStat(nvclist, respStr);

                                #region 交易状态码（1：交易成功，-1：交易失败，2：处理中）

                                switch (state)
                                {
                                    case 1:

                                        //获取ChinaPay系统内部流水
                                        string cpSeqId = ChinaPayForAnother.VerificationCpSeqId(nvclist, respStr);
                                        //修改银行打款对接表相关信息
                                        flag = bll.UpdateBankPayHD(handelPay.batchnumber, 1, merSeqId, cpSeqId, DateTime.Now);
                                        if (!flag)
                                        {
                                            Logger.PayForAnotherLog("修改银行打款对接表状态失败", "批次号：" + handelPay.batchnumber + "交易状态:" + 1 + "代付返回参数：" + respStr);
                                        }
                                        break;
                                    case 2:

                                        //修改银行打款对接表相关信息
                                        flag = bll.UpdateBankPayHD(handelPay.batchnumber, 2, merSeqId, "", DateTime.Now);

                                        if (!flag)
                                        {
                                            Logger.PayForAnotherLog("修改银行打款对接表状态失败", "批次号：" + handelPay.batchnumber + "交易状态:" + 2 + "代付返回参数：" + respStr);
                                        }
                                        break;
                                    case -1:
                                        //修改银行打款对接表相关信息
                                        flag = bll.UpdateBankPayHD(handelPay.batchnumber, -1, merSeqId, "", DateTime.Now);
                                        if (!flag)
                                        {
                                            Logger.PayForAnotherLog("修改银行打款对接表状态失败", "批次号：" + handelPay.batchnumber + "交易状态:" + -1 + "代付返回参数：" + respStr);
                                        }
                                        Logger.PayForAnotherLog("代付提现失败返回参数", "批次号：" + handelPay.batchnumber + "代付返回参数：" + respStr);

                                        break;
                                }

                                #endregion
                            }
                            else
                            {

                                //响应码不为0000时，记录失败日志
                                flag = bll.UpdateBankPayHD(handelPay.batchnumber, -1, merSeqId, "", DateTime.Now);
                                if (!flag)
                                {
                                    Logger.PayForAnotherLog("修改银行打款对接表状态失败", "批次号：" + handelPay.batchnumber + "交易状态:" + 2 + "代付返回参数：" + respStr);
                                }

                                Logger.PayForAnotherLog("代付提现失败返回参数", "批次号：" + handelPay.batchnumber + "代付返回参数：" + respStr);
                            }
                            #endregion
                        }
                        else
                        {
                            //修改银行打款对接表相关信息
                            flag = bll.UpdateBankPayHD(handelPay.batchnumber, -1, merSeqId, "", DateTime.Now);

                            Logger.PayForAnotherLog("代付提现回传参数验证签名失败", "批次号：" + handelPay.batchnumber + "代付返回参数：" + respStr);

                            if (!flag)
                            {
                                Logger.PayForAnotherLog("修改银行打款对接表状态失败", "批次号：" + handelPay.batchnumber + "交易状态:" + -1 + "代付返回参数：" + respStr);

                            }
                        }
                    }
                    else
                    {
                        //修改银行打款对接表相关信息
                        flag = bll.UpdateBankPayHD(handelPay.batchnumber, 3, merSeqId, "", DateTime.Now);

                        if (!flag)
                        {
                            Logger.PayForAnotherLog("修改银行打款对接表状态失败", "批次号：" + handelPay.batchnumber + "交易状态:" + -1 + "代付返回参数：" + respStr);

                        }

                        Logger.PayForAnotherLog("代付返回参数转换键值集合出错", "批次号：" + handelPay.batchnumber + "代付返回参数：" + respStr);
                    }

                    #endregion
                }
                else
                {
                    #region 代付异常时调用一次查询接口

                    SelectChinaPayParameter PayParameter = new SelectChinaPayParameter
                    {
                        merSeqId = merSeqId,
                        merDate = DateTime.Now.ToString("yyyyMMdd"),
                        pid = 0,
                        p_PublicKey = handelPay.PublicKey,
                        merId = handelPay.MerchantNumber,
                        PrivateKey = handelPay.PrivateKey
                    };
                    SelectChinaPay ChinaPay = new SelectChinaPay();

                    flag = ChinaPay.SubmitSelectChinaPay(PayParameter);

                    #endregion
                }
            }
            else
            {
                Logger.PayForAnotherLog("修改[jmp_BankPlaymoney]表交易编号失败（重复提交也会报这个错误）", "批次号：" + handelPay.batchnumber + "交易编号:" + merSeqId);
            }
            return flag;
        }
    }
}
