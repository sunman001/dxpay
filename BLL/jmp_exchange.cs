using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    /// <summary>
    /// 工单交流表
    /// </summary>
    public partial class jmp_exchange
    {

        private readonly JMP.DAL.jmp_exchange dal = new JMP.DAL.jmp_exchange();
        public jmp_exchange()
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
        public int Add(JMP.MDL.jmp_exchange model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_exchange model)
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
        public JMP.MDL.jmp_exchange GetModel(int id)
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
        public List<JMP.MDL.jmp_exchange> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_exchange> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_exchange> modelList = new List<JMP.MDL.jmp_exchange>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_exchange model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_exchange();
                    if (dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    if (dt.Rows[n]["workorderid"].ToString() != "")
                    {
                        model.workorderid = int.Parse(dt.Rows[n]["workorderid"].ToString());
                    }
                    if (dt.Rows[n]["handlerid"].ToString() != "")
                    {
                        model.handlerid = int.Parse(dt.Rows[n]["handlerid"].ToString());
                    }
                    if (dt.Rows[n]["handledate"].ToString() != "")
                    {
                        model.handledate = DateTime.Parse(dt.Rows[n]["handledate"].ToString());
                    }
                    model.handleresultdescription = dt.Rows[n]["handleresultdescription"].ToString();


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
        public List<JMP.MDL.jmp_exchange> SelectListByworkorderid(int workorderid , int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectListByworkorderid(workorderid, pageIndexs, PageSize, out pageCount);
        }

    }
}