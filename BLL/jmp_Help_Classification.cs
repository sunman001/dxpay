using JMP.Model.Query.Doc;
using System;
using System.Collections.Generic;
using System.Data;

namespace JMP.BLL
{
    public class jmp_Help_Classification
    {
        private readonly JMP.DAL.jmp_Help_Classification dal = new JMP.DAL.jmp_Help_Classification();
        public jmp_Help_Classification()
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
        public int Add(JMP.MDL.jmp_Help_Classification model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_Help_Classification model)
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
        public JMP.MDL.jmp_Help_Classification GetModel(int ID)
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
        public List<JMP.MDL.jmp_Help_Classification> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_Help_Classification> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_Help_Classification> modelList = new List<JMP.MDL.jmp_Help_Classification>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_Help_Classification model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_Help_Classification();
                    if (dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    model.Icon = dt.Rows[n]["Icon"].ToString();
                    model.Description = dt.Rows[n]["Description"].ToString();
                    model.ClassName = dt.Rows[n]["ClassName"].ToString();
                    if (dt.Rows[n]["ParentID"].ToString() != "")
                    {
                        model.ParentID = int.Parse(dt.Rows[n]["ParentID"].ToString());
                    }
                    if (dt.Rows[n]["Sort"].ToString() != "")
                    {
                        model.Sort = int.Parse(dt.Rows[n]["Sort"].ToString());
                    }
                    if (dt.Rows[n]["State"].ToString() != "")
                    {
                        model.State = int.Parse(dt.Rows[n]["State"].ToString());
                    }
                    if (dt.Rows[n]["Count"].ToString() != "")
                    {
                        model.Count = int.Parse(dt.Rows[n]["Count"].ToString());
                    }
                    if (dt.Rows[n]["CreateOn"].ToString() != "")
                    {
                        model.CreateOn = DateTime.Parse(dt.Rows[n]["CreateOn"].ToString());
                    }
                    if (dt.Rows[n]["CreateByID"].ToString() != "")
                    {
                        model.CreateByID = int.Parse(dt.Rows[n]["CreateByID"].ToString());
                    }
                    model.CreateByName = dt.Rows[n]["CreateByName"].ToString();


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
        public List<JMP.MDL.jmp_Help_Classification> SelectList(int sType, int ParentID,string ClassName, int type, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(sType, ParentID,ClassName, type, pageIndexs, PageSize, out pageCount);
        }

        public bool UpdateState(string u_idlist, int state)
        {
            return dal.UpdateState(u_idlist, state);
        }

        public List<JMP.MDL.jmp_Help_Classification> FindAllEnabledList()
        {
            return GetModelList("State=0");
        }

        /// <summary>
        /// 读取所有可用的帮助中心集合
        /// </summary>
        /// <returns></returns>
        public List<MDL.jmp_Help_Classification> FindAllEnabledHelpList()
        {
            return GetModelList("State=0 and Type=0");
        }
        /// <summary>
        /// 读取所有可用的开发文档集合
        /// </summary>
        /// <returns></returns>
        public List<DocMenuQueryModel> FindAllEnabledDocList()
        {
            return dal.FindAllEnabledDocList();
        }

    }
}
