using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;

namespace JMP.BLL
{
    public partial class CoAgent
    {
        private readonly JMP.DAL.CoAgent dal = new JMP.DAL.CoAgent();
        public CoAgent()
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
        public int Add(JMP.MDL.CoAgent model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.CoAgent model)
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
        public JMP.MDL.CoAgent GetModel(int Id)
        {

            return dal.GetModel(Id);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.CoAgent GetModel(string  username)
        {

            return dal.GetModel(username);
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

        /// <summary>
        /// 查询代理商列表
        /// </summary>
        /// <param name="s_type">查询类型</param>
        /// <param name="s_keys">查询内容</param>
        /// <param name="status">账号状态</param>
        /// <param name="AuditState">审核状态</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.CoAgent> SelectList(int s_type, string s_keys, string status, string AuditState, int searchDesc, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(s_type, s_keys, status, AuditState, searchDesc, pageIndexs, PageSize, out pageCount);
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="u_idlist">ID</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateState(int id, int state)
        {
            return dal.UpdateState(id, state);
        }


        /// <summary>
        /// 批量更新代理商状态
        /// </summary>
        /// <param name="coid">商务id列表</param>
        /// <param name="state">状态值</param>
        /// <returns></returns>
        public bool UpdateAgentState(string coid, int state)
        {
            return dal.UpdateAgentState(coid, state);
        }


        ///////////////////////////////////////验证方法///////////////////////////////////////////////////

        /// <summary>
        /// 是否存在该登录账号
        /// </summary>
        /// <param name="temp">登录账号</param>
        /// <param name="userid">用户id（可不传）</param>
        /// <returns></returns>
        public bool ExistsLogName(string temp, string userid = "")
        {
            return dal.ExistsLogName(temp, userid);
        }


        /// <summary>
        /// 是否存在该邮箱地址
        /// </summary>
        /// <param name="temp">邮箱地址</param>
        /// <param name="userid">用户id（可不传）</param>
        /// <returns></returns>
        public bool ExistsEmail(string temp, string userid = "")
        {
            return dal.ExistsEmail(temp, userid);
        }

        /// <summary>
        /// 是否存在该身份证号
        /// </summary>
        /// <param name="idcard">身份证号</param>
        /// <param name="uid">用户id（可不传）</param>
        /// <returns></returns>
        public bool ExistsIdno(string idcard, string uid = "")
        {
            return dal.ExistsIdno(idcard, uid);
        }

        /// <summary>
        /// 是否存在该营业执照
        /// </summary>
        /// <param name="tmp">营业执照</param>
        /// <param name="uid">用户id（可不传）</param>
        /// <returns></returns>
        public bool ExistsYyzz(string tmp, string uid = "")
        {
            return dal.ExistsYyzz(tmp, uid);
        }

        /// <summary>
        /// 是否存在该银行卡号
        /// </summary>
        /// <param name="tmp">银行卡号</param>
        /// <param name="uid">用户id（可不传）</param>
        /// <returns></returns>
        public bool ExistsBankNo(string tmp, string uid = "")
        {
            return dal.ExistsBankNo(tmp, uid);
        }

        #endregion
    }
}
