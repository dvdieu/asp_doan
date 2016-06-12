using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }
    }
}