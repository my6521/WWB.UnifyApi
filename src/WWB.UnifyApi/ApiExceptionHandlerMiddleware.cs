using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WWB.UnifyApi.Models;

namespace WWB.UnifyApi
{
    public class ApiExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionHandlerMiddleware> _logger;

        public ApiExceptionHandlerMiddleware(RequestDelegate next, ILogger<ApiExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Exception exception = null;
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex = null)
        {
            if (ex != null)
            {
                var errorResult = new ErrorApiResult();
                if (ex is FriendlyException appFriendlyException)
                {
                    errorResult.ErrorCode = appFriendlyException.ErrorCode;
                    errorResult.Message = appFriendlyException.Message;
                }
                else
                {
                    errorResult.ErrorCode = context.Response.StatusCode;
                    errorResult.Message = ex.Message;
                }
                await WriteResponse(context, errorResult);
            }
            else
            {
                var code = context.Response.StatusCode;
                switch (code)
                {
                    case 200:
                        return;

                    case 204:
                        return;

                    case 401:
                        await WriteResponse(context, new ErrorApiResult(code, "token已过期,请重新登录"));
                        break;

                    default:
                        await WriteResponse(context, new ErrorApiResult(code, "未知错误"));
                        break;
                }
            }
        }

        private async Task WriteResponse(HttpContext context, ErrorApiResult errorResult)
        {
            context.Response.Clear();
            context.Response.ContentType = "application/json;charset=utf-8";
            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResult));
        }
    }
}