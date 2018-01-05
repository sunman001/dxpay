using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.MDL;

namespace JMP.BLL
{
    ///<summary>
    ///账单表
    ///</summary>
    public partial class jmp_bill
    {

        private readonly JMP.DAL.jmp_bill dal = new JMP.DAL.jmp_bill();
        public jmp_bill()
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
        public int Add(JMP.MDL.jmp_bill model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_bill model)
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
        public JMP.MDL.jmp_bill GetModel(int b_id)
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
        public List<JMP.MDL.jmp_bill> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_bill> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_bill> modelList = new List<JMP.MDL.jmp_bill>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_bill model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_bill();
                    if (dt.Rows[n]["b_id"].ToString() != "")
                    {
                        model.b_id = int.Parse(dt.Rows[n]["b_id"].ToString());
                    }
                    if (dt.Rows[n]["SdWxOfficalAccountPay"].ToString() != "")
                    {
                        model.SdWxOfficalAccountPay = decimal.Parse(dt.Rows[n]["SdWxOfficalAccountPay"].ToString());
                    }
                    if (dt.Rows[n]["SdWxAppPay"].ToString() != "")
                    {
                        model.SdWxAppPay = decimal.Parse(dt.Rows[n]["SdWxAppPay"].ToString());
                    }
                    if (dt.Rows[n]["SdWxQrCodePay"].ToString() != "")
                    {
                        model.SdWxQrCodePay = decimal.Parse(dt.Rows[n]["SdWxQrCodePay"].ToString());
                    }
                    if (dt.Rows[n]["SdAliQrCodePay"].ToString() != "")
                    {
                        model.SdAliQrCodePay = decimal.Parse(dt.Rows[n]["SdAliQrCodePay"].ToString());
                    }
                    if (dt.Rows[n]["BlTotalAmount"].ToString() != "")
                    {
                        model.BlTotalAmount = decimal.Parse(dt.Rows[n]["BlTotalAmount"].ToString());
                    }
                    if (dt.Rows[n]["BlAliPay"].ToString() != "")
                    {
                        model.BlAliPay = decimal.Parse(dt.Rows[n]["BlAliPay"].ToString());
                    }
                    if (dt.Rows[n]["BlWxPay"].ToString() != "")
                    {
                        model.BlWxPay = decimal.Parse(dt.Rows[n]["BlWxPay"].ToString());
                    }
                    if (dt.Rows[n]["BlUnionPay"].ToString() != "")
                    {
                        model.BlUnionPay = decimal.Parse(dt.Rows[n]["BlUnionPay"].ToString());
                    }
                    if (dt.Rows[n]["BlWxOfficalAccountPay"].ToString() != "")
                    {
                        model.BlWxOfficalAccountPay = decimal.Parse(dt.Rows[n]["BlWxOfficalAccountPay"].ToString());
                    }
                    if (dt.Rows[n]["BlWxAppPay"].ToString() != "")
                    {
                        model.BlWxAppPay = decimal.Parse(dt.Rows[n]["BlWxAppPay"].ToString());
                    }
                    if (dt.Rows[n]["OrderDate"].ToString() != "")
                    {
                        model.OrderDate = DateTime.Parse(dt.Rows[n]["OrderDate"].ToString());
                    }
                    if (dt.Rows[n]["BlWxQrCodePay"].ToString() != "")
                    {
                        model.BlWxQrCodePay = decimal.Parse(dt.Rows[n]["BlWxQrCodePay"].ToString());
                    }
                    if (dt.Rows[n]["BlAliQrCodePay"].ToString() != "")
                    {
                        model.BlAliQrCodePay = decimal.Parse(dt.Rows[n]["BlAliQrCodePay"].ToString());
                    }
                    if (dt.Rows[n]["UserId"].ToString() != "")
                    {
                        model.UserId = int.Parse(dt.Rows[n]["UserId"].ToString());
                    }
                    model.UserName = dt.Rows[n]["UserName"].ToString();
                    if (dt.Rows[n]["CreatedOn"].ToString() != "")
                    {
                        model.CreatedOn = DateTime.Parse(dt.Rows[n]["CreatedOn"].ToString());
                    }
                    if (dt.Rows[n]["SdTotalAmount"].ToString() != "")
                    {
                        model.SdTotalAmount = decimal.Parse(dt.Rows[n]["SdTotalAmount"].ToString());
                    }
                    if (dt.Rows[n]["SdAliPay"].ToString() != "")
                    {
                        model.SdAliPay = decimal.Parse(dt.Rows[n]["SdAliPay"].ToString());
                    }
                    if (dt.Rows[n]["SdWxPay"].ToString() != "")
                    {
                        model.SdWxPay = decimal.Parse(dt.Rows[n]["SdWxPay"].ToString());
                    }
                    if (dt.Rows[n]["SdUnionPay"].ToString() != "")
                    {
                        model.SdUnionPay = decimal.Parse(dt.Rows[n]["SdUnionPay"].ToString());
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
        /// 根据id字符串查询提款总金额
        /// </summary>
        /// <param name="bid">id字符串</param>
        /// <returns></returns>
        public JMP.MDL.jmp_bill GetselectSum(string bid)
        {
            return dal.GetselectSum(bid);
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateLocUserState(string u_idlist)
        {
            return dal.UpdateLocUserState(u_idlist);
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


        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="Order">排序字段</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="Count">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_bill> GetLists(string sql, string Order, int PageIndex, int PageSize, out int Count)
        {
            return dal.GetLists(sql, Order, PageIndex, PageSize, out Count);
        }
        /// <summary>
        /// 根据sql查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable SelectSum(string sql)
        {
            return dal.SelectSum(sql);
        }
    }
}