using JMP.MDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JmPayParameter
{
    /// <summary>
    /// 应用的查询方法
    /// </summary>
    public class SelectAPP
    {

        /// <summary>
        /// 根据应用id查询应用
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="CacheTime">缓存时间从配置文件中读取的</param>
        /// <returns></returns>
        public  jmp_app SelectAppId(int appid, int CacheTime)
        {
            jmp_app app = new jmp_app();
            JMP.BLL.jmp_app bll = new JMP.BLL.jmp_app();
            string Cachekey = "Cachekey" + appid;
            if (JMP.TOOL.CacheHelper.IsCache(Cachekey))//判读是否存在缓存
            {
                app = JMP.TOOL.CacheHelper.GetCaChe<jmp_app>(Cachekey);//获取缓存
                if (app == null)
                {
                    app = bll.SelectAppIdStat(appid);
                    if (app != null)
                    {
                        JMP.TOOL.CacheHelper.CacheObjectLocak<jmp_app>(app, Cachekey, CacheTime);//存入缓存
                    }
                }
            }
            else
            {
                app = bll.SelectAppIdStat(appid);
                if (app != null)
                {
                    JMP.TOOL.CacheHelper.CacheObjectLocak<jmp_app>(app, Cachekey, CacheTime);//存入缓存

                }
            }
            return app;
        }
    }
}
