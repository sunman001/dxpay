using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.BLL
{
    public class jmp_apprate
    {
        private readonly JMP.DAL.jmp_apprate dal = new JMP.DAL.jmp_apprate();
        public jmp_apprate()
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
        public void Add(JMP.MDL.jmp_apprate model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_apprate model)
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
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_apprate GetModel(int r_id)
        {

            return dal.GetModel(r_id);
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
        public List<JMP.MDL.jmp_apprate> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_apprate> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_apprate> modelList = new List<JMP.MDL.jmp_apprate>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_apprate model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_apprate();
                    if (dt.Rows[n]["r_id"].ToString() != "")
                    {
                        model.r_id = int.Parse(dt.Rows[n]["r_id"].ToString());
                    }
                    if (dt.Rows[n]["r_appid"].ToString() != "")
                    {
                        model.r_appid = int.Parse(dt.Rows[n]["r_appid"].ToString());
                    }
                    if (dt.Rows[n]["r_paymodeid"].ToString() != "")
                    {
                        model.r_paymodeid = int.Parse(dt.Rows[n]["r_paymodeid"].ToString());
                    }
                    if (dt.Rows[n]["r_proportion"].ToString() != "")
                    {
                        model.r_proportion = decimal.Parse(dt.Rows[n]["r_proportion"].ToString());
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
        #endregion

        /// <summary>
        /// 根据用户id查询相关信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_apprate> SelectListAppid(int userid)
        {
            return dal.SelectListAppid(userid);
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
        /// <summary>
        /// 根据应用id和支付类型判断是否对该支付类型设置汇率
        /// </summary>
        /// <param name="Appid">应用id</param>
        /// <param name="PayType">支付类型</param>
        /// <returns></returns>
        public JMP.MDL.jmp_apprate SelectAppidState(int Appid, int PayType)
        {
            return dal.SelectAppidState(Appid, PayType);
        }
    }
}
