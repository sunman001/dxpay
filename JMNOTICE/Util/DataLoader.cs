using System;
using System.Collections.Generic;
using JMNOTICE.Models;
using JMP.DBA;

namespace JMNOTICE.Util
{
    /// <summary>
    /// 订单通知数据加载器接口实现
    /// </summary>
    public class DataLoader :IDataLoader<OrderNotifyQueueListModel>
    {
        /// <summary>
        /// 加载指定通知次数的订单到队列
        /// </summary>
        /// <param name="notifyTimes">要加载的订单通知次数</param>
        /// <param name="topCount">每次从数据加载的记录行数</param>
        public Queue<OrderNotifyQueueListModel> LoadSpecifiedTimesData(int notifyTimes,int topCount)
        {
            try
            {
                var dt =
                    DbHelperSQL.Query(string.Format("EXEC updatequeue @times={0},@top={1}", notifyTimes, topCount))
                        .Tables[0];
                var list = DbHelperSQL.ConvertToList<OrderNotifyQueueListModel>(dt);
                return new Queue<OrderNotifyQueueListModel>(list);
            }
            catch (Exception ex)
            {
                throw ex;
                //return new Queue<OrderNotifyQueueListModel>(new List<OrderNotifyQueueListModel>());
            }
            
        }
    }
}
