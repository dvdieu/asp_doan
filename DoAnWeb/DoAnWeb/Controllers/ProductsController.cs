using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
using DoAnWeb.Models.Helper;
namespace DoAnWeb.Controllers
{
    enum Loai
    {
        LoaiSanPham=0,
        NhaSanXuat=1
    }
    
    public class ProductsController : Controller
    {
        Loai DangTruyVan;
        private int PhanTrang(List<tbl_SanPhams> list,int soPhanTu)
        {
            if(list.Count%soPhanTu==0)
            {
                return list.Count / soPhanTu;
            }
            return (list.Count / soPhanTu)+1;
        }
        // GET: Products/ByCartegory
        public ActionResult ByCartegory(int? id, int page = 1)
        {
            DangTruyVan = Loai.LoaiSanPham;
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            using (ModelEntities ctx = new ModelEntities())
            {
                ViewBag.CatID = id;
                int soPhanTu = 4;
                List<tbl_SanPhams> list = ctx.tbl_SanPhams.Where(p => p.LoaiSanPhamID == id && p.DaXoa == false && p.TinhTrang == true).ToList();
                Poco_Product_Page item = new Poco_Product_Page();
                int soTrang = PhanTrang(list, soPhanTu);
                item.DanhSachSanPham = list.Skip((page - 1) * soPhanTu).Take(soPhanTu).ToList();
                item.SoPage = PhanTrang(list, soPhanTu);
                item.CurPage = page;
                item.MaSanPham = id.Value;
                ViewBag.Pages = item.SoPage;
                ViewBag.CurPage = page;
                ViewBag.NextPage = page + 1;
                ViewBag.NextPage = page - 1;
                item.TenLoaiSanPham = ctx.tbl_LoaiSanPhams.First(p => p.LoaiSanPhamID == id.Value).TenLoaiSanPham;
                if (list.FirstOrDefault()!=null)
                {
                    int NSXID = list.First().NhaSanXuatID.Value;
                    item.TenNhaSanXuat = ctx.tbl_NhaSanXuats.First(p => p.NhaSanXuatID == NSXID).TenNhaSanXuat;
                }
                else
                {
                    item.TenNhaSanXuat = string.Empty;
                }
                return View(item);
            }
        }
        // GET: Products/ByFactory
        public ActionResult ByFactory(int? id,int page=1)
        {
            DangTruyVan = Loai.NhaSanXuat;
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            using (ModelEntities ctx = new ModelEntities())
            {
                ViewBag.CatID = id;
                int soPhanTu = 4;
                List<tbl_SanPhams> list = ctx.tbl_SanPhams.Where(p=>p.NhaSanXuatID==id &&p.DaXoa==false && p.TinhTrang==true).ToList();
                Poco_Product_Page item = new Poco_Product_Page();
                int soTrang = PhanTrang(list, soPhanTu);
                item.DanhSachSanPham = list.Skip((page - 1) * soPhanTu).Take(soPhanTu).ToList();
                item.SoPage = PhanTrang(list, soPhanTu);
                item.CurPage = page;
                item.MaSanPham = id.Value;
                ViewBag.Pages = item.SoPage;
                ViewBag.CurPage = page;
                ViewBag.NextPage = page + 1;
                ViewBag.NextPage = page - 1;
                if(list.FirstOrDefault()!=null)
                {
                    int LSPID = list.First().LoaiSanPhamID.Value;
                    item.TenLoaiSanPham = ctx.tbl_LoaiSanPhams.First(p => p.LoaiSanPhamID == LSPID).TenLoaiSanPham;
                }
                else
                {
                    item.TenLoaiSanPham = string.Empty;
                }
                item.TenNhaSanXuat = ctx.tbl_NhaSanXuats.FirstOrDefault(p => p.NhaSanXuatID == id.Value).TenNhaSanXuat;
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
                tbl_SanPhams item = ctx.tbl_SanPhams.Where(p => p.SanPhamID == id.Value).FirstOrDefault();
                if(item !=null)
                {
                    item.SoLanXem++;
                    ctx.SaveChanges();
                }
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
                if (DangTruyVan == Loai.LoaiSanPham)
                {
                    ViewBag.TittlePartialProduct = "SẢN PHẨM CÙNG LOẠI";
                    return PartialView(ctx.tbl_SanPhams.Where(p => p.LoaiSanPhamID == id.Value && p.SanPhamID != spID && p.DaXoa == false && p.TinhTrang == true).ToList());
                }
                else
                {
                    ViewBag.TittlePartialProduct = "SẢN PHẨN CÙNG NHÀ SẢN XUẤT";
                    return PartialView(ctx.tbl_SanPhams.Where(p => p.NhaSanXuatID == id.Value && p.SanPhamID != spID && p.DaXoa == false && p.TinhTrang == true).ToList());
                }
            }
        }
    }
}