using JMP.Model.Query;
using System;
using System.Collections.Generic;
using System.Data;

namespace JMP.BLL
{
    //用户表
    public partial class jmp_user
    {
        private readonly JMP.DAL.jmp_user dal = new JMP.DAL.jmp_user();
        public jmp_user()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int u_id)
        {
            return dal.Exists(u_id);
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

        /// <summary>
        /// 是否存在该身份证号
        /// </summary>
        /// <param name="idcard">身份证号</param>
        /// <param name="mail">邮箱</param>
        /// <returns></returns>
        public bool ExistsIdnos(string idcard, string mail)
        {
            return dal.ExistsIdnos(idcard, mail);
        }

        /// <summary>
        /// 是否存在该营业执照
        /// </summary>
        /// <param name="tmp">营业执照</param>
        /// <param name="mail">邮箱</param>
        /// <returns></returns>
        public bool ExistsYyzzs(string tmp, string mail)
        {
            return dal.ExistsYyzzs(tmp, mail);
        }

        /// <summary>
        /// 是否存在该银行卡号
        /// </summary>
        /// <param name="tmp">银行卡号</param>
        /// <param name="mail">邮箱</param>
        /// <returns></returns>
        public bool ExistsBankNos(string tmp, string mail)
        {
            return dal.ExistsBankNos(tmp, mail);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_user model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_user model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 提交认证资料
        /// </summary>
        /// <param name="model">用户实体</param>
        /// <returns></returns>
        public bool UpdateByEmail(JMP.MDL.jmp_user model)
        {
            return dal.UpdateByEmail(model);
        }

        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="model">用户实体</param>
        /// <returns></returns>
        public bool UpdateInfo(JMP.MDL.jmp_user model)
        {
            return dal.UpdateInfo(model);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="u_email">登录邮箱账号</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public bool UpdatePwd(string u_email, string pwd)
        {
            return dal.UpdatePwd(u_email, pwd);
        }

        /// <summary>
        /// 通过手机号码更改用户密码
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public bool UpdatePwdByPhone(string phone, string pwd)
        {
            return dal.UpdatePwdByPhone(phone, pwd);
        }

        /// <summary>
        /// 批量更新用户状态
        /// </summary>
        /// <param name="uids">用户id列表</param>
        /// <param name="state">状态值</param>
        /// <returns></returns>
        public bool UpdateState(string uids, int state)
        {
            return dal.UpdateState(uids, state);
        }

        /// <summary>
        /// 更新用户状态
        /// </summary>
        /// <param name="uids">用户ID</param>
        /// <param name="state">状态</param>
        /// <param name="name">审核人</param>
        /// <returns></returns>
        public bool UpdateAuditState(int uids, int state, string name)
        {
            return dal.UpdateAuditState(uids, state, name);
        }
        public bool UpdateRiskSH(int id,int IsSignContract, int IsRecord)
        {
            return dal.UpdateRiskSH(id, IsSignContract, IsRecord);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int u_id)
        {
            return dal.Delete(u_id);
        }

        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string u_idlist)
        {
            return dal.DeleteList(u_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_user GetModel(int u_id)
        {
            return dal.GetModel(u_id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_user GetModel(string u_email)
        {
            return dal.GetModel(u_email);
        }

        public JMP.MDL.jmp_user GetModelByTel(string tel)
        {
            return dal.GetModelByTel(tel);
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
        public List<JMP.MDL.jmp_user> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_user> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_user> modelList = new List<JMP.MDL.jmp_user>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_user model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_user();
                    if (dt.Rows[n]["u_id"].ToString() != "")
                    {
                        model.u_id = int.Parse(dt.Rows[n]["u_id"].ToString());
                    }
                    model.u_email = dt.Rows[n]["u_email"].ToString();
                    model.u_password = dt.Rows[n]["u_password"].ToString();
                    model.u_realname = dt.Rows[n]["u_realname"].ToString();
                    model.u_phone = dt.Rows[n]["u_phone"].ToString();
                    model.u_qq = dt.Rows[n]["u_qq"].ToString();
                    model.u_bankname = dt.Rows[n]["u_bankname"].ToString();
                    model.u_name = dt.Rows[n]["u_name"].ToString();
                    model.u_account = dt.Rows[n]["u_account"].ToString();
                    if (dt.Rows[n]["u_category"].ToString() != "")
                    {
                        model.u_category = int.Parse(dt.Rows[n]["u_category"].ToString());
                    }
                    model.u_idnumber = dt.Rows[n]["u_idnumber"].ToString();
                    model.u_photo = dt.Rows[n]["u_photo"].ToString();
                    model.u_blicense = dt.Rows[n]["u_blicense"].ToString();
                    model.u_blicensenumber = dt.Rows[n]["u_blicensenumber"].ToString();
                    if (dt.Rows[n]["u_count"].ToString() != "")
                    {
                        model.u_count = int.Parse(dt.Rows[n]["u_count"].ToString());
                    }
                    if (dt.Rows[n]["u_state"].ToString() != "")
                    {
                        model.u_state = int.Parse(dt.Rows[n]["u_state"].ToString());
                    }
                    if (dt.Rows[n]["u_auditstate"].ToString() != "")
                    {
                        model.u_auditstate = int.Parse(dt.Rows[n]["u_auditstate"].ToString());
                    }
                    if (dt.Rows[n]["u_topid"].ToString() != "")
                    {
                        model.u_topid = int.Parse(dt.Rows[n]["u_topid"].ToString());
                    }
                    model.u_address = dt.Rows[n]["u_address"].ToString();
                    //if (dt.Rows[n]["u_poundage"].ToString() != "")
                    //{
                    //    model.u_poundage = decimal.Parse(dt.Rows[n]["u_poundage"].ToString());
                    //}
                    if (dt.Rows[n]["u_role_id"].ToString() != "")
                    {
                        model.u_role_id = int.Parse(dt.Rows[n]["u_role_id"].ToString());
                    }
                    if (dt.Rows[n]["u_drawing"].ToString() != "")
                    {
                        model.u_drawing = int.Parse(dt.Rows[n]["u_drawing"].ToString());
                    }
                    if (dt.Rows[n]["u_merchant_id"].ToString() != "")
                    {
                        model.u_merchant_id = int.Parse(dt.Rows[n]["u_merchant_id"].ToString());
                    }
                    if (dt.Rows[n]["u_time"].ToString() != "")
                    {
                        model.u_time = DateTime.Parse(dt.Rows[n]["u_time"].ToString());
                    }
                    model.u_auditor = dt.Rows[n]["u_auditor"].ToString();

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
        /// 获取数据列表
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="Order">排序字段</param>
        /// <param name="OrderType">排序方式</param>
        /// <param name="currPage">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_user> GetLists(string sql, string Order, int currPage, int pageSize, out int pageCount)
        {
            return dal.GetLists(sql, Order, currPage, pageSize, out pageCount);
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="Order">排序字段</param>
        /// <param name="OrderType">排序方式</param>
        /// <param name="currPage">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<AppUserMerchant> GetAppUserMerchantLists(string sql, string Order, int currPage, int pageSize, out int pageCount)
        {
            return dal.GetAppUserMerchantLists(sql, Order, currPage, pageSize, out pageCount);
        }

        /// <summary>
        /// 获取未读消息数
        /// </summary>
        /// <param name="u_id">用户id</param>
        /// <returns></returns>
        public int GetUserMsgCount(int u_id)
        {
            return dal.GetUserMsgCount(u_id);
        }

        #endregion

        /// <summary>
        /// 指定到商务
        /// </summary>
        /// <param name="uids"></param>
        /// <param name="mid">商务ID</param>
        /// <returns></returns>
        public bool AssignToMerchant(string uids, int mid)
        {
            return dal.AssignToMerchant(uids, mid);
        }

        /// <summary>
        /// 获取商务所属的用户总数
        /// </summary>
        /// <param name="merchantId">商务ID</param>
        /// <returns></returns>
        public int GetAppUserCount(int merchantId, string u_state)
        {
            return dal.GetAppUserCount(merchantId, u_state);
        }
        /// <summary>
        /// 查询活跃用户
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="kstime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public int GetActiveAppUserCount(int merchantId, string kstime, string endtime)
        {
            return dal.GetActiveAppUserCount(merchantId, kstime, endtime);
        }

        /// <summary>
        /// 是否存在该手机号码
        /// </summary>
        /// <param name="phone">邮箱地址</param>
        /// <param name="userid">用户id（可不传）</param>
        /// <returns></returns>
        public bool ExistsPhone(string phone, string userid = "")
        {
            return dal.ExistsPhone(phone, userid);
        }

        /// <summary>
        /// 修改开发者的费率
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ServiceFeeRatioGradeId"></param>
        /// <returns></returns>
        public bool UpdateServiceFeeRatioGradeId(int id, int ServiceFeeRatioGradeId)
        {
            return dal.UpdateServiceFeeRatioGradeId(id, ServiceFeeRatioGradeId);
        }

        /// <summary>
        /// 修改开发者特批状态
        /// </summary>
        /// <param name="uids">用户ID</param>
        /// <param name="state">状态</param>
        /// <param name="fl">特批时需要减掉的服务费率</param>
        /// <returns></returns>
        public bool UpdateIsSpecialApproval(int uids, int state, string fl)
        {
            return dal.UpdateIsSpecialApproval(uids, state, fl);
        }

        /// <summary>
        /// 设置支付密码
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="Pwd">支付密码</param>
        /// <returns></returns>
        public bool UpdateUserPayPwd(int id, string Pwd)
        {
            return dal.UpdateUserPayPwd(id, Pwd);
        }

        public MDL.jmp_user FindByRealName(string realName)
        {
            return dal.FindByRealName(realName);
        }


        /// <summary>
        /// 修改开发者的冻结金额
        /// </summary>
        /// <param name="id">开发者ID</param>
        /// <param name="money">金额</param>
        /// <returns></returns>
        public bool UpdateFrozenMoney(int id, decimal FrozenMoney)
        {
            return dal.UpdateFrozenMoney(id, FrozenMoney);
        }
    }
}