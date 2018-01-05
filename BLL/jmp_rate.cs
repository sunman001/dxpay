using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMP.DAL;
using JMP.MDL;
namespace JMP.BLL
{
    ///<summary>
    ///手续费比例以及扣量设置
    ///</summary>
    public partial class jmp_rate
    {

        private readonly JMP.DAL.jmp_rate dal = new JMP.DAL.jmp_rate();
        public jmp_rate()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int r_id)
        {
            return dal.Exists(r_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_rate model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_rate model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int r_id)
        {

            return dal.Delete(r_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string r_idlist)
        {
            return dal.DeleteList(r_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_rate GetModel(int r_id)
        {

            return dal.GetModel(r_id);
        }

        /// <summary>
        /// 根据应用获取应用ID
        /// </summary>
        /// <param name="APPID"></param>
        /// <returns></returns>
        public object GetRatebyAPPid(int APPID,int payid)
        {

            return dal.getratebyid(APPID, payid);
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
        public List<JMP.MDL.jmp_rate> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_rate> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_rate> modelList = new List<JMP.MDL.jmp_rate>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_rate model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_rate();
                    if (dt.Rows[n]["r_id"].ToString() != "")
                    {
                        model.r_id = int.Parse(dt.Rows[n]["r_id"].ToString());
                    }
                    if (dt.Rows[n]["r_userid"].ToString() != "")
                    {
                        model.r_userid = int.Parse(dt.Rows[n]["r_userid"].ToString());
                    }
                    if (dt.Rows[n]["r_paymodeid"].ToString() != "")
                    {
                        model.r_paymodeid = int.Parse(dt.Rows[n]["r_paymodeid"].ToString());
                    }
                    if (dt.Rows[n]["r_proportion"].ToString() != "")
                    {
                        model.r_proportion = decimal.Parse(dt.Rows[n]["r_proportion"].ToString());
                    }
                    if (dt.Rows[n]["r_klproportion"].ToString() != "")
                    {
                        model.r_klproportion = decimal.Parse(dt.Rows[n]["r_klproportion"].ToString());
                    }
                    if (dt.Rows[n]["r_state"].ToString() != "")
                    {
                        model.r_state = int.Parse(dt.Rows[n]["r_state"].ToString());
                    }
                    if (dt.Rows[n]["r_time"].ToString() != "")
                    {
                        model.r_time = DateTime.Parse(dt.Rows[n]["r_time"].ToString());
                    }
                    model.r_name = dt.Rows[n]["r_name"].ToString();


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
        /// 根据用户id查询相关信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_rate> SelectListUserid(int userid)
        {
            return dal.SelectListUserid(userid);
        }
        /// <summary>
        /// 手续费设置
        /// </summary>
        /// <param name="cmdList">sql语句集合</param>
        /// <returns></returns>
        public int InserSxF(string[] list)
        {
            return dal.InserSxF(list);
        }
            #endregion

        }
}
