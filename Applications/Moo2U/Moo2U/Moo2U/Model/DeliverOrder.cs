namespace Moo2U.Model {
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class DeliverOrder : Order {

        public String CustomerName { get; set; }

        public ObservableCollection<DeliverOrderItem> DeliverOrderItems { get; set; }

        public DeliverOrder() {
        }

        public void SetOrderStatus() {
            if (this.DeliverOrderItems.All(x => x.OrderItemStatus == OrderItemStatus.NotDelivered)) {
                this.OrderStatus = OrderStatus.New;
                this.ItemPercentDelivered = 0d;
                return;
            }
            var count = this.DeliverOrderItems.Count;
            if (count > 0) {
                var countDelivered = this.DeliverOrderItems.Count(x => x.OrderItemStatus == OrderItemStatus.Delivered);
                this.ItemPercentDelivered = countDelivered / (Double)count * 100;
            } else {
                this.ItemPercentDelivered = 0;
            }
            

            if (this.DeliverOrderItems.Any(x => x.OrderItemStatus == OrderItemStatus.NotDelivered)) {
                this.OrderStatus = OrderStatus.Partial;
                return;
            }
            if (this.Signature == null) {
                this.OrderStatus = OrderStatus.ReadyForSignature;
                return;
            }

            this.OrderStatus = OrderStatus.Completed;
        }

    }
}
