using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.MDL;
namespace JMP.BLL
{
    //商品表
    public partial class jmp_goods
    {

        private readonly JMP.DAL.jmp_goods dal = new JMP.DAL.jmp_goods();
        public jmp_goods()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int g_id)
        {
            return dal.Exists(g_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_goods model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_goods model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int g_id)
        {

            return dal.Delete(g_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string g_idlist)
        {
            return dal.DeleteList(g_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_goods GetModel(int g_id)
        {

            return dal.GetModel(g_id);
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
        public List<JMP.MDL.jmp_goods> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_goods> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_goods> modelList = new List<JMP.MDL.jmp_goods>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_goods model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_goods();
                    if (dt.Rows[n]["g_id"].ToString() != "")
                    {
                        model.g_id = int.Parse(dt.Rows[n]["g_id"].ToString());
                    }
                    if (dt.Rows[n]["g_app_id"].ToString() != "")
                    {
                        model.g_app_id = int.Parse(dt.Rows[n]["g_app_id"].ToString());
                    }
                    model.g_name = dt.Rows[n]["g_name"].ToString();
                    if (dt.Rows[n]["g_saletype_id"].ToString() != "")
                    {
                        model.g_saletype_id = int.Parse(dt.Rows[n]["g_saletype_id"].ToString());
                    }
                    if (dt.Rows[n]["g_price"].ToString() != "")
                    {
                        model.g_price = decimal.Parse(dt.Rows[n]["g_price"].ToString());
                    }
                    if (dt.Rows[n]["g_state"].ToString() != "")
                    {
                        model.g_state = int.Parse(dt.Rows[n]["g_state"].ToString());
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
        /// 查询商品信息
        /// </summary>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="SelectState">状态</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_goods> SelectList( string sea_name, int type, int SelectState, int searchDesc, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList( sea_name, type, SelectState, searchDesc, pageIndexs, PageSize, out pageCount);
        }

        public List<JMP.MDL.jmp_goods> SelectListById(int id, string sea_name, int type, int SelectState, int searchDesc, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectListById(id, sea_name, type, SelectState, searchDesc, pageIndexs, PageSize, out pageCount);
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateLocUserState(string u_idlist, int state)
        {
            return dal.UpdateLocUserState(u_idlist, state);
        }
         /// <summary>
        /// 局部试图根据应用id获取商品信息
        /// </summary>
        /// <param name="g_app_id">应用id</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_goods> SelectListEj(int g_app_id)
        {
            return dal.SelectListEj(g_app_id);
        }
         /// <summary>
        /// 用户根据应用id查询正常状态的商品
        /// </summary>
        /// <param name="g_app_id">应用id</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_goods> UserSelectList(int g_app_id)
        {
            return dal.UserSelectList(g_app_id);
        }
    }
}