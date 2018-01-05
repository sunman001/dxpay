using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.MDL;
namespace JMP.BLL
{
    //管理员用户日志
    public partial class jmp_locuserlog
    {

        private readonly JMP.DAL.jmp_locuserlog dal = new JMP.DAL.jmp_locuserlog();
        public jmp_locuserlog()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int l_id)
        {
            return dal.Exists(l_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_locuserlog model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_locuserlog model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int l_id)
        {

            return dal.Delete(l_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string l_idlist)
        {
            return dal.DeleteList(l_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_locuserlog GetModel(int l_id)
        {

            return dal.GetModel(l_id);
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
        public List<JMP.MDL.jmp_locuserlog> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_locuserlog> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_locuserlog> modelList = new List<JMP.MDL.jmp_locuserlog>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_locuserlog model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_locuserlog();
                    if (dt.Rows[n]["l_id"].ToString() != "")
                    {
                        model.l_id = int.Parse(dt.Rows[n]["l_id"].ToString());
                    }
                    if (dt.Rows[n]["l_user_id"].ToString() != "")
                    {
                        model.l_user_id = int.Parse(dt.Rows[n]["l_user_id"].ToString());
                    }
                    if (dt.Rows[n]["l_logtype_id"].ToString() != "")
                    {
                        model.l_logtype_id = int.Parse(dt.Rows[n]["l_logtype_id"].ToString());
                    }
                    model.l_ip = dt.Rows[n]["l_ip"].ToString();
                    model.l_location = dt.Rows[n]["l_location"].ToString();
                    model.l_info = dt.Rows[n]["l_info"].ToString();
                    model.l_sms = dt.Rows[n]["l_sms"].ToString();
                    if (dt.Rows[n]["l_time"].ToString() != "")
                    {
                        model.l_time = DateTime.Parse(dt.Rows[n]["l_time"].ToString());
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
        /// 分页查询
        /// </summary>
        /// <param name="sqls">SQL语句</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_locuserlog> SelectList(string sqls, string Order, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(sqls, Order, pageIndexs, PageSize, out pageCount);
        }
    }
}