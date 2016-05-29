using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
namespace DoAnWeb.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category/DanhMucPartial
        [ChildActionOnly]
        public ActionResult DanhMucPartial()
        {
            using (ModelEntities context = new ModelEntities())
            {

               return PartialView(context.tbl_LoaiSanPhams.ToList());
            }
        }
    }
}