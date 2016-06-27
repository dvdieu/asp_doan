using DoAnWeb.ClassHelper;
using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Areas.Admin.Filter
{
    public class CheckLoginAdminAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (CurrentContext.isLogin() == false)
            {
                string controller = filterContext.RouteData.Values["controller"].ToString();
                string action = filterContext.RouteData.Values["action"].ToString();
                filterContext.Result = new RedirectResult(string.Format(
                    "~/Account/Login?retURL=/{0}/{1}", controller, action
                    )
                    );
                return;
            }
            else
            {
                tbl_NguoiSuDungs temp = CurrentContext.getCurrenUser();
                if (temp.Quyen != 0)
                {
                    string controller = filterContext.RouteData.Values["controller"].ToString();
                    string action = filterContext.RouteData.Values["action"].ToString();
                    filterContext.Result = new RedirectResult(string.Format(
                        "~/Home/Index", controller, action
                        )
                        );
                    return;
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}