using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.MDL;
namespace JMP.BLL
{
    //终端属性分析表
    public partial class jmp_terminal
    {

        private readonly JMP.DAL.jmp_terminal dal = new JMP.DAL.jmp_terminal();
        public jmp_terminal()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int t_id)
        {
            return dal.Exists(t_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_terminal model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_terminal model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int t_id)
        {

            return dal.Delete(t_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string t_idlist)
        {
            return dal.DeleteList(t_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_terminal GetModel(int t_id)
        {

            return dal.GetModel(t_id);
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
        public List<JMP.MDL.jmp_terminal> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_terminal> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_terminal> modelList = new List<JMP.MDL.jmp_terminal>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_terminal model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_terminal();
                    if (dt.Rows[n]["t_id"].ToString() != "")
                    {
                        model.t_id = int.Parse(dt.Rows[n]["t_id"].ToString());
                    }
                    model.t_system = dt.Rows[n]["t_system"].ToString();
                    model.t_hardware = dt.Rows[n]["t_hardware"].ToString();
                    model.t_sdkver = dt.Rows[n]["t_sdkver"].ToString();
                    if (dt.Rows[n]["t_time"].ToString() != "")
                    {
                        model.t_time = DateTime.Parse(dt.Rows[n]["t_time"].ToString());
                    }
                    model.t_screen = dt.Rows[n]["t_screen"].ToString();
                    model.t_network = dt.Rows[n]["t_network"].ToString();
                    model.t_key = dt.Rows[n]["t_key"].ToString();
                    model.t_mark = dt.Rows[n]["t_mark"].ToString();
                    model.t_ip = dt.Rows[n]["t_ip"].ToString();
                    model.t_province = dt.Rows[n]["t_province"].ToString();
                    model.t_imsi = dt.Rows[n]["t_imsi"].ToString();
                    model.t_nettype = dt.Rows[n]["t_nettype"].ToString();
                    model.t_brand = dt.Rows[n]["t_brand"].ToString();


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

        #region 报表模块--陶涛20160322
        /// <summary>
        /// 查询新增用户（小时）
        /// <param name="times">查询日期（2016-03-18）</param>
        /// </summary>
        /// <returns></returns>
        public DataSet GetListReportAddUser(string times)
        {
            return dal.GetListReportAddUser(times);
        }
        #endregion

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sqls">SQL语句</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_terminal> SelectList(string sqls, string order, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(sqls, order, pageIndexs, PageSize, out pageCount);
        }

        /// <summary>
        /// 查询新增用户（每小时）
        /// </summary>
        /// <param name="baseDbName">设备数据库名称</param>
        /// <param name="tTime">查询日期（2016-03-31）</param>
        /// <param name="merchantId">商务ID</param>
        /// <returns></returns>
        public DataSet GetMerchantListReportAddUser(string baseDbName, string tTime, int merchantId)
        {
            return dal.GetMerchantListReportAddUser(baseDbName, tTime, merchantId);
        }
    }
}