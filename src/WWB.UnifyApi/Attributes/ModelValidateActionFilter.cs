using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace WWB.UnifyApi.Attributes
{
    // [DebuggerStepThrough]
    public class ModelValidateActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var error = context.ModelState.GetValidationSummary();

                throw new FriendlyException(error);
            }
            base.OnActionExecuting(context);
        }
    }

    public static class ModelStateExtensions
    {
        /// <summary>
        /// 获取验证消息提示并格式化提示
        /// </summary>
        public static string GetValidationSummary(this ModelStateDictionary modelState, string separator = "\r\n")
        {
            if (modelState.IsValid) return null;

            var errors = modelState.Where(x => x.Value.Errors.Count > 0).Select(x => x.Value.Errors.First().ErrorMessage).ToList();

            return string.Join("|", errors);
        }
    }
}