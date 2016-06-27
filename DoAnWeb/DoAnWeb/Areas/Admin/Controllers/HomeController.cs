using System.Web.Mvc;
using DoAnWeb.Models.Helper;
using DoAnWeb.Areas.Admin.Filter;

namespace DoAnWeb.Areas.Admin.Controllers
{
    [CheckLoginAdmin]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            Poco_Index_Page item = new Poco_Index_Page();
            item.DanhSachSanPhamBanChayNhat = null;
            return View(item);
        }
    }
}