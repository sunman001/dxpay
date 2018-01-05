using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    //投诉类型表
    public partial class CsComplainType
    {

        private readonly JMP.DAL.CsComplainType dal = new JMP.DAL.CsComplainType();
        public CsComplainType()
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
        public int Add(JMP.MDL.CsComplainType model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.CsComplainType model)
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
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.CsComplainType GetModel(int Id)
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
        public List<JMP.MDL.CsComplainType> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.CsComplainType> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.CsComplainType> modelList = new List<JMP.MDL.CsComplainType>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.CsComplainType model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.CsComplainType();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    model.Name = dt.Rows[n]["Name"].ToString();
                    model.Description = dt.Rows[n]["Description"].ToString();
                    if (dt.Rows[n]["state"].ToString() != "")
                    {
                        model.state = int.Parse(dt.Rows[n]["state"].ToString());
                    }
                    if (dt.Rows[n]["CreatedOn"].ToString() != "")
                    {
                        model.CreatedOn = DateTime.Parse(dt.Rows[n]["CreatedOn"].ToString());
                    }
                    if (dt.Rows[n]["CreatedByUserId"].ToString() != "")
                    {
                        model.CreatedByUserId = int.Parse(dt.Rows[n]["CreatedByUserId"].ToString());
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
        /// 查询投诉类型
        /// </summary>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="SelectState">状态</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.CsComplainType> SelectList(string sea_name, string type, int SelectState, int searchDesc, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(sea_name, type, SelectState, searchDesc, pageIndexs, PageSize, out pageCount);
        }


        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateCustomState(string idlist, int state)
        {
            return dal.UpdateCustomState(idlist, state);
        }

        #endregion

    }
}