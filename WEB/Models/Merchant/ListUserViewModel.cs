using System.Collections.Generic;
using JMP.MDL;

namespace WEB.Models.Merchant
{
    public class ListUserViewModel : Pagination
    {
        public ListUserViewModel()
        {
            Users = new List<jmp_locuser>();
            MerchantSearchModel = new UserSearchModel();
        }
        public IEnumerable<jmp_locuser> Users { get; set; }
        public string ButtonsTags { get; set; }
        public UserSearchModel MerchantSearchModel { get; set; }

    }

    public class UserSearchModel
    {
        public string SearchKey { get; set; }
        public string State { get; set; }
        public int Sort { get; set; }
    }
}