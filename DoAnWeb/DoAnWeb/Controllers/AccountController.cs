using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
using DoAnWeb.Models.Helper;
using DoAnWeb.ClassHelper;
using BotDetect.Web.Mvc;
using DoAnWeb.Filter;

namespace DoAnWeb.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account/Login
        public ActionResult Login()
        {
            if(CurrentContext.isLogin()==true)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        // POST: Account/Login
        [HttpPost]
        public ActionResult Login(Poco_Login item)
        {
            using (ModelEntities ctx = new ModelEntities())
            {
                tbl_NguoiSuDungs usr = ctx.tbl_NguoiSuDungs.Where(p => p.TenDangNhap == item.UserName).FirstOrDefault();
                if(usr==null)
                {
                    //Khong ton tai use
                    ViewBag.Error = "zzz";
                    return View(item);
                }
                else
                {
                    if(String.Compare(StringUtils.Md5(item.Password),usr.MatKhau)!=0)
                    {
                        ViewBag.Error = "zzz";
                        return View(item);
                    }
                    else
                    {
                        Session["IsLogin"] = 1;
                        Session["CurUser"] = usr;
                        if(item.Remeber)
                        {
                            HttpContext.Response.Cookies["Username"].Value = usr.TenDangNhap;
                            HttpContext.Response.Cookies["Username"].Expires = DateTime.Now.AddDays(7);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
        }

        //POST: Account/Logout
        [HttpPost]
        public ActionResult Logout()
        {
            CurrentContext.detroySesstion();
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Register
        public ActionResult Register()
        {
            if(CurrentContext.isLogin()==true)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(1);
        }
        // POST: Account/Register
        [HttpPost]
        [CaptchaValidation("CaptchaCode", "ExampleCaptcha")]
        public ActionResult Register(Poco_DangKy item)
        {   
            if(!ModelState.IsValid)
            {
                return View(3);
            }
            tbl_NguoiSuDungs obj = new tbl_NguoiSuDungs();
            obj.TenDangNhap = item.TenDangNhap;
            using (ModelEntities ctx = new ModelEntities())
            {
                if(ctx.tbl_NguoiSuDungs.Any(p => p.TenDangNhap == item.TenDangNhap))
                {
                    return View(2);
                }
            }
            obj.MatKhau = StringUtils.Md5(item.MatKhau);
            obj.GioiTinh = item.GioiTinh;
            obj.NgaySinh = DateTime.ParseExact(item.NgaySinh, "dd/MM/yyyy", null);
            obj.DiaChi = item.DiaChi;
            obj.SoDienThoai = item.SoDienThoai;
            obj.TenNguoiSuDung = item.HoTen;
            obj.Quyen = 0;
            obj.DaXoa = false;
            using (ModelEntities ctx = new ModelEntities())
            {
                ctx.tbl_NguoiSuDungs.Add(obj);
                ctx.SaveChanges();
            }
            return View(0);
        }

        // GET: Account/Profiles
        [CheckLogin]
        public ActionResult Profiles()
        {
            return View();
        }
    }
}