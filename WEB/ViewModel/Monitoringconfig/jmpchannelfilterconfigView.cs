using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB.ViewModel.Monitoringconfig
{
    public class jmpchannelfilterconfigView
    {
        public int TypeId { get; set; }
        public int TargetId { get; set; }
        public int RelatedId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedByUserId { get; set; }
       
    
        public string CreatedByUserName { get; set; }
    }
}