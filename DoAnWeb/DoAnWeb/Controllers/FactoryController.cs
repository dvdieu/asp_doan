using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class FactoryController : Controller
    {
        // GET: Factory
        [ChildActionOnly]
        public ActionResult NhaSanXuatPartial()
        {
            using (ModelEntities context = new ModelEntities())
            {
                return PartialView(context.tbl_NhaSanXuats.Where(p=>p.DaXoa==false).ToList());
            }
        }
    }
}