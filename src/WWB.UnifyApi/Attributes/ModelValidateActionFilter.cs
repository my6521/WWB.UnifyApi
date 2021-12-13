using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics;
using System.Text;

namespace WWB.UnifyApi.Attributes
{
    [DebuggerStepThrough]
    public class ModelValidateActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var str = new StringBuilder();
                foreach (var item in context.ModelState.Values)
                {
                    //遍历所有项目的中的所有错误信息
                    foreach (var err in item.Errors)
                    {
                        //消息拼接,用|隔开，前端根据容易解析
                        if (str.Length > 0) str.Append(",");
                        str.Append(err.ErrorMessage);
                    }
                }

                throw new ArgumentNullException(str.ToString());
            }
            base.OnActionExecuting(context);
        }
    }
}