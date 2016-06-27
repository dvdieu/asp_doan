using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
using DoAnWeb.Models.Helper;
using System.ComponentModel.DataAnnotations;

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
        private int PhanTrang(int list,int soPhanTu)
        {
            if(list%soPhanTu==0)
            {
                return list / soPhanTu;
            }
            return (list/ soPhanTu)+1;
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
                int soTrang = PhanTrang(list.Count, soPhanTu);
                item.DanhSachSanPham = list.Skip((page - 1) * soPhanTu).Take(soPhanTu).ToList();
                item.SoPage = PhanTrang(list.Count, soPhanTu);
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
                int soTrang = PhanTrang(list.Count, soPhanTu);
                item.DanhSachSanPham = list.Skip((page - 1) * soPhanTu).Take(soPhanTu).ToList();
                item.SoPage = PhanTrang(list.Count, soPhanTu);
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
                tbl_SanPhams n = ctx.tbl_SanPhams.Where(p => p.SanPhamID == id).FirstOrDefault();
                Poco_Detail detail = new Poco_Detail();
                detail.ThongTinSanPham = new tbl_SanPhams();
                detail.ThongTinSanPham.SanPhamID = n.SanPhamID;
                detail.ThongTinSanPham.TenSanPham = n.TenSanPham;
                detail.ThongTinSanPham.DungTich = n.DungTich;
                detail.ThongTinSanPham.NongDo = n.NongDo;
                detail.ThongTinSanPham.Gia = n.Gia;
                detail.ThongTinSanPham.SoLanMua = n.SoLanMua;
                detail.ThongTinSanPham.SoLanXem = n.SoLanXem;
                detail.ThongTinSanPham.SoLuong = n.SoLuong;
                detail.ThongTinSanPham.TinhTrang = n.TinhTrang;
                detail.ThongTinSanPham.DaXoa = n.DaXoa;
                detail.ThongTinSanPham.MoTaDai = n.MoTaDai;
                detail.ThongTinSanPham.MoTaNgan = n.MoTaNgan;
                detail.ThongTinSanPham.NgayNhap = n.NgayNhap;
                detail.ThongTinSanPham.LoaiSanPhamID = n.LoaiSanPhamID;
                detail.ThongTinSanPham.NhaSanXuatID = n.NhaSanXuatID;

                detail.TenNhaSanXuat = ctx.tbl_NhaSanXuats.Where(p => p.NhaSanXuatID == n.NhaSanXuatID).First().TenNhaSanXuat;
                detail.TenLoaiSanPham = ctx.tbl_LoaiSanPhams.Where(p => p.LoaiSanPhamID == n.LoaiSanPhamID).First().TenLoaiSanPham;

                return View(detail);
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
        //public class Search
        //{
        //    [RegularExpression("[a-zA-Z0-9]{1,10}")]
        //    public String Searchs { get; set; }
        //}

        [HttpGet]
        public ActionResult SearchProduct(String name, int page = 1)
        {
            if (name != null)
            {
               
                    using (ModelEntities ctx = new ModelEntities())
                    {

                    List<tbl_SanPhams> list = ctx.tbl_SanPhams.ToList();
                    var l = list.Where(p => p.TenSanPham.ToLower().Contains(name.ToLower()) || 
                                        p.Gia.ToString().Contains(name.ToLower()) ||
                                        p.MoTaNgan.ToLower().Contains(name.ToLower()) ||
                                        p.NongDo.ToString().ToLower().Contains(name.ToLower()) ||
                                        p.DungTich.ToString().ToLower().Contains(name.ToLower())
                                        && p.TinhTrang==true && p.DaXoa==false
                                        ).ToList();
                    int soPhanTu = 4;
                    Poco_Product_Page item = new Poco_Product_Page();
                    int soTrang = PhanTrang(l.Count, soPhanTu);
                    item.DanhSachSanPham = l.Skip((page - 1) * soPhanTu).Take(soPhanTu).ToList();
                    item.SoPage = PhanTrang(l.Count, soPhanTu);
                    item.CurPage = page;
                    ViewBag.Pages = item.SoPage;
                    ViewBag.CurPage = page;
                    ViewBag.NextPage = page + 1;
                    ViewBag.NextPage = page - 1;
                    ViewBag.SearchKey = name.Trim();
                    return View(item);

                    }
               
            }
            else//ko co name thi lay toan bo du lieu
            {
                using (ModelEntities ctx  =
                    new ModelEntities())
                {
                    List<tbl_SanPhams> list = ctx.tbl_SanPhams.Where(p => p.DaXoa == false && p.TinhTrang == true).ToList();
                    if (list.Count > 0)
                    {
                        ViewBag.data = list;
                    }
                    return View(list);
                }
            }
        }
    }

}