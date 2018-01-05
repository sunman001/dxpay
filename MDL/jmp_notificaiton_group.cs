using JMP.Model;
using System;

namespace JMP.MDL
{
    //通知短信分组信息表
    public class jmp_notificaiton_group
    {

        /// <summary>
        /// Id
        /// </summary>		
        private int _id;
        /// <summary>
        /// Id
        /// </summary>
        [EntityTracker(Label = "Id", Description = "Id")]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 分组名称
        /// </summary>		
        private string _name;
        /// <summary>
		/// 分组名称
        /// </summary>
        [EntityTracker(Label = "分组名称", Description = "分组名称")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
     
        /// <summary>
        /// 任务惟一编码
        /// </summary>		
        private string _code;
        /// <summary>
        /// 任务惟一编码
        /// </summary>
        [EntityTracker(Label = "任务惟一编码", Description = "任务惟一编码")]
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }
        /// <summary>
        /// 任务分组描述
        /// </summary>		
        private string _description;
        /// <summary>
        /// 任务分组描述
        /// </summary>
        [EntityTracker(Label = "任务分组描述", Description = "任务分组描述")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        /// <summary>
        /// 要通知的手机号码,多个手机号码以逗号(,)分隔
        /// </summary>		
        private string _notifymobilelist;
        /// <summary>
        /// 要通知的手机号码,多个手机号码以逗号(,)分隔
        /// </summary>
        [EntityTracker(Label = "要通知的手机号码", Description = "要通知的手机号码")]
        public string NotifyMobileList
        {
            get { return _notifymobilelist; }
            set { _notifymobilelist = value; }
        }
        /// <summary>
        /// 短信模板
        /// </summary>		
        private string _messagetemplate;
        /// <summary>
        /// 短信模板
        /// </summary>
        [EntityTracker(Label = "短信模板", Description = "短信模板")]
        public string MessageTemplate
        {
            get { return _messagetemplate; }
            set { _messagetemplate = value; }
        }
        /// <summary>
        /// 是否已删除
        /// </summary>		
        private bool _isdeleted;
        /// <summary>
        /// 是否已删除
        /// </summary>
        [EntityTracker(Label = "是否已删除", Description = "是否已删除")]
        public bool IsDeleted
        {
            get { return _isdeleted; }
            set { _isdeleted = value; }
        }
        /// <summary>
        /// 任务是否可用
        /// </summary>		
        private bool _isenabled;
        /// <summary>
        /// 任务是否可用
        /// </summary>
        [EntityTracker(Label = "任务是否可用", Description = "任务是否可用")]
        public bool IsEnabled
        {
            get { return _isenabled; }
            set { _isenabled = value; }
        }
        /// <summary>
        /// 是否允许发送短信(默认值:1)
        /// </summary>		
        private bool _isallowsendmessage;
        /// <summary>
        /// 是否允许发送短信(默认值:1)
        /// </summary>
        [EntityTracker(Label = "是否允许发送短信", Description = "是否允许发送短信")]
        public bool IsAllowSendMessage
        {
            get { return _isallowsendmessage; }
            set { _isallowsendmessage = value; }
        }
        /// <summary>
        /// 任务执行周期
        /// </summary>		
        private int _intervalvalue;
        /// <summary>
		/// 任务执行周期
        /// </summary>
        [EntityTracker(Label = "任务执行周期", Description = "任务执行周期")]
        public int IntervalValue
        {
            get { return _intervalvalue; }
            set { _intervalvalue = value; }
        }
        /// <summary>
        /// 任务执行周期单位,可选值[second,minute,hour,day,month,year]
        /// </summary>		
        private string _intervalunit;
        /// <summary>
        /// 任务执行周期单位,可选值[second,minute,hour,day,month,year]
        /// </summary>
        [EntityTracker(Label = "任务执行周期单位", Description = "任务执行周期单位")]
        public string IntervalUnit
        {
            get { return _intervalunit; }
            set { _intervalunit = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>		
        private DateTime _createdon;
        /// <summary>
        /// 创建时间
        /// </summary>
        [EntityTracker(Label = "创建时间", Description = "创建时间")]
        public DateTime CreatedOn
        {
            get { return _createdon; }
            set { _createdon = value; }
        }
        /// <summary>
        /// 创建者ID
        /// </summary>		
        private int _createdby;
        /// <summary>
        /// 创建者ID
        /// </summary>
        [EntityTracker(Label = "创建者ID", Description = "创建者ID")]
        public int CreatedBy
        {
            get { return _createdby; }
            set { _createdby = value; }
        }
        /// <summary>
        /// 创建者名称
        /// </summary>		
        private string _createdbyuser;
        /// <summary>
        /// 创建者名称
        /// </summary>
        [EntityTracker(Label = "创建者名称", Description = "创建者名称")]
        public string CreatedByUser
        {
            get { return _createdbyuser; }
            set { _createdbyuser = value; }
        }
        /// <summary>
        /// 修改时间
        /// </summary>		
        private DateTime _modifiedon;
        /// <summary>
		/// 修改时间
        /// </summary>
        [EntityTracker(Label = "修改时间", Description = "修改时间")]
        public DateTime ModifiedOn
        {
            get { return _modifiedon; }
            set { _modifiedon = value; }
        }
        /// <summary>
        /// 修改者ID
        /// </summary>		
        private int _modifiedby;
        /// <summary>
        /// 修改者ID
        /// </summary>
        [EntityTracker(Label = "修改者ID", Description = "修改者ID")]
        public int ModifiedBy
        {
            get { return _modifiedby; }
            set { _modifiedby = value; }
        }
        /// <summary>
        /// 修改者名称
        /// </summary>		
        private string _modifiedbyuser;
        /// <summary>
        /// 修改者名称
        /// </summary>
        [EntityTracker(Label = "修改者名称", Description = "修改者名称")]
        public string ModifiedByUser
        {
            get { return _modifiedbyuser; }
            set { _modifiedbyuser = value; }
        }

        /// <summary>
        /// 消息发送方式[短信,语音]
        /// </summary>
        [EntityTracker(Label = "消息发送方式", Description = "消息发送方式")]
        public string SendMode { get; set; }
        /// <summary>
        /// 语音模版ID
        /// </summary>
        [EntityTracker(Label = "语音模版ID", Description = "语音模版ID")]
        public long AudioTelTempId { get; set; }
        /// <summary>
        /// 语音模板内容
        /// </summary>
        [EntityTracker(Label = "语音模板内容", Description = "语音模板内容")]
        public string AudioTelTempContent { get; set; }
    }
}