/************聚米支付平台__获取用户信息************/
//描述：
//功能：获取用户信息
//开发者：谭玉科
//开发时间: 2016.03.04
/************聚米支付平台__获取用户信息************/

using System.Web;

namespace JMP.TOOL
{
    /// <summary>
    /// 类名：UserInfo
    /// 功能：获取用户信息
    /// 详细：获取用户名、部门等信息
    /// 修改日期：2016.03.04
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public static int UserId
        {
            get
            {
                return HttpContext.Current.Session["u_id"] == null
                    ? 0
                    : int.Parse(HttpContext.Current.Session["u_id"].ToString());
            }
            set { HttpContext.Current.Session["u_id"] = value; }
        }
        /// <summary>
        /// 从登录信息中获取用户ID
        /// </summary>
        public static int Uid
        {
            get
            {
                return HttpContext.Current.Session["u_id"] == null ? 0 : int.Parse(HttpContext.Current.Session["u_id"].ToString());
            }
        }

        public static bool IsLogin
        {
            get { return Uid > 0; }
        }

        /// <summary>
        /// 用户角色编码
        /// </summary>
        public static int UserRoleId
        {
            get { return HttpContext.Current.Session["u_role_id"] == null ? 0 : int.Parse(HttpContext.Current.Session["u_role_id"].ToString()); }
            set { HttpContext.Current.Session["u_role_id"] = value; }
        }

        /// <summary>
        /// 用户角色编码
        /// </summary>
        public static int UserRid
        {
            get
            {
                return HttpContext.Current.Session["u_role_id"] == null ? 0 : int.Parse(HttpContext.Current.Session["u_role_id"].ToString());
            }
        }

        /// <summary>
        /// 用户角色名
        /// </summary>
        public static string UserRoleName
        {
            get { return HttpContext.Current.Session["u_role_name"] == null ? null : HttpContext.Current.Session["u_role_name"].ToString(); }
            set { HttpContext.Current.Session["u_role_name"] = value; }
        }

        /// <summary>
        /// 用户登陆名
        /// </summary>
        public static string UserNo
        {
            get { return HttpContext.Current.Session["u_login_name"] == null ? null : HttpContext.Current.Session["u_login_name"].ToString(); }
            set { HttpContext.Current.Session["u_login_name"] = value; }
        }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public static string UserName
        {
            get { return HttpContext.Current.Session["u_real_name"] == null ? null : HttpContext.Current.Session["u_real_name"].ToString(); }
            set { HttpContext.Current.Session["u_real_name"] = value; }
        }

        /// <summary>
        /// 部门
        /// </summary>
        public static string UserDept
        {
            get { return HttpContext.Current.Session["u_department"] == null ? null : HttpContext.Current.Session["u_department"].ToString(); }
            set { HttpContext.Current.Session["u_department"] = value; }
        }

        /// <summary>
        /// 部门
        /// </summary>
        public static string UserPostion
        {
            get { return HttpContext.Current.Session["u_position"] == null ? null : HttpContext.Current.Session["u_position"].ToString(); }
            set { HttpContext.Current.Session["u_position"] = value; }
        }

        /// <summary>
        /// 账户状态：0 冻结 1 正常
        /// </summary>
        public static string UserState
        {
            get { return HttpContext.Current.Session["u_state"] == null ? null : HttpContext.Current.Session["u_state"].ToString(); }
            set { HttpContext.Current.Session["u_state"] = value; }
        }

        /// <summary>
        /// 登录次数
        /// </summary>
        public static string UserLoginCount
        {
            get { return HttpContext.Current.Session["u_count"] == null ? null : HttpContext.Current.Session["u_count"].ToString(); }
            set { HttpContext.Current.Session["u_count"] = value; }
        }
        /// <summary>
        /// 审核状态：-1 未通过 0 等待审核
        /// </summary>
        public static string auditstate
        {
            get { return HttpContext.Current.Session["u_auditstate"] == null ? null : HttpContext.Current.Session["u_auditstate"].ToString(); }
            set { HttpContext.Current.Session["u_auditstate"] = value; }
        }

        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        public static bool IsSuperAdmin
        {
            get
            {
                return UserRoleId == 1;
            }
        }
    }
}
