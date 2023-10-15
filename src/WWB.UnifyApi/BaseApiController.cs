using Microsoft.AspNetCore.Mvc;
using WWB.UnifyApi.Models;

namespace WWB.UnifyApi
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseApiController : ControllerBase
    {
        protected IActionResult Success<T>(T obj, string msg = "success")
        {
            var response = new ApiResult<T>
            {
                Data = obj,
                IsSuccess = true,
                Message = msg
            };

            return Ok(response);
        }

        protected IActionResult Success(string msg = "success")
        {
            var response = new ApiResult
            {
                IsSuccess = true,
                Message = msg
            };

            return Ok(response);
        }

        protected IActionResult Error(string msg = "error")
        {
            var response = new ApiResult
            {
                IsSuccess = false,
                Message = msg
            };

            return Ok(response);
        }
    }
}