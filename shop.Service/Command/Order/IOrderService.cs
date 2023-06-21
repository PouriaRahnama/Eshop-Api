﻿using shop.Service.DTOs.OrderCommand;
using shop.Service.Extension.Util;

namespace shop.Service.Command
{
    public interface IOrderService
    {
        Task<OperationResult> AddOrderItem(CreateOrderItemDto CreateOrderItemDto);
        Task<OperationResult> AddOrderAddress(AddOrderAddressDto AddOrderAddressDto);
        Task<OperationResult> DecreaseOrderItem(DecreaseOrderItemCountDto DecreaseOrderItemCountDto);
        Task<OperationResult> IncreaseOrderItem(IncreaseOrderItemCountDto IncreaseOrderItemCountDto);
        Task<OperationResult> RemoveOrderItem(RemoveOrderItemDto RemoveOrderItemDto);
        Task<OperationResult> RemoveOrderAddress(RemoveOrderAddressDto RemoveOrderAddressDto);
    }
}
