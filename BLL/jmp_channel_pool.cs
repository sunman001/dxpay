using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    //新增通道池配置表,一对多的关系
    public partial class jmp_channel_pool
    {

        private readonly JMP.DAL.jmp_channel_pool dal = new JMP.DAL.jmp_channel_pool();
        public jmp_channel_pool()
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
        public int Add(JMP.MDL.jmp_channel_pool model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_channel_pool model)
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
        public JMP.MDL.jmp_channel_pool GetModel(int Id)
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
        public List<JMP.MDL.jmp_channel_pool> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_channel_pool> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_channel_pool> modelList = new List<JMP.MDL.jmp_channel_pool>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_channel_pool model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_channel_pool();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    model.PoolName = dt.Rows[n]["PoolName"].ToString();
                    if (dt.Rows[n]["IsEnabled"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsEnabled"].ToString() == "1") || (dt.Rows[n]["IsEnabled"].ToString().ToLower() == "true"))
                        {
                            model.IsEnabled = true;
                        }
                        else
                        {
                            model.IsEnabled = false;
                        }
                    }
                    if (dt.Rows[n]["CreatedOn"].ToString() != "")
                    {
                        model.CreatedOn = DateTime.Parse(dt.Rows[n]["CreatedOn"].ToString());
                    }
                    if (dt.Rows[n]["CreatedByUserId"].ToString() != "")
                    {
                        model.CreatedByUserId = int.Parse(dt.Rows[n]["CreatedByUserId"].ToString());
                    }
                    model.Description = dt.Rows[n]["Description"].ToString();


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
        /// 查询通道池弹窗列表
        /// </summary>
        /// <param name="pageIndexs"></param>
        /// <param name="PageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<MDL.jmp_channel_pool> SelectListTc(int type,string sea_name, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectListTc(type, sea_name, pageIndexs, PageSize, out pageCount);
        }


        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="pageIndexs"></param>
        /// <param name="PageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<MDL.jmp_channel_pool> poolList(int PoolName,string searchKey,int IsEnabled,int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.poolList(PoolName, searchKey, IsEnabled,pageIndexs, PageSize, out pageCount);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public bool UpdatePoolState(int id, int state)
        {
            return dal.UpdatePoolState(id, state);
        }

    }
}