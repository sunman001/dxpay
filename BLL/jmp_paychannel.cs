using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.BLL
{
    ///<summary>
    ///支付渠道汇总
    ///</summary>
    public partial class jmp_paychannel
    {

        private readonly JMP.DAL.jmp_paychannel dal = new JMP.DAL.jmp_paychannel();
        public jmp_paychannel()
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
        public int Add(JMP.MDL.jmp_paychannel model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_paychannel model)
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
        public JMP.MDL.jmp_paychannel GetModel(int id)
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
        public List<JMP.MDL.jmp_paychannel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_paychannel> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_paychannel> modelList = new List<JMP.MDL.jmp_paychannel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_paychannel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_paychannel();
                    if (dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    model.payname = dt.Rows[n]["payname"].ToString();
                    if (dt.Rows[n]["payid"].ToString() != "")
                    {
                        model.payid = int.Parse(dt.Rows[n]["payid"].ToString());
                    }
                    if (dt.Rows[n]["money"].ToString() != "")
                    {
                        model.money = decimal.Parse(dt.Rows[n]["money"].ToString());
                    }
                    if (dt.Rows[n]["datetimes"].ToString() != "")
                    {
                        model.datetimes = DateTime.Parse(dt.Rows[n]["datetimes"].ToString());
                    }
                    model.paytype = dt.Rows[n]["paytype"].ToString();
                    if (dt.Rows[n]["success"].ToString() != "")
                    {
                        model.success = int.Parse(dt.Rows[n]["success"].ToString());
                    }
                    if (dt.Rows[n]["successratio"].ToString() != "")
                    {
                        model.successratio = decimal.Parse(dt.Rows[n]["successratio"].ToString());
                    }
                    if (dt.Rows[n]["notpay"].ToString() != "")
                    {
                        model.notpay = int.Parse(dt.Rows[n]["notpay"].ToString());
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
        /// <param name="OrderBy">排序</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="Count">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_paychannel> GetLists(string sql, string OrderBy, int PageIndex, int PageSize, out int Count)
        {
            return dal.GetLists(sql, OrderBy, PageIndex, PageSize, out  Count);
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
