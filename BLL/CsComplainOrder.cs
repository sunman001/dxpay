using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace JMP.BLL
{
    //投诉订单表
    public partial class CsComplainOrder
    {

        private readonly JMP.DAL.CsComplainOrder dal = new JMP.DAL.CsComplainOrder();
        public CsComplainOrder()
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
        public int Add(JMP.MDL.CsComplainOrder model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.CsComplainOrder model)
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
        public JMP.MDL.CsComplainOrder GetModel(int Id)
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
        public List<JMP.MDL.CsComplainOrder> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.CsComplainOrder> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.CsComplainOrder> modelList = new List<JMP.MDL.CsComplainOrder>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.CsComplainOrder model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.CsComplainOrder();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    model.Envidence = dt.Rows[n]["Envidence"].ToString();
                    if (dt.Rows[n]["HandlerId"].ToString() != "")
                    {
                        model.HandlerId = int.Parse(dt.Rows[n]["HandlerId"].ToString());
                    }
                    model.HandlerName = dt.Rows[n]["HandlerName"].ToString();
                    if (dt.Rows[n]["HandleDate"].ToString() != "")
                    {
                        model.HandleDate = DateTime.Parse(dt.Rows[n]["HandleDate"].ToString());
                    }
                    model.HandleResult = dt.Rows[n]["HandleResult"].ToString();
                    if (dt.Rows[n]["state"].ToString() != "")
                    {
                        model.state = int.Parse(dt.Rows[n]["state"].ToString());
                    }
                    if (dt.Rows[n]["FounderId"].ToString() != "")
                    {
                        model.FounderId = int.Parse(dt.Rows[n]["FounderId"].ToString());
                    }
                    model.FounderName = dt.Rows[n]["FounderName"].ToString();
                    if (dt.Rows[n]["IsRefund"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsRefund"].ToString() == "1") || (dt.Rows[n]["IsRefund"].ToString().ToLower() == "true"))
                        {
                            model.IsRefund = true;
                        }
                        else
                        {
                            model.IsRefund = false;
                        }
                    }
                    if (dt.Rows[n]["Price"].ToString() != "")
                    {
                        model.Price = decimal.Parse(dt.Rows[n]["Price"].ToString());
                    }
                    model.OrderNumber = dt.Rows[n]["OrderNumber"].ToString();
                    if (dt.Rows[n]["DownstreamStartTime"].ToString() != "")
                    {
                        model.DownstreamStartTime = DateTime.Parse(dt.Rows[n]["DownstreamStartTime"].ToString());
                    }
                    if (dt.Rows[n]["DownstreamEndTime"].ToString() != "")
                    {
                        model.DownstreamEndTime = DateTime.Parse(dt.Rows[n]["DownstreamEndTime"].ToString());
                    }
                    if (dt.Rows[n]["ChannelId"].ToString() != "")
                    {
                        model.ChannelId = int.Parse(dt.Rows[n]["ChannelId"].ToString());
                    }
                    model.OrderTable = dt.Rows[n]["OrderTable"].ToString();
                    if (dt.Rows[n]["UserId"].ToString() != "")
                    {
                        model.UserId = int.Parse(dt.Rows[n]["UserId"].ToString());
                    }
                    if (dt.Rows[n]["AppId"].ToString() != "")
                    {
                        model.AppId = int.Parse(dt.Rows[n]["AppId"].ToString());
                    }
                    if (dt.Rows[n]["ComplainTypeId"].ToString() != "")
                    {
                        model.ComplainTypeId = int.Parse(dt.Rows[n]["ComplainTypeId"].ToString());
                    }
                    model.ComplainTypeName = dt.Rows[n]["ComplainTypeName"].ToString();
                    if (dt.Rows[n]["ComplainDate"].ToString() != "")
                    {
                        model.ComplainDate = DateTime.Parse(dt.Rows[n]["ComplainDate"].ToString());
                    }
                    if (dt.Rows[n]["CreatedOn"].ToString() != "")
                    {
                        model.CreatedOn = DateTime.Parse(dt.Rows[n]["CreatedOn"].ToString());
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
        /// 查询投诉
        /// </summary>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="SelectState">状态</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.CsComplainOrder> SelectList(string SeachDate, string stime, string etime, string sea_name, string type, int SelectState, int searchDesc, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(SeachDate, stime, etime, sea_name, type, SelectState, searchDesc, pageIndexs, PageSize, out pageCount);
        }


        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateCustomState(string idlist, int state)
        {
            return dal.UpdateCustomState(idlist, state);
        }

        /// <summary>
        /// 处理投诉
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="HandlerId">处理者id</param>
        /// <param name="HandlerName">处理者姓名</param>
        /// <param name="HandleResult">处理结果</param>
        /// <param name="isRefund">是否退款</param>
        /// <returns></returns>
        public bool UpdateCustomHandleResult(int id, int HandlerId, string HandlerName, string HandleResult, bool isRefund)
        {
            return dal.UpdateCustomHandleResult(id, HandlerId, HandlerName, HandleResult, isRefund);
        }

        /// <summary>
        /// 根据订单号查询投诉类型
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public JMP.MDL.CsComplainOrder SelectListOrder(string order)
        {
            return dal.SelectListOrder(order);
        }

    }

}