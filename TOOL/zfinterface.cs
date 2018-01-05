using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace JMP.TOOL
{
    /// <summary>
    /// 支付配置接口
    /// </summary>
    public class zfinterface
    {
        JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
        JMP.MDL.jmp_interface mo = new JMP.MDL.jmp_interface();
        /// <summary>
        /// 获取支付宝配置信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string zfpzjk(int tid)
        {
            string str = "";
            mo = bll.strzf("ZFB", tid);
            if (mo != null)
            {
                str = mo.l_str + "," + mo.l_id.ToString();
            }
            return str;
        }
        /// <summary>
        /// 获取微信配置接口
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string wxpzjk(int tid)
        {
            string str = "";
            mo = bll.strzf("WX", tid);
            if (mo != null)
            {
                str = mo.l_str;
            }
            return str;
        }
        /// <summary>
        /// 获取威富通配置接口
        /// </summary>
        /// <returns></returns>
        public string wftpzjk(int tid)
        {
            string str = "";
            mo = bll.strzf("WFT", tid);
            if (mo != null)
            {
                str = mo.l_str + "," + mo.l_id.ToString();
            }
            return str;
        }
    }
}
