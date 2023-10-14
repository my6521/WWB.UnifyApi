﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace WWB.UnifyApi.Attributes
{
    public abstract class OperateFilter : ActionFilterAttribute
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

            var opContext = new OperateContext()
            {
                Context = context,
                Path = path,
                Method = method,
                Ip = ip,
                Browser = browser,
                Param = paramStr,
                Result = result,
                CostTime = costTime,
                ActionName = descriptor.ActionName,
                ControllerName = descriptor.ControllerName,
            };

            Log(opContext);
        }

        public override sealed Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            return base.OnResultExecutionAsync(context, next);
        }

        public virtual void Log(OperateContext context)
        {
        }
    }

    public class OperateContext
    {
        public ActionContext Context { get; internal set; }
        public string Path { get; internal set; }
        public string Method { get; internal set; }
        public string Ip { get; internal set; }
        public string Browser { get; internal set; }
        public string Param { get; internal set; }
        public string Result { get; internal set; }
        public double CostTime { get; internal set; }
        public string ActionName { get; internal set; }
        public string ControllerName { get; internal set; }
    }
}