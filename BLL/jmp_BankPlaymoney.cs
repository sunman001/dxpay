using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.BLL
{
    //银行打款对接表
    public partial class jmp_BankPlaymoney
    {

        private readonly JMP.DAL.jmp_BankPlaymoney dal = new JMP.DAL.jmp_BankPlaymoney();
        public jmp_BankPlaymoney()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int b_id)
        {
            return dal.Exists(b_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_BankPlaymoney model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_BankPlaymoney model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int b_id)
        {

            return dal.Delete(b_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string b_idlist)
        {
            return dal.DeleteList(b_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_BankPlaymoney GetModel(int b_id)
        {

            return dal.GetModel(b_id);
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
        public List<JMP.MDL.jmp_BankPlaymoney> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_BankPlaymoney> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_BankPlaymoney> modelList = new List<JMP.MDL.jmp_BankPlaymoney>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_BankPlaymoney model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_BankPlaymoney();
                    if (dt.Rows[n]["b_id"].ToString() != "")
                    {
                        model.b_id = int.Parse(dt.Rows[n]["b_id"].ToString());
                    }
                    if (dt.Rows[n]["b_payfashion"].ToString() != "")
                    {
                        model.b_payfashion = int.Parse(dt.Rows[n]["b_payfashion"].ToString());
                    }
                    model.b_remark = dt.Rows[n]["b_remark"].ToString();
                    if (dt.Rows[n]["b_payforanotherId"].ToString() != "")
                    {
                        model.b_payforanotherId = int.Parse(dt.Rows[n]["b_payforanotherId"].ToString());
                    }
                    model.b_batchnumber = dt.Rows[n]["b_batchnumber"].ToString();
                    model.b_number = dt.Rows[n]["b_number"].ToString();
                    model.b_tradeno = dt.Rows[n]["b_tradeno"].ToString();
                    if (dt.Rows[n]["b_tradestate"].ToString() != "")
                    {
                        model.b_tradestate = int.Parse(dt.Rows[n]["b_tradestate"].ToString());
                    }
                    if (dt.Rows[n]["b_date"].ToString() != "")
                    {
                        model.b_date = DateTime.Parse(dt.Rows[n]["b_date"].ToString());
                    }
                    if (dt.Rows[n]["b_bankid"].ToString() != "")
                    {
                        model.b_bankid = int.Parse(dt.Rows[n]["b_bankid"].ToString());
                    }
                    if (dt.Rows[n]["b_money"].ToString() != "")
                    {
                        model.b_money = decimal.Parse(dt.Rows[n]["b_money"].ToString());
                    }
                    if (dt.Rows[n]["b_paydate"].ToString() != "")
                    {
                        model.b_paydate = DateTime.Parse(dt.Rows[n]["b_paydate"].ToString());
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
        /// 获取数据列表
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="Order">排序字段</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="Count">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_BankPlaymoney> GetLists(string sql, string Order, int PageIndex, int PageSize, out int Count)
        {
            return dal.GetLists(sql, Order, PageIndex, PageSize, out Count);
        }

        /// <summary>
        /// 审核提款
        /// </summary>
        /// <param name="batchnumber"></param>
        /// <param name="state"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool UpdateState(string batchnumber, int state, string name, int payId)
        {
            return dal.UpdateState(batchnumber, state, name, payId);
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int SelectBankPayMoney(List<string> sqlstr)
        {
            return dal.SelectBankPayMoney(sqlstr);
        }

        /// <summary>
        /// 根据sql查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable SelectSQL(string sql)
        {

            return dal.SelectSQL(sql);
        }

        /// <summary>
        /// 根据代付结果修改相对应的状态
        /// </summary>
        /// <param name="batchnumber">提款批次号</param>
        /// <param name="tradestate">交易状态</param>
        /// <param name="number">交易编号</param>
        /// <param name="tradeno">交易流水号</param>
        /// <param name="paydate">到账日期</param>
        /// <returns></returns>
        public bool UpdateBankPayHD(string batchnumber, int tradestate, string number, string tradeno, DateTime paydate)
        {
            return dal.UpdateBankPayHD(batchnumber, tradestate, number, tradeno, paydate);
        }

        /// <summary>
        /// 根据交易编号修改交易流水号
        /// </summary>
        /// <param name="tradestate">交易状态</param>
        /// <param name="number">交易编号</param>
        /// <param name="tradeno">交易流水号</param>
        /// <returns></returns>
        public bool UpdateBankPayTradeno(int tradestate, string number, string tradeno)
        {
            return dal.UpdateBankPayTradeno(tradestate, number, tradeno);
        }


        /// <summary>
        /// 修改交易编号
        /// </summary>
        /// <param name="batchnumber">批次号</param>
        /// <param name="number">交易编号</param>
        /// <param name="payfashion">交易方式</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public bool UpdateBankPayNumber(string batchnumber, string number, int payfashion, string remark)
        {
            return dal.UpdateBankPayNumber(batchnumber, number, payfashion, remark);
        }

        /// <summary>
        /// 修改状态码
        /// </summary>
        /// <param name="batchnumber">批次号</param>
        /// <param name="number">状态码</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public bool UpdateBankPayTradestate(string batchnumber, int tradestate, string remark)
        {
            return dal.UpdateBankPayTradestate(batchnumber, tradestate, remark);
        }

        /// <summary>
        /// 根据交易编号修改交易流水号
        /// </summary>
        /// <param name="tradestate">交易状态</param>
        /// <param name="number">交易编号</param>
        /// <returns></returns>
        public bool UpdateBankPayNumberTradestate(int tradestate, string number)
        {
            return dal.UpdateBankPayNumberTradestate(tradestate, number);
        }


        /// <summary>
        /// 手动打款
        /// </summary>
        /// <param name="batchnumber">提款批次号</param>
        /// <param name="tradestate">交易状态</param>
        /// <param name="number">交易编号</param>
        /// <param name="tradeno">交易流水号</param>
        /// <param name="paydate">到账日期</param>
        /// <param name="payfashion">交易类型</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public bool UpdateBankPayHandMovement(string batchnumber, int tradestate, string number, string tradeno, DateTime paydate, int payfashion, string remark)
        {
            return dal.UpdateBankPayHandMovement(batchnumber, tradestate, number, tradeno, paydate, payfashion, remark);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="batchnumber">批次号</param>
        /// <returns></returns>
        public JMP.MDL.jmp_BankPlaymoney GetBankPlaymoneyModel(string batchnumber)
        {
            return dal.GetBankPlaymoneyModel(batchnumber);
        }
        /// <summary>
        /// 根据sql语句查询信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable CountSect(string sql)
        {
            return dal.CountSect(sql);
        }
    }
}
