using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    //阶梯手续费表
    public class jmp_poundage
    {

        /// <summary>
        /// 主键
        /// </summary>		
        private int _p_id;
        [EntityTracker(Label = "主键", Description = "主键")]
        public int p_id
        {
            get { return _p_id; }
            set { _p_id = value; }
        }

        /// <summary>
        /// 区间价格开始
        /// </summary>		
        private int _p_sprice;
        [EntityTracker(Label = "区间价格开始", Description = "区间价格开始")]
        public int p_sprice
        {
            get { return _p_sprice; }
            set { _p_sprice = value; }
        }

        /// <summary>
        /// 区间价格结束
        /// </summary>		
        private int _p_eprice;
        [EntityTracker(Label = "区间价格结束", Description = "区间价格结束")]
        public int p_eprice
        {
            get { return _p_eprice; }
            set { _p_eprice = value; }
        }

        /// <summary>
        /// 手续费率
        /// </summary>		
        private decimal _p_rate;
        [EntityTracker(Label = "手续费率", Description = "手续费率")]
        public decimal p_rate
        {
            get { return _p_rate; }
            set { _p_rate = value; }
        }

        /// <summary>
        /// 是否启用
        /// </summary>		
        private int _p_state;
        [EntityTracker(Label = "是否启用", Description = "是否启用")]
        public int p_state
        {
            get { return _p_state; }
            set { _p_state = value; }
        }

    }
}