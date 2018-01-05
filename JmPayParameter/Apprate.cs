using JMP.MDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JmPayParameter
{
    public class Apprate
    {
        /// <summary>
        /// 根据应用id和支付类型查询是否设置费率
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="paytype">支付类型</param>
        /// <param name="CacheTime">缓存时间</param>
        /// <returns></returns>
        public bool SelectApprate(int appid, int paytype, int CacheTime)
        {
            bool ISZero = false;
            JMP.BLL.jmp_apprate bll = new JMP.BLL.jmp_apprate();
            string CacheName = "CacheName" + appid + paytype;
            JMP.MDL.jmp_apprate jmp_Apprate = new jmp_apprate();
            if (JMP.TOOL.CacheHelper.IsCache(CacheName))
            {
                jmp_Apprate = JMP.TOOL.CacheHelper.GetCaChe<JMP.MDL.jmp_apprate>(CacheName);
                if (jmp_Apprate != null)
                {
                    if (jmp_Apprate.r_proportion > 0)
                    {
                        ISZero = true;
                    }
                    else
                    {
                        jmp_Apprate = bll.SelectAppidState(appid, paytype);
                        if (jmp_Apprate != null)
                        {
                            if (jmp_Apprate.r_proportion > 0)
                            {
                                ISZero = true;
                            }
                            JMP.TOOL.CacheHelper.CacheObjectLocak<JMP.MDL.jmp_apprate>(jmp_Apprate, CacheName, CacheTime);//存入缓存
                        }
                    }
                }
            }
            else
            {
                jmp_Apprate = bll.SelectAppidState(appid, paytype);
                if (jmp_Apprate != null)
                {
                    if (jmp_Apprate.r_proportion > 0)
                    {
                        ISZero = true;
                    }
                    JMP.TOOL.CacheHelper.CacheObjectLocak<JMP.MDL.jmp_apprate>(jmp_Apprate, CacheName, CacheTime);//存入缓存
                }
            }
            return ISZero;
        }
    }
}
