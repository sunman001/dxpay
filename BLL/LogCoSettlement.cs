using System;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    //结算日志详情记录表
    public class LogCoSettlement
    {

        private readonly JMP.DAL.LogCoSettlement dal = new JMP.DAL.LogCoSettlement();
        public LogCoSettlement()
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
        public int Add(JMP.MDL.LogCoSettlement model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.LogCoSettlement model)
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
        public JMP.MDL.LogCoSettlement GetModel(int Id)
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
        public List<JMP.MDL.LogCoSettlement> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.LogCoSettlement> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.LogCoSettlement> modelList = new List<JMP.MDL.LogCoSettlement>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.LogCoSettlement model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.LogCoSettlement();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["UserId"].ToString() != "")
                    {
                        model.UserId = int.Parse(dt.Rows[n]["UserId"].ToString());
                    }
                    if (dt.Rows[n]["TypeId"].ToString() != "")
                    {
                        model.TypeId = int.Parse(dt.Rows[n]["TypeId"].ToString());
                    }
                    model.IpAddress = dt.Rows[n]["IpAddress"].ToString();
                    model.Location = dt.Rows[n]["Location"].ToString();
                    model.Summary = dt.Rows[n]["Summary"].ToString();
                    model.Message = dt.Rows[n]["Message"].ToString();
                    if (dt.Rows[n]["CreatedOn"].ToString() != "")
                    {
                        model.CreatedOn = DateTime.Parse(dt.Rows[n]["CreatedOn"].ToString());
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
        /// 分页查询
        /// </summary>
        /// <param name="where">WHERE查询条件,请带上WHERE关键字</param>
        /// <param name="order"></param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<JMP.MDL.LogCoSettlement> SelectList(string where, string order, int pageIndexs, int pageSize, out int pageCount)
        {
            return dal.SelectList(where, order, pageIndexs, pageSize, out pageCount);
        }

    }
}