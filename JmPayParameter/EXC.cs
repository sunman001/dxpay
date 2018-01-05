using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JmPayParameter
{

    /// <summary>
    /// 继承一个错误提示码跑出异常使用
    /// </summary>
    public class Exc : Exception
    {
        public InnerResponse Response { get; set; }
    }
}
