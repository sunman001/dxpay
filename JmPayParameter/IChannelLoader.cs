using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JmPayParameter
{
    /// <summary>
    /// 加载支付通道接口 入口
    /// </summary>
    public interface IChannelLoader
    {
        /// <summary>
        /// 加载通道
        /// </summary>
        PayChannelAbstract LoadChannel(int paymode, int apptype, int CacheTime, int appid);
    }
}
