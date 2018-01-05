using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.MDL;
namespace JMP.BLL
{
    ///<summary>
    ///应用上传
    ///</summary>
    public partial class jmp_appsdk
    {

        private readonly JMP.DAL.jmp_appsdk dal = new JMP.DAL.jmp_appsdk();
        public jmp_appsdk()
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
        public int Add(JMP.MDL.jmp_appsdk model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_appsdk model)
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
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            return dal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_appsdk GetModel(int id)
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
        public List<JMP.MDL.jmp_appsdk> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_appsdk> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_appsdk> modelList = new List<JMP.MDL.jmp_appsdk>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_appsdk model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_appsdk();
                    if (dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    if (dt.Rows[n]["appid"].ToString() != "")
                    {
                        model.appid = int.Parse(dt.Rows[n]["appid"].ToString());
                    }
                    model.appurl = dt.Rows[n]["appurl"].ToString();
                    if (dt.Rows[n]["uptimes"].ToString() != "")
                    {
                        model.uptimes = DateTime.Parse(dt.Rows[n]["uptimes"].ToString());
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
        /// 根据应用id查询上传信息
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        public JMP.MDL.jmp_appsdk SelectModel(int appid)
        {
            return dal.SelectModel(appid);
        }
    }
}