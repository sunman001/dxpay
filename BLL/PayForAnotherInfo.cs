using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    //代付信息PayForAnotherInfo
    public partial class PayForAnotherInfo
    {

        private readonly JMP.DAL.PayForAnotherInfo dal = new JMP.DAL.PayForAnotherInfo();
        public PayForAnotherInfo()
        { }


        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int p_Id)
        {
            return dal.Exists(p_Id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.PayForAnotherInfo model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.PayForAnotherInfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int p_Id)
        {

            return dal.Delete(p_Id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string p_Idlist)
        {
            return dal.DeleteList(p_Idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.PayForAnotherInfo GetModel(int p_Id)
        {

            return dal.GetModel(p_Id);
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
        public List<JMP.MDL.PayForAnotherInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.PayForAnotherInfo> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.PayForAnotherInfo> modelList = new List<JMP.MDL.PayForAnotherInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.PayForAnotherInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.PayForAnotherInfo();
                    if (dt.Rows[n]["p_Id"].ToString() != "")
                    {
                        model.p_Id = int.Parse(dt.Rows[n]["p_Id"].ToString());
                    }
                    if (dt.Rows[n]["p_auditortime"].ToString() != "")
                    {
                        model.p_auditortime = DateTime.Parse(dt.Rows[n]["p_auditortime"].ToString());
                    }
                    model.p_append = dt.Rows[n]["p_append"].ToString();
                    if (dt.Rows[n]["p_appendtime"].ToString() != "")
                    {
                        model.p_appendtime = DateTime.Parse(dt.Rows[n]["p_appendtime"].ToString());
                    }
                    model.p_InterfaceName = dt.Rows[n]["p_InterfaceName"].ToString();
                    if (dt.Rows[n]["p_InterfaceType"].ToString() != "")
                    {
                        model.p_InterfaceType = int.Parse(dt.Rows[n]["p_InterfaceType"].ToString());
                    }
                    model.p_MerchantNumber = dt.Rows[n]["p_MerchantNumber"].ToString();
                    if (dt.Rows[n]["p_KeyType"].ToString() != "")
                    {
                        model.p_KeyType = int.Parse(dt.Rows[n]["p_KeyType"].ToString());
                    }
                    model.p_PrivateKey = dt.Rows[n]["p_PrivateKey"].ToString();
                    model.p_PublicKey = dt.Rows[n]["p_PublicKey"].ToString();
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
                    model.p_auditor = dt.Rows[n]["p_auditor"].ToString();


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
        /// 查询代付通道列
        /// </summary>
        /// <param name="PayType">查询条件类型</param>
        /// <param name="searchKey">查询值</param>
        /// <param name="IsEnabled">启用禁用状态</param>
        /// <param name="pageIndexs"></param>
        /// <param name="PageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<MDL.PayForAnotherInfo> PayForAnotherInfoList(int PayType, string searchKey, int IsEnabled, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.PayForAnotherInfoList(PayType, searchKey, IsEnabled, pageIndexs, PageSize, out pageCount);
        }

        /// <summary>
        /// 查询代付通道列用于财务选择
        /// </summary>
        /// <param name="PayType">查询条件类型</param>
        /// <param name="searchKey">查询值</param>
        /// <param name="pageIndexs"></param>
        /// <param name="PageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<MDL.PayForAnotherInfo> PayForAnotherIsEnabledList(int PayType, string searchKey, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.PayForAnotherIsEnabledList(PayType, searchKey, pageIndexs, PageSize, out pageCount);
        }


        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdatePayForAnotherState(int id, int state)
        {
            return dal.UpdatePayForAnotherState(id, state);
        }

        /// <summary>
        /// 通过代付接口ID查询信息
        /// </summary>
        /// <param name="Id">代付接口ID</param>
        /// <returns></returns>
        public JMP.MDL.PayForAnotherInfo GetPayInfoId(int Id)
        {
            return dal.GetPayInfoId(Id);
        }
    }
}