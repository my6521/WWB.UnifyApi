using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWB.UnifyApi.Models;

namespace WWB.UnifyApi
{
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