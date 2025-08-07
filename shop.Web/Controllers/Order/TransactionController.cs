using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shop.Core.Domain.Order;
using shop.Frameworks.Commons;
using shop.Service.Command;
using shop.Service.DTOs.OrderCommand;
using shop.Service.Extension.Util;
using shop.Service.Query;
using shop.Web.Infrastructure;
using Shop.Api.Infrastructure.Gateways.Zibal;
using Shop.Api.Infrastructure.Gateways.Zibal.DTOs;
using Shop.Api.ViewModels.Transactions;

namespace Shop.Api.Controllers
{
    public class TransactionController : ShopController
    {
        private readonly IZibalService _zibalService;
        private readonly IOrderService _orderService;
        private readonly IOrderQueryService _orderQueryService;

        public TransactionController(IZibalService zibalService, IOrderService orderService, IOrderQueryService orderQueryService)
        {
            _zibalService = zibalService;
            _orderService = orderService;
            _orderQueryService = orderQueryService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ApiResult<string>> CreateTransaction(CreateTransactionViewModel command)
        {
            var order = await _orderQueryService.GetOrderById(command.OrderId);
            if (order == null)
                return CommandResult(OperationResult<string>.NotFound());


            var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var result = await _zibalService.StartPay(new ZibalPaymentRequest()
            {
                Amount = order.OrderTotal,
                CallBackUrl = $"{url}/transaction?orderId={order.Id}&errorRedirect={command.ErrorCallBackUrl}&successRedirect={command.SuccessCallBackUrl}",
                Description = $"پرداخت سفارش با شناسه {order.Id}",
                LinkToPay = false,
                Merchant = "zibal",
                SendSms = false,
                Mobile = User.GetPhoneNumber()
            });
            return CommandResult(OperationResult<string>.Success(result));
        }

        [HttpGet]
        public async Task<IActionResult> Verify(int orderId, long trackId, int success, string errorRedirect, string successRedirect)
        {
            if (success == 0)
            {
                await _orderService.CancelOrder(new CancelOrderDto() { OrderId= orderId }); // 🔴 لغو سفارش چون پرداخت انجام نشده
                return Redirect(errorRedirect);
            }

            var order = await _orderQueryService.GetOrderById(orderId);

            if (order == null)
                return Redirect(errorRedirect);

            // 🛑 جلوگیری از پردازش مجدد سفارش‌هایی که نهایی یا رد شده‌اند
            if (order.Status == OrderStatus.Finally)
            {
                return Redirect(successRedirect); // یا یک پیام مشخص، مثلا "پرداخت قبلاً انجام شده است"
            }

            // 🛑 جلوگیری از پردازش مجدد سفارش‌هایی که نهایی یا رد شده‌اند
            if (order.Status == OrderStatus.Rejected)
            {
                return Redirect(errorRedirect); // یا یک پیام مشخص، مثلا "پرداخت قبلاً انجام شده است"
            }

            var result = await _zibalService.Verify(new ZibalVeriyfyRequest(trackId, "zibal"));
            if (result.Status != 1)
            {
                await _orderService.CancelOrder(new CancelOrderDto() { OrderId = orderId }); // 🔴 لغو سفارش چون پرداخت تایید نشده
                return Redirect(errorRedirect);
            }


            if (result.Amount != order.OrderTotal)
            {
                await _orderService.CancelOrder(new CancelOrderDto() { OrderId = orderId });  // 🔴 لغو سفارش چون مبلغ تطابق نداره
                return Redirect(errorRedirect);
            }

            var commandResult = await _orderService.OrderFinally(new OrderFinallyDto() { OrderId= orderId });

            if (commandResult.Status == OperationResultStatus.Success)
                return Redirect(successRedirect);

            await _orderService.CancelOrder(new CancelOrderDto() { OrderId = orderId }); // 🔴 لغو سفارش چون مبلغ تطابق نداره
            return Redirect(errorRedirect);
        }
    }
}
