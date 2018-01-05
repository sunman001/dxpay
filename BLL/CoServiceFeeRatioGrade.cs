using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.DAL;
using JMP.MDL;

namespace JMP.BLL
{
   public partial class CoServiceFeeRatioGrade
    {
        private readonly JMP.DAL.CoServiceFeeRatioGrade dal = new JMP.DAL.CoServiceFeeRatioGrade();
        public CoServiceFeeRatioGrade()
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
        public int Add(JMP.MDL.CoServiceFeeRatioGrade model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.CoServiceFeeRatioGrade model)
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
        public JMP.MDL.CoServiceFeeRatioGrade GetModel(int Id)
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
        public DataSet GetAllList()
        {
            return GetList("");
        }
        #endregion
        /// <summary>
        /// 查询服务费等级信息
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
        public List<JMP.MDL.CoServiceFeeRatioGrade> SelectList(string sea_name, int type, int searchDesc, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList( sea_name, type, searchDesc,pageIndexs, PageSize, out pageCount);
        }
        /// <summary>
        /// 根据应用id查询服务费等级
        /// </summary>
        /// <param name="c_id">应用id</param>
        /// <returns></returns>
        public JMP.MDL.CoServiceFeeRatioGrade SelectId(int c_id)
        {
            return dal.SelectId(c_id);
        }

        public JMP.MDL.CoServiceFeeRatioGrade GetModelByName(string Name)
        {

            return dal.GetModelByName(Name);
        }


        /// <summary>
        /// 查询默认费率
        /// </summary>
        /// <returns></returns>
        public JMP.MDL.CoServiceFeeRatioGrade GetModelById()
        {
            return dal.GetModelById();
        }
    }
}
