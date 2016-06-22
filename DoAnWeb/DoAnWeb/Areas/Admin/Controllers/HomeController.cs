using DoAnWeb.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models.Helper;

namespace DoAnWeb.Areas.Admin.Controllers
{
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