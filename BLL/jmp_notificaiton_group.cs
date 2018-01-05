using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    //通知短信分组信息表
    public partial class jmp_notificaiton_group
    {

        private readonly JMP.DAL.jmp_notificaiton_group dal = new JMP.DAL.jmp_notificaiton_group();
        public jmp_notificaiton_group()
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
        public int Add(JMP.MDL.jmp_notificaiton_group model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_notificaiton_group model)
        {
            return dal.Update(model);
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
        public JMP.MDL.jmp_notificaiton_group GetModel(int Id)
        {

            return dal.GetModel(Id);
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
        public List<JMP.MDL.jmp_notificaiton_group> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_notificaiton_group> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_notificaiton_group> modelList = new List<JMP.MDL.jmp_notificaiton_group>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_notificaiton_group model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_notificaiton_group();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["IntervalValue"].ToString() != "")
                    {
                        model.IntervalValue = int.Parse(dt.Rows[n]["IntervalValue"].ToString());
                    }
                    model.IntervalUnit = dt.Rows[n]["IntervalUnit"].ToString();
                    if (dt.Rows[n]["CreatedOn"].ToString() != "")
                    {
                        model.CreatedOn = DateTime.Parse(dt.Rows[n]["CreatedOn"].ToString());
                    }
                    if (dt.Rows[n]["CreatedBy"].ToString() != "")
                    {
                        model.CreatedBy = int.Parse(dt.Rows[n]["CreatedBy"].ToString());
                    }
                    model.CreatedByUser = dt.Rows[n]["CreatedByUser"].ToString();
                    if (dt.Rows[n]["ModifiedOn"].ToString() != "")
                    {
                        model.ModifiedOn = DateTime.Parse(dt.Rows[n]["ModifiedOn"].ToString());
                    }
                    if (dt.Rows[n]["ModifiedBy"].ToString() != "")
                    {
                        model.ModifiedBy = int.Parse(dt.Rows[n]["ModifiedBy"].ToString());
                    }
                    model.ModifiedByUser = dt.Rows[n]["ModifiedByUser"].ToString();
                    model.Name = dt.Rows[n]["Name"].ToString();
                    model.Code = dt.Rows[n]["Code"].ToString();
                    model.SendMode = dt.Rows[n]["SendMode"].ToString();
                    model.Description = dt.Rows[n]["Description"].ToString();
                    model.NotifyMobileList = dt.Rows[n]["NotifyMobileList"].ToString();
                    model.MessageTemplate = dt.Rows[n]["MessageTemplate"].ToString();
                    model.AudioTelTempContent = dt.Rows[n]["AudioTelTempContent"].ToString();
                    if (dt.Rows[n]["AudioTelTempId"].ToString() != "")
                    {
                        model.AudioTelTempId = int.Parse(dt.Rows[n]["AudioTelTempId"].ToString());
                    }
                    if (dt.Rows[n]["IsDeleted"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsDeleted"].ToString() == "1") || (dt.Rows[n]["IsDeleted"].ToString().ToLower() == "true"))
                        {
                            model.IsDeleted = true;
                        }
                        else
                        {
                            model.IsDeleted = false;
                        }
                    }
                    if (dt.Rows[n]["IsEnabled"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsEnabled"].ToString() == "1") || (dt.Rows[n]["IsEnabled"].ToString().ToLower() == "true"))
                        {
                            model.IsEnabled = true;
                        }
                        else
                        {
                            model.IsEnabled = false;
                        }
                    }
                    if (dt.Rows[n]["IsAllowSendMessage"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsAllowSendMessage"].ToString() == "1") || (dt.Rows[n]["IsAllowSendMessage"].ToString().ToLower() == "true"))
                        {
                            model.IsAllowSendMessage = true;
                        }
                        else
                        {
                            model.IsAllowSendMessage = false;
                        }
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
        /// 查询
        /// </summary>
        /// <param name="userid">用户id（后台默认传0，开发者平台默认传用户id）</param>
        /// <param name="auditstate">处理状态</param>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="SelectState">状态</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_notificaiton_group> SelectList(string SelectState, string IntervalUnit, string sea_name, int type, int searchDesc, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(SelectState, IntervalUnit, sea_name, type, searchDesc, pageIndexs, PageSize, out pageCount);
        }

        /// <summary>
        /// 批量更新状态
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateState(string u_idlist, int state)
        {
            return dal.UpdateState(u_idlist, state);
        }

        public bool Delete(string u_idlist)
        {
            return dal.Delete(u_idlist);
        }

        /// <summary>
        /// 根据应用id查询通知短信分组信息
        /// </summary>
        /// <param name="c_id">应用id</param>
        /// <returns></returns>
        public JMP.MDL.jmp_notificaiton_group SelectId(int c_id)
        {
            return dal.SelectId(c_id);
        }
        #endregion

    }
}