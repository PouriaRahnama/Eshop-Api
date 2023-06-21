namespace shop.Service.Middleware
{
    public interface IErrorHandler
    {
        string ErrorMessage { get; set; }
        int StatusCode { get; set; }
        void GetError(Exception ex);
    }
}
