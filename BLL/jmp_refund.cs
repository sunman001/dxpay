using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.DAL;
using JMP.MDL;
namespace JMP.BLL
{
    ///<summary>
    ///退款申请表
    ///</summary>
    public partial class jmp_refund
    {

        private readonly JMP.DAL.jmp_refund dal = new JMP.DAL.jmp_refund();
        public jmp_refund()
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
        public int Add(JMP.MDL.jmp_refund model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_refund model)
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
        public JMP.MDL.jmp_refund GetModel(int r_id)
        {

            return dal.GetModel(r_id);
        }
        /// <summary>
        /// 根据应用id查询信息
        /// </summary>
        /// <param name="a_id">应用id</param>
        /// <returns></returns>
        public JMP.MDL.jmp_refund SelectId(int r_id)
        {
            return dal.SelectId(r_id);
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
        public List<JMP.MDL.jmp_refund> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_refund> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_refund> modelList = new List<JMP.MDL.jmp_refund>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_refund model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_refund();
                    if (dt.Rows[n]["r_id"].ToString() != "")
                    {
                        model.r_id = int.Parse(dt.Rows[n]["r_id"].ToString());
                    }
                    if (dt.Rows[n]["r_time"].ToString() != "")
                    {
                        model.r_time = DateTime.Parse(dt.Rows[n]["r_time"].ToString());
                    }
                    if (dt.Rows[n]["r_static"].ToString() != "")
                    {
                        model.r_static = int.Parse(dt.Rows[n]["r_static"].ToString());
                    }
                    model.r_auditor = dt.Rows[n]["r_auditor"].ToString();
                    if (dt.Rows[n]["r_auditortime"].ToString() != "")
                    {
                        model.r_auditortime = DateTime.Parse(dt.Rows[n]["r_auditortime"].ToString());
                    }
                    model.r_remark = dt.Rows[n]["r_remark"].ToString();
                    model.r_name = dt.Rows[n]["r_name"].ToString();
                    model.r_tel = dt.Rows[n]["r_tel"].ToString();
                    if (dt.Rows[n]["r_userid"].ToString() != "")
                    {
                        model.r_userid = int.Parse(dt.Rows[n]["r_userid"].ToString());
                    }
                    if (dt.Rows[n]["r_payid"].ToString() != "")
                    {
                        model.r_payid = int.Parse(dt.Rows[n]["r_payid"].ToString());
                    }
                    if (dt.Rows[n]["r_appid"].ToString() != "")
                    {
                        model.r_appid = int.Parse(dt.Rows[n]["r_appid"].ToString());
                    }
                    model.r_tradeno = dt.Rows[n]["r_tradeno"].ToString();
                    model.r_code = dt.Rows[n]["r_code"].ToString();
                    if (dt.Rows[n]["r_price"].ToString() != "")
                    {
                        model.r_price = decimal.Parse(dt.Rows[n]["r_price"].ToString());
                    }
                    if (dt.Rows[n]["r_money"].ToString() != "")
                    {
                        model.r_money = decimal.Parse(dt.Rows[n]["r_money"].ToString());
                    }
                    if (dt.Rows[n]["r_date"].ToString() != "")
                    {
                        model.r_date = DateTime.Parse(dt.Rows[n]["r_date"].ToString());
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
        /// <param name="userid">用户id（后台默认传0，开发者平台默认传用户id）</param>
        /// <param name="auditstate">审核状态</param>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="SelectState">状态</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_refund> SelectList(int UserRoleId, string UserId, string auditstate, string sea_name, int type, int searchDesc, string stime, string endtime, int pageIndexs, int PageSize, out int pageCount,int RoleID)
        {
            return dal.SelectList(UserRoleId, UserId, auditstate, sea_name, type, searchDesc, stime, endtime, pageIndexs, PageSize, out pageCount, RoleID);
        }


        /// <summary>
        /// 审核退款信息
        /// </summary>
        /// <param name="rid">选择的退款ID</param>

        /// <returns></returns>
        public bool AuditorToRefund(string rid, string r_static, string r_auditor, string r_remark,string r_payid)
        {
            return dal.AuditorToRefund(rid, r_static, r_auditor, r_remark, r_payid);
        }
        /// <summary>
        /// 数据导出
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_refund> SelectDc(string sql)
        {
            return dal.SelectDc(sql);
        }

    }
}