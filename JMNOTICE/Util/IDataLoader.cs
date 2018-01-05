using System.Collections.Generic;

namespace JMNOTICE.Util
{
    /// <summary>
    /// 订单通知数据加载器接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataLoader<T>
    {
        /// <summary>
        /// 加载指定通知次数的待通知的数据
        /// </summary>
        /// <param name="notifyTimes">通知次数</param>
        /// <param name="topCount">每次从数据加载的记录行数</param>
        /// <returns></returns>
        Queue<T> LoadSpecifiedTimesData(int notifyTimes, int topCount);
    }
}
