using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.MDL;

namespace JMP.BLL
{
    //提款表
    public partial class jmp_pay
    {
        private readonly JMP.DAL.jmp_pay dal = new JMP.DAL.jmp_pay();
        public jmp_pay()
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
        public int Add(JMP.MDL.jmp_pay model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_pay model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 更新交易号
        /// </summary>
        /// <param name="p_id">提款id</param>
        /// <param name="dno">交易号</param>
        /// <returns></returns>
        public bool Update(int p_id, string dno)
        {
            return dal.Update(p_id, dno);
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
        public JMP.MDL.jmp_pay GetModel(int p_id)
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
        public List<JMP.MDL.jmp_pay> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_pay> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_pay> modelList = new List<JMP.MDL.jmp_pay>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_pay model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_pay();
                    if (dt.Rows[n]["p_id"].ToString() != "")
                    {
                        model.p_id = int.Parse(dt.Rows[n]["p_id"].ToString());
                    }
                    model.p_tradeno = dt.Rows[n]["p_tradeno"].ToString();
                    if (dt.Rows[n]["p_applytime"].ToString() != "")
                    {
                        model.p_applytime = DateTime.Parse(dt.Rows[n]["p_applytime"].ToString());
                    }
                    model.p_dealno = dt.Rows[n]["p_dealno"].ToString();
                    if (dt.Rows[n]["p_paytime"].ToString() != "")
                    {
                        model.p_paytime = DateTime.Parse(dt.Rows[n]["p_paytime"].ToString());
                    }
                    if (dt.Rows[n]["p_state"].ToString() != "")
                    {
                        model.p_state = int.Parse(dt.Rows[n]["p_state"].ToString());
                    }
                    if (dt.Rows[n]["p_money"].ToString() != "")
                    {
                        model.p_money = decimal.Parse(dt.Rows[n]["p_money"].ToString());
                    }
                    model.p_bill_id = dt.Rows[n]["p_bill_id"].ToString();
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
        /// 获取数据列表
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="Order">排序字段</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="Count">总记录数</param>
        /// <returns></returns>
        public DataTable GetLists(string sql, string Order, int PageIndex, int PageSize, out int Count)
        {
            return dal.GetLists(sql, Order, PageIndex, PageSize, out Count);
        }
         /// <summary>
        /// 查询编号
        /// </summary>
        /// <returns></returns>
        public string SelectBh()
        {
            return dal.SelectBh();
        }
        #endregion
        
    }
}