using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JmPayParameter.PayTypeFactory
{
    /// <summary>
    /// 抽象接口
    /// </summary>
    public abstract class PayTypeAbstract : IChannelLoader
    {
        public InnerResponse Response { get; set; }
        /// <summary>
        /// 根据参数,读取通道信息(非appid方式)
        /// </summary>
        /// <param name="paymode">关联平台（1：安卓，2：ios，3:H5）</param>
        /// <param name="arid">风控配置id</param>
        /// <param name="CacheTime">缓存时间</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        public abstract PayChannelAbstract LoadChannel(int paymode, int arid, int CacheTime,int appid);
    }
}
