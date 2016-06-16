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
        }
    }
}