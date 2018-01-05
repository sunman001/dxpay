using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayForAnother.Model
{
  public   class SelectChinaPayParameter
    {
        public string merDate { get; set; }
        public string merSeqId { get; set; }
        public  int pid { get; set; }

        public string PrivateKey { get; set; }

        public string p_PublicKey { get; set; }
        public string merId { get; set; }
    }
}
