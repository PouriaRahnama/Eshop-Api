using Microsoft.AspNetCore.Mvc;
using shop.Service.Extension.Util;
using System.Net;

namespace shop.Frameworks.Commons
{
    [Route("[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private const string SuccessMessage = "عملیات انجام شد";
        protected ApiResult CommandResult(OperationResult operation)
        {
            bool isSuccess = operation.Status == OperationResultStatus.Success;
            return new ApiResult()
            {
                IsSuccess = isSuccess,
                MetaData = new()
                {
                    Message = operation.Message,
                    AppStatusCode = operation.Status.MapStatusCode()
                }
            };
        }

        protected ApiResult<TData> CreatedResult<TData>(OperationResult<TData> operation, string location)
        {
            bool isSuccess = operation.Status == OperationResultStatus.Success;
            if (isSuccess)
            {
                HttpContext.Response.Headers.Add("location", location);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
            }
            return new ApiResult<TData>()
            {
                IsSuccess = isSuccess,
                Data = isSuccess ? operation.Data : default(TData),
                MetaData = new()
                {
                    Message = operation.Message,
                    AppStatusCode = operation.Status.MapStatusCode()
                }
            };
        }
        protected ApiResult CreatedResult(OperationResult operation, string location)
        {
            bool isSuccess = operation.Status == OperationResultStatus.Success;
            if (isSuccess)
            {
                HttpContext.Response.Headers.Add("location", location);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
            }
            return new ApiResult()
            {
                IsSuccess = isSuccess,
                MetaData = new()
                {
                    Message = operation.Message,
                    AppStatusCode = operation.Status.MapStatusCode()
                }
            };
        }
        protected ApiResult<TData?> CommandResult<TData>(OperationResult<TData?> operation)
        {
            bool isSuccess = operation.Status == OperationResultStatus.Success;
            return new ApiResult<TData?>()
            {
                IsSuccess = isSuccess,
                Data = isSuccess ? operation.Data : default(TData),
                MetaData = new()
                {
                    Message = operation.Message,
                    AppStatusCode = operation.Status.MapStatusCode()
                }
            };
        }
        protected ApiResult<TData?> QueryResult<TData>(TData? data)
        {
            return new ApiResult<TData?>()
            {
                IsSuccess = true,
                Data = data,
                MetaData = new()
                {
                    Message = SuccessMessage,
                    AppStatusCode = AppStatusCode.Success
                }
            };
        }

    }
    public static class EnumMapper
    {
        public static AppStatusCode MapStatusCode(this OperationResultStatus result)
        {
            switch (result)
            {
                case OperationResultStatus.Success:
                    return AppStatusCode.Success;

                case OperationResultStatus.NotFound:
                    return AppStatusCode.NotFound;

                case OperationResultStatus.Error:
                    return AppStatusCode.LogicError;
            }

            return AppStatusCode.BadRequest;
        }
    }
}
