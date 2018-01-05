using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    //[结算]按应用分组的开发者每日结算详情数据表
    public partial class CoSettlementDeveloperAppDetails
    {

        private readonly JMP.DAL.CoSettlementDeveloperAppDetails dal = new JMP.DAL.CoSettlementDeveloperAppDetails();
        public CoSettlementDeveloperAppDetails()
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
        public int Add(JMP.MDL.CoSettlementDeveloperAppDetails model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.CoSettlementDeveloperAppDetails model)
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
        public JMP.MDL.CoSettlementDeveloperAppDetails GetModel(int Id)
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
        public List<JMP.MDL.CoSettlementDeveloperAppDetails> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.CoSettlementDeveloperAppDetails> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.CoSettlementDeveloperAppDetails> modelList = new List<JMP.MDL.CoSettlementDeveloperAppDetails>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.CoSettlementDeveloperAppDetails model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.CoSettlementDeveloperAppDetails();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["TotalAmount"].ToString() != "")
                    {
                        model.TotalAmount = decimal.Parse(dt.Rows[n]["TotalAmount"].ToString());
                    }
                    if (dt.Rows[n]["OriginServiceFee"].ToString() != "")
                    {
                        model.OriginServiceFee = decimal.Parse(dt.Rows[n]["OriginServiceFee"].ToString());
                    }
                    if (dt.Rows[n]["ServiceFee"].ToString() != "")
                    {
                        model.ServiceFee = decimal.Parse(dt.Rows[n]["ServiceFee"].ToString());
                    }
                    if (dt.Rows[n]["ServiceFeeRatio"].ToString() != "")
                    {
                        model.ServiceFeeRatio = decimal.Parse(dt.Rows[n]["ServiceFeeRatio"].ToString());
                    }
                    if (dt.Rows[n]["IsSpecialApproval"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsSpecialApproval"].ToString() == "1") || (dt.Rows[n]["IsSpecialApproval"].ToString().ToLower() == "true"))
                        {
                            model.IsSpecialApproval = true;
                        }
                        else
                        {
                            model.IsSpecialApproval = false;
                        }
                    }
                    if (dt.Rows[n]["SpecialApprovalServiceFee"].ToString() != "")
                    {
                        model.SpecialApprovalServiceFee = decimal.Parse(dt.Rows[n]["SpecialApprovalServiceFee"].ToString());
                    }
                    if (dt.Rows[n]["SpecialApprovalFeeRatio"].ToString() != "")
                    {
                        model.SpecialApprovalFeeRatio = decimal.Parse(dt.Rows[n]["SpecialApprovalFeeRatio"].ToString());
                    }
                    if (dt.Rows[n]["PortFee"].ToString() != "")
                    {
                        model.PortFee = decimal.Parse(dt.Rows[n]["PortFee"].ToString());
                    }
                    if (dt.Rows[n]["PortFeeRatio"].ToString() != "")
                    {
                        model.PortFeeRatio = decimal.Parse(dt.Rows[n]["PortFeeRatio"].ToString());
                    }
                    if (dt.Rows[n]["DefaultPortFeeRatio"].ToString() != "")
                    {
                        model.DefaultPortFeeRatio = decimal.Parse(dt.Rows[n]["DefaultPortFeeRatio"].ToString());
                    }
                    if (dt.Rows[n]["SettlementDay"].ToString() != "")
                    {
                        model.SettlementDay = DateTime.Parse(dt.Rows[n]["SettlementDay"].ToString());
                    }
                    if (dt.Rows[n]["CreatedOn"].ToString() != "")
                    {
                        model.CreatedOn = DateTime.Parse(dt.Rows[n]["CreatedOn"].ToString());
                    }
                    if (dt.Rows[n]["ChannelId"].ToString() != "")
                    {
                        model.ChannelId = int.Parse(dt.Rows[n]["ChannelId"].ToString());
                    }
                    model.ChannelName = dt.Rows[n]["ChannelName"].ToString();
                    if (dt.Rows[n]["ChannelCostFee"].ToString() != "")
                    {
                        model.ChannelCostFee = decimal.Parse(dt.Rows[n]["ChannelCostFee"].ToString());
                    }
                    if (dt.Rows[n]["ChannelCostRatio"].ToString() != "")
                    {
                        model.ChannelCostRatio = decimal.Parse(dt.Rows[n]["ChannelCostRatio"].ToString());
                    }
                    if (dt.Rows[n]["ChannelRefundAmount"].ToString() != "")
                    {
                        model.ChannelRefundAmount = decimal.Parse(dt.Rows[n]["ChannelRefundAmount"].ToString());
                    }
                    if (dt.Rows[n]["DeveloperId"].ToString() != "")
                    {
                        model.DeveloperId = int.Parse(dt.Rows[n]["DeveloperId"].ToString());
                    }
                    model.DeveloperName = dt.Rows[n]["DeveloperName"].ToString();
                    if (dt.Rows[n]["AppId"].ToString() != "")
                    {
                        model.AppId = int.Parse(dt.Rows[n]["AppId"].ToString());
                    }
                    model.AppName = dt.Rows[n]["AppName"].ToString();
                    if (dt.Rows[n]["PayModeId"].ToString() != "")
                    {
                        model.PayModeId = int.Parse(dt.Rows[n]["PayModeId"].ToString());
                    }
                    model.PayModeName = dt.Rows[n]["PayModeName"].ToString();
                    if (dt.Rows[n]["OrderCount"].ToString() != "")
                    {
                        model.OrderCount = int.Parse(dt.Rows[n]["OrderCount"].ToString());
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
        /// 查询明细根据支付方式分组
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public DataSet GetModel_Statistics(int id, string date)
        {
            return dal.GetModelKFZ(id, date);
        }

        /// <summary>
        /// 查询明细根据应用分组
        /// </summary>
        /// <returns></returns>
        public DataSet GetModelAppName(int id, string date)
        {
            return dal.GetModelAppName(id, date);
        }

        /// <summary>
        /// 查询明细根据支付方式分组
        /// </summary>
        /// <param name="id">APPID</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public DataSet GetModelPayType(int id, string date)
        {
            return dal.GetModelPayType(id, date);
        }

        /// <summary>
        /// 开发者首页根据开发者查询数据（昨天，本月，上月）
        /// </summary>
        /// <param name="id">开发者ID</param>
        /// <param name="date">日期</param>
        /// <param name="start">状态</param>
        /// <returns></returns>
        public JMP.MDL.CoSettlementDeveloperAppDetails GetModelKFZ_total(int id, string date, int start)
        {
            return dal.GetModelKFZ_total(id, date, start);
        }
    }
}