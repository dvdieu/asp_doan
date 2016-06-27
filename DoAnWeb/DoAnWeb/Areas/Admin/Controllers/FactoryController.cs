using DoAnWeb.Areas.Admin.Filter;
using DoAnWeb.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DoAnWeb.Areas.Admin.Controllers
{
    [CheckLoginAdmin]
    public class FactoryController : Controller
    {
        // GET: Admin/Factory
        public ActionResult Index()
        {
            using (ModelEntities ctx = new ModelEntities())
            {
                List<tbl_NhaSanXuats> item = ctx.tbl_NhaSanXuats.ToList();
                return View(item);
            }
        }
        // GET: Admin/Factory/Add
        public ActionResult Add()
        {
             return View();
        }
        // POST: Admin/Factory/Add
        [HttpPost]
        public ActionResult Add(tbl_NhaSanXuats item)
        {
            using (ModelEntities ctx = new ModelEntities())
            {
                tbl_NhaSanXuats itemAdd = new tbl_NhaSanXuats();
                itemAdd.TenNhaSanXuat = item.TenNhaSanXuat;
                itemAdd.DaXoa = !item.DaXoa;
                ctx.tbl_NhaSanXuats.Add(itemAdd);
                ctx.SaveChanges();
                return View();
            }
        }
        // GET: Admin/Edit
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            using (ModelEntities ctx = new ModelEntities())
            {
                tbl_NhaSanXuats item = ctx.tbl_NhaSanXuats.Where(p => p.NhaSanXuatID == id.Value).FirstOrDefault();
                if (item != null)
                {
                    return View(item);
                }
                return RedirectToAction("Index");
            }
        }
        // POST: Admin/Edit
        [HttpPost]
        public ActionResult Edit(tbl_NhaSanXuats item)
        {
            using (ModelEntities ctx = new ModelEntities())
            {
                tbl_NhaSanXuats item2 = ctx.tbl_NhaSanXuats.Where(p => p.NhaSanXuatID == item.NhaSanXuatID).FirstOrDefault();
                if (item2 != null)
                {
                    item2.TenNhaSanXuat = item.TenNhaSanXuat;
                    ctx.SaveChanges();
                    return RedirectToAction("Edit", new { id = item2.NhaSanXuatID });
                }
                return RedirectToAction("Index");
            }
        }
        // POST: Admin/DeleteCat
        [HttpPost]
        public ActionResult DeleteFac(int? id)
        {
            using (ModelEntities ctx = new ModelEntities())
            {
                tbl_NhaSanXuats item2 = ctx.tbl_NhaSanXuats.Where(p => p.NhaSanXuatID == id.Value).FirstOrDefault();
                if (item2 != null)
                {
                    item2.DaXoa = true;
                    ctx.SaveChanges();
                    return RedirectToAction("Edit", new { id = id });
                }
                return RedirectToAction("Index");
            }
        }
        // POST: Admin/RestoreCat
        [HttpPost]
        public ActionResult RestoreFac(int? id)
        {
            using (ModelEntities ctx = new ModelEntities())
            {
                tbl_NhaSanXuats item2 = ctx.tbl_NhaSanXuats.Where(p => p.NhaSanXuatID == id).FirstOrDefault();
                if (item2 != null)
                {
                    item2.DaXoa = false;
                    ctx.SaveChanges();
                    return RedirectToAction("Edit", new { id = id });
                }
                return RedirectToAction("Index");
            }
        }
    }
}