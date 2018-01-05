using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.DAL;
using JMP.MDL;
namespace JMP.BLL
{
    ///<summary>
    ///商户表
    ///</summary>
    public partial class jmp_merchant
    {

        private readonly JMP.DAL.jmp_merchant dal = new JMP.DAL.jmp_merchant();
        public jmp_merchant()
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
        public int Add(JMP.MDL.jmp_merchant model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_merchant model)
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
        public JMP.MDL.jmp_merchant GetModel(int m_id)
        {

            return dal.GetModel(m_id);
        }
         /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_merchant GetModel(string m_loginname)
        {
            return dal.GetModel(m_loginname);
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
        public List<JMP.MDL.jmp_merchant> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_merchant> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_merchant> modelList = new List<JMP.MDL.jmp_merchant>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_merchant model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_merchant();
                    if (dt.Rows[n]["m_id"].ToString() != "")
                    {
                        model.m_id = int.Parse(dt.Rows[n]["m_id"].ToString());
                    }
                    model.m_loginname = dt.Rows[n]["m_loginname"].ToString();
                    model.m_pwd = dt.Rows[n]["m_pwd"].ToString();
                    model.m_realname = dt.Rows[n]["m_realname"].ToString();
                    if (dt.Rows[n]["m_count"].ToString() != "")
                    {
                        model.m_count = int.Parse(dt.Rows[n]["m_count"].ToString());
                    }
                    if (dt.Rows[n]["m_state"].ToString() != "")
                    {
                        model.m_state = int.Parse(dt.Rows[n]["m_state"].ToString());
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


        public bool UpdateState(int state, string ids)
        {
            return dal.UpdateState(state,ids);
        }
    }
}