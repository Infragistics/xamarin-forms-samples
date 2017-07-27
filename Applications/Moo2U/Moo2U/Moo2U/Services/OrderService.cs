namespace Moo2U.Services {
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Moo2U.Model;
    using Moo2U.SampleData;
    using SQLite.Net;

    public class OrderService : IOrderService {

        readonly SQLiteConnection _cn;
        readonly IDataGenerator _dataGenerator;
        readonly IDeliveryHistoryService _deliveryHistoryService;

        public OrderService(ISQLite sqLite, IDeliveryHistoryService deliveryHistoryService, IDataGenerator dataGenerator) {
            if (sqLite == null) {
                throw new ArgumentNullException(nameof(sqLite));
            }
            if (deliveryHistoryService == null) {
                throw new ArgumentNullException(nameof(deliveryHistoryService));
            }
            if (dataGenerator == null) {
                throw new ArgumentNullException(nameof(dataGenerator));
            }
            _cn = sqLite.GetConnection();
            _deliveryHistoryService = deliveryHistoryService;
            _dataGenerator = dataGenerator;
        }

        public async Task CompleteOrderAsync(Int32 orderId, Byte[] signature) {
            if (orderId <= 0) {
                throw new ArgumentOutOfRangeException(nameof(orderId));
            }
            if (signature == null) {
                throw new ArgumentNullException(nameof(signature));
            }
            var task = Task
                .Factory
                .StartNew(() => {
                    _cn.BeginTransaction();
                    _cn.Execute("UPDATE [Order] SET Signature = ?, OrderStatus = ? WHERE Id = ?", signature, OrderStatus.Completed, orderId);

                    var order = Get(orderId);

                    var dh = new DeliveryHistory();
                    dh.Revenue = order.TotalDelivered;
                    dh.AverageSpeed = _dataGenerator.GetInteger(35, 50);
                    dh.CustomerId = order.CustomerId;
                    dh.DateDelivered = DateTime.Now;
                    dh.FuelEfficiency = _dataGenerator.GetInteger(25, 30);
                    dh.MileDriven = _dataGenerator.GetInteger(5, 12);
                    dh.OrderId = orderId;
                    dh.ItemsDelivered = GetOrderItems(orderId).Where(x => x.OrderItemStatus == OrderItemStatus.Delivered).Sum(x => x.Quantity);
                    _deliveryHistoryService.Insert(dh);
                    _cn.Commit();
                });

            await task;
        }

        Order Get(Int32 orderId) {
            if (orderId <= 0) {
                throw new ArgumentOutOfRangeException(nameof(orderId));
            }

            return _cn.Get<Order>(x => x.Id == orderId);
        }

        public async Task<DeliverOrder> GetDeliverOrderAsync(Int32 orderId) {
            if (orderId <= 0) {
                throw new ArgumentOutOfRangeException(nameof(orderId));
            }
            var task = Task<DeliverOrder>
                .Factory
                .StartNew(() => {
                    var deliverOrder = _cn.Query<DeliverOrder>("SELECT * FROM [Order] WHERE Id = ?", orderId).SingleOrDefault();
                    if (deliverOrder == null) {
                        throw new InvalidOperationException("Order not found");
                    }
                    var items = _cn.Query<DeliverOrderItem>("SELECT OI.*, P.Description, P.Icon, P.Price, P.UnitOfMeasure FROM OrderItem OI INNER JOIN Product P ON OI.ProductId = P.Id WHERE OI.OrderId = ?", orderId);
                    deliverOrder.DeliverOrderItems = new ObservableCollection<DeliverOrderItem>(items);
                    deliverOrder.CustomerName = _cn.Get<Customer>(x => x.Id == deliverOrder.CustomerId).Name;
                    return deliverOrder;
                });

            return await task;
        }

        IList<OrderItem> GetOrderItems(Int32 orderId) {
            if (orderId <= 0) {
                throw new ArgumentOutOfRangeException(nameof(orderId));
            }

            return _cn.Query<OrderItem>("SELECT * FROM [OrderItem] WHERE OrderId = ?", orderId);
        }

        public Int32 Insert(Order order, IEnumerable<OrderItem> orderItems) {
            if (order == null) {
                throw new ArgumentNullException(nameof(order));
            }
            if (orderItems == null) {
                throw new ArgumentNullException(nameof(orderItems));
            }

            var count = 1;
            _cn.Insert(order);
            foreach (var item in orderItems) {
                item.OrderId = order.Id;
                _cn.Insert(item);
                count += 1;
            }
            return count;
        }

        public void UpdateDeliverOrder(Int32 orderId, OrderStatus orderStatus, Double itemPercentDelivered, Double totalDelivered) {
            if (orderId <= 0) {
                throw new ArgumentOutOfRangeException(nameof(orderId));
            }
            if (!Enum.IsDefined(typeof(OrderStatus), orderStatus)) {
                throw new ArgumentOutOfRangeException(nameof(orderStatus), "Value should be defined in the OrderStatus enum.");
            }

            _cn.Execute("UPDATE [Order] SET OrderStatus = ?, ItemPercentDelivered = ?, TotalDelivered = ? WHERE Id = ?", orderStatus, itemPercentDelivered, totalDelivered, orderId);
        }

        public void UpdateDeliverOrderItem(Int32 orderItemId, OrderItemStatus orderItemStatus) {
            if (orderItemId <= 0) {
                throw new ArgumentOutOfRangeException(nameof(orderItemId));
            }
            if (!Enum.IsDefined(typeof(OrderItemStatus), orderItemStatus)) {
                throw new ArgumentOutOfRangeException(nameof(orderItemStatus), "Value should be defined in the OrderItemStatus enum.");
            }

            _cn.Execute("UPDATE OrderItem SET OrderItemStatus = ? WHERE Id = ?", orderItemStatus, orderItemId);
        }

    }
}
