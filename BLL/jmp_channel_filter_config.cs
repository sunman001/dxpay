using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    //通道过滤规则配置表
    public partial class jmp_channel_filter_config
    {

        private readonly JMP.DAL.jmp_channel_filter_config dal = new JMP.DAL.jmp_channel_filter_config();
        public jmp_channel_filter_config()
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
        public int  Add(JMP.MDL.jmp_channel_filter_config model)
        {
          return   dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_channel_filter_config model)
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
        public bool DeleteByType(int TypeId,int TargetId,int RelatedId)
        {
            return dal.DeleteByType(TypeId, TargetId, RelatedId);
        }
        public bool Exists(int TypeId, int TargetId, int RelatedId)
        {
            return dal.Exists(TypeId, TargetId, RelatedId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_channel_filter_config GetModel(int Id)
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
        public List<JMP.MDL.jmp_channel_filter_config> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_channel_filter_config> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_channel_filter_config> modelList = new List<JMP.MDL.jmp_channel_filter_config>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_channel_filter_config model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_channel_filter_config();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    model.CreatedByUserName = dt.Rows[n]["CreatedByUserName"].ToString();
                    if (dt.Rows[n]["TypeId"].ToString() != "")
                    {
                        model.TypeId = int.Parse(dt.Rows[n]["TypeId"].ToString());
                    }
                    if (dt.Rows[n]["TargetId"].ToString() != "")
                    {
                        model.TargetId = int.Parse(dt.Rows[n]["TargetId"].ToString());
                    }
                    if (dt.Rows[n]["WhichHour"].ToString() != "")
                    {
                        model.WhichHour = int.Parse(dt.Rows[n]["WhichHour"].ToString());
                    }
                    if (dt.Rows[n]["Threshold"].ToString() != "")
                    {
                        model.Threshold = decimal.Parse(dt.Rows[n]["Threshold"].ToString());
                    }
                    if (dt.Rows[n]["RelatedId"].ToString() != "")
                    {
                        model.RelatedId = int.Parse(dt.Rows[n]["RelatedId"].ToString());
                    }
                    if (dt.Rows[n]["IntervalOfRecover"].ToString() != "")
                    {
                        model.IntervalOfRecover = int.Parse(dt.Rows[n]["IntervalOfRecover"].ToString());
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
        /// 获取数量列表
        /// </summary>
        /// <param name="Sname">值班人</param>
        /// <param name="startdate">值班开始时间</param>
        /// <param name="enddate">值班结束</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总页数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_channel_filter_config> SelectList( int type,string sea_name, int TypeId,int TargetId,  int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(type, sea_name,TypeId, TargetId, pageIndexs, PageSize, out pageCount);
        }

       
        #endregion

    }
}