using System;
using System.Collections.Generic;
using System.Data;

namespace JMP.BLL
{
    //支付项目全局错误日志记录表
    public partial class DxGlobalLogError
    {

        private readonly DAL.DxGlobalLogError dal = new DAL.DxGlobalLogError();
        public DxGlobalLogError()
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
        public int Add(JMP.Model.DxGlobalLogError model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.Model.DxGlobalLogError model)
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
        public JMP.Model.DxGlobalLogError GetModel(int Id)
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
        public List<JMP.Model.DxGlobalLogError> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.Model.DxGlobalLogError> DataTableToList(DataTable dt)
        {
            List<JMP.Model.DxGlobalLogError> modelList = new List<JMP.Model.DxGlobalLogError>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.Model.DxGlobalLogError model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.Model.DxGlobalLogError();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["CreatedOn"].ToString() != "")
                    {
                        model.CreatedOn = DateTime.Parse(dt.Rows[n]["CreatedOn"].ToString());
                    }
                    if (dt.Rows[n]["ClientId"].ToString() != "")
                    {
                        model.ClientId = int.Parse(dt.Rows[n]["ClientId"].ToString());
                    }
                    model.ClientName = dt.Rows[n]["ClientName"].ToString();
                    if (dt.Rows[n]["UserId"].ToString() != "")
                    {
                        model.UserId = int.Parse(dt.Rows[n]["UserId"].ToString());
                    }
                    if (dt.Rows[n]["TypeValue"].ToString() != "")
                    {
                        model.TypeValue = int.Parse(dt.Rows[n]["TypeValue"].ToString());
                    }
                    model.IpAddress = dt.Rows[n]["IpAddress"].ToString();
                    model.Location = dt.Rows[n]["Location"].ToString();
                    model.Summary = dt.Rows[n]["Summary"].ToString();
                    model.Message = dt.Rows[n]["Message"].ToString();


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
        /// <param name="order"></param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<Model.DxGlobalLogError> SelectList(string sqls, string order, int pageIndexs, int pageSize, out int pageCount)
        {
            return dal.SelectList(sqls, order, pageIndexs, pageSize, out pageCount);
        }

    }
}