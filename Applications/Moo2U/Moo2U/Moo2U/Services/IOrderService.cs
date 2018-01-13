namespace Moo2U.Services {
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Moo2U.Model;

    public interface IOrderService {

        Int32 Insert(Order order, IEnumerable<OrderItem> orderItems);

        Task<DeliverOrder> GetDeliverOrderAsync(Int32 orderId);

        void UpdateDeliverOrderItem(Int32 orderItemId, OrderItemStatus orderItemStatus);

        void UpdateDeliverOrder(Int32 orderId, OrderStatus orderStatus, Double itemPercentDelivered, Double totalDelivered);

        Task CompleteOrderAsync(Int32 orderId, Byte[] signature);

    }
}