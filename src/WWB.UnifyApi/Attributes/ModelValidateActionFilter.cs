using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWB.UnifyApi.Attributes
{
    [DebuggerStepThrough]
    public class ModelValidateActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var message = string.Empty;
                using var enumerator = context.ModelState.Keys.GetEnumerator();
                if (enumerator.MoveNext())
                    message = enumerator.Current + ":参数有误！";
                throw new ArgumentNullException(message);
            }
            base.OnActionExecuting(context);
        }
    }
}