using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.BLL
{
    //提款表
    public class jmp_pays
    {
        private readonly JMP.DAL.jmp_pays dal = new JMP.DAL.jmp_pays();

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int p_id)
        {
            return dal.Exists(p_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_pays model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_pays model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int p_id)
        {

            return dal.Delete(p_id);
        }

        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string p_idlist)
        {
            return dal.DeleteList(p_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_pays GetModel(int p_id)
        {

            return dal.GetModel(p_id);
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
        public List<JMP.MDL.jmp_pays> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_pays> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_pays> modelList = new List<JMP.MDL.jmp_pays>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_pays model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_pays();
                    if (dt.Rows[n]["p_id"].ToString() != "")
                    {
                        model.p_id = int.Parse(dt.Rows[n]["p_id"].ToString());
                    }
                    model.p_remarks = dt.Rows[n]["p_remarks"].ToString();
                    if (dt.Rows[n]["p_applytime"].ToString() != "")
                    {
                        model.p_applytime = DateTime.Parse(dt.Rows[n]["p_applytime"].ToString());
                    }
                    if (dt.Rows[n]["p_money"].ToString() != "")
                    {
                        model.p_money = decimal.Parse(dt.Rows[n]["p_money"].ToString());
                    }
                    if (dt.Rows[n]["p_bill_id"].ToString() != "")
                    {
                        model.p_bill_id = int.Parse(dt.Rows[n]["p_bill_id"].ToString());
                    }
                    if (dt.Rows[n]["p_userid"].ToString() != "")
                    {
                        model.p_userid = int.Parse(dt.Rows[n]["p_userid"].ToString());
                    }
                    if (dt.Rows[n]["p_state"].ToString() != "")
                    {
                        model.p_state = int.Parse(dt.Rows[n]["p_state"].ToString());
                    }
                    model.p_auditor = dt.Rows[n]["p_auditor"].ToString();
                    model.p_batchnumber = dt.Rows[n]["p_batchnumber"].ToString();
                    if (dt.Rows[n]["p_date"].ToString() != "")
                    {
                        model.p_date = DateTime.Parse(dt.Rows[n]["p_date"].ToString());
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
        /// 根据sql查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable SelectList(string sql)
        {
            return dal.SelectList(sql);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_pays GetPaysModel(string batchnumber)
        {

            return dal.GetPaysModel(batchnumber);
        }
    }
}

