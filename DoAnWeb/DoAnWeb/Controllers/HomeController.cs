using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
using DoAnWeb.Models.Helper;
namespace DoAnWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home/Index
        public ActionResult Index()
        {
            Poco_Index_Page item = new Poco_Index_Page();
            item.DanhSachSanPhamBanChayNhat = new List<tbl_SanPhams>();
            item.DanhSachSanPhamMoiNhat = new List<tbl_SanPhams>();
            item.DanhSachSanPhamXemNhieuNhat = new List<tbl_SanPhams>();

            using (ModelEntities ctx = new ModelEntities())
            {
                item.DanhSachSanPhamBanChayNhat = ctx.tbl_SanPhams.OrderByDescending(p => p.SoLanMua).Take(12).ToList();
                item.DanhSachSanPhamMoiNhat = ctx.tbl_SanPhams.OrderByDescending(p => p.NgayNhap).Take(12).ToList();
                item.DanhSachSanPhamXemNhieuNhat = ctx.tbl_SanPhams.OrderByDescending(p => p.SoLanXem).Take(12).ToList();
            }

            return View(item);
        }
    }
}