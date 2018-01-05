using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.DAL;
using JMP.MDL;
namespace JMP.BLL
{
    ///<summary>
    ///应用核查表:应用异常请求监控数据记录表
    ///</summary>
    public partial class jmp_app_request_audit
    {

        private readonly JMP.DAL.jmp_app_request_audit dal = new JMP.DAL.jmp_app_request_audit();
        public jmp_app_request_audit()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(JMP.MDL.jmp_app_request_audit model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_app_request_audit model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            return dal.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_app_request_audit GetModel(int id)
        {

            return dal.GetModel(id);
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
        public List<JMP.MDL.jmp_app_request_audit> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_app_request_audit> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_app_request_audit> modelList = new List<JMP.MDL.jmp_app_request_audit>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_app_request_audit model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_app_request_audit();
                    if (dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    if (dt.Rows[n]["app_id"].ToString() != "")
                    {
                        model.app_id = int.Parse(dt.Rows[n]["app_id"].ToString());
                    }
                    model.app_name = dt.Rows[n]["app_name"].ToString();
                    model.message = dt.Rows[n]["message"].ToString();
                    if (dt.Rows[n]["created_on"].ToString() != "")
                    {
                        model.created_on = DateTime.Parse(dt.Rows[n]["created_on"].ToString());
                    }
                    if (dt.Rows[n]["is_processed"].ToString() != "")
                    {
                        model.is_processed = int.Parse(dt.Rows[n]["is_processed"].ToString());
                    }
                    if (dt.Rows[n]["processed_time"].ToString() != "")
                    {
                        model.processed_time = DateTime.Parse(dt.Rows[n]["processed_time"].ToString());
                    }
                    model.processed_by = dt.Rows[n]["processed_by"].ToString();
                    model.processed_result = dt.Rows[n]["processed_result"].ToString();
                    if (dt.Rows[n]["message_send_time"].ToString() != "")
                    {
                        model.message_send_time = DateTime.Parse(dt.Rows[n]["message_send_time"].ToString());
                    }
                    if (dt.Rows[n]["is_send_message"].ToString() != "")
                    {
                        model.is_send_message = int.Parse(dt.Rows[n]["is_send_message"].ToString());
                    }
                    if (dt.Rows[n]["type"].ToString() != "")
                    {
                        model.type = int.Parse(dt.Rows[n]["type"].ToString());
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


        public List<JMP.MDL.jmp_app_request_audit> SelectList( string typeclass, string auditstate, string sea_name, int type, int searchDesc, string stime, string endtime, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(typeclass,auditstate, sea_name, type, searchDesc, stime, endtime, pageIndexs, PageSize, out pageCount);
        }


        /// <summary>
        /// 处理应用投诉管理
        /// </summary>
        /// <param name="cid">选择的应用投诉ID</param>

        /// <returns></returns>
        public bool OrderAuditLC(string cid, string remark, string r_auditor)
        {
            return dal.OrderAuditLC(cid, remark, r_auditor);
        }

        /// <summary>
        /// 根据sql查询信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_app_request_audit> DcSelectList(string sql)
        {
            return dal.DcSelectList(sql);
        }
        #endregion


        public int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }

        /// <summary>
        /// 将未发送标识的设置为已发送
        /// </summary>
        /// <returns></returns>
        public int SetSentMessage()
        {
            return dal.SetSentMessage();
        }


        /// <summary>
        /// 将未发送标识的设置为已发送
        /// </summary>
        /// <param name="monitorType">监控类型[0:应用支付成功率,1:应用无订单,2:应用金额成功率,20:通道无订单]</param>
        /// <returns></returns>
        public int SetSentMessage(int monitorType)
        {
            return dal.SetSentMessage(monitorType);
        }
    }
}