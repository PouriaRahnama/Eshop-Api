using shop.Core.Domain.Order;
using shop.Core.Domain.Seller;
using shop.Core.Domain.User;
using shop.Data.Repository;
using shop.Service.DTOs.OrderCommand;
using shop.Service.Extension.Util;

namespace shop.Service.Command
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<SellerInventory> _sellerInventoryRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<OrderAddress> _orderAddressRepository;
        public OrderService(IRepository<Order> OrderRepository,
            IRepository<OrderItem> orderItemRepository,
            IRepository<SellerInventory> sellerInventoryRepository,
            IRepository<OrderAddress> OrderAddressRepository,
            IRepository<User> userRepository)
        {
            _orderRepository = OrderRepository;
            _orderItemRepository = orderItemRepository;
            _sellerInventoryRepository = sellerInventoryRepository;
            _orderAddressRepository = OrderAddressRepository;
            _userRepository = userRepository;
        }

        public async Task<OperationResult> AddOrderItem(CreateOrderItemDto CreateOrderItemDto)
        {
            var inventory = await _sellerInventoryRepository.FindByIdAsync(CreateOrderItemDto.inventoryId);
            if (inventory == null)
                return OperationResult.NotFound();

            if (inventory.Count <= CreateOrderItemDto.Count)
                return OperationResult.Error("تعداد محصولات موجود کمتر از حد درخواستی است.");

            var Order = await _orderRepository.GetEntity(o => o.UserId == CreateOrderItemDto.userId && o.Status == OrderStatus.Pending);
            OrderItem? OrderItem = null;
            if (Order == null)
            {
                Order = new Order()
                {
                    UserId = CreateOrderItemDto.userId,
                    Status = OrderStatus.Pending,
                };

                await _orderRepository.AddAsync(Order);

                OrderItem = new OrderItem()
                {
                    Count = CreateOrderItemDto.Count,
                    InventoryId = CreateOrderItemDto.inventoryId,
                    OrderId = Order.Id,
                    Price = inventory.Price
                };
                if (ItemCountBeggerThanInventoryCount(inventory, OrderItem))
                    return OperationResult.Error("تعداد محصولات موجود کمتر از حد درخواستی است.");

                await _orderItemRepository.AddAsync(OrderItem);
            }
            else if (Order != null)
            {
                OrderItem = await _orderItemRepository.GetEntity(f => f.InventoryId == CreateOrderItemDto.inventoryId);
                if (OrderItem != null)
                {
                    OrderItem.Count += CreateOrderItemDto.Count;
                    OrderItem.UpdateON = DateTime.Now;
                }

                if (ItemCountBeggerThanInventoryCount(inventory, OrderItem))
                    return OperationResult.Error("تعداد محصولات موجود کمتر از حد درخواستی است.");

                _orderItemRepository.Update(OrderItem);
            }

            return OperationResult.Success();
        }
        public async Task<OperationResult> DecreaseOrderItem(DecreaseOrderItemCountDto DecreaseOrderItemCountDto)
        {
            var currentOrder = await _orderRepository.GetEntity(f => f.UserId == DecreaseOrderItemCountDto.UserId
            && f.Status == OrderStatus.Pending);

            if (currentOrder == null)
                return OperationResult.NotFound();

            var currentItem = await _orderItemRepository.FindByIdAsync(DecreaseOrderItemCountDto.OrderItemId);
            if (currentItem == null)
                return OperationResult.NotFound();

            currentItem.Count -= DecreaseOrderItemCountDto.Count;

            if (currentItem.Count <= 0)
                currentItem.Count = 0;

            _orderItemRepository.Update(currentItem);
            return OperationResult.Success();
        }
        public async Task<OperationResult> IncreaseOrderItem(IncreaseOrderItemCountDto IncreaseOrderItemCountDto)
        {
            var currentOrder = await _orderRepository.GetEntity(f => f.UserId == IncreaseOrderItemCountDto.UserId);

            if (currentOrder == null)
                return OperationResult.NotFound();

            var currentItem = await _orderItemRepository.FindByIdAsync(IncreaseOrderItemCountDto.OrderItemId);
            if (currentItem == null)
                return OperationResult.NotFound();

            currentItem.Count += IncreaseOrderItemCountDto.Count;
            _orderItemRepository.Update(currentItem);

            return OperationResult.Success();
        }
        public async Task<OperationResult> RemoveOrderItem(RemoveOrderItemDto RemoveOrderItemDto)
        {
            var currentOrder = await _orderRepository.GetEntity(f => f.UserId == RemoveOrderItemDto.UserId
               && f.Status == OrderStatus.Pending);

            if (currentOrder == null)
                return OperationResult.NotFound();

            var currentItem = await _orderItemRepository.GetEntity(f => f.Id == RemoveOrderItemDto.OrderItemId);
            if (currentItem != null)         
                _orderItemRepository.Delete(currentItem);          

            return OperationResult.Success();
        }
        public async Task<OperationResult> AddOrderAddress(AddOrderAddressDto AddOrderAddressDto)
        {
            var currentUser = await _userRepository.FindByIdAsync(AddOrderAddressDto.UserId);

            if (currentUser == null)
                return OperationResult.NotFound("!کاربری با این مشخصات وجود ندارد");

            var ExistOrderAddress = await _orderAddressRepository.GetEntity(ad => ad.OrderId == AddOrderAddressDto.OrderId);

            if (ExistOrderAddress != null)
                return OperationResult.NotFound("!آدرسی مربوط به این سفارش موجوداست");

            var address = new OrderAddress()
            {
                NationalCode = AddOrderAddressDto.NationalCode,
                Name = AddOrderAddressDto.Name,
                OrderId = AddOrderAddressDto.OrderId,
                Family = AddOrderAddressDto.Family,
                PhoneNumber = AddOrderAddressDto.PhoneNumber,
                PostalAddress = AddOrderAddressDto.PostalAddress,
                PostalCode = AddOrderAddressDto.PostalCode,
                Shire = AddOrderAddressDto.Shire,
                City = AddOrderAddressDto.City

            };

            await _orderAddressRepository.AddAsync(address);
            return OperationResult.Success();
        }
        public async Task<OperationResult> RemoveOrderAddress(RemoveOrderAddressDto RemoveOrderAddressDto)
        {
            var currentOrder = await _orderRepository.FindByIdAsync(RemoveOrderAddressDto.OrderId);
            if (currentOrder == null)
                return OperationResult.NotFound();

            var Address = await _orderAddressRepository.FindByIdAsync(RemoveOrderAddressDto.AddressId);
            if (Address == null)
                return OperationResult.NotFound();

            Address.Deleted = true;
            _orderAddressRepository.Update(Address);
            return OperationResult.Success();

        }  
        public async Task<OperationResult> CancelOrder(CancelOrderDto cancelOrderDto)
        {
            var order = await _orderRepository.FindByIdAsync(cancelOrderDto.OrderId);
            if (order == null)
                return OperationResult.NotFound();

            foreach (var item in order.OrderItems)
            {
                var inventory = await _sellerInventoryRepository.FindByIdAsync(item.InventoryId);
                if (inventory == null)
                    return OperationResult.Error($"موجودی آیتم با شناسه {item.InventoryId} یافت نشد.");

                inventory.ReservedCount -= item.Count;

                _sellerInventoryRepository.Update(inventory);
            }

            order.Status = OrderStatus.Rejected;
            _orderRepository.Update(order);

            return OperationResult.Success();
        }
        public async Task<OperationResult> CheckoutOrder(CheckoutOrderDto checkoutOrderDto)
        {
            var currentOrder = await _orderRepository.GetEntity(f => f.UserId == checkoutOrderDto.UserId &&
                (f.Status == OrderStatus.Pending || f.Status == OrderStatus.CheckedOut));

            if (currentOrder == null)
                return OperationResult.NotFound();

            if (currentOrder.Status == OrderStatus.Finally || currentOrder.Status == OrderStatus.Rejected)
                return OperationResult.Error("این سفارش قبلاً نهایی شده یا رد شده است.");

            if (currentOrder.Status == OrderStatus.Pending)
            {
                currentOrder.Status = OrderStatus.CheckedOut;
                currentOrder.UpdateON = DateTime.Now;

                _orderRepository.Update(currentOrder);

                foreach (var item in currentOrder.OrderItems)
                {
                    var inventory = await _sellerInventoryRepository.FindByIdAsync(item.InventoryId);
                    if (inventory == null)
                        return OperationResult.Error("موجودی کالا یافت نشد.");

                    if (item.Count < 1 || inventory.AvailableCount < item.Count)
                        return OperationResult.Error("موجودی کافی برای رزرو وجود ندارد.");

                    inventory.ReservedCount += item.Count;

                    _sellerInventoryRepository.Update(inventory);
                }         
            }

            return OperationResult.Success();
        }
        public async Task<OperationResult> OrderFinally(OrderFinallyDto orderFinallyDto)
        {
            var order = await _orderRepository.FindByIdAsync(orderFinallyDto.OrderId);
            if (order == null)
                return OperationResult.NotFound();

            order.Status = OrderStatus.Finally;
            order.UpdateON = DateTime.Now;

            _orderRepository.Update(order);

            foreach (var item in order.OrderItems)
            {
                var inventory = await _sellerInventoryRepository.FindByIdAsync(item.InventoryId);

                if (item.Count < 1 || inventory.ReservedCount < item.Count)
                    return OperationResult.Error("موجودی رزرو شده کافی نیست.");

                inventory.ReservedCount -= item.Count;
                inventory.Count -= item.Count;

                _sellerInventoryRepository.Update(inventory);
            }
            return OperationResult.Success();
        }
        public async Task<OperationResult> SendOrder(SendOrderDto sendOrderDto)
        {
            var order = await _orderRepository.FindByIdAsync(sendOrderDto.OrderId);
            if (order == null)
                return OperationResult.NotFound();

            order.Status = OrderStatus.Shipping;
            order.UpdateON = DateTime.Now;

            _orderRepository.Update(order);

            return OperationResult.Success();
        }

        private bool ItemCountBeggerThanInventoryCount(SellerInventory inventory, OrderItem orderItem)
        {
            if (orderItem.Count > inventory.Count)
                return true;
            return false;
        }
    }
}
