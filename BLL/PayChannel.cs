using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace JMP.BLL
{
    //代付通道
    public partial class PayChannel
    {

        private readonly JMP.DAL.PayChannel dal = new JMP.DAL.PayChannel();
        public PayChannel()
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
        public int Add(JMP.MDL.PayChannel model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.PayChannel model)
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
        public JMP.MDL.PayChannel GetModel(int Id)
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
        public List<JMP.MDL.PayChannel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.PayChannel> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.PayChannel> modelList = new List<JMP.MDL.PayChannel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.PayChannel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.PayChannel();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    model.ChannelName = dt.Rows[n]["ChannelName"].ToString();
                    model.ChannelIdentifier = dt.Rows[n]["ChannelIdentifier"].ToString();
                    model.Append = dt.Rows[n]["Append"].ToString();
                    if (dt.Rows[n]["Appendtime"].ToString() != "")
                    {
                        model.Appendtime = DateTime.Parse(dt.Rows[n]["Appendtime"].ToString());
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
        /// 查询代付通道列表
        /// </summary>
        /// <param name="pageIndexs"></param>
        /// <param name="PageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<MDL.PayChannel> PayChannelList(int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.PayChannelList(pageIndexs, PageSize, out pageCount);
        }

    }
}