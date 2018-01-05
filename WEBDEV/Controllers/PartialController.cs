using System.Linq;
using System.Web.Mvc;
using JMP.TOOL;
using WEBDEV.Util;

namespace WEBDEV.Controllers
{
    public class PartialController : Controller
    {
        public ActionResult Menu()
        {
            var uId = UserInfo.Uid;
            var rId = UserInfo.UserRid;
            var menuLoader = new MenuLoader();
            var model = menuLoader.Load(uId, rId).ToList();
            return PartialView("_Menu", model);
        }

        [LoginCheckFilter(IsCheck = true, IsRole = false)]
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
