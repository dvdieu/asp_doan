using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
using DoAnWeb.Models.Helper;
namespace DoAnWeb.Controllers
{
    public class ProductsController : Controller
    {
        private int PhanTrang(List<tbl_SanPhams> list,int soPhanTu)
        {
            return (list.Count / soPhanTu)+1;
        }
        // GET: Products/ByCartegory
        public ActionResult ByCartegory(int? id,int page=1)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            using (ModelEntities ctx = new ModelEntities())
            {
                int soPhanTu = 4;
                List<tbl_SanPhams> list = ctx.tbl_SanPhams.Where(p=>p.LoaiSanPhamID==id).ToList();
                Poco_Product_Page item = new Poco_Product_Page();
                int soTrang = PhanTrang(list, soPhanTu);
                item.DanhSachSanPham = list.Skip((page - 1) * soPhanTu).Take(soPhanTu).ToList();
                item.SoPage = PhanTrang(list, soPhanTu);
                item.CurPage = page;
                item.MaSanPham = id.Value;
                item.TenLoaiSanPham = ctx.tbl_LoaiSanPhams.First(p => p.LoaiSanPhamID == id.Value).TenLoaiSanPham;
                return View(item);
            }
        }

        public ActionResult Detail(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            using (ModelEntities ctx = new ModelEntities())
            {

                return View(ctx.tbl_SanPhams.Where(p => p.SanPhamID == id).FirstOrDefault());
            }
        }
        public ActionResult PartialViewCungLoai(int? id,int? spID)
        {
            if(!id.HasValue)
            {
                return null;
            }
            using (ModelEntities ctx = new ModelEntities())
            {
                return PartialView(ctx.tbl_SanPhams.Where(p => p.LoaiSanPhamID == id.Value && p.SanPhamID != spID).ToList());
            }
        }
    }
}