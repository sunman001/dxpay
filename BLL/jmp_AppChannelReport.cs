using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.DAL;
using JMP.MDL;

namespace JMP.BLL
{
    
    public   class jmp_AppChannelReport
    {
        private readonly JMP.DAL.jmp_AppChannelReport dal = new JMP.DAL.jmp_AppChannelReport();
        public jmp_AppChannelReport()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_AppChannelReport model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_AppChannelReport model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            return dal.Delete(ID);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(IDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_AppChannelReport GetModel(int ID)
        {

            return dal.GetModel(ID);
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
        public List<JMP.MDL.jmp_AppChannelReport> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_AppChannelReport> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_AppChannelReport> modelList = new List<JMP.MDL.jmp_AppChannelReport>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_AppChannelReport model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_AppChannelReport();
                    if (dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["CreatedOn"].ToString() != "")
                    {
                        model.CreatedOn = DateTime.Parse(dt.Rows[n]["CreatedOn"].ToString());
                    }
                    if (dt.Rows[n]["CreatedDate"].ToString() != "")
                    {
                        model.CreatedDate = DateTime.Parse(dt.Rows[n]["CreatedDate"].ToString());
                    }
                    if (dt.Rows[n]["ChannelId"].ToString() != "")
                    {
                        model.ChannelId = int.Parse(dt.Rows[n]["ChannelId"].ToString());
                    }
                    model.ChannelName = dt.Rows[n]["ChannelName"].ToString();
                    model.PayTypeName = dt.Rows[n]["PayTypeName"].ToString();
                    if (dt.Rows[n]["AppId"].ToString() != "")
                    {
                        model.AppId = int.Parse(dt.Rows[n]["AppId"].ToString());
                    }
                    if (dt.Rows[n]["Money"].ToString() != "")
                    {
                        model.Money = decimal.Parse(dt.Rows[n]["Money"].ToString());
                    }
                    if (dt.Rows[n]["Notpay"].ToString() != "")
                    {
                        model.Notpay = decimal.Parse(dt.Rows[n]["Notpay"].ToString());
                    }
                    if (dt.Rows[n]["Success"].ToString() != "")
                    {
                        model.Success = decimal.Parse(dt.Rows[n]["Success"].ToString());
                    }
                    if (dt.Rows[n]["Successratio"].ToString() != "")
                    {
                        model.Successratio = decimal.Parse(dt.Rows[n]["Successratio"].ToString());
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
