using DoAnWeb.ClassHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Filter
{
    public class CheckLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           if(CurrentContext.isLogin()==false)
            {
                string controller = filterContext.RouteData.Values["controller"].ToString();
                string action = filterContext.RouteData.Values["action"].ToString();
                filterContext.Result = new RedirectResult(string.Format(
                    "~/Account/Login?retURL=/{0}/{1}",controller,action
                    )
                    );
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}