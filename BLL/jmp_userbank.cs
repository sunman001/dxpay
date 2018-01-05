using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JMP.BLL
{
    //提款银行卡表
    public partial class jmp_userbank
    {

        private readonly JMP.DAL.jmp_userbank dal = new JMP.DAL.jmp_userbank();
        public jmp_userbank()
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
        public int Add(JMP.MDL.jmp_userbank model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_userbank model)
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
        public JMP.MDL.jmp_userbank GetModel(int u_id)
        {

            return dal.GetModel(u_id);
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
        public List<JMP.MDL.jmp_userbank> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_userbank> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_userbank> modelList = new List<JMP.MDL.jmp_userbank>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_userbank model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_userbank();
                    if (dt.Rows[n]["u_id"].ToString() != "")
                    {
                        model.u_id = int.Parse(dt.Rows[n]["u_id"].ToString());
                    }
                    if (dt.Rows[n]["u_date"].ToString() != "")
                    {
                        model.u_date = DateTime.Parse(dt.Rows[n]["u_date"].ToString());
                    }
                    if (dt.Rows[n]["u_freeze"].ToString() != "")
                    {
                        model.u_freeze = int.Parse(dt.Rows[n]["u_freeze"].ToString());
                    }
                    model.u_province = dt.Rows[n]["u_province"].ToString();
                    model.u_area = dt.Rows[n]["u_area"].ToString();
                    model.u_flag = dt.Rows[n]["u_flag"].ToString();
                    if (dt.Rows[n]["u_userid"].ToString() != "")
                    {
                        model.u_userid = int.Parse(dt.Rows[n]["u_userid"].ToString());
                    }
                    model.u_banknumber = dt.Rows[n]["u_banknumber"].ToString();
                    model.u_bankname = dt.Rows[n]["u_bankname"].ToString();
                    model.u_openbankname = dt.Rows[n]["u_openbankname"].ToString();
                    model.u_name = dt.Rows[n]["u_name"].ToString();
                    if (dt.Rows[n]["u_state"].ToString() != "")
                    {
                        model.u_state = int.Parse(dt.Rows[n]["u_state"].ToString());
                    }
                    model.u_remarks = dt.Rows[n]["u_remarks"].ToString();
                    model.u_auditor = dt.Rows[n]["u_auditor"].ToString();


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
        ///  查询开发者绑定的银行卡信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="searchType">搜索条件</param>
        /// <param name="banknumber">搜索信息</param>
        /// <param name="flag">付款标识</param>
        /// <param name="state">审核状态</param>
        /// <param name="freeze">冻结状态</param>
        /// <param name="pageIndexs"></param>
        /// <param name="PageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_userbank> SelectUserBankList(int id, string searchType, string banknumber, string flag, string state, string freeze, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectUserBankList(id, searchType, banknumber, flag, state, freeze, pageIndexs, PageSize, out pageCount);
        }



        /// <summary>
        ///  查询开发者绑定的银行卡信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="searchType">搜索条件</param>
        /// <param name="banknumber">搜索信息</param>
        /// <param name="flag">付款标识</param>
        /// <param name="pageIndexs"></param>
        /// <param name="PageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_userbank> SelectUserBankListStart(int id, string searchType, string banknumber, string flag, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectUserBankListStart(id, searchType, banknumber, flag, pageIndexs, PageSize, out pageCount);
        }


        /// <summary>
        /// 是否存在该银行卡号
        /// </summary>
        /// <param name="yyzz">银行卡号</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public bool ExistsBankNo(string yyzz, string uid)
        {
            return dal.ExistsBankNo(yyzz, uid);
        }
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="Order">排序字段</param>
        /// <param name="OrderType">排序方式</param>
        /// <param name="currPage">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_userbank> GetAppuserbankLists(string sql, string Order, int currPage, int pageSize, out int pageCount)
        {
            return dal.GetAppUserBankLists(sql, Order, currPage, pageSize, out pageCount);
        }

        /// <summary>
        /// 批量更新用户状态
        /// </summary>
        /// <param name="uids">用户id列表</param>
        /// <param name="state">状态值</param>
        /// <returns></returns>
        public bool UpdateState(string uids, int state)
        {
            return dal.UpdateState(uids, state);
        }

        /// <summary>
        /// 更新用户状态
        /// </summary>
        /// <param name="uids">用户ID</param>
        /// <param name="state">状态</param>
        /// <param name="name">审核人</param>
        /// <returns></returns>
        public bool UpdateAuditState(int uids, int state, string name, string u_remarks)
        {
            return dal.UpdateAuditState(uids, state, name, u_remarks);
        }


        /// <summary>
        /// 根据交易批次号查询数据
        /// </summary>
        /// <param name="batchnumber">交易批次号</param>
        /// <returns></returns>
        public JMP.MDL.jmp_userbank SelectUserBank_Paymoney(string batchnumber)
        {
            return dal.SelectUserBank_paymoney(batchnumber);
        }

        /// <summary>
        /// 查询冻结的银行卡数据信息
        /// </summary>
        /// <param name="u_banknumber"></param>
        /// <param name="u_userid"></param>
        /// <returns></returns>
        public JMP.MDL.jmp_userbank GetUserBankByBankNo(string u_banknumber,int   u_userid)
        {
            return dal.GetUserBankByBankNo(u_banknumber, u_userid);
        }
    }
}
