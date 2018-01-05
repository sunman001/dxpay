using System.Collections.Generic;

namespace WEB.Models.AppUser
{
    public class MerchantListViewModel
    {
        public MerchantListViewModel()
        {
            SelectedUsers = new List<SelectedAppUserModel>();
            Merchants = new List<MerchantAssignModel>();
        }
        public List<SelectedAppUserModel> SelectedUsers { get; set; }
        public List<MerchantAssignModel> Merchants { get; set; }
    }

    public class SelectedAppUserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
    }

    public class MerchantAssignModel
    {
        public int MerchantId { get; set; }
        public string MerchantRealName { get; set; }
    }
}