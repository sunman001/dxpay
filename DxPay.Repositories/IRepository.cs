using System.Collections.Generic;
using DxPay.Infrastructure;

namespace DxPay.Repositories
{
    public interface IRepository<T>
    {
        /// <summary>
        /// 根据ID查询单条数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        T FindById(int id);
        /// <summary>
        /// 查询所有数据(无分页,请慎用)
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> FindAll();
        /// <summary>
        /// 根据条件查询前N条数据
        /// </summary>
        /// <param name="top">前N条数据</param>
        /// <param name="orderBy">排序字段,不允许为空</param>
        /// <param name="where">查询条件</param>
        /// <param name="parameters">查询条件参数对象</param>
        /// <returns></returns>
        IEnumerable<T> FindByClause(int top, string orderBy, string @where = "", object parameters = null);

        /// <summary>
        /// 根据条件查询分页数据
        /// </summary>
        /// <param name="orderBy">排序字段,不允许为空</param>
        /// <param name="where">查询条件</param>
        /// <param name="parameters">查询条件参数对象</param>
        /// <param name="pageIndex">当前页面索引</param>
        /// <param name="pageSize">分布大小</param>
        /// <returns></returns>
        IPagedList<T> FindPagedList(string orderBy, string @where = "", object parameters = null, int pageIndex = 0, int pageSize = 20);


        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        int Insert(T entity);

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update(T entity);

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// 删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool Delete(string ids);

    }
}
