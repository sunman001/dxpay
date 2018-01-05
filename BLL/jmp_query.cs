using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.BLL
{
    ///<summary>
    ///查询记录表
    ///</summary>
    public partial class jmp_query
    {

        private readonly JMP.DAL.jmp_query dal = new JMP.DAL.jmp_query();
        public jmp_query()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int q_id, string q_code)
        {
            return dal.Exists(q_id, q_code);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_query model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_query model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int q_id)
        {

            return dal.Delete(q_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string q_idlist)
        {
            return dal.DeleteList(q_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_query GetModel(int q_id)
        {

            return dal.GetModel(q_id);
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
        public List<JMP.MDL.jmp_query> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_query> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_query> modelList = new List<JMP.MDL.jmp_query>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_query model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_query();
                    if (dt.Rows[n]["q_id"].ToString() != "")
                    {
                        model.q_id = int.Parse(dt.Rows[n]["q_id"].ToString());
                    }
                    model.q_code = dt.Rows[n]["q_code"].ToString();
                    if (dt.Rows[n]["q_time"].ToString() != "")
                    {
                        model.q_time = int.Parse(dt.Rows[n]["q_time"].ToString());
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
        /// 根据订单编号查询信息
        /// </summary>
        /// <param name="code">订单编号</param>
        /// <returns></returns>
        public JMP.MDL.jmp_query SelectCode(string code)
        {
            return dal.SelectCode(code);
        }
    }
}
