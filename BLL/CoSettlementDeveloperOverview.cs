using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    //开发者每日结算详情表
    public partial class CoSettlementDeveloperOverview
    {

        private readonly JMP.DAL.CoSettlementDeveloperOverview dal = new JMP.DAL.CoSettlementDeveloperOverview();
        public CoSettlementDeveloperOverview()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.CoSettlementDeveloperOverview model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.CoSettlementDeveloperOverview model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            return dal.Delete(Id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            return dal.DeleteList(Idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.CoSettlementDeveloperOverview GetModel(int Id)
        {

            return dal.GetModel(Id);
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
        public List<JMP.MDL.CoSettlementDeveloperOverview> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.CoSettlementDeveloperOverview> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.CoSettlementDeveloperOverview> modelList = new List<JMP.MDL.CoSettlementDeveloperOverview>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.CoSettlementDeveloperOverview model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.CoSettlementDeveloperOverview();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["BpPushMoneyRatio"].ToString() != "")
                    {
                        model.BpPushMoneyRatio = decimal.Parse(dt.Rows[n]["BpPushMoneyRatio"].ToString());
                    }
                    if (dt.Rows[n]["AgentPushMoney"].ToString() != "")
                    {
                        model.AgentPushMoney = decimal.Parse(dt.Rows[n]["AgentPushMoney"].ToString());
                    }
                    if (dt.Rows[n]["AgentPushMoneyRatio"].ToString() != "")
                    {
                        model.AgentPushMoneyRatio = decimal.Parse(dt.Rows[n]["AgentPushMoneyRatio"].ToString());
                    }
                    if (dt.Rows[n]["PortFee"].ToString() != "")
                    {
                        model.PortFee = decimal.Parse(dt.Rows[n]["PortFee"].ToString());
                    }
                    if (dt.Rows[n]["CostFee"].ToString() != "")
                    {
                        model.CostFee = decimal.Parse(dt.Rows[n]["CostFee"].ToString());
                    }
                    if (dt.Rows[n]["DeveloperId"].ToString() != "")
                    {
                        model.DeveloperId = int.Parse(dt.Rows[n]["DeveloperId"].ToString());
                    }
                    model.DeveloperName = dt.Rows[n]["DeveloperName"].ToString();
                    if (dt.Rows[n]["SettlementDay"].ToString() != "")
                    {
                        model.SettlementDay = DateTime.Parse(dt.Rows[n]["SettlementDay"].ToString());
                    }
                    if (dt.Rows[n]["CreatedOn"].ToString() != "")
                    {
                        model.CreatedOn = DateTime.Parse(dt.Rows[n]["CreatedOn"].ToString());
                    }
                    if (dt.Rows[n]["TotalAmount"].ToString() != "")
                    {
                        model.TotalAmount = decimal.Parse(dt.Rows[n]["TotalAmount"].ToString());
                    }
                    if (dt.Rows[n]["ServiceFee"].ToString() != "")
                    {
                        model.ServiceFee = decimal.Parse(dt.Rows[n]["ServiceFee"].ToString());
                    }
                    if (dt.Rows[n]["ServiceFeeRatio"].ToString() != "")
                    {
                        model.ServiceFeeRatio = decimal.Parse(dt.Rows[n]["ServiceFeeRatio"].ToString());
                    }
                    if (dt.Rows[n]["BpPushMoney"].ToString() != "")
                    {
                        model.BpPushMoney = decimal.Parse(dt.Rows[n]["BpPushMoney"].ToString());
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
        /// 获取数据列表
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="Order">排序字段</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="Count">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.CoSettlementDeveloperOverview> GetLists(string sql, string Order, int PageIndex, int PageSize, out int Count)
        {
            return dal.GetLists(sql, Order, PageIndex, PageSize, out Count);
        }

        /// <summary>
        /// 根据sql查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable SelectSum(string sql)
        {
            return dal.SelectSum(sql);
        }
        #endregion

    }
}