﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
using DoAnWeb.Areas.Admin.Models;
namespace DoAnWeb.Areas.Admin.Controllers
{
    public class CartController : Controller
    {
        // GET: Admin/Cart
        public ActionResult Index()
        {
            List<Poco_DonHang_IndexPage> danhSachDonHang = new List<Poco_DonHang_IndexPage>();
            using (ModelEntities ctx = new ModelEntities())
            {
                List<tbl_PhieuOrders> list = ctx.tbl_PhieuOrders.ToList();
                List<tbl_NguoiSuDungs> listUser = ctx.tbl_NguoiSuDungs.ToList();
                foreach(tbl_PhieuOrders item in list)
                {
                    Poco_DonHang_IndexPage itemAdd = new Poco_DonHang_IndexPage();
                    itemAdd.ThongTinDonHang = new Poco_Info_DonHang();
                    itemAdd.ThongTinKhachHang = new Poco_Info_KhachHang();
                    itemAdd.ThongTinDonHang.MaDonHang = item.PhieuOrderID.ToString();
                    itemAdd.ThongTinDonHang.NgayLapPhieu = item.NgayLapPhieu.ToShortDateString();
                    itemAdd.ThongTinDonHang.GioLap = item.NgayLapPhieu.ToShortTimeString();
                    var User = listUser.Where(p => p.NguoiSuDungID == item.NguoiSuDungID).FirstOrDefault();
                    if(User == null)
                    {
                        itemAdd.ThongTinKhachHang.TenKhachHang = "Không xác định";
                    }
                    else
                    {
                        itemAdd.ThongTinKhachHang.TenKhachHang = User.TenNguoiSuDung;
                    }
                    itemAdd.ThongTinDonHang.TongTien = item.TongTien;
                    itemAdd.ThongTinDonHang.TinhTrangThanhToan = item.TinhTrangThanhToan.Value;
                    itemAdd.ThongTinDonHang.TinhTrangGiaoHang = item.TinhTrangGiaoHang.Value;
                    itemAdd.ThongTinDonHang.TongSoLuong = item.TongSoLuong.ToString();
                    danhSachDonHang.Add(itemAdd);
                }

                return View(danhSachDonHang);
            }
                
        }
        // GET: Admin/Cart/UpdateThanhToan
        public ActionResult SetThanhToan(int ?id)
        {
            if(!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            else
            {
                using (ModelEntities ctx = new ModelEntities())
                {
                    tbl_PhieuOrders itemFind = ctx.tbl_PhieuOrders.Where(p => p.PhieuOrderID == id.Value).FirstOrDefault();
                    itemFind.TinhTrangThanhToan = true;
                    ctx.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        // GET: Admin/Cart/UpdateGiaoHang
        public ActionResult SetGiaoHang(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            else
            {
                using (ModelEntities ctx = new ModelEntities())
                {
                    tbl_PhieuOrders itemFind = ctx.tbl_PhieuOrders.Where(p => p.PhieuOrderID == id.Value).FirstOrDefault();
                    itemFind.TinhTrangGiaoHang = true;
                    ctx.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        // GET: Admin/Cart/Edit
        public ActionResult Edit(int? id)
        {
            if(!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            Poco_DonHang_ChiTiet chiTietDonHang = new Poco_DonHang_ChiTiet();
            using (ModelEntities ctx = new ModelEntities())
            {
                tbl_PhieuOrders phieuOder = ctx.tbl_PhieuOrders.Where(p=>p.PhieuOrderID==id).ToList().FirstOrDefault();
                chiTietDonHang.ThongTinDonHang = new Poco_Info_DonHang();
                chiTietDonHang.ThongTinDonHang.MaDonHang = phieuOder.PhieuOrderID.ToString();
                chiTietDonHang.ThongTinDonHang.NgayLapPhieu = phieuOder.NgayLapPhieu.ToString();
                
                if(string.IsNullOrEmpty(phieuOder.SoDienThoai))
                {
                    chiTietDonHang.ThongTinDonHang.SoDienThoaiNhanHang = "Không xác định";
                }
                if (string.IsNullOrEmpty(phieuOder.DiaChi))
                {
                    chiTietDonHang.ThongTinDonHang.DiaChiGiaoHang = "Không xác định";
                }
                chiTietDonHang.ThongTinDonHang.TinhTrangGiaoHang = phieuOder.TinhTrangGiaoHang.Value;
                chiTietDonHang.ThongTinDonHang.TinhTrangThanhToan = phieuOder.TinhTrangGiaoHang.Value;
                chiTietDonHang.ThongTinDonHang.TongSoLuong = phieuOder.TongSoLuong.ToString();
                chiTietDonHang.ThongTinDonHang.TongTien = phieuOder.TongTien;
                chiTietDonHang.ThongTinDonHang.GioLap = phieuOder.NgayLapPhieu.ToShortTimeString();

                tbl_NguoiSuDungs user = ctx.tbl_NguoiSuDungs.Where(p => p.NguoiSuDungID == phieuOder.NguoiSuDungID).FirstOrDefault();

                chiTietDonHang.ThongTinKhachHang = new Poco_Info_KhachHang();
                chiTietDonHang.ThongTinKhachHang.MaKhachHang = user.NguoiSuDungID.ToString();
                chiTietDonHang.ThongTinKhachHang.TenKhachHang = user.TenNguoiSuDung;
                
                chiTietDonHang.ChiTietDonHang = new List<Poco_ChiTiet_Order>();
                List<tbl_ChiTietOrders> listOrder = ctx.tbl_ChiTietOrders.Where(p => p.PhieuOrderID == id).ToList();
                foreach(tbl_ChiTietOrders item in listOrder)
                {
                    Poco_ChiTiet_Order itemAdd = new Poco_ChiTiet_Order();
                    var a = ctx.tbl_SanPhams.Where(p => p.SanPhamID == item.SanPhamID).FirstOrDefault();
                    if(a!=null)
                    {
                        itemAdd.TenSanPham = a.TenSanPham;
                    }
                    itemAdd.SanPhamID = item.SanPhamID.ToString();
                    itemAdd.DonGia = item.DonGia;
                    itemAdd.SoLuong = item.SoLuong;
                    itemAdd.ThanhTien = item.ThanhTien;
                    chiTietDonHang.ChiTietDonHang.Add(itemAdd);
                }
                return View(chiTietDonHang);
            }

        }
    }
}