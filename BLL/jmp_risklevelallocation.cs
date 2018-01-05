using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    /// <summary>
    /// 风险等级表
    /// </summary>
    public partial class jmp_risklevelallocation
    {

        private readonly JMP.DAL.jmp_risklevelallocation dal = new JMP.DAL.jmp_risklevelallocation();
        public jmp_risklevelallocation()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int r_id)
        {
            return dal.Exists(r_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_risklevelallocation model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_risklevelallocation model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int r_id)
        {

            return dal.Delete(r_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string r_idlist)
        {
            return dal.DeleteList(r_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_risklevelallocation GetModel(int r_id)
        {

            return dal.GetModel(r_id);
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
        public List<JMP.MDL.jmp_risklevelallocation> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_risklevelallocation> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_risklevelallocation> modelList = new List<JMP.MDL.jmp_risklevelallocation>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_risklevelallocation model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_risklevelallocation();
                    if (dt.Rows[n]["r_id"].ToString() != "")
                    {
                        model.r_id = int.Parse(dt.Rows[n]["r_id"].ToString());
                    }
                    if (dt.Rows[n]["r_apptypeid"].ToString() != "")
                    {
                        model.r_apptypeid = int.Parse(dt.Rows[n]["r_apptypeid"].ToString());
                    }
                    if (dt.Rows[n]["r_risklevel"].ToString() != "")
                    {
                        model.r_risklevel = int.Parse(dt.Rows[n]["r_risklevel"].ToString());
                    }
                    if (dt.Rows[n]["r_state"].ToString() != "")
                    {
                        model.r_state = int.Parse(dt.Rows[n]["r_state"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }
        /// <summary>
        /// 列表分页查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="order">排序字段</param>
        /// <param name="pageIndexs">当前页数</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_risklevelallocation> SelectPage(string sql, string order, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectPage(sql, order, pageIndexs, PageSize, out pageCount);
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateState(string u_idlist, int state)
        {
            return dal.UpdateState(u_idlist, state);
        }
        /// <summary>
        /// 根据应用子类型查询对应的应用的风险等级
        /// </summary>
        /// <param name="apptypeid">应用类型子id</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_risklevelallocation> SelectAppType(int apptypeid)
        {
            return dal.SelectAppType(apptypeid);
        }
        /// <summary>
        /// 根据rid字符串查询相关风控配置信息
        /// </summary>
        /// <param name="rid">rid字符串（1,2）</param>
        /// <returns></returns>
        public DataTable SelectRid(string rid)
        {
            return dal.SelectRid(rid);
        }
        #endregion

    }
}