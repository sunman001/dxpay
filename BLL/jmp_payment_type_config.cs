using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.BLL
{
    /// <summary>
    /// 支付通道配置表
    /// </summary>
    public class jmp_payment_type_config
    {

        private readonly JMP.DAL.jmp_payment_type_config dal = new JMP.DAL.jmp_payment_type_config();

        /// <summary>
        /// 添加方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(JMP.MDL.jmp_payment_type_config model)
        {
            return dal.Add(model);
        }
        /// <summary>
        /// 修改方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(JMP.MDL.jmp_payment_type_config model)
        {
            return dal.Update(model);
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_payment_type_config> ListPage(int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.ListPage(pageIndexs, PageSize, out pageCount);
        }
        /// <summary>
        /// 根据支付类型查询支付参数信息
        /// </summary>
        /// <param name="PaymentTypeId"></param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_payment_type_config> SelectPaymentTypeId(int PaymentTypeId)
        {
            return dal.SelectPaymentTypeId(PaymentTypeId);
        }

        /// <summary>
        /// 查询支付配置信息
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">分页数量</param>
        /// <param name="pageCount">总数量</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_payment_type_config> SelectList(string sql, string Order, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(sql, Order, pageIndexs, PageSize, out pageCount);
        }

        /// <summary>
        /// 根据id查询相关信息
        /// </summary>
        public JMP.MDL.jmp_payment_type_config GetModels(int l_id)
        {
            return dal.GetModels(l_id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_payment_type_config GetModel(int l_id)
        {

            return dal.GetModel(l_id);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateLocUserState(string u_idlist, int state)
        {
            return dal.UpdateState(u_idlist, state);
        }
    }
}
