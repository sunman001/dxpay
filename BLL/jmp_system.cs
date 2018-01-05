using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    //系统配置表
    public partial class jmp_system
    {

        private readonly JMP.DAL.jmp_system dal = new JMP.DAL.jmp_system();
        public jmp_system()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int s_id)
        {
            return dal.Exists(s_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_system model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_system model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int s_id)
        {

            return dal.Delete(s_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string s_idlist)
        {
            return dal.DeleteList(s_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_system GetModel(int s_id)
        {

            return dal.GetModel(s_id);
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
        public List<JMP.MDL.jmp_system> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_system> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_system> modelList = new List<JMP.MDL.jmp_system>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_system model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_system();
                    if (dt.Rows[n]["s_id"].ToString() != "")
                    {
                        model.s_id = int.Parse(dt.Rows[n]["s_id"].ToString());
                    }
                    model.s_name = dt.Rows[n]["s_name"].ToString();
                    model.s_value = dt.Rows[n]["s_value"].ToString();
                    if (dt.Rows[n]["s_state"].ToString() != "")
                    {
                        model.s_state = int.Parse(dt.Rows[n]["s_state"].ToString());
                    }
                    model.s_remarks = dt.Rows[n]["s_remarks"].ToString();


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


        /// <summary>
        /// 查询特批value
        /// </summary>
        /// <param name="name">状态</param>
        /// <returns></returns>
        public JMP.MDL.jmp_system GetModel_name(string name)
        {
            return dal.GetModel_name(name);
        }

        #endregion

        /// <summary>
        /// 根据配置名称查询实体
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <returns></returns>
        public MDL.jmp_system FindByName(string name)
        {
            return dal.GetModel_name(name);
        }


    }
}