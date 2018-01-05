using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    //[通道池]通道池-应用映射表
    public partial class jmp_channel_app_mapping
    {

        private readonly JMP.DAL.jmp_channel_app_mapping dal = new JMP.DAL.jmp_channel_app_mapping();
        public jmp_channel_app_mapping()
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
        public int Add(JMP.MDL.jmp_channel_app_mapping model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_channel_app_mapping model)
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
        public JMP.MDL.jmp_channel_app_mapping GetModel(int Id)
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
        public List<JMP.MDL.jmp_channel_app_mapping> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_channel_app_mapping> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_channel_app_mapping> modelList = new List<JMP.MDL.jmp_channel_app_mapping>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_channel_app_mapping model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_channel_app_mapping();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["ChannelId"].ToString() != "")
                    {
                        model.ChannelId = int.Parse(dt.Rows[n]["ChannelId"].ToString());
                    }
                    if (dt.Rows[n]["AppId"].ToString() != "")
                    {
                        model.AppId = int.Parse(dt.Rows[n]["AppId"].ToString());
                    }
                    if (dt.Rows[n]["CreatedOn"].ToString() != "")
                    {
                        model.CreatedOn = DateTime.Parse(dt.Rows[n]["CreatedOn"].ToString());
                    }
                    if (dt.Rows[n]["CreatedByUerId"].ToString() != "")
                    {
                        model.CreatedByUerId = int.Parse(dt.Rows[n]["CreatedByUerId"].ToString());
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
        /// 获取数据
        /// </summary>
        public DataSet GetModelChannelId(int Id)
        {
            return dal.GetModelChannelId(Id);
        }


        /// <summary>
        /// 执行SQL(实现事务)
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int AddAppMapping(string[] list)
        {
            return dal.AddAppMapping(list);
        }

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