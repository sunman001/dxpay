using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.DAL;
using JMP.MDL;
namespace JMP.BLL
{
    ///<summary>
    ///应用投诉表
    ///</summary>
    public partial class jmp_complaint
    {

        private readonly JMP.DAL.jmp_complaint dal = new JMP.DAL.jmp_complaint();
        public jmp_complaint()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int c_id)
        {
            return dal.Exists(c_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_complaint model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_complaint model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int c_id)
        {

            return dal.Delete(c_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string c_idlist)
        {
            return dal.DeleteList(c_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_complaint GetModel(int c_id)
        {

            return dal.GetModel(c_id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 根据应用id查询应用投诉信息
        /// </summary>
        /// <param name="c_id">应用id</param>
        /// <returns></returns>
        public JMP.MDL.jmp_complaint SelectId(int c_id)
        {
            return dal.SelectId(c_id);
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
        public List<JMP.MDL.jmp_complaint> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_complaint> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_complaint> modelList = new List<JMP.MDL.jmp_complaint>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_complaint model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_complaint();
                    if (dt.Rows[n]["c_id"].ToString() != "")
                    {
                        model.c_id = int.Parse(dt.Rows[n]["c_id"].ToString());
                    }
                    if (dt.Rows[n]["c_tjtimes"].ToString() != "")
                    {
                        model.c_tjtimes = DateTime.Parse(dt.Rows[n]["c_tjtimes"].ToString());
                    }
                    model.c_tjname = dt.Rows[n]["c_tjname"].ToString();
                    model.c_clname = dt.Rows[n]["c_clname"].ToString();
                    if (dt.Rows[n]["c_cltimes"].ToString() != "")
                    {
                        model.c_cltimes = DateTime.Parse(dt.Rows[n]["c_cltimes"].ToString());
                    }
                    model.c_result = dt.Rows[n]["c_result"].ToString();
                    model.c_reason = dt.Rows[n]["c_reason"].ToString();
                    model.c_state = dt.Rows[n]["c_state"].ToString();
                    if (dt.Rows[n]["c_appid"].ToString() != "")
                    {
                        model.c_appid = int.Parse(dt.Rows[n]["c_appid"].ToString());
                    }
                    if (dt.Rows[n]["c_userid"].ToString() != "")
                    {
                        model.c_userid = int.Parse(dt.Rows[n]["c_userid"].ToString());
                    }
                    if (dt.Rows[n]["c_payid"].ToString() != "")
                    {
                        model.c_payid = int.Parse(dt.Rows[n]["c_payid"].ToString());
                    }
                    model.c_tradeno = dt.Rows[n]["c_tradeno"].ToString();
                    model.c_code = dt.Rows[n]["c_code"].ToString();
                    if (dt.Rows[n]["c_money"].ToString() != "")
                    {
                        model.c_money = decimal.Parse(dt.Rows[n]["c_money"].ToString());
                    }
                    if (dt.Rows[n]["c_times"].ToString() != "")
                    {
                        model.c_times = DateTime.Parse(dt.Rows[n]["c_times"].ToString());
                    }
                    if (dt.Rows[n]["c_datimes"].ToString() != "")
                    {
                        model.c_datimes = DateTime.Parse(dt.Rows[n]["c_datimes"].ToString());
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
        /// 查询应用投诉管理
        /// </summary>
        /// <param name="userid">用户id（后台默认传0，开发者平台默认传用户id）</param>
        /// <param name="auditstate">处理状态</param>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="SelectState">状态</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_complaint> SelectList(int UserDept,string  UserId,string auditstate, string sea_name, int type, int searchDesc, string stime, string endtime, int pageIndexs, int PageSize, out int pageCount,int dept)
        {
            return dal.SelectList(UserDept, UserId, auditstate, sea_name, type, searchDesc, stime, endtime, pageIndexs, PageSize, out pageCount,dept);
        }


        /// <summary>
        /// 处理应用投诉管理
        /// </summary>
        /// <param name="cid">选择的应用投诉ID</param>

        /// <returns></returns>
        public bool ComplaintLC(string cid,  string remark,string r_auditor)
        {
            return dal.ComplaintLC(cid, remark, r_auditor);
        }

        /// <summary>
        /// 根据sql查询信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_complaint> DcSelectList(string sql)
        {
            return dal.DcSelectList(sql);
        }

    }
}