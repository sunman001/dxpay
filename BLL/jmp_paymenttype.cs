using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    ///<summary>
    ///支付类型
    ///</summary>
    public partial class jmp_paymenttype
    {

        private readonly JMP.DAL.jmp_paymenttype dal = new JMP.DAL.jmp_paymenttype();
        public jmp_paymenttype()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int p_id)
        {
            return dal.Exists(p_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_paymenttype model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_paymenttype model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int p_id)
        {

            return dal.Delete(p_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string p_idlist)
        {
            return dal.DeleteList(p_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_paymenttype GetModel(int p_id)
        {

            return dal.GetModel(p_id);
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
        public List<JMP.MDL.jmp_paymenttype> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_paymenttype> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_paymenttype> modelList = new List<JMP.MDL.jmp_paymenttype>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_paymenttype model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_paymenttype();
                    if (dt.Rows[n]["p_id"].ToString() != "")
                    {
                        model.p_id = int.Parse(dt.Rows[n]["p_id"].ToString());
                    }
                    model.p_name = dt.Rows[n]["p_name"].ToString();
                    if (dt.Rows[n]["p_type"].ToString() != "")
                    {
                        model.p_type = int.Parse(dt.Rows[n]["p_type"].ToString());
                    }
                    model.p_extend = dt.Rows[n]["p_extend"].ToString();
                    if (dt.Rows[n]["p_priority"].ToString() != "")
                    {
                        model.p_priority = int.Parse(dt.Rows[n]["p_priority"].ToString());
                    }
                    if (dt.Rows[n]["p_forbidden"].ToString() != "")
                    {
                        model.p_id = int.Parse(dt.Rows[n]["p_forbidden"].ToString());
                    }
                    model.p_platform = dt.Rows[n]["p_platform"].ToString();

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
        #endregion

        /// <summary>
        /// 查询支付通道信息
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="Order">排序字段</param>
        /// <param name="pageIndexs">当前页数</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总数量</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_paymenttype> SelectListPage(string sql, string Order, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectListPage(sql, Order, pageIndexs, PageSize, out pageCount);
        }
        /// <summary>
        /// 根据风控配置或者应用id查询一条可用信息
        /// </summary>
        /// <param name="p_type">支付类型</param>
        /// <param name="glpt">关联平台</param>
        /// <param name="apptype">应用id或者风险配置id</param>
        ///  <param name="risk">风控类型（0：风险等级，1：应用配置）</param>
        /// <returns></returns>
        public DataTable SelectModesType(int p_type, int glpt, int apptype, int risk)
        {
            return dal.SelectModesType(p_type, glpt, apptype, risk);
        }
        /// <summary>
        /// 根据应用id和支付类型查询通道池相关联的支付通道
        /// </summary>
        /// <param name="p_type">支付类型</param>
        /// <param name="glpt">关联平台</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        public DataTable SelectInterface(int p_type, int glpt, int appid)
        {
            return dal.SelectInterface(p_type, glpt, appid);
        }
        /// <summary>
        /// 查询微信appid支付通道
        /// </summary>
        /// <param name="p_type">支付类型</param>
        /// <param name="glpt">关联平台</param>
        /// <param name="appid">应用id或者风险配置id</param>
        /// <param name="risk">风控类型（0：风险等级，1：应用配置）</param>
        /// <returns></returns>
        public string SelectWXapp(int p_type, int glpt, int appid, int risk)
        {
            return dal.SelectWXapp(p_type, glpt, appid, risk);
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdatState(string u_idlist, int state)
        {
            return dal.UpdateState(u_idlist, state);
        }


        /// <summary>
        /// 修改通道成本费率
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="costratio">成本费率</param>
        /// <returns></returns>
        public bool UpdateCostRatio(int id, string costratio)
        {
            return dal.UpdateCostRatio(id, costratio);
        }
    }
}