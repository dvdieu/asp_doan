using DoAnWeb.Areas.Admin.Filter;
using DoAnWeb.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DoAnWeb.Areas.Admin.Controllers
{
    [CheckLoginAdmin]
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        public ActionResult Index()
        {
            using (ModelEntities ctx = new ModelEntities())
            {
                List<tbl_LoaiSanPhams> item = ctx.tbl_LoaiSanPhams.ToList();
                return View(item);
            }     
        }
        // GET: Admin/Category/Add
        public ActionResult Add()
        {
           return View();
        }
        // POST: Admin/Category/Add
        [HttpPost]
        public ActionResult Add(tbl_LoaiSanPhams item)
        {
            using (ModelEntities ctx = new ModelEntities())
            {
                tbl_LoaiSanPhams itemAdd = new tbl_LoaiSanPhams();
                itemAdd.TenLoaiSanPham = item.TenLoaiSanPham;
                itemAdd.DaXoa = !item.DaXoa;
                ctx.tbl_LoaiSanPhams.Add(itemAdd);
                ctx.SaveChanges();
                return View();
            }
        }
        // GET: Admin/Edit
        public ActionResult Edit(int? id)
        {
            if(!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            using (ModelEntities ctx = new ModelEntities())
            {
                tbl_LoaiSanPhams item = ctx.tbl_LoaiSanPhams.Where(p=>p.LoaiSanPhamID == id.Value).FirstOrDefault();
                if(item !=null)
                {
                    return View(item);
                }
                return RedirectToAction("Index");
            }
        }
        // POST: Admin/Edit
        [HttpPost]
        public ActionResult Edit(tbl_LoaiSanPhams item)
        {
            using (ModelEntities ctx = new ModelEntities())
            {
                tbl_LoaiSanPhams item2 = ctx.tbl_LoaiSanPhams.Where(p => p.LoaiSanPhamID == item.LoaiSanPhamID).FirstOrDefault();
                if (item2 != null)
                {
                    item2.TenLoaiSanPham = item.TenLoaiSanPham;
                    ctx.SaveChanges();
                    return View(item2);
                }
                return RedirectToAction("Index");
            }
        }
        // POST: Admin/DeleteCat
        [HttpPost]
        public ActionResult DeleteCat(int? id)
        {
            using (ModelEntities ctx = new ModelEntities())
            {
                tbl_LoaiSanPhams item2 = ctx.tbl_LoaiSanPhams.Where(p => p.LoaiSanPhamID == id.Value).FirstOrDefault();
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
        public ActionResult RestoreCat(int? id)
        {
            using (ModelEntities ctx = new ModelEntities())
            {
                tbl_LoaiSanPhams item2 = ctx.tbl_LoaiSanPhams.Where(p => p.LoaiSanPhamID == id).FirstOrDefault();
                if (item2 != null)
                {
                    item2.DaXoa = false;
                    ctx.SaveChanges();
                    return RedirectToAction("Edit",new { id = id });
                }
                return RedirectToAction("Index");
            }
        }
    }
}