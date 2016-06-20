using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models.Helper;
using DoAnWeb.Models;

namespace DoAnWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home/Index
        public ActionResult Index()
        {
            return View();
        }
        public Poco_Index_Page GetData()
        {
            Poco_Index_Page item = new Poco_Index_Page();
            using (ModelEntities ctx = new ModelEntities())
            {
                item.DanhSachSanPhamMoiNhat = ctx.tbl_SanPhams.OrderByDescending(p => p.NgayNhap).Take(12).ToList();
                item.DanhSachSanPhamBanChayNhat = ctx.tbl_SanPhams.OrderByDescending(p => p.SoLanMua).Take(12).ToList();
                item.DanhSachSanPhamXemNhieuNhat = ctx.tbl_SanPhams.OrderByDescending(p => p.SoLanXem).Take(12).ToList();
                return item;
            }
        }
    }
}