using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    //[结算]按通道和应用分组的开发者成本详情统计表
    public partial class CoSettlementChannelCost
    {

        private readonly JMP.DAL.CoSettlementChannelCost dal = new JMP.DAL.CoSettlementChannelCost();
        public CoSettlementChannelCost()
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
        public int Add(JMP.MDL.CoSettlementChannelCost model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.CoSettlementChannelCost model)
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
        public JMP.MDL.CoSettlementChannelCost GetModel(int Id)
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
        public List<JMP.MDL.CoSettlementChannelCost> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.CoSettlementChannelCost> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.CoSettlementChannelCost> modelList = new List<JMP.MDL.CoSettlementChannelCost>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.CoSettlementChannelCost model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.CoSettlementChannelCost();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["TotalAmount"].ToString() != "")
                    {
                        model.TotalAmount = decimal.Parse(dt.Rows[n]["TotalAmount"].ToString());
                    }
                    if (dt.Rows[n]["CostRatio"].ToString() != "")
                    {
                        model.CostRatio = decimal.Parse(dt.Rows[n]["CostRatio"].ToString());
                    }
                    if (dt.Rows[n]["CostFee"].ToString() != "")
                    {
                        model.CostFee = decimal.Parse(dt.Rows[n]["CostFee"].ToString());
                    }
                    if (dt.Rows[n]["CreatedOn"].ToString() != "")
                    {
                        model.CreatedOn = DateTime.Parse(dt.Rows[n]["CreatedOn"].ToString());
                    }
                    model.SettlementDay = DateTime.Parse( dt.Rows[n]["SettlementDay"].ToString());
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
                    if (dt.Rows[n]["ChannelId"].ToString() != "")
                    {
                        model.ChannelId = int.Parse(dt.Rows[n]["ChannelId"].ToString());
                    }
                    model.ChannelName = dt.Rows[n]["ChannelName"].ToString();
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
        /// 根据sql查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable SelectList(string sql)
        {
            return dal.SelectList(sql);
        }

    }
}