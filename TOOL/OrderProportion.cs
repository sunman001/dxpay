using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.TOOL
{
    /// <summary>
    /// 调单比列公共方法
    /// </summary>
    public class OrderProportion
    {
        /// <summary>
        /// 掉单比例
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        public static bool ddbl(int appid, decimal price,int payid)
        {
            bool kl = false;  //是否扣量
            int klbl = 0;              //扣量比例
            if (price > 5)
            {
                string qdhc = "gjyyiddd_" + appid+"_"+payid; //缓存根据应用ID掉单

                //缓存根据应用ID掉单
                if (JMP.TOOL.CacheHelper.IsCache(qdhc))
                {
                    klbl = Convert.ToInt32(JMP.TOOL.CacheHelper.GetCaChe(qdhc));
                }
                else
                {
                    try
                    {

                        object ddjj = new JMP.BLL.jmp_rate().GetRatebyAPPid(appid,payid);
                        if (ddjj == null)
                        {
                            JMP.TOOL.CacheHelper.CacheObject(qdhc, 0, 5);//存入缓存
                        }
                        else
                        {
                            klbl = Convert.ToInt32(Convert.ToDouble(ddjj) * 10000) + 1;
                            JMP.TOOL.CacheHelper.CacheObject(qdhc, klbl, 5);//存入缓存
                        }
                    }
                    catch
                    {
                        JMP.TOOL.CacheHelper.CacheObject(qdhc, 0, 5);//存入缓存
                    }
                }
            }

            //掉单计算
            if (new Random().Next(1, 10001) < klbl)
            {

                kl = true;

            }

            return kl;
        }
    }
}
