using JMP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.MDL
{
    ///<summary>
    ///每日应用汇总按10分钟统计
    ///</summary>
    public class jmp_appcountminute
    {

        /// <summary>
        /// 主键
        /// </summary>		
        [EntityTracker(Label = "主键", Description = "主键")]
        public int a_id { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>	
        [EntityTracker(Label = "应用名称", Description = "应用名称")]
        public string a_appname { get; set; }
        /// <summary>
        /// 应用id
        /// </summary>		
        [EntityTracker(Label = "应用id", Description = "应用id")]
        public int a_appid { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>	
        [EntityTracker(Label = "用户id", Description = "用户id")]
        public int a_uerid { get; set; }
        /// <summary>
        /// 设备量
        /// </summary>	
        [EntityTracker(Label = "设备量", Description = "设备量")]
        public decimal a_equipment { get; set; }
        /// <summary>
        /// 请求量
        /// </summary>	
        [EntityTracker(Label = "请求量", Description = "请求量")]
        public decimal a_count { get; set; }
        /// <summary>
        /// 成功量
        /// </summary>	
        [EntityTracker(Label = "成功量", Description = "成功量")]
        public decimal a_success { get; set; }
        /// <summary>
        /// 未支付量
        /// </summary>	
        [EntityTracker(Label = "未支付量", Description = "未支付量")]
        public decimal a_notpay { get; set; }
        /// <summary>
        /// 请求率
        /// </summary>	
        [EntityTracker(Label = "请求率", Description = "请求率")]
        public decimal a_request { get; set; }
        /// <summary>
        /// 付费成功率
        /// </summary>	
        [EntityTracker(Label = "付费成功率", Description = "付费成功率")]
        public decimal a_successratio { get; set; }
        /// <summary>
        /// 支付宝收入
        /// </summary>	
        [EntityTracker(Label = "支付宝收入", Description = "支付宝收入")]
        public decimal a_alipay { get; set; }
        /// <summary>
        /// 微信收入
        /// </summary>	
        [EntityTracker(Label = "微信收入", Description = "微信收入")]
        public decimal a_wechat { get; set; }
        /// <summary>
        /// 合计收入
        /// </summary>	
        [EntityTracker(Label = "合计收入", Description = "合计收入")]
        public decimal a_curr { get; set; }
        /// <summary>
        /// arpu值
        /// </summary>	
        [EntityTracker(Label = "arpu值", Description = "arpu值")]
        public decimal a_arpur { get; set; }
        /// <summary>
        /// 生成时间
        /// </summary>	
        [EntityTracker(Label = "生成时间", Description = "生成时间")]
        public DateTime a_datetime { get; set; }
        /// <summary>
        /// 银联收入
        /// </summary>	
        [EntityTracker(Label = "银联收入", Description = "银联收入")]
        public decimal a_unionpay { get; set; }
        /// <summary>
        /// 下单总金额
        /// </summary>	
        [EntityTracker(Label = "下单总金额", Description = "下单总金额")]
        public decimal a_money { get; set; }

        /// <summary>
        /// QQ钱包收入
        /// </summary>
        [EntityTracker(Label = "QQ钱包收入", Description = "QQ钱包收入")]
        public decimal a_qqwallet { get; set; }

    }
}
