using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnWeb.Models;
namespace DoAnWeb.ClassHelper
{
    public class CurrentContext
    {
        public static bool isLogin()
        {
            if((int)HttpContext.Current.Session["IsLogin"]==1)
            {
                return true;
            }
            else if(HttpContext.Current.Request.Cookies["Username"] !=null)
            {
                //Không có session - > Kiểm tra cookies
                string userName = HttpContext.Current.Request.Cookies["Username"].Value;
                //Tìm trong CSDL
                using (ModelEntities ctx = new ModelEntities())
                {
                    tbl_NguoiSuDungs user = ctx.tbl_NguoiSuDungs.Where(p => String.Compare(p.TenDangNhap, userName) == 0).FirstOrDefault();
                    if(user != null)
                    {
                        //Tạo lại Sesstion
                        HttpContext.Current.Session["IsLogin"] = 1;
                        HttpContext.Current.Session["CurUser"] = user;
                        return true;
                    }
                }
            }   
            return false;
        }
        public static tbl_NguoiSuDungs getCurrenUser()
        {
            return (tbl_NguoiSuDungs)HttpContext.Current.Session["CurUser"];
        }
        public static void detroySesstion()
        {
            HttpContext.Current.Session["IsLogin"] = 0;
            HttpContext.Current.Session["CurUser"] = null;
            HttpContext.Current.Response.Cookies["Username"].Value = null;
            HttpContext.Current.Response.Cookies["Username"].Expires = DateTime.Now.AddDays(-1);
        }
    }
}