using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JMP.DAL;
using JMP.MDL;

namespace JMP.BLL
{
    public partial class CoCooperationApplication
    {
        private readonly JMP.DAL.CoCooperationApplication dal = new JMP.DAL.CoCooperationApplication();
        public CoCooperationApplication()
        { }

       
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.CoCooperationApplication model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.CoCooperationApplication model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            return dal.Delete(Id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            return dal.DeleteList(Idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.CoCooperationApplication GetModel(int Id)
        {

            return dal.GetModel(Id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 查询未抢代理商信息
        /// </summary>
        /// <param name="status">工单状态</param>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="SelectState">状态</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.CoCooperationApplication> SelectList(string status,  string sea_name, int type, int searchDesc,  int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(status,sea_name, type, searchDesc, pageIndexs, PageSize, out pageCount);
        }

        /// <summary>
        /// 查询已抢代理商信息
        /// </summary>
        /// <param name="status"></param>
        /// <param name="sea_name"></param>
        /// <param name="type"></param>
        /// <param name="searchDesc"></param>
        /// <param name="pageIndexs"></param>
        /// <param name="PageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<JMP.MDL.CoCooperationApplication> SelectListById( string status, string sea_name, int type, int searchDesc,  int pageIndexs, int PageSize, out int pageCount,int userid)
        {
            return dal.SelectListById(status, sea_name, type, searchDesc, pageIndexs, PageSize, out pageCount,userid);
        }

    /// <summary>
    /// 修改状态
    /// </summary>
    /// <param name="u_idlist">ID</param>
    /// <param name="state">更新状态</param>
    /// <returns></returns>
    public bool UpdateState(int id, string state , DateTime GrabbedDate, int GrabbedById, string  GrabbedByName)
        {
            return dal.UpdateState(id, state, GrabbedDate, GrabbedById, GrabbedByName);
        }

    }
}

