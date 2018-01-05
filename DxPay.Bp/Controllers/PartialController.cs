using System.Linq;
using System.Web.Mvc;
using JMP.TOOL;
using DxPay.Bp.Util;

namespace DxPay.Bp.Controllers
{
    public class PartialController : Controller
    {
        [LoginCheckFilter(IsCheck =false,IsRole =false)]
        public ActionResult Menu()
        {
            var uId = UserInfo.Uid;
            var rId = UserInfo.UserRid;
            var menuLoader = new MenuLoader();
            var model = menuLoader.Load(uId, rId).ToList();
            return PartialView("_Menu",model);
        }

        [LoginCheckFilter(IsCheck = false, IsRole = false)]
        public ActionResult MenuMobile()
        {
            var uId = UserInfo.Uid;
            var rId = UserInfo.UserRid;
            var menuLoader = new MenuLoader();
            var model = menuLoader.Load(uId, rId).ToList();
            return PartialView("_Menu_Mobile", model);
        }
    }
}
