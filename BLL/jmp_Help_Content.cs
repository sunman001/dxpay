using JMP.Model.Query.Help;
using System;
using System.Collections.Generic;
using System.Data;

namespace JMP.BLL
{
    public  class jmp_Help_Content
    {
        private readonly JMP.DAL.jmp_Help_Content dal = new JMP.DAL.jmp_Help_Content();
        public jmp_Help_Content()
        { }
        
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_Help_Content model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_Help_Content model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            return dal.Delete(ID);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(IDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_Help_Content GetModel(int ID)
        {

            return dal.GetModel(ID);
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
        public List<JMP.MDL.jmp_Help_Content> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_Help_Content> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_Help_Content> modelList = new List<JMP.MDL.jmp_Help_Content>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_Help_Content model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_Help_Content();
                    if (dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["ClassId"].ToString() != "")
                    {
                        model.ClassId = int.Parse(dt.Rows[n]["ClassId"].ToString());
                    }
                    model.Title = dt.Rows[n]["Title"].ToString();
                    model.Content = dt.Rows[n]["Content"].ToString();
                    if (dt.Rows[n]["Count"].ToString() != "")
                    {
                        model.Count = int.Parse(dt.Rows[n]["Count"].ToString());
                    }
                    if (dt.Rows[n]["CreateById"].ToString() != "")
                    {
                        model.CreateById = int.Parse(dt.Rows[n]["CreateById"].ToString());
                    }
                    model.CreateByName = dt.Rows[n]["CreateByName"].ToString();
                    if (dt.Rows[n]["CreateOn"].ToString() != "")
                    {
                        model.CreateOn = DateTime.Parse(dt.Rows[n]["CreateOn"].ToString());
                    }
                    if (dt.Rows[n]["ISOverhead"].ToString() != "")
                    {
                        if ((dt.Rows[n]["ISOverhead"].ToString() == "1") || (dt.Rows[n]["ISOverhead"].ToString().ToLower() == "true"))
                        {
                            model.ISOverhead = true;
                        }
                        else
                        {
                            model.ISOverhead = false;
                        }
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
        /// 获取数量列表
        /// </summary>
        /// <param name="Sname">值班人</param>
        /// <param name="startdate">值班开始时间</param>
        /// <param name="enddate">值班结束</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总页数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_Help_Content> SelectList( int sType, string Type,  string sea_name, int State, int PrentID, int ClassId, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(sType,Type, sea_name, State, PrentID, ClassId, pageIndexs, PageSize, out pageCount);
        }

        public bool UpdateState(string u_idlist, int state)
        {
            return dal.UpdateState(u_idlist, state);
        }

        public bool UpdateClassCount( int PrentId,int type)
        {
            return dal.UpdateClassCount(PrentId, type);
        }


        public bool ArticleViewed(int articleId, int viewCount)
        {
            return dal.UpdateViewCount(articleId, viewCount);
        }
        public List<JMP.MDL.jmp_Help_Content> FindHotQuestionList(int topN = 5)
        {
            var ds = dal.GetList(topN, "State=0 and Type=0", "Count DESC");
            var list = DataTableToList(ds.Tables[0]);
            return list;
        }

        /// <summary>
        /// 返回主分类下按子分类分组的前5篇文章
        /// </summary>
        /// <param name="mainCatalogId">主分类ID</param>
        /// <returns></returns>
        public List<ArticleWithTotalCountQueryModel> FindTopArticleListGroupBySubCatalog(int mainCatalogId)
        {
            return dal.FindTopArticleListGroupBySubCatalog(mainCatalogId);
        }

        public List<MDL.jmp_Help_Content> Search(string[] keywords)
        {
            return dal.Search(keywords);
        }

        public List<MDL.jmp_Help_Content> FindListByCatalogId(int catalogId,int topN = 50)
        {
            var ds = dal.GetList(topN, string.Format("State=0 AND ClassId={0}",catalogId), "Id DESC");
            var list = DataTableToList(ds.Tables[0]);
            return list;
        }
    }
}
