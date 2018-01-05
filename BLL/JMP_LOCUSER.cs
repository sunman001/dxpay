using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.MDL;
namespace JMP.BLL
{
    //管理员用户表
    public partial class jmp_locuser
    {
        private readonly JMP.DAL.jmp_locuser dal = new JMP.DAL.jmp_locuser();
        public jmp_locuser()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int u_id)
        {
            return dal.Exists(u_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_locuser model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_locuser model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int u_id)
        {
            return dal.Delete(u_id);
        }

        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string u_idlist)
        {
            return dal.DeleteList(u_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_locuser GetModel(int u_id)
        {
            return dal.GetModel(u_id);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_locuser GetModel(string userName)
        {
            return dal.GetModel(userName);
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
        public List<JMP.MDL.jmp_locuser> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_locuser> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_locuser> modelList = new List<JMP.MDL.jmp_locuser>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_locuser model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_locuser();
                    if (dt.Rows[n]["u_id"].ToString() != "")
                    {
                        model.u_id = int.Parse(dt.Rows[n]["u_id"].ToString());
                    }
                    if (dt.Rows[n]["u_role_id"].ToString() != "")
                    {
                        model.u_role_id = int.Parse(dt.Rows[n]["u_role_id"].ToString());
                    }
                    model.u_loginname = dt.Rows[n]["u_loginname"].ToString();
                    model.u_pwd = dt.Rows[n]["u_pwd"].ToString();
                    model.u_realname = dt.Rows[n]["u_realname"].ToString();
                    model.u_department = dt.Rows[n]["u_department"].ToString();
                    model.u_position = dt.Rows[n]["u_position"].ToString();
                    if (dt.Rows[n]["u_count"].ToString() != "")
                    {
                        model.u_count = int.Parse(dt.Rows[n]["u_count"].ToString());
                    }
                    if (dt.Rows[n]["u_state"].ToString() != "")
                    {
                        model.u_state = int.Parse(dt.Rows[n]["u_state"].ToString());
                    }
                    model.u_mobilenumber = dt.Rows[n]["u_mobilenumber"].ToString();
                    model.u_emailaddress = dt.Rows[n]["u_emailaddress"].ToString();
                    model.u_qq = dt.Rows[n]["u_qq"].ToString();

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

        DataTable dt = new DataTable();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sqls">SQL语句</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_locuser> SelectList(string sqls, string Order, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(sqls, Order, pageIndexs, PageSize, out pageCount);
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
        #endregion

        /// <summary>
        /// 根据用户名判断是否存在该记录
        /// </summary>
        /// <param name="u_loginname">登录名</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public bool ExistsName(string u_loginname, string uid = "")
        {
            return dal.ExistsName(u_loginname, uid);
        }

    }
}