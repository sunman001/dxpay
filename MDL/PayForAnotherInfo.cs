using JMP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.MDL
{
    public class PayForAnotherInfo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [EntityTracker(Label = "主键", Description = "主键")]
        public int p_Id { get; set; }

        /// <summary>
        /// 接口名称
        /// </summary>
        [EntityTracker(Label = "接口名称", Description = "接口名称")]
        public string p_InterfaceName { get; set; }

        /// <summary>
        /// 接口类型(代付通道表ID)
        /// </summary>
        [EntityTracker(Label = "接口类型", Description = "接口类型")]
        public int p_InterfaceType { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        [EntityTracker(Label = "商户号", Description = "商户号")]
        public string p_MerchantNumber { get; set; }

        /// <summary>
        /// 秘钥类型 1:秘钥字符串 ，2：秘钥路径地址
        /// </summary>
        [EntityTracker(Label = "秘钥类型", Description = "秘钥类型")]
        public int p_KeyType { get; set; }

        /// <summary>
        /// 私钥
        /// </summary>
        [EntityTracker(Label = "私钥", Description = "私钥")]
        public string p_PrivateKey { get; set; }

        /// <summary>
        /// 公钥
        /// </summary>
        [EntityTracker(Label = "公钥", Description = "公钥")]
        public string p_PublicKey { get; set; }

        /// <summary>
        /// 启用状态（0：禁用,1：启用）
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        [EntityTracker(Label = "操作人", Description = "操作人")]
        public string p_auditor { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [EntityTracker(Label = "操作时间", Description = "操作时间")]
        public DateTime? p_auditortime { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>
        [EntityTracker(Label = "添加人", Description = "添加人")]
        public string p_append { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [EntityTracker(Label = "添加时间", Description = "添加时间")]
        public DateTime? p_appendtime { get; set; }

        /// <summary>
        /// 主通道
        /// </summary>
        [EntityTracker(Label = "主通道", Description = "主通道", Ignore = true)]
        public string ChannelName { get; set; }

        /// <summary>
        /// 主通道标识
        /// </summary>
        [EntityTracker(Label = "主通道标识", Description = "主通道标识", Ignore = true)]
        public string ChannelIdentifier { get; set; }

      
    }
}
