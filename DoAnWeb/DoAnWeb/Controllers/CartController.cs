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
           
            using (ModelEntities ctx = new ModelEntities()){ 

                foreach (CartItem ci in cart.Items) {
                    var pro = ctx.tbl_SanPhams.Where(p => p.SanPhamID == ci.ProID).FirstOrDefault();
                    if (pro != null) {
                        var cim = new CartItemModel {
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
        public ActionResult Add(CartItem item) {

            CurrentContext.Cart().Add(item);
            return RedirectToAction("Detail", "Products", new { id = item.ProID});
        }

        [HttpPost]
        public ActionResult AddInByCatView(CartItem item, int catId, int curpage)
        {

            CurrentContext.Cart().Add(item);
            return RedirectToAction("ByCartegory", "Products", new {
                id = catId,
                page = curpage
            });
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

            CurrentContext.Cart().UpdateItem(proId,quantity);
            return RedirectToAction("Index", "Cart");
        }
    }
}