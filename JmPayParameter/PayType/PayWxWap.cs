using JmPayParameter.PayChannel;
using JmPayParameter.PayTypeFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JmPayParameter.PayType
{
    /// <summary>
    /// 微信读取通道入口
    /// </summary>
    public class PayWxWap : PayTypeAbstract
    {
        /// <summary>
        /// 根据参数,读取通道信息
        /// </summary>
        /// <param name="paymode">关联平台（1：安卓，2：ios，3:H5）</param>
        /// <param name="arid">风空配置表id</param>
        /// <param name="CacheTime">缓存时间</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>

        public override PayChannelAbstract LoadChannel(int paymode, int arid, int CacheTime, int appid)
        {

            PayChannel payc = new PayChannel();
            payc.PassName = SelectPayChannel.SelectPass(2, paymode, arid, CacheTime, appid);
            return payc;
        }
        public class PayChannel : PayChannelAbstract
        {

        }
    }
}
