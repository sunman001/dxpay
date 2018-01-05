using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.MDL
{
  public   class CsCustomerServiceRecordReprot
    {
        public int MainCategory { get; set; }
        public int SubCategory { get; set; }
        public string  CreatedOn { get; set; }

        public int HandlerId { get; set; }
        public string HandlerName { get; set; }
        public float AvgRepsonse { get; set; }

        public float MaxRepsonse { get; set; }

        public float MinRepsonse { get; set; }

        public int cmonth { get; set; }
        public int yyear { get; set; }
      
    }
}
