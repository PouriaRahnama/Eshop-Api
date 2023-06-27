using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace shop.Service.Middleware
{
    public class ErrorHandler: IErrorHandler
    {
        public string ErrorMessage { get; set; } = "خطا";
        public int StatusCode { get; set; } = 500;
        public void GetError(Exception ex)
        {
            ErrorMessage = "خطا - لطفا با مدیر سایت تماس بگیرید";
            if (ex.GetType() == typeof(DivideByZeroException))
            {
                ErrorMessage = "خطای تقسیم بر صفر";
                StatusCode = 521;
            }
            if (ex.GetType() == typeof(System.Security.Cryptography.CryptographicException))
            {
                ErrorMessage = "خطا در رمز گشایی";
                StatusCode = 522;
            }

            if (ex.GetType() == typeof(FormatException))
            {
                ErrorMessage = "خطای فرمت";
                StatusCode = 523;
            }
            if (ex.GetType() == typeof(SqlException))
            {

                ErrorMessage = "خطای پایگاه داده";
                SqlException exsql = ex as SqlException;
                ErrorMessage = GetSqlServerError(exsql);
            }
            if (ex.GetType() == typeof(DbUpdateConcurrencyException))
            {
                ErrorMessage = "اطلاعات مورد نظر توسط کاربر دیگر تغییر کرده است. مجددا سعی نمایید";
            }
            if (ex.GetType() == typeof(DbUpdateException))
            {
                SqlException exsql = ex.InnerException as SqlException;
                ErrorMessage = GetSqlServerError(exsql);
            }

        }
        private string GetSqlServerError(SqlException exsql)
        {

            if (exsql.Number == 515)
            {
                ErrorMessage = "اطلاعات باکس های ستاره دار را وارد نمایید";
                StatusCode = 530;
            }
            if (exsql.Number == 2627)
            {
                ErrorMessage = "اطلاعات تکراری است";
                StatusCode = 531;
            }
            if (exsql.Number == 547)
            {
                ErrorMessage = " به دلیل اینکه اطلاعات به قسمت های دیگر وابسته است، قابلیت تغییر نیست ";
                StatusCode = 532;
            }
            if (exsql.Number == 0 || exsql.Number == 2 || exsql.Number == -2)
            {
                ErrorMessage = " دسترسی به پایگاه داده ممکن نیست ";
                StatusCode = 533;
            }
            return ErrorMessage;
        }

    }
}
