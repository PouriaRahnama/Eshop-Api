using shop.Core.Domain.Order;
using shop.Core.Domain.Seller;
using shop.Data.Repository;
using shop.Service.DTOs.OrderCommand;
using shop.Service.Extension.Util;

namespace shop.Service.Command
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _OrderRepository;
        private readonly IRepository<SellerInventory> _SellerInventoryRepository;
        private readonly IRepository<OrderItem> _OrderItemRepository;
        private readonly IRepository<OrderAddress> _OrderAddressRepository;
        public OrderService(IRepository<Order> OrderRepository,
            IRepository<OrderItem> orderItemRepository,
            IRepository<SellerInventory> sellerInventoryRepository,
            IRepository<OrderAddress> OrderAddressRepository)
        {
            _OrderRepository = OrderRepository;
            _OrderItemRepository = orderItemRepository;
            _SellerInventoryRepository = sellerInventoryRepository;
            _OrderAddressRepository = OrderAddressRepository;
        }

        public async Task<OperationResult> AddOrderItem(CreateOrderItemDto CreateOrderItemDto)
        {
            var inventory = await _SellerInventoryRepository.FindByIdAsync(CreateOrderItemDto.inventoryId);
            if (inventory == null)
                return OperationResult.NotFound();

            if (inventory.Count <= CreateOrderItemDto.Count)
                return OperationResult.Error("تعداد محصولات موجود کمتر از حد درخواستی است.");

            var order = await _OrderRepository.GetEntity(f => f.UserId == CreateOrderItemDto.userId
            && f.orderStatus == OrderStatus.Pending);
            if (order == null)
                order = new Order()
                {
                    UserId = CreateOrderItemDto.userId,
                    orderStatus = OrderStatus.Pending,
                };
            _OrderRepository.Add(order);

            var OrderItem = new OrderItem()
            {
                Count = CreateOrderItemDto.Count,
                InventoryId = CreateOrderItemDto.inventoryId,
                OrderId = order.Id,
                Price = inventory.Price
            };

            if (OrderItem.Count > inventory.Count)
                return OperationResult.Error("تعداد محصولات موجود کمتر از حد درخواستی است.");

            _OrderItemRepository.Add(OrderItem);
            return OperationResult.Success();
        }
        public async Task<OperationResult> DecreaseOrderItem(DecreaseOrderItemCountDto DecreaseOrderItemCountDto)
        {
            var currentOrder = await _OrderRepository.GetEntity(f => f.UserId == DecreaseOrderItemCountDto.UserId
                && f.orderStatus == OrderStatus.Pending);
            if (currentOrder == null)
                return OperationResult.NotFound();

            var currentItem = _OrderItemRepository.FindById(DecreaseOrderItemCountDto.OrderItemId);
            if (currentItem == null)
                return OperationResult.NotFound();

            currentItem.Count += DecreaseOrderItemCountDto.Count;

            await _OrderItemRepository.UpdateAsync(currentItem);
            return OperationResult.Success();
        }
        public async Task<OperationResult> IncreaseOrderItem(IncreaseOrderItemCountDto IncreaseOrderItemCountDto)
        {
            var currentOrder = await _OrderRepository.GetEntity(f => f.UserId == IncreaseOrderItemCountDto.UserId
                && f.orderStatus == OrderStatus.Pending);
            if (currentOrder == null)
                return OperationResult.NotFound();

            var currentItem = _OrderItemRepository.FindById(IncreaseOrderItemCountDto.OrderItemId);
            if (currentItem == null)
                return OperationResult.NotFound();

            currentItem.Count -= IncreaseOrderItemCountDto.Count;

            await _OrderItemRepository.UpdateAsync(currentItem);
            return OperationResult.Success();
        }
        public async Task<OperationResult> RemoveOrderItem(RemoveOrderItemDto RemoveOrderItemDto)
        {
            var currentOrder = await _OrderRepository.GetEntity(f => f.UserId == RemoveOrderItemDto.UserId
               && f.orderStatus == OrderStatus.Pending);
            if (currentOrder == null)
                return OperationResult.NotFound();

            var currentItem = await _OrderItemRepository.GetEntity(f => f.Id == RemoveOrderItemDto.OrderItemId);
            if (currentItem != null)
                await _OrderItemRepository.DeleteAsync(currentItem);

            return OperationResult.Success();
        }
        public async Task<OperationResult> AddOrderAddress(AddOrderAddressDto AddOrderAddressDto)
        {
            var currentOrder = await _OrderRepository.GetEntity(f => f.UserId == AddOrderAddressDto.UserId
            && f.orderStatus == OrderStatus.Pending);
            if (currentOrder == null)
                return OperationResult.NotFound("!سفارشی با این مشخصات وجود ندارد");

            var ExistOrderAddress = await _OrderAddressRepository.GetEntity(ad => ad.OrderId == currentOrder.Id);
            if (ExistOrderAddress == null)
                return OperationResult.NotFound("!آدرسی مربوط به این سفارش موجوداست");

            var address = new OrderAddress()
            {
                NationalCode = AddOrderAddressDto.NationalCode,
                Name = AddOrderAddressDto.Name,
                OrderId = currentOrder.Id,
                Family = AddOrderAddressDto.Family,
                PhoneNumber = AddOrderAddressDto.PhoneNumber,
                PostalAddress = AddOrderAddressDto.PostalAddress,
                PostalCode = AddOrderAddressDto.PostalCode,
                Shire = AddOrderAddressDto.Shire,
            };

            await _OrderAddressRepository.AddAsync(address);
            return OperationResult.Success();
        }
        public async Task<OperationResult> RemoveOrderAddress(RemoveOrderAddressDto RemoveOrderAddressDto)
        {
            var currentOrder = await _OrderRepository.GetEntity(f => f.Id == RemoveOrderAddressDto.OrderId
                && f.orderStatus == OrderStatus.Pending);
            if (currentOrder == null)
                return OperationResult.NotFound();

            var Address = _OrderAddressRepository.FindById(RemoveOrderAddressDto.AddressId);
            if (Address == null)
                return OperationResult.NotFound();

            await _OrderAddressRepository.DeleteAsync(Address);
            return OperationResult.Success();

        }

    }
}
