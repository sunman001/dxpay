using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model.Query;

namespace JMP.BLL
{
    ///<summary>
    ///支付接口配置表
    ///</summary>
    public partial class jmp_interface
    {

        private readonly JMP.DAL.jmp_interface dal = new JMP.DAL.jmp_interface();
        public jmp_interface()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int l_id)
        {
            return dal.Exists(l_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_interface model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_interface model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int l_id)
        {

            return dal.Delete(l_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string l_idlist)
        {
            return dal.DeleteList(l_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_interface GetModel(int l_id)
        {

            return dal.GetModel(l_id);
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
        public List<JMP.MDL.jmp_interface> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_interface> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_interface> modelList = new List<JMP.MDL.jmp_interface>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_interface model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_interface();
                    if (dt.Rows[n]["l_id"].ToString() != "")
                    {
                        model.l_id = int.Parse(dt.Rows[n]["l_id"].ToString());
                    }
                    if (dt.Rows[n]["l_risk"].ToString() != "")
                    {
                        model.l_risk = int.Parse(dt.Rows[n]["l_risk"].ToString());
                    }
                    if (dt.Rows[n]["l_daymoney"].ToString() != "")
                    {
                        model.l_daymoney = decimal.Parse(dt.Rows[n]["l_daymoney"].ToString());
                    }
                    if (dt.Rows[n]["l_time"].ToString() != "")
                    {
                        model.l_time = DateTime.Parse(dt.Rows[n]["l_time"].ToString());
                    }
                    if (dt.Rows[n]["l_minimum"].ToString() != "")
                    {
                        model.l_minimum = decimal.Parse(dt.Rows[n]["l_minimum"].ToString());
                    }
                    if (dt.Rows[n]["l_maximum"].ToString() != "")
                    {
                        model.l_maximum = decimal.Parse(dt.Rows[n]["l_maximum"].ToString());
                    }
                    if (dt.Rows[n]["l_CostRatio"].ToString() != "")
                    {
                        model.l_CostRatio = decimal.Parse(dt.Rows[n]["l_CostRatio"].ToString());
                    }
                    model.l_str = dt.Rows[n]["l_str"].ToString();
                    if (dt.Rows[n]["l_sort"].ToString() != "")
                    {
                        model.l_sort = int.Parse(dt.Rows[n]["l_sort"].ToString());
                    }
                    if (dt.Rows[n]["l_isenable"].ToString() != "")
                    {
                        model.l_isenable = int.Parse(dt.Rows[n]["l_isenable"].ToString());
                    }
                    if (dt.Rows[n]["l_paymenttype_id"].ToString() != "")
                    {
                        model.l_paymenttype_id = int.Parse(dt.Rows[n]["l_paymenttype_id"].ToString());
                    }
                    model.l_apptypeid = dt.Rows[n]["l_apptypeid"].ToString();
                    model.l_corporatename = dt.Rows[n]["l_corporatename"].ToString();
                    if (dt.Rows[n]["l_priority"].ToString() != "")
                    {
                        model.l_priority = int.Parse(dt.Rows[n]["l_priority"].ToString());
                    }
                    model.l_jsonstr = dt.Rows[n]["l_jsonstr"].ToString();


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
        /// 查询支付配置信息
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">分页数量</param>
        /// <param name="pageCount">总数量</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_interface> SelectList(string sql, string Order, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(sql, Order, pageIndexs, PageSize, out pageCount);
        }
        /// <summary>
        /// 根据id查询相关信息
        /// </summary>
        public JMP.MDL.jmp_interface GetModels(int l_id)
        {
            return dal.GetModels(l_id);
        }

        /// <summary>
        /// 根据支付类型获取第一条支付配置信息
        /// </summary>
        /// <param name="type">支付类型（0：支付宝，1：微信，2：网银）</param>
        /// <returns></returns>
        public JMP.MDL.jmp_interface strzf(string type, int tid)
        {
            return dal.strzf(type, tid);
        }
        /// <summary>
        /// 根据支付类型获取支付通道配置信息
        /// </summary>
        /// <param name="type">支付类型</param>
        /// <param name="tid">风控配置表id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        public DataTable SelectPay(string type, int tid, int appid)
        {
            return dal.SelectPay(type, tid, appid);
        }

        /// <summary>
        /// 根据应用id查询支付通道信息
        /// </summary>
        /// <param name="type">支付类型</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        public DataTable selectAppid(string type, int appid)
        {
            return dal.selectAppid(type, appid);
        }
        /// <summary>
        /// 根据支付类型获取第一条支付配置信息
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public string strzf_monitor(int tid)
        {
            return dal.strzf_monitor(tid);
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
        /// 根据字id符传查询除此之外的数据用于冻结判断
        /// </summary>
        /// <param name="str">id字符串</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public DataTable SelectDataTable(string str, int type)
        {
            return dal.SelectDataTable(str, type);
        }

        /// <summary>
        /// 获取今日已使用过的支付通道实体列表
        /// </summary>
        /// <param name="tableName">当前时间对应的订单表名</param>
        /// <returns></returns>
        public List<OrderedInterface> GetTodayOrderedInterfaces(string tableName)
        {
            return dal.GetTodayOrderedInterfaces(tableName);
        }

        /// <summary>
        /// 根据支付ID获取今日订单数量
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public int GetTodayOrderedInterfaces_byid(string tableName)
        {
            return dal.GetTodayOrderedInterfaces_byid(tableName);
        }

        /// <summary>
        /// 修改通道成本费率
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="costratio">费率</param>
        /// <returns></returns>
        public bool UpdateInterfaceCostRatio(int id, string costratio)
        {
            return dal.UpdateInterfaceCostRatio(id, costratio);
        }
    }
}