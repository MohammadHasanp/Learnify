using Common.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Common.AspNetCore
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected ApiResult CommandResult(OperationResult result)
        {
            return new ApiResult()
            {
                IsSuccess = result.Status == EnOperationResultStatus.Success,
                MetaData = new()
                {
                    Message = result.Message,
                    AppStatusCode = result.Status.MapOperationStatus()
                }
            };
        }
        protected ApiResult<TData?> CommandResult<TData>(OperationResult<TData> result
             , HttpStatusCode statusCode = HttpStatusCode.OK, string locationUrl = null)
        {
            bool isSuccess = result.Status == EnOperationResultStatus.Success;
            if (isSuccess)
            {
                HttpContext.Response.StatusCode = (int)statusCode;
                if (!string.IsNullOrWhiteSpace(locationUrl))
                {
                    HttpContext.Response.Headers.Add("location", locationUrl);
                }
            }
            return new ApiResult<TData?>()
            {
                IsSuccess = isSuccess,
                Data = isSuccess ? result.Data : default,
                MetaData = new()
                {
                    Message = result.Message,
                    AppStatusCode = result.Status.MapOperationStatus()
                }
            };
        }

        protected ApiResult<TData> QueryResult<TData>(TData result)
        {
            return new ApiResult<TData>()
            {
                IsSuccess = true,
                Data = result,
                MetaData = new()
                {
                    Message = "عملیات با موفقیت انجام شد",
                    AppStatusCode = AppStatusCode.Success
                }
            };
        }
    }

    public static class EnumHelper
    {
        public static AppStatusCode MapOperationStatus(this EnOperationResultStatus status)
        {
            switch (status)
            {
                case EnOperationResultStatus.Success:
                    return AppStatusCode.Success;

                case EnOperationResultStatus.NotFound:
                    return AppStatusCode.NotFound;

                case EnOperationResultStatus.Error:
                    return AppStatusCode.LogicError;
            }
            return AppStatusCode.LogicError;
        }
    }
}
