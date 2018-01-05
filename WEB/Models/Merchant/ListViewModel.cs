using System.Collections.Generic;
using JMP.MDL;

namespace WEB.Models.Merchant
{
    public class ListViewModel : Pagination
    {
        public ListViewModel()
        {
            Merchants = new List<jmp_merchant>();
            MerchantSearchModel = new MerchantSearchModel();
        }
        public IEnumerable<jmp_merchant> Merchants { get; set; }
        public string ButtonsTags { get; set; }
        public MerchantSearchModel MerchantSearchModel { get; set; }

    }

    public class MerchantSearchModel
    {
        public string SearchKey { get; set; }
        public string State { get; set; }
        public int Sort { get; set; }
    }
}