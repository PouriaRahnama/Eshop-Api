﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using shop.Core.Domain.Order;
using shop.Data.ApplicationContext;

namespace shop.Service.Query
{
    public static class OrderQueryMapper
    {
        public static OrderQueryDto Map(this Order order)
        {

            var result =  new OrderQueryDto()
            {
                CreationDate = order.CreateON,
                Id = order.Id,
                Status = order.Status,
                Discount = order?.Discount,
                LastUpdate = order?.UpdateON,
                UserFullName = "",
                UserId = order.UserId,
                City = order.Addresses?.City,
                NationalCode = order.Addresses?.NationalCode,
                PhoneNumber = order.Addresses?.PhoneNumber,
                PostalAddress = order.Addresses?.PostalAddress,
                PostalCode = order.Addresses?.PostalCode,
                Shire = order.Addresses?.Shire,
                OrderTotal = order.OrderTotal,
                OrderItem = new()
            };

            return result;
        }

        public static async Task<List<OrderItemDto>> GetOrderItems(this OrderQueryDto orderDto, IApplicationContext context)
        {

            var OrderItems = await context.Set<OrderItem>().Where(c => c.OrderId == orderDto.Id).ToListAsync();
            var list = new List<OrderItemDto>();
            OrderItems.ForEach(orderItem =>
            {
                list.Add(new OrderItemDto()
                {
                    ShopName = orderItem.Inventory.Seller.ShopName,
                    InventoryId = orderItem.InventoryId,
                    CreationDate = orderItem.CreateON,
                    Count = orderItem.Count,
                    Id = orderItem.Id,
                    Price = orderItem.Price,
                    OrderId = orderItem.OrderId,
                    ProductName = orderItem.Inventory.Product.Name,
                    ProductImageName = orderItem.Inventory.Product.ImageName

                });               
            });
            return list;
        }

        public static OrderFilterData MapFilterData(this Order order)
        {
            return new OrderFilterData()
            {
                Status = order.Status,
                Id = order.Id,
                CreationDate = order.CreateON,
                City = order.Addresses?.City,
                Shire = order.Addresses?.Shire,
                TotalItemCount = order.OrderItems.Count,
                TotalPrice = order.OrderTotal,
                Name = order.User.Name,
                Family = order.User.Family,
                UserId = order.UserId
            };
        }

    }
}
