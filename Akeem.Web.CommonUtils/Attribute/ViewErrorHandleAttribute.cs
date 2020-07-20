using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akeem.Web.CommonUtils.Attribute
{
    /// <summary>
    /// 处理错误信息
    /// </summary>
    public class ViewErrorHandleAttribute: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            //获取异常信息，入库保存
            var exception = filterContext.Exception;
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();
            var msg = $"出错位置:{controllerName}/{actionName}";
            CommonTools.Ex(msg, exception);
            filterContext.Result = new RedirectResult("/Home/Error");
            base.OnException(filterContext);
        }
    }
}
