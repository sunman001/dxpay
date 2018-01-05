using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    /// <summary>
    /// 风险等级类型表
    /// </summary>
    public partial class jmp_risklevel
    {

        private readonly JMP.DAL.jmp_risklevel dal = new JMP.DAL.jmp_risklevel();
        public jmp_risklevel()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int r_id)
        {
            return dal.Exists(r_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_risklevel model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_risklevel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int r_id)
        {

            return dal.Delete(r_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string r_idlist)
        {
            return dal.DeleteList(r_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_risklevel GetModel(int r_id)
        {

            return dal.GetModel(r_id);
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
        public List<JMP.MDL.jmp_risklevel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_risklevel> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_risklevel> modelList = new List<JMP.MDL.jmp_risklevel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_risklevel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_risklevel();
                    if (dt.Rows[n]["r_id"].ToString() != "")
                    {
                        model.r_id = int.Parse(dt.Rows[n]["r_id"].ToString());
                    }
                    model.r_name = dt.Rows[n]["r_name"].ToString();


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

    }
}