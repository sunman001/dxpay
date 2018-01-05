using JMP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.MDL
{
    /// <summary>
    /// 应用报表
    /// </summary>
    public class jmp_app_report
    {
        public jmp_app_report()
        { }
        #region Model
        private int _r_id;
        private DateTime _r_times;
        private DateTime _r_starttime;
        private string _r_username;
        private int _r_uid;
        private string _r_app_key;
        private string _r_app_name;
        private int _r_equipment;
        private int _r_succeed;
        private int _r_notpay;
        private decimal _r_alipay;
        /// <summary>
        /// 应用报表id
        /// </summary>
        [EntityTracker(Label = "应用报表id", Description = "应用报表id")]
        public int r_id
        {
            set { _r_id = value; }
            get { return _r_id; }
        }
        /// <summary>
        /// 时间（2016-03-23）
        /// </summary>
        [EntityTracker(Label = "时间", Description = "时间")]
        public DateTime r_times
        {
            set { _r_times = value; }
            get { return _r_times; }
        }
        /// <summary>
        /// 报表生成时间
        /// </summary>
        [EntityTracker(Label = "报表生成时间", Description = "报表生成时间")]
        public DateTime r_starttime
        {
            set { _r_starttime = value; }
            get { return _r_starttime; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        [EntityTracker(Label = "用户名", Description = "用户名")]
        public string r_username
        {
            set { _r_username = value; }
            get { return _r_username; }
        }
        /// <summary>
        /// 用户id
        /// </summary>
        [EntityTracker(Label = "用户id", Description = "用户id")]
        public int r_uid
        {
            set { _r_uid = value; }
            get { return _r_uid; }
        }
        /// <summary>
        /// 应用key
        /// </summary>
        [EntityTracker(Label = "应用key", Description = "应用key")]
        public string r_app_key
        {
            set { _r_app_key = value; }
            get { return _r_app_key; }
        }
        /// <summary>
        /// 应用名称
        /// </summary>
        [EntityTracker(Label = "应用名称", Description = "应用名称")]
        public string r_app_name
        {
            set { _r_app_name = value; }
            get { return _r_app_name; }
        }
        /// <summary>
        /// 设备量
        /// </summary>
        [EntityTracker(Label = "设备量", Description = "设备量")]
        public int r_equipment
        {
            set { _r_equipment = value; }
            get { return _r_equipment; }
        }
        /// <summary>
        /// 支付成功量
        /// </summary>
        [EntityTracker(Label = "支付成功量", Description = "支付成功量")]
        public int r_succeed
        {
            set { _r_succeed = value; }
            get { return _r_succeed; }
        }
        /// <summary>
        /// 未支付量
        /// </summary>
        [EntityTracker(Label = "支付成功量", Description = "支付成功量")]
        public int r_notpay
        {
            set { _r_notpay = value; }
            get { return _r_notpay; }
        }
        /// <summary>
        /// 支付宝收入
        /// </summary>
        [EntityTracker(Label = "支付宝收入", Description = "支付宝收入")]
        public decimal r_alipay
        {
            set { _r_alipay = value; }
            get { return _r_alipay; }
        }
        #endregion Model
    }
}
