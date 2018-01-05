using JMP.Model.Query;
using System.Collections.Generic;
using System.Data;

namespace JMP.BLL
{
    //角色表
    public partial class jmp_role
    {

        private readonly JMP.DAL.jmp_role dal = new JMP.DAL.jmp_role();
        public jmp_role()
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
        public int Add(JMP.MDL.jmp_role model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_role model)
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
        public JMP.MDL.jmp_role GetModel(int r_id)
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
        public List<JMP.MDL.jmp_role> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_role> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_role> modelList = new List<JMP.MDL.jmp_role>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_role model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_role();
                    if (dt.Rows[n]["r_id"].ToString() != "")
                    {
                        model.r_id = int.Parse(dt.Rows[n]["r_id"].ToString());
                    }
                    if (dt.Rows[n]["r_type"].ToString() != "")
                    {
                        model.r_type = int.Parse(dt.Rows[n]["r_type"].ToString());
                    }
                    model.r_name = dt.Rows[n]["r_name"].ToString();
                    model.r_value = dt.Rows[n]["r_value"].ToString();
                    if (dt.Rows[n]["r_state"].ToString() != "")
                    {
                        model.r_state = int.Parse(dt.Rows[n]["r_state"].ToString());
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
        public List<JMP.MDL.jmp_role> SelectList(string sqls,string Order, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(sqls, Order, pageIndexs, PageSize, out pageCount);
        }

        /// <summary>
        /// 更新权限
        /// </summary>
        public bool UpdateValue(JMP.MDL.jmp_role model)
        {
            return dal.UpdateValue(model);
        }

        /// <summary>
        /// 查询运营平台的角色-权限映射关系集合
        /// </summary>
        /// <returns></returns>
        public List<RolePermissionMappingQueryModel> FindAdminRolePermissionMappingList()
        {
            return dal.FindAdminRolePermissionMappingList();
        }
    }
}