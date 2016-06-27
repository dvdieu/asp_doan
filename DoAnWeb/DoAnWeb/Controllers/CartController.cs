using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
using DoAnWeb.Models.Helper;
using DoAnWeb.ClassHelper;

namespace DoAnWeb.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            decimal total = 0;
            var cart = CurrentContext.Cart();
            var list = new List<CartItemModel>();

            using (ModelEntities ctx = new ModelEntities())
            {

                foreach (CartItem ci in cart.Items)
                {
                    var pro = ctx.tbl_SanPhams.Where(p => p.SanPhamID == ci.ProID).FirstOrDefault();
                    if (pro != null)
                    {
                        var cim = new CartItemModel
                        {
                            Item = ci,
                            Product = pro
                        };

                        total += (decimal)(pro.Gia * ci.Quantity);
                        list.Add(cim);
                    }
                }
            }
            ViewBag.Total = total;
            return View(list);
        }

        [HttpPost]
        public ActionResult Add(CartItem item)
        {

            CurrentContext.Cart().Add(item);
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public ActionResult AddInByCatView(CartItem item, int catId, int curpage)
        {

            CurrentContext.Cart().Add(item);
            return RedirectToAction("Index", "Cart");
            //return RedirectToAction("ByCartegory", "Products", new {
            //    id = catId,
            //    page = curpage
            //});
        }

        //POST: /Cart/RemoveItem
        [HttpPost]
        public ActionResult RemoveItem(int proId)
        {

            CurrentContext.Cart().RemoveItem(proId);
            return RedirectToAction("Index", "Cart");
        }

        //POST: /Cart/RemoveItem
        [HttpPost]
        public ActionResult UpdateItem(int proId, int quantity)
        {

            CurrentContext.Cart().UpdateItem(proId, quantity);
            return RedirectToAction("Index", "Cart");
        }

        //POST: /Cart/Checkout
        [HttpPost]
        public ActionResult Checkout(tbl_NguoiSuDungs itemUser)
        {
            tbl_PhieuOrders ord = new tbl_PhieuOrders
            {
                NgayLapPhieu = DateTime.Now,
                NguoiSuDungID = CurrentContext.getCurrenUser().NguoiSuDungID,
                TongSoLuong = 0,
                TongTien = 0,
                DiaChi = null,
                SoDienThoai = null,
                TinhTrangGiaoHang = false,
                TinhTrangThanhToan = false,
                DaXoa = false
            };

            using (ModelEntities ctx = new ModelEntities())
            {
                decimal total = 0;
                int totalAmount = 0;
                foreach (CartItem item in CurrentContext.Cart().Items)
                {
                    tbl_SanPhams pro = ctx.tbl_SanPhams.Where(p => p.SanPhamID == item.ProID).FirstOrDefault();
                    if (pro != null)
                    {
                        tbl_ChiTietOrders d = new tbl_ChiTietOrders
                        {
                            SanPhamID = item.ProID,
                            SoLuong = item.Quantity,
                            DonGia = (decimal)pro.Gia,
                            ThanhTien = (decimal)(item.Quantity * pro.Gia)
                        };

                        ord.tbl_ChiTietOrders.Add(d);
                        totalAmount += d.SoLuong;
                        total += d.ThanhTien;
                    }
                }
                ord.TongSoLuong = totalAmount;
                ord.TongTien = total;
                ord.TinhTrangGiaoHang = false;
                ord.TinhTrangThanhToan = false;
                int curID = CurrentContext.getCurrenUser().NguoiSuDungID;
                var user = ctx.tbl_NguoiSuDungs.Where(p => p.NguoiSuDungID == curID).FirstOrDefault();
                if (itemUser.DiaChi == null)
                    ord.DiaChi = user.DiaChi;
                else
                    ord.DiaChi = itemUser.DiaChi;
                if (itemUser.SoDienThoai == null)
                    ord.SoDienThoai = user.SoDienThoai;
                else
                    ord.SoDienThoai = itemUser.SoDienThoai;

                ctx.tbl_PhieuOrders.Add(ord);
                ctx.SaveChanges();
                CurrentContext.Cart().Items.Clear();
                return RedirectToAction("Index", "Cart");
            }
        }
    }
}