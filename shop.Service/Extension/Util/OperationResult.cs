namespace shop.Service.Extension.Util
{
    public class OperationResult<TData>
    {
        public const string SuccessMessage = "عملیات با موفقیت انجام شد";
        public const string ErrorMessage = "عملیات با شکست مواجه شد";

        public string Message { get; set; }
        public string Title { get; set; } = null;
        public OperationResultStatus Status { get; set; }
        public TData Data { get; set; }
        public static OperationResult<TData> Success(TData data)
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.Success,
                Title = SuccessMessage,
                Data = data,
            };
        }
        public static OperationResult<TData> NotFound()
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.NotFound,
                Title = "NotFound",
                Data = default(TData),
            };
        }
        public static OperationResult<TData> Error(string message = ErrorMessage)
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.Error,
                Title = "مشکلی در عملیات رخ داده",
                Data = default(TData),
                Message = message
            };
        }
    }
    public class OperationResult
    {
        public string Message { get; set; }
        public OperationResultStatus Status { get; set; }

        #region Errors
        public static OperationResult Error()
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Error,
                Message = "عملیات ناموفق",
            };
        }
        public static OperationResult Error(string message)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Error,
                Message = message,
            };
        }
        #endregion

        #region NotFound

        public static OperationResult NotFound(string message)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.NotFound,
                Message = message,
            };
        }
        public static OperationResult NotFound()
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.NotFound,
                Message = "اطلاعات درخواستی یافت نشد",
            };
        }

        #endregion

        #region Succsess

        public static OperationResult Success()
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Success,
                Message = "عملیات با موفقیت انجام شد",
            };
        }
        public static OperationResult Success(string message)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Success,
                Message = message,
            };
        }
        #endregion
    }
    public enum OperationResultStatus
    {
        Error = 10,
        Success = 200,
        NotFound = 404
    }
}