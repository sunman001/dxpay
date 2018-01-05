using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.MDL;
namespace JMP.BLL
{
    //应用表
    public partial class jmp_app
    {

        private readonly JMP.DAL.jmp_app dal = new JMP.DAL.jmp_app();
        public jmp_app()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int a_id)
        {
            return dal.Exists(a_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_app model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_app model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int a_id)
        {

            return dal.Delete(a_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string a_idlist)
        {
            return dal.DeleteList(a_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_app GetModel(int a_id)
        {

            return dal.GetModel(a_id);
        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 根据sql查询信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable selectsql(string sql)
        {
            return dal.selectsql(sql);
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
        public List<JMP.MDL.jmp_app> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_app> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_app> modelList = new List<JMP.MDL.jmp_app>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_app model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_app();
                    if (dt.Rows[n]["a_id"].ToString() != "")
                    {
                        model.a_id = int.Parse(dt.Rows[n]["a_id"].ToString());
                    }
                    if (dt.Rows[n]["a_auditstate"].ToString() != "")
                    {
                        model.a_auditstate = int.Parse(dt.Rows[n]["a_auditstate"].ToString());
                    }
                    model.a_secretkey = dt.Rows[n]["a_secretkey"].ToString();
                    if (dt.Rows[n]["a_time"].ToString() != "")
                    {
                        model.a_time = DateTime.Parse(dt.Rows[n]["a_time"].ToString());
                    }
                    model.a_showurl = dt.Rows[n]["a_showurl"].ToString();
                    model.a_auditor = dt.Rows[n]["a_auditor"].ToString();
                    if (dt.Rows[n]["a_rid"].ToString() != "")
                    {
                        model.a_rid = int.Parse(dt.Rows[n]["a_rid"].ToString());
                    }
                    model.a_appurl = dt.Rows[n]["a_appurl"].ToString();
                    model.a_appsynopsis = dt.Rows[n]["a_appsynopsis"].ToString();
                    if (dt.Rows[n]["a_user_id"].ToString() != "")
                    {
                        model.a_user_id = int.Parse(dt.Rows[n]["a_user_id"].ToString());
                    }
                    model.a_name = dt.Rows[n]["a_name"].ToString();
                    if (dt.Rows[n]["a_platform_id"].ToString() != "")
                    {
                        model.a_platform_id = int.Parse(dt.Rows[n]["a_platform_id"].ToString());
                    }
                    model.a_paymode_id = dt.Rows[n]["a_paymode_id"].ToString();
                    if (dt.Rows[n]["a_apptype_id"].ToString() != "")
                    {
                        model.a_apptype_id = int.Parse(dt.Rows[n]["a_apptype_id"].ToString());
                    }
                    model.a_notifyurl = dt.Rows[n]["a_notifyurl"].ToString();
                    model.a_key = dt.Rows[n]["a_key"].ToString();
                    if (dt.Rows[n]["a_state"].ToString() != "")
                    {
                        model.a_state = int.Parse(dt.Rows[n]["a_state"].ToString());
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
        #endregion
        /// <summary>
        /// 查询应用信息
        /// </summary>
        /// <param name="auditstate">审核状态</param>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="SelectState">状态</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_app> SelectList( int paytype, int r_id,int platformid, int auditstate, string sea_name, int type, int SelectState, int appType , int searchDesc, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(paytype, r_id,platformid, auditstate, sea_name, type, SelectState, appType, searchDesc, pageIndexs, PageSize, out pageCount);
        }
        /// <summary>
        /// 查询应用信息根据商务过滤
        /// </summary>
        /// <param name="id">商务ID</param>
        /// <param name="auditstate">审核状态</param>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="SelectState">状态</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_app> SelectListById(int id, int auditstate, string sea_name, int type, int SelectState, int searchDesc, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectListById(id, auditstate, sea_name, type, SelectState, searchDesc, pageIndexs, PageSize, out pageCount);
        }
        /// <summary>
        /// 根据用户查询所属应用
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_app> SelectUserList(string userid, string searchname, int terrace, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectUserList(userid, searchname, terrace, pageIndexs, PageSize, out pageCount);
        }
        /// <summary>
        /// 根据生成的key查询是否存在重复信息
        /// </summary>
        /// <param name="a_key">key值</param>
        /// <returns></returns>
        public bool Existss(string a_key)
        {
            return dal.Existss(a_key);
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateLocUserState(string u_idlist, int state)
        {
            return dal.UpdateLocUserState(u_idlist, state);
        }
        /// <summary>
        /// 根据应用id查询信息
        /// </summary>
        /// <param name="a_id">应用id</param>
        /// <returns></returns>
        public JMP.MDL.jmp_app SelectId(int a_id)
        {
            return dal.SelectId(a_id);
        }
        /// <summary>
        /// 接口根据传入应用key查询应用信息和商品信息
        /// </summary>
        /// <param name="key">应用key</param>
        /// <returns></returns>
        public DataSet GetListjK(string key)
        {
            return dal.GetListjK(key);
        }
        /// <summary>
        /// 根据应用id查询所属平台信息
        /// </summary>
        /// <param name="a_id">应用id</param>
        /// <returns></returns>
        public JMP.MDL.jmp_app SelectAppId(int a_id)
        {
            return dal.SelectAppId(a_id);
        }
        /// <summary>
        /// 根据应用id查询正常能使用的应用（非冻结和非未审核的）
        /// </summary>
        /// <param name="a_id">应用id</param>
        /// <returns></returns>
        public JMP.MDL.jmp_app SelectAppIdStat(int a_id)
        {
            return dal.SelectAppIdStat(a_id);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateAppAuditState(string u_idlist, int state)
        {
            return dal.UpdateAppAuditState(u_idlist, state);
        }
        /// <summary>
        /// 应用弹窗
        /// </summary>
        /// <param name="orders">排序（0：降序，1：升序）</param>
        /// <param name="types">查询条件（1：应用编号，2：应用名称，3：用户名称）</param>
        /// <param name="typesname">查询内容</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_app> SelectListTc(int platformid, int orders, int types, string typesname, int pageIndexs, int PageSize, out int pageCount)
        {

            return dal.SelectListTc(platformid, orders, types, typesname, pageIndexs, PageSize, out pageCount);
        }
        /// <summary>
        /// 应用弹窗用于支付通道
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="order">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_app> SelectTClist(string sql, string order, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectTClist(sql, order, pageIndexs, PageSize, out pageCount);
        }


        /// <summary>
        /// 审核应用
        /// </summary>
        /// <param name="id">应用ID</param>
        /// <param name="start">审核状态</param>
        /// <param name="rid">风控等级</param>
        /// <param name="name">审核人</param>
        /// <returns></returns>
        public bool Update_auditstate(int id, int start,int rid,string name)
        {
            return dal.Update_auditstate(id, start,rid,name);
        }
    }
}