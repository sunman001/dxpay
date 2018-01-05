using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    ///<summary>
    ///消息表
    ///</summary>
    public partial class jmp_message
    {

        private readonly JMP.DAL.jmp_message dal = new JMP.DAL.jmp_message();
        public jmp_message()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int m_id)
        {
            return dal.Exists(m_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_message model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_message model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int m_id)
        {

            return dal.Delete(m_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string m_idlist)
        {
            return dal.DeleteList(m_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_message GetModel(int m_id)
        {

            return dal.GetModel(m_id);
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
        public List<JMP.MDL.jmp_message> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_message> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_message> modelList = new List<JMP.MDL.jmp_message>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_message model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_message();
                    if (dt.Rows[n]["m_id"].ToString() != "")
                    {
                        model.m_id = int.Parse(dt.Rows[n]["m_id"].ToString());
                    }
                    if (dt.Rows[n]["m_sender"].ToString() != "")
                    {
                        model.m_sender = int.Parse(dt.Rows[n]["m_sender"].ToString());
                    }
                    model.m_receiver = dt.Rows[n]["m_receiver"].ToString();
                    if (dt.Rows[n]["m_type"].ToString() != "")
                    {
                        model.m_type = int.Parse(dt.Rows[n]["m_type"].ToString());
                    }
                    if (dt.Rows[n]["m_time"].ToString() != "")
                    {
                        model.m_time = DateTime.Parse(dt.Rows[n]["m_time"].ToString());
                    }
                    if (dt.Rows[n]["m_state"].ToString() != "")
                    {
                        model.m_state = int.Parse(dt.Rows[n]["m_state"].ToString());
                    }
                    model.m_content = dt.Rows[n]["m_content"].ToString();
                    if (dt.Rows[n]["m_topid"].ToString() != "")
                    {
                        model.m_topid = int.Parse(dt.Rows[n]["m_topid"].ToString());
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
        /// 查询消息信息
        /// </summary>
        /// <param name="StrSql">查询语句</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_message> SelectList(string StrSql, string order, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(StrSql,order, pageIndexs, PageSize, out pageCount);
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateLocUserState(string u_idlist, int state)
        {
            return dal.UpdateLocUserState(u_idlist, state);
        }
        /// <summary>
        /// 用户删除消息操作
        /// </summary>
        /// <param name="mid">消息id</param>
        /// <param name="m_userdelete">用户id</param>
        /// <returns></returns>
        public int UserDeleteMagess(int mid, string m_userdelete)
        {
            return dal.UserDeleteMagess(mid, m_userdelete);
        }
         /// <summary>
        /// 用事物执行批量添加
        /// </summary>
        public int AdminAdd(StringBuilder strSql)
        {
            return dal.AdminAdd(strSql);
        }
         /// <summary>
        /// 查询回复消息
        /// </summary>
        /// <param name="topid"></param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_message> ReplySelect(int topid) {
            return dal.ReplySelect(topid);
        }
    }
}