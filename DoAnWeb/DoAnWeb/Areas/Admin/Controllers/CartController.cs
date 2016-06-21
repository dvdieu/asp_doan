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
                    itemAdd.MaDonHang = item.PhieuOrderID.ToString();
                    itemAdd.NgayLapPhieu = item.NgayLapPhieu.ToShortDateString();
                    itemAdd.GioLap = item.NgayLapPhieu.ToShortTimeString();
                    var User = listUser.Where(p => p.NguoiSuDungID == item.NguoiSuDungID).FirstOrDefault();
                    if(User ==null)
                    {
                        itemAdd.TenKhachHang = "Không xác định";
                    }
                    else
                    {
                        itemAdd.TenKhachHang = User.TenNguoiSuDung;
                    }
                    itemAdd.TongTien = item.TongTien;
                    itemAdd.TinhTrangThanhToan = item.TinhTrangThanhToan.Value;
                    itemAdd.TinhTrangGiaoHang = item.TinhTrangGiaoHang.Value;
                    itemAdd.TongSoLuong = item.TongSoLuong.ToString();
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
        public ActionResult Edit()
        {
            List<Poco_DonHang_IndexPage> danhSachDonHang = new List<Poco_DonHang_IndexPage>();
            using (ModelEntities ctx = new ModelEntities())
            {
                List<tbl_PhieuOrders> list = ctx.tbl_PhieuOrders.ToList();
                List<tbl_NguoiSuDungs> listUser = ctx.tbl_NguoiSuDungs.ToList();
                foreach (tbl_PhieuOrders item in list)
                {
                    Poco_DonHang_IndexPage itemAdd = new Poco_DonHang_IndexPage();
                    itemAdd.MaDonHang = item.PhieuOrderID.ToString();
                    itemAdd.NgayLapPhieu = item.NgayLapPhieu.ToShortDateString();
                    itemAdd.GioLap = item.NgayLapPhieu.ToShortTimeString();
                    var User = listUser.Where(p => p.NguoiSuDungID == item.NguoiSuDungID).FirstOrDefault();
                    if (User == null)
                    {
                        itemAdd.TenKhachHang = "Không xác định";
                    }
                    else
                    {
                        itemAdd.TenKhachHang = User.TenNguoiSuDung;
                    }
                    itemAdd.TongTien = item.TongTien;
                    itemAdd.TinhTrangThanhToan = item.TinhTrangThanhToan.Value;
                    itemAdd.TinhTrangGiaoHang = item.TinhTrangGiaoHang.Value;
                    itemAdd.TongSoLuong = item.TongSoLuong.ToString();
                    danhSachDonHang.Add(itemAdd);
                }

                return View(danhSachDonHang);
            }

        }
    }
}