using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Akeem.Web.CommonUtils
{
    public class IsJsonpCallbackAttribute : ActionFilterAttribute
    {
        private const string CallbackQueryParameter = "callback";
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string text = context.HttpContext.Request.QueryString.Value;
            string[] arrys = text.Split('&');
            string callbackQuery = arrys.FirstOrDefault(item => item.ToLower().Contains(CallbackQueryParameter));
            
            if (!string.IsNullOrEmpty(callbackQuery))
            {
                string myCallBackValue = callbackQuery.Split('=')[1];
                string result = $"{myCallBackValue}({((Microsoft.AspNetCore.Mvc.JsonResult)context.Result).Value.Obj2Str()})";
                context.HttpContext.Response.WriteAsync(result);
            }

            base.OnActionExecuted(context);
        }
    }
}
