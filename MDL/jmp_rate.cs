using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMP.Model;

namespace JMP.MDL
{
    ///<summary>
    ///手续费比例以及扣量设置
    ///</summary>
    public class jmp_rate
    {

        /// <summary>
        /// 主键
        /// </summary>	
        [EntityTracker(Label = "主键", Description = "主键")]
        public int r_id { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>	
        [EntityTracker(Label = "用户id", Description = "用户id")]
        public int r_userid { get; set; }

        /// <summary>
        /// 支付类型id
        /// </summary>	
        [EntityTracker(Label = "支付类型id", Description = "支付类型id")]
        public int r_paymodeid { get; set; }

        /// <summary>
        /// 手续费比例
        /// </summary>	
        [EntityTracker(Label = "手续费比例", Description = "手续费比例")]
        public decimal r_proportion { get; set; }

        /// <summary>
        /// 扣量比例
        /// </summary>		
        [EntityTracker(Label = "扣量比例", Description = "扣量比例")]
        public decimal r_klproportion { get; set; }

        /// <summary>
        /// 状态（0：正常，1冻结）
        /// </summary>		
        [EntityTracker(Label = "状态", Description = "状态")]
        public int r_state { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>		
        [EntityTracker(Label = "添加时间", Description = "添加时间")]
        public DateTime r_time { get; set; }

        /// <summary>
        /// 录入人
        /// </summary>	
        [EntityTracker(Label = "录入人", Description = "录入人")]
        public string r_name { get; set; }


        #region 新增其他表实体
        /// <summary>
        /// 支付类型名称
        /// </summary>
        [EntityTracker(Label = "支付类型名称", Ignore = true)]
        public string p_name { get; set; }

        /// <summary>
        /// 用户真实姓名
        /// </summary>
        [EntityTracker(Label = "用户真实姓名", Ignore = true)]
        public string u_realname { get; set; }

        /// <summary>
        /// 用户登陆名
        /// </summary>
        [EntityTracker(Label = "用户登陆名", Ignore = true)]
        public string u_email { get; set; }

        /// <summary>
        /// 支付类型id
        /// </summary>
        [EntityTracker(Label = "支付类型id", Ignore = true)]
        public int p_id { get; set; }
        #endregion

    }
}
