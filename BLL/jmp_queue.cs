using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMP.MDL;
using System.Data;
namespace JMP.BLL
{
    ///<summary>
    ///订单通知队列表
    ///</summary>
    public class jmp_queue
    {

        private readonly JMP.DAL.jmp_queue dal = new JMP.DAL.jmp_queue();
        public jmp_queue()
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
        public void Add(JMP.MDL.jmp_queue model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_queue model)
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
        /// 删除全部不为等待通知状态的数据
        /// </summary>
        public bool Deleteall()
        {

            return dal.Deleteall();
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_queue GetModel(int q_id)
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
        public List<JMP.MDL.jmp_queue> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_queue> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_queue> modelList = new List<JMP.MDL.jmp_queue>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_queue model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_queue();
                    if (dt.Rows[n]["q_id"].ToString() != "")
                    {
                        model.q_id = int.Parse(dt.Rows[n]["q_id"].ToString());
                    }
                    model.q_order_code = dt.Rows[n]["q_order_code"].ToString();
                    model.q_bizcode = dt.Rows[n]["q_bizcode"].ToString();
                    model.q_address = dt.Rows[n]["q_address"].ToString();
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
                    model.q_sign = dt.Rows[n]["q_sign"].ToString();
                    model.q_privateinfo = dt.Rows[n]["q_privateinfo"].ToString();
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

    }
}
