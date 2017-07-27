namespace Moo2U.Model {
    using System;
    using Prism.Mvvm;
    using SQLite.Net.Attributes;

    public class OrderItem : BindableBase {

        Double _extendedPrice;
        Int32 _id;
        Int32 _orderId;
        OrderItemStatus _orderItemStatus = OrderItemStatus.NotDelivered;
        Int32 _productId;
        Int32 _quantity;

        [NotNull]
        public Double ExtendedPrice {
            get { return _extendedPrice; }
            set {
                _extendedPrice = value;
                RaisePropertyChanged();
            }
        }

        [PrimaryKey, AutoIncrement]
        public Int32 Id {
            get { return _id; }
            set {
                _id = value;
                RaisePropertyChanged();
            }
        }

        [NotNull, Indexed]
        public Int32 OrderId {
            get { return _orderId; }
            set {
                _orderId = value;
                RaisePropertyChanged();
            }
        }

        [NotNull]
        public OrderItemStatus OrderItemStatus {
            get { return _orderItemStatus; }
            set {
                _orderItemStatus = value;
                RaisePropertyChanged();
                RaisePropertyChanged("StatusIcon");
            }
        }

        [NotNull]
        public Int32 ProductId {
            get { return _productId; }
            set {
                _productId = value;
                RaisePropertyChanged();
            }
        }

        [NotNull]
        public Int32 Quantity {
            get { return _quantity; }
            set {
                _quantity = value;
                RaisePropertyChanged();
            }
        }

        public String StatusIcon {
            get {
                switch (this.OrderItemStatus) {
                    case OrderItemStatus.Delivered:
                        return Constants.OrderItemStatusDeliveredIcon;
                    case OrderItemStatus.Canceled:
                        return Constants.OrderItemStatusCanceledIcon;
                    case OrderItemStatus.Damaged:
                        return Constants.OrderItemStatusDamagedIcon;
                }
                return null;
            }
        }

        public OrderItem() {
        }

    }
}
