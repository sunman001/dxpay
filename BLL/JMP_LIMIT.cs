using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JMP.BLL
{
    ///<summary>
    ///权限管理
    ///</summary>
    public partial class jmp_limit
    {

        private readonly JMP.DAL.jmp_limit dal = new JMP.DAL.jmp_limit();
        public jmp_limit()
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
        public int Add(JMP.MDL.jmp_limit model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_limit model)
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
        public JMP.MDL.jmp_limit GetModel(int l_id)
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
        public List<JMP.MDL.jmp_limit> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_limit> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_limit> modelList = new List<JMP.MDL.jmp_limit>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_limit model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_limit();
                    if (dt.Rows[n]["l_id"].ToString() != "")
                    {
                        model.l_id = int.Parse(dt.Rows[n]["l_id"].ToString());
                    }
                    model.l_name = dt.Rows[n]["l_name"].ToString();
                    if (dt.Rows[n]["l_topid"].ToString() != "")
                    {
                        model.l_topid = int.Parse(dt.Rows[n]["l_topid"].ToString());
                    }
                    model.l_url = dt.Rows[n]["l_url"].ToString();
                    if (dt.Rows[n]["l_sort"].ToString() != "")
                    {
                        model.l_sort = int.Parse(dt.Rows[n]["l_sort"].ToString());
                    }
                    if (dt.Rows[n]["l_state"].ToString() != "")
                    {
                        model.l_state = int.Parse(dt.Rows[n]["l_state"].ToString());
                    }
                    model.l_icon = dt.Rows[n]["l_icon"].ToString();
                    if (dt.Rows[n]["l_type"].ToString() != "")
                    {
                        model.l_type = int.Parse(dt.Rows[n]["l_type"].ToString());
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
        /// 查询方法是否有权限
        /// </summary>
        /// <param name="voids">方法名（AddDialog()</param>
        /// <param name="locUserId">用户id</param>
        /// <param name="roleId">权限id</param>
        /// <returns></returns>
        public bool GetLocUserLimitVoids(string voids, string locUserId, int roleId)
        {
            return dal.GetLocUserLimitVoids(voids, locUserId, roleId);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sqls">SQL语句</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_limit> SelectList(string sqls,string Order, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(sqls, Order, pageIndexs, PageSize, out pageCount);
        }

        /// <summary>
        /// 获取权限列表[当前用户下的所有权限]
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="rid">角色id</param>
        /// <returns></returns>
        public DataTable GetUserLimit(int uid, int rid)
        {
            string query = string.Format(@"l_id in(select a from dbo.f_split((select r_value from jmp_locuser as a,jmp_role as b 
where a.u_id={0} and b.r_id={1} and a.u_role_id=b.r_id and a.u_state=1 and b.r_state=1),','))
 and l_state=1 order by l_sort desc", uid, rid);
            return GetList(query).Tables[0];
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="rid">角色id</param>
        /// <param name="topid">父级id</param>
        /// <returns></returns>
        public DataTable GetUserLimit(int uid, int rid, int topid)
        {
            string query = string.Format(@"l_id in(select a from dbo.f_split((select r_value from jmp_locuser as a,jmp_role as b 
where a.u_id={0} and b.r_id={1} and a.u_role_id=b.r_id and a.u_state=1 and b.r_state=1),','))
and l_topid={2} and l_state=1 order by l_sort desc", uid, rid, topid);
            return GetList(query).Tables[0];
        }

        /// <summary>
        /// 获取权限列表（开发者）
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="rid">角色id</param>
        /// <param name="topid">父级id</param>
        /// <returns></returns>
        public DataTable GetAppUserLimit(int uid, int rid)
        {
            string query = string.Format(@"l_id in(select a from dbo.f_split((select r_value from jmp_user as a,jmp_role as b where a.u_id={0} and b.r_id={1} and b.r_type=1 and a.u_role_id=b.r_id and a.u_state=1 and b.r_state=1),',')) and l_state=1 and l_type=1 order by l_sort desc", uid, rid);
            return GetList(query).Tables[0];
        }

        /// <summary>
        /// 获取权限列表（商务）
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="rid">角色id</param>
        /// <param name="topid">父级id</param>
        /// <returns></returns>
        public DataTable GetAppBusinessLimit(int uid, int rid)
        {
            string query = string.Format(@"l_id in
            (select a from dbo.f_split((select r_value from CoBusinessPersonnel as a,jmp_role as b 
            where a.Id={0} and b.r_id={1} and a.RoleId=b.r_id and a.State=0 and b.r_state=1),',')) and l_state=1
            order by l_sort desc", uid, rid);
            return GetList(query).Tables[0];
        }

        /// <summary>
        /// 获取权限列表（代理商）
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="rid">角色id</param>
        /// <param name="topid">父级id</param>
        /// <returns></returns>
        public DataTable GetAppAgentLimit(int uid, int rid)
        {
            string query = string.Format(@"l_id in
            (select a from dbo.f_split((select r_value from CoAgent as a,jmp_role as b 
            where a.Id={0} and b.r_id={1} and a.RoleId=b.r_id and a.State=0 and b.r_state=1),',')) and l_state=1
            order by l_sort desc", uid, rid);
            return GetList(query).Tables[0];
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="rid">角色id</param>
        /// <param name="topid">父级id</param>
        /// <returns></returns>
        public DataTable GetUserLimitSession(int uid, int rid)
        {
            string query = string.Format(@"l_id in
            (select a from dbo.f_split((select r_value from JMP_LOCUSER as a,jmp_role as b 
            where a.u_id={0} and b.r_id={1} and a.u_role_id=b.r_id and a.u_state=1 and b.r_state=1),',')) and l_state=1
            order by l_sort desc", uid, rid);
            return GetList(query).Tables[0];
        }

      public DataTable GetBusinessLimitSession(int uid, int rid)
        {
            string query = string.Format(@"l_id in
            (select a from dbo.f_split((select r_value from CoBusinessPersonnel as a,jmp_role as b 
            where a.Id={0} and b.r_id={1} and a.RoleId=b.r_id and a.State=0 and b.r_state=1),',')) and l_state=1
            order by l_sort desc", uid, rid);
            return GetList(query).Tables[0];
        }


        public DataTable GetAgentLimitSession(int uid, int rid)
        {
            string query = string.Format(@"l_id in
            (select a from dbo.f_split((select r_value from CoAgent as a,jmp_role as b 
            where a.Id={0} and b.r_id={1} and a.RoleId=b.r_id and a.State=0 and b.r_state=1),',')) and l_state=1
            order by l_sort desc", uid, rid);
            return GetList(query).Tables[0];
        }

        public string getrolelimit(int rid)
        {
            return dal.getrolelimit(rid);
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="rid">角色id</param>
        /// <param name="topid">父级id</param>
        /// <returns></returns>
        public DataTable GetAppUserLimitSession(int uid, int rid)
        {
            string query = string.Format(@"l_id in(select a from dbo.f_split((select r_value from jmp_user as a,jmp_role as b where a.u_id={0} and b.r_id={1} and b.r_type=1 and a.u_role_id=b.r_id and a.u_state=1 and b.r_state=1),',')) and l_state=1 and l_type=1 order by l_sort desc", uid, rid);
            return GetList(query).Tables[0];
        }

        /// <summary>
        /// 批量更新状态
        /// <param name="l_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// </summary>
        public bool UpdateLimitState(string l_idlist, int state)
        {
            return dal.UpdateLimitState(l_idlist, state);
        }

        /// <summary>
        /// 获取单个值
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns></returns>
        public object RunSql(string strSql)
        {
            return dal.GetSingle(strSql);
        }

        /// <summary>
        /// 根据URL路径查询对应的权限数据集
        /// </summary>
        /// <param name="path">URL路径</param>
        /// <returns></returns>
        public MDL.jmp_limit GetByUrlPath(string path)
        {
            var ds = dal.GetByUrlPath(path);
            var model = DataTableToList(ds.Tables[0]).FirstOrDefault();
            return model;
        }
    }
}