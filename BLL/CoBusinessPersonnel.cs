using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JMP.DAL;
using JMP.MDL;

namespace JMP.BLL
{
    //商务信息表
    public partial class CoBusinessPersonnel
    {

        private readonly JMP.DAL.CoBusinessPersonnel dal = new JMP.DAL.CoBusinessPersonnel();
        public CoBusinessPersonnel()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.CoBusinessPersonnel model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.CoBusinessPersonnel model)
        {
            return dal.Update(model);
        }


        /// <summary>
        /// 批量更新商务状态
        /// </summary>
        /// <param name="coid">商务id列表</param>
        /// <param name="state">状态值</param>
        /// <returns></returns>
        public bool UpdateState(string coid, int state)
        {
            return dal.UpdateState(coid, state);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            return dal.Delete(Id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            return dal.DeleteList(Idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.CoBusinessPersonnel GetModel(int Id)
        {

            return dal.GetModel(Id);
        }

        /// <summary>
        /// 是否存在该登录账号
        /// </summary>
        /// <param name="lname">登录账号</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public bool ExistsLogName(string temp, string userid = "")
        {

            return dal.ExistsLogName(temp, userid);
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
        public List<JMP.MDL.CoBusinessPersonnel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);

        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.CoBusinessPersonnel> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.CoBusinessPersonnel> modelList = new List<JMP.MDL.CoBusinessPersonnel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.CoBusinessPersonnel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.CoBusinessPersonnel();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["CreatedById"].ToString() != "")
                    {
                        model.CreatedById = int.Parse(dt.Rows[n]["CreatedById"].ToString());
                    }
                    model.CreatedByName = dt.Rows[n]["CreatedByName"].ToString();
                    if (dt.Rows[n]["LoginCount"].ToString() != "")
                    {
                        model.LoginCount = int.Parse(dt.Rows[n]["LoginCount"].ToString());
                    }
                    if (dt.Rows[n]["State"].ToString() != "")
                    {
                        model.State = int.Parse(dt.Rows[n]["State"].ToString());
                    }
                    if (dt.Rows[n]["RoleId"].ToString() != "")
                    {
                        model.RoleId = int.Parse(dt.Rows[n]["RoleId"].ToString());
                    }
                    if (dt.Rows[n]["CreatedOn"].ToString() != "")
                    {
                        model.CreatedOn = DateTime.Parse(dt.Rows[n]["CreatedOn"].ToString());
                    }
                    model.LoginName = dt.Rows[n]["LoginName"].ToString();
                    model.Password = dt.Rows[n]["Password"].ToString();
                    model.DisplayName = dt.Rows[n]["DisplayName"].ToString();
                    model.EmailAddress = dt.Rows[n]["EmailAddress"].ToString();
                    model.MobilePhone = dt.Rows[n]["MobilePhone"].ToString();
                    model.QQ = dt.Rows[n]["QQ"].ToString();
                    model.Website = dt.Rows[n]["Website"].ToString();
                    if (dt.Rows[n]["LogintTime"].ToString() != "")
                    {
                        model.LogintTime = DateTime.Parse(dt.Rows[n]["LogintTime"].ToString());
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

        /// <summary>
        /// 查询所有商务信息
        /// </summary>
        /// <param name="pageIndexs"></param>
        /// <param name="PageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>

        public List<JMP.MDL.CoBusinessPersonnel> SelectList(string s_type, string s_keys, string s_state, int pageIndexs, int PageSize, out int pageCount)
        {

            return dal.SelectList(s_type, s_keys, s_state, pageIndexs, PageSize, out pageCount);
        }

        /// <summary>
        /// 随机获取一个当天登录过的商务
        /// </summary>
        /// <returns></returns>
        public JMP.MDL.CoBusinessPersonnel getModelLoginTime()
        {
            return dal.getModelLoginTime();
        }


        #endregion
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.CoBusinessPersonnel GetModel(string userName)
        {
            return dal.GetModel(userName);
        }

    }
}
