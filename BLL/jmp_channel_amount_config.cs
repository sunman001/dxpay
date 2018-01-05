using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    //根据时间段设置通道池每次调用支付通道的数量
    public partial class jmp_channel_amount_config
    {

        private readonly JMP.DAL.jmp_channel_amount_config dal = new JMP.DAL.jmp_channel_amount_config();
        public jmp_channel_amount_config()
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
        public int Add(JMP.MDL.jmp_channel_amount_config model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_channel_amount_config model)
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
        public JMP.MDL.jmp_channel_amount_config GetModel(int Id)
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
        public List<JMP.MDL.jmp_channel_amount_config> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_channel_amount_config> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_channel_amount_config> modelList = new List<JMP.MDL.jmp_channel_amount_config>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_channel_amount_config model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_channel_amount_config();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["ChannelPoolId"].ToString() != "")
                    {
                        model.ChannelPoolId = int.Parse(dt.Rows[n]["ChannelPoolId"].ToString());
                    }
                    if (dt.Rows[n]["WhichHour"].ToString() != "")
                    {
                        model.WhichHour = int.Parse(dt.Rows[n]["WhichHour"].ToString());
                    }
                    if (dt.Rows[n]["Amount"].ToString() != "")
                    {
                        model.Amount = int.Parse(dt.Rows[n]["Amount"].ToString());
                    }
                    if (dt.Rows[n]["CreatedOn"].ToString() != "")
                    {
                        model.CreatedOn = DateTime.Parse(dt.Rows[n]["CreatedOn"].ToString());
                    }
                    if (dt.Rows[n]["CreatedByUserId"].ToString() != "")
                    {
                        model.CreatedByUserId = int.Parse(dt.Rows[n]["CreatedByUserId"].ToString());
                    }
                    model.CreatedByUserName = dt.Rows[n]["CreatedByUserName"].ToString();


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
        /// 获取数据列
        /// </summary>
        public DataSet GetModelChannelPoolId(int Id)
        {
            return dal.GetModelChannelPoolId(Id);
        }


        /// <summary>
        /// 设置通道池配置数量
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int AddAmount(string[] list)
        {
            return dal.AddAmount(list);
        }

    }
}