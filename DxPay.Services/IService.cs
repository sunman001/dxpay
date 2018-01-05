using System.Collections.Generic;
using DxPay.Infrastructure;

namespace DxPay.Services
{
    /// <summary>
    /// 数据服务层泛型接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IService<T> where T : class
    {
        /// <summary>
        /// 查询全部(泛型)
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> FindAll();

        IPagedList<T> FindPagedList(string orderBy, string @where="",object parameters =null, int pageIndex = 0, int pageSize = 20);

        T FindById(int id);

        /// <summary>
        /// 写入数据(泛型)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert(T entity);

        /// <summary>
        /// 更新数据(泛型)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update(T entity);

        /// <summary>
        /// 删除数据(泛型)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);

        bool Delete(string ids);
    }
}
