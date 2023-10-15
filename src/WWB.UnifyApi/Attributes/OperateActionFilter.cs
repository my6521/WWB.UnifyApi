using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace WWB.UnifyApi.Attributes
{
    public abstract class OperateActionFilter : ActionFilterAttribute
    {
        public override sealed void OnActionExecuting(ActionExecutingContext context)
        {
            //记录进入请求的时间
            context.ActionDescriptor.Properties["enterTime"] = DateTime.Now.ToBinary();

            string param = string.Empty;
            foreach (var arg in context.ActionArguments)
            {
                string value = JsonConvert.SerializeObject(arg.Value);
                param += $"{arg.Key} : {value} \r\n";
            }
            if (!string.IsNullOrEmpty(param))
            {
                context.ActionDescriptor.Properties["param"] = param;
            }
        }

        public override sealed void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        public override sealed Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return base.OnActionExecutionAsync(context, next);
        }

        public override sealed void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
        }

        public override sealed void OnResultExecuted(ResultExecutedContext context)
        {
            string paramStr = string.Empty;
            string result = string.Empty;
            double costTime = 0;

            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

            if (descriptor.Properties.TryGetValue("param", out object param))
            {
                paramStr = param?.ToString();
            }
            if (context.Result is ObjectResult)
            {
                result = JsonConvert.SerializeObject(((ObjectResult)context.Result).Value);
            }
            if (descriptor.Properties.TryGetValue("enterTime", out object beginTime))
            {
                DateTime time = DateTime.FromBinary(Convert.ToInt64(beginTime));
                costTime = (DateTime.Now - time).TotalMilliseconds;
            }

            var path = context.HttpContext?.Request.Path;
            var method = context.HttpContext?.Request.Method;
            var ip = context.HttpContext?.Connection.RemoteIpAddress?.MapToIPv4()?.ToString();
            var browser = context.HttpContext?.Request.Headers["User-Agent"];

            var model = new OperateModel()
            {
                Path = path,
                Method = method,
                IpAddress = ip,
                Browser = browser,
                RequestParam = paramStr,
                ResultParam = result,
                CostTime = costTime,
                ActionName = descriptor.ActionName,
                ControllerName = descriptor.ControllerName,
                StatusCode = context.HttpContext.Response.StatusCode
            };

            Log(context, model);
        }

        public override sealed Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            return base.OnResultExecutionAsync(context, next);
        }

        public virtual void Log(ActionContext context, OperateModel operateModel)
        {
        }
    }

    public class OperateModel
    {
        /// <summary>
        /// 方法名称
        /// </summary>
        public string ActionName { get; internal set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName { get; internal set; }

        /// <summary>
        /// 请求路径
        /// </summary>
        public string Path { get; internal set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string Method { get; internal set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; internal set; }

        /// <summary>
        /// 浏览器信息
        /// </summary>
        public string Browser { get; internal set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public string RequestParam { get; internal set; }

        /// <summary>
        /// 返回参数
        /// </summary>
        public string ResultParam { get; internal set; }

        /// <summary>
        /// 消耗时间
        /// </summary>
        public double CostTime { get; internal set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int StatusCode { get; internal set; }
    }
}