using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    /// <summary>
    /// 工单
    /// </summary>
    public partial class jmp_workorder
    {

        private readonly JMP.DAL.jmp_workorder dal = new JMP.DAL.jmp_workorder();
        public jmp_workorder()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_workorder model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_workorder model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            return dal.Delete(id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            return dal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_workorder GetModel(int id)
        {

            return dal.GetModel(id);
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
        public List<JMP.MDL.jmp_workorder> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_workorder> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_workorder> modelList = new List<JMP.MDL.jmp_workorder>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_workorder model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_workorder();
                    if (dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    if (dt.Rows[n]["viewcount"].ToString() != "")
                    {
                        model.viewcount = int.Parse(dt.Rows[n]["viewcount"].ToString());
                    }
                    if (dt.Rows[n]["latestviewdate"].ToString() != "")
                    {
                        model.latestviewdate = DateTime.Parse(dt.Rows[n]["latestviewdate"].ToString());
                    }
                    if (dt.Rows[n]["score"].ToString() != "")
                    {
                        model.score = int.Parse(dt.Rows[n]["score"].ToString());
                    }
                    if (dt.Rows[n]["pushedremind"].ToString() != "")
                    {
                        if ((dt.Rows[n]["pushedremind"].ToString() == "1") || (dt.Rows[n]["pushedremind"].ToString().ToLower() == "true"))
                        {
                            model.pushedremind = true;
                        }
                        else
                        {
                            model.pushedremind = false;
                        }
                    }
                    if (dt.Rows[n]["pushreminddate"].ToString() != "")
                    {
                        model.pushreminddate = DateTime.Parse(dt.Rows[n]["pushreminddate"].ToString());
                    }
                    model.closereason = dt.Rows[n]["closereason"].ToString();
                    model.initiatorreason = dt.Rows[n]["initiatorreason"].ToString();
                    model.handlerreason = dt.Rows[n]["handlerreason"].ToString();
                    model.result = dt.Rows[n]["result"].ToString();
                    if (dt.Rows[n]["catalog"].ToString() != "")
                    {
                        model.catalog = int.Parse(dt.Rows[n]["catalog"].ToString());
                    }
                    model.title = dt.Rows[n]["title"].ToString();
                    model.content = dt.Rows[n]["content"].ToString();
                    if (dt.Rows[n]["status"].ToString() != "")
                    {
                        model.status = int.Parse(dt.Rows[n]["status"].ToString());
                    }
                    if (dt.Rows[n]["progress"].ToString() != "")
                    {
                        model.progress = int.Parse(dt.Rows[n]["progress"].ToString());
                    }
                    if (dt.Rows[n]["createdon"].ToString() != "")
                    {
                        model.createdon = DateTime.Parse(dt.Rows[n]["createdon"].ToString());
                    }
                    if (dt.Rows[n]["createdbyid"].ToString() != "")
                    {
                        model.createdbyid = int.Parse(dt.Rows[n]["createdbyid"].ToString());
                    }
                    model.watchidsoftheday = dt.Rows[n]["watchidsoftheday"].ToString();


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
        /// 根据当前时间查询当前排班人
        /// </summary>
        /// <param name="a_id">应用id</param>
        /// <returns></returns>
        public JMP.MDL.jmp_scheduling SelectByDate(DateTime datatime)
        {
            return dal.SelectByDate(datatime);
        }

        /// <summary>
        /// 查询工单列表
        /// </summary>
        /// <param name="status">工单状态</param>
        /// <param name="progress">进度</param>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="SelectState">状态</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_workorder> SelectList( string status, string progress, string sea_name, int type, int searchDesc, string stime, string endtime, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(status, progress,sea_name, type, searchDesc, stime, endtime, pageIndexs, PageSize, out pageCount);
        }

        /// <summary>
        /// 关闭工单
        /// </summary>
        /// <param name="u_idlist">ID</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateState(int  id,string state,string reason)
        {
            return dal.UpdateState(id,state, reason);
        }
        /// <summary>
        /// 处理人处理工单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public bool UpdateHandState(int id, string state, string reason)
        {
            return dal.UpdateHandState(id, state, reason);
        }


        //根据ID查询工单详细信息
        public JMP.MDL.jmp_workorder SelectId(int id)
        {
            return dal.SelectId(id);
        }

        /// <summary>
        /// 修改进度
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool UpdateProgress( int id ,int state)
        {
            return dal.UpdateProgress(id, state);
        }

        //修改响应结果
        public bool updateResult(int id , string result)
        {
            return dal.UpdateResult(id, result);
        }
        public bool UpdateView(int id,DateTime date)
        {
            return dal.UpdateView(id, date);
        }
        /// <summary>
        /// 进行评分
        /// </summary>
        /// <param name="id"></param>
        /// <param name="score"></param>
        /// <param name="scorereason"></param>
        /// <returns></returns>
        public bool UpdateScore(JMP.MDL.jmp_workorder mode)
        {
            return dal.UpdateScore(mode);
        }

        /// <summary>
        /// 获得数据列表（工单统计）
        /// </summary>
        /// <param name="sdate">开始日期</param>
        /// <param name="eate">结束日期</param>
        /// <param name="sumTypes">汇总方式（应用名称、开发者邮箱）</param>
        /// <param name="sumkeys">汇总值</param>
        /// <returns></returns>
        public DataTable GetListys(string sdate, string eate)
        {
            return dal.GetListys(sdate, eate);
        }

        /// <summary>
        /// 读取未提醒给值班人员的工单[读取后会将标识设为已提醒]
        /// </summary>
        public List<MDL.jmp_workorder> GetUnRemindWorkOrders()
        {
            var ds = dal.GetUnRemindWorkOrders();
            var lst = DataTableToList(ds.Tables[0]);
            return lst;
        }
        /// <summary>
        /// 值班人工单统计
        /// </summary>
        /// <param name="sea_name"></param>
        /// <param name="stime"></param>
        /// <param name="endtime"></param>
        /// <param name="pageIndexs"></param>
        /// <param name="PageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_workorderReport> Getlist( string sea_name, int searchDesc,  string stime, string endtime, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.Getlist( sea_name, searchDesc, stime, endtime, pageIndexs, PageSize, out pageCount);
        }
    }
}