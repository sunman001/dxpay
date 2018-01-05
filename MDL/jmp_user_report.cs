using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMP.Model;

namespace JMP.MDL
{
    //用户报表
    public class jmp_user_report
    {

        /// <summary>
        /// 用户报表id
        /// </summary>
        [EntityTracker(Label = "用户报表id", Description = "用户报表id")]
        public int r_id { get; set; }

        /// <summary>
        /// 应用key
        /// </summary>
        [EntityTracker(Label = "应用key", Description = "应用key")]
        public string r_app_key { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        [EntityTracker(Label = "应用名称", Description = "应用名称")]
        public string r_app_name { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [EntityTracker(Label = "用户id", Description = "用户id")]
        public int r_user_id { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [EntityTracker(Label = "用户名称", Description = "用户名称")]
        public string r_user_name { get; set; }

        /// <summary>
        /// 报表时间（统计）
        /// </summary>
        [EntityTracker(Label = "报表时间", Description = "报表时间")]
        public DateTime r_date { get; set; }

        /// <summary>
        /// 生成时间（生成）
        /// </summary>
        [EntityTracker(Label = "生成时间", Description = "生成时间")]
        public DateTime r_create { get; set; }

        /// <summary>
        /// 设备量
        /// </summary>
        [EntityTracker(Label = "设备量", Description = "设备量")]
        public decimal r_equipment { get; set; }

        /// <summary>
        /// 成功量
        /// </summary>
        [EntityTracker(Label = "成功量", Ignore = true)]
        public decimal a_succeed { get; set; }

        /// <summary>
        /// 请求量
        /// </summary>
        [EntityTracker(Label = "请求量", Ignore = true)]
        public decimal a_count { get; set; }

        /// <summary>
        /// 未支付量
        /// </summary>
        [EntityTracker(Label = "未支付量", Ignore = true)]
        public decimal a_notpay { get; set; }


        /// <summary>
        /// 支付宝收入
        /// </summary>
        [EntityTracker(Label = "支付宝收入", Ignore = true)]
        public decimal a_alipay { get; set; }

        /// <summary>
        /// 微信收入
        /// </summary>
        [EntityTracker(Label = "微信收入", Ignore = true)]
        public decimal a_wechat { get; set; }

        /// <summary>
        /// 合计
        /// </summary>
        [EntityTracker(Label = "微信收入", Ignore = true)]
        public decimal a_curr { get; set; }

        /// <summary>
        /// 投诉量
        /// </summary>
        [EntityTracker(Label = "投诉量", Ignore = true)]
        public decimal   complaint_count { get; set; }

        /// <summary>
        /// 补单量
        /// </summary>
        [EntityTracker(Label = "补单量", Ignore = true)]
        public decimal refund_count { get; set; }

        /// <summary>
        /// 银联收入
        /// </summary>
        [EntityTracker(Label = "银联收入", Ignore = true)]
        public decimal a_unionpay { get; set; }

        [EntityTracker(Label = "银联收入", Ignore = true)]
        public DateTime a_time { get; set; }

        [EntityTracker(Label = "应用名称", Ignore = true)]
        public string a_appname { get; set; }
        [EntityTracker(Label = "开发者姓名", Ignore = true)]
        public string u_realname { get; set; }
        [EntityTracker(Label = "数量", Ignore = true)]
        public string  a_equipment { get; set; }
        [EntityTracker(Label = "成功量", Ignore = true)]
        public string a_success { get; set; }
        [EntityTracker(Label = "应用ID", Ignore = true)]
        public int a_appid { get; set; }

        /// <summary>
        /// 关联关系（0:未指定,1:商务,2:代理商）
        /// </summary>
        [EntityTracker(Label = "关联关系", Ignore = true)]
        public int relation_type { get; set; }
        /// <summary>
        /// 代理商名称
        /// </summary>
        [EntityTracker(Label = "代理商名称", Ignore = true)]
        public string DisplayName { get; set; }

        /// <summary>
        /// QQ钱包收入
        /// </summary>
        [EntityTracker(Label = "QQ钱包收入", Ignore = true)]
        public decimal a_qqwallet { get; set; }
    }
}