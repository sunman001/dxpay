using System;
using JMP.Model;
namespace JMP.MDL
{
    //jmp_app_monitor_time_range
    public class jmp_app_monitor_time_range
    {

        /// <summary>
        /// Id
        /// </summary>		
        private int _id;
        /// <summary>
        /// Id
        /// </summary>
        [EntityTracker(Label = "主键", Description = "主键")]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 应用监控ID外键
        /// </summary>		
        private int _appmonitorid;
        /// <summary>
        /// 应用监控ID外键
        /// </summary>
        [EntityTracker(Label = "应用监控ID外键", Description = "应用监控ID外键")]
        public int AppMonitorId
        {
            get { return _appmonitorid; }
            set { _appmonitorid = value; }
        }
        /// <summary>
        /// 哪一个小时
        /// </summary>		
        private int _whichhour;
        /// <summary>
        /// 哪一个小时
        /// </summary>
        [EntityTracker(Label = "哪一个小时", Description = "哪一个小时")]
        public int WhichHour
        {
            get { return _whichhour; }
            set { _whichhour = value; }
        }
        /// <summary>
        /// 分钟数(默认:5)
        /// </summary>	
        [EntityTracker(Label = "分钟数", Description = "分钟数")]
        private int _minutes;
        /// <summary>
		/// 分钟数(默认:5)
        /// </summary>
        public int Minutes
        {
            get { return _minutes; }
            set { _minutes = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>	
        [EntityTracker(Label = "创建时间", Description = "创建时间")]
        private DateTime _createdon;
        /// <summary>
		/// 创建时间
        /// </summary>
        public DateTime CreatedOn
        {
            get { return _createdon; }
            set { _createdon = value; }
        }
        /// <summary>
        /// 创建者
        /// </summary>		
        private int _createdby;
        /// <summary>
        /// 创建者
        /// </summary>
        [EntityTracker(Label = "创建者", Description = "创建者")]
        public int CreatedBy
        {
            get { return _createdby; }
            set { _createdby = value; }
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

    }
}