using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.BLL
{
    ///<summary>
    ///通知队列
    ///</summary>
    public partial class jmp_queuelist
    {

        private readonly JMP.DAL.jmp_queuelist dal = new JMP.DAL.jmp_queuelist();
        public jmp_queuelist()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int q_id)
        {

            return dal.Exists(q_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_queuelist model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_queuelist model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int q_id)
        {

            return dal.Delete(q_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string q_idlist)
        {
            return dal.DeleteList(q_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_queuelist GetModel(int q_id)
        {

            return dal.GetModel(q_id);
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
        public List<JMP.MDL.jmp_queuelist> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_queuelist> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_queuelist> modelList = new List<JMP.MDL.jmp_queuelist>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_queuelist model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_queuelist();
                    if (dt.Rows[n]["q_id"].ToString() != "")
                    {
                        model.q_id = int.Parse(dt.Rows[n]["q_id"].ToString());
                    }
                    if (dt.Rows[n]["trade_time"].ToString() != "")
                    {
                        model.trade_time = DateTime.Parse(dt.Rows[n]["trade_time"].ToString());
                    }
                    if (dt.Rows[n]["trade_price"].ToString() != "")
                    {
                        model.trade_price = decimal.Parse(dt.Rows[n]["trade_price"].ToString());
                    }
                    model.trade_paycode = dt.Rows[n]["trade_paycode"].ToString();
                    model.trade_code = dt.Rows[n]["trade_code"].ToString();
                    model.trade_no = dt.Rows[n]["trade_no"].ToString();
                    model.q_privateinfo = dt.Rows[n]["q_privateinfo"].ToString();
                    model.q_address = dt.Rows[n]["q_address"].ToString();
                    model.q_sign = dt.Rows[n]["q_sign"].ToString();
                    if (dt.Rows[n]["q_noticestate"].ToString() != "")
                    {
                        model.q_noticestate = int.Parse(dt.Rows[n]["q_noticestate"].ToString());
                    }
                    if (dt.Rows[n]["q_times"].ToString() != "")
                    {
                        model.q_times = int.Parse(dt.Rows[n]["q_times"].ToString());
                    }
                    if (dt.Rows[n]["q_noticetimes"].ToString() != "")
                    {
                        model.q_noticetimes = DateTime.Parse(dt.Rows[n]["q_noticetimes"].ToString());
                    }
                    model.q_tablename = dt.Rows[n]["q_tablename"].ToString();
                    if (dt.Rows[n]["q_o_id"].ToString() != "")
                    {
                        model.q_o_id = int.Parse(dt.Rows[n]["q_o_id"].ToString());
                    }
                    if (dt.Rows[n]["trade_type"].ToString() != "")
                    {
                        model.trade_type = int.Parse(dt.Rows[n]["trade_type"].ToString());
                    }
                    if (dt.Rows[n]["q_uersid"].ToString() != "")
                    {
                        model.q_uersid = int.Parse(dt.Rows[n]["q_uersid"].ToString());
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
        /// 根据订单编号查询信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public JMP.MDL.jmp_queuelist SelectGetModel(string code)
        {
            return dal.SelectGetModel(code);
        }
        /// <summary>
        /// 跟订单号修改通知次数
        /// </summary>
        /// <param name="code">订单号</param>
        /// <param name="q_times">通知次数</param>
        /// <returns></returns>
        public int UpdateOrder(string code, int q_times)
        {
            return dal.UpdateOrder(code, q_times);
        }
        #endregion

    }
}
