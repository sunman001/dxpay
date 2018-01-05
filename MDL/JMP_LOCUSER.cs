using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    //管理员用户表
    public class jmp_locuser
    {

        /// <summary>
        /// 主键
        /// </summary>		
        private int _u_id;
        [EntityTracker(Label = "主键", Description = "主键")]
        public int u_id
        {
            get { return _u_id; }
            set { _u_id = value; }
        }
        /// <summary>
        /// 用户角色
        /// </summary>		
        private int _u_role_id;
        [EntityTracker(Label = "用户角色", Description = "用户角色")]
        public int u_role_id
        {
            get { return _u_role_id; }
            set { _u_role_id = value; }
        }
        /// <summary>
        /// 登陆名
        /// </summary>		
        private string _u_loginname;
        [EntityTracker(Label = "登陆名", Description = "登陆名")]
        public string u_loginname
        {
            get { return _u_loginname; }
            set { _u_loginname = value; }
        }
        /// <summary>
        /// 用户密码
        /// </summary>		
        private string _u_pwd;
        [EntityTracker(Label = "用户密码", Description = "用户密码")]
        public string u_pwd
        {
            get { return _u_pwd; }
            set { _u_pwd = value; }
        }
        /// <summary>
        /// 真实名称
        /// </summary>		
        private string _u_realname;
        [EntityTracker(Label = "真实名称", Description = "真实名称")]
        public string u_realname
        {
            get { return _u_realname; }
            set { _u_realname = value; }
        }
        /// <summary>
        /// 部门
        /// </summary>		
        private string _u_department;
        [EntityTracker(Label = "部门", Description = "部门")]
        public string u_department
        {
            get { return _u_department; }
            set { _u_department = value; }
        }
        /// <summary>
        /// 职位
        /// </summary>		
        private string _u_position;
        [EntityTracker(Label = "职位", Description = "职位")]
        public string u_position
        {
            get { return _u_position; }
            set { _u_position = value; }
        }
        /// <summary>
        /// 登陆次数
        /// </summary>		
        private int _u_count;
        [EntityTracker(Label = "登陆次数", Description = "登陆次数")]
        public int u_count
        {
            get { return _u_count; }
            set { _u_count = value; }
        }
        /// <summary>
        /// 状态
        /// </summary>		
        private int _u_state;
        [EntityTracker(Label = "状态", Description = "状态")]
        public int u_state
        {
            get { return _u_state; }
            set { _u_state = value; }
        }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string u_mobilenumber { get; set; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string u_emailaddress { get; set; }
        /// <summary>
        /// qq号
        /// </summary>
        public string u_qq { get; set; }
    }
}