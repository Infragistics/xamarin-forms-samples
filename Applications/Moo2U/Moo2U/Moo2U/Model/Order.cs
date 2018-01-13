namespace Moo2U.Model {
    using System;
    using Prism.Mvvm;
    using SQLite;

    public class Order : BindableBase {

        Int32 _customerId;
        DateTime? _dateDelivered;
        DateTime _dateOrdered;
        DateTime _dateScheduledDelivery;
        Int32 _id;
        Double _itemPercentDelivered;
        OrderStatus _orderStatus = OrderStatus.New;
        Byte[] _signature;
        Double _total;
        Double _TotalDelivered;

        [NotNull, Indexed]
        public Int32 CustomerId {
            get { return _customerId; }
            set {
                _customerId = value;
                RaisePropertyChanged();
            }
        }

        public DateTime? DateDelivered {
            get { return _dateDelivered; }
            set {
                _dateDelivered = value;
                RaisePropertyChanged();
            }
        }

        [NotNull]
        public DateTime DateOrdered {
            get { return _dateOrdered; }
            set {
                _dateOrdered = value;
                RaisePropertyChanged();
            }
        }

        [NotNull]
        public DateTime DateScheduledDelivery {
            get { return _dateScheduledDelivery; }
            set {
                _dateScheduledDelivery = value;
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

        [NotNull]
        public Double ItemPercentDelivered {
            get { return _itemPercentDelivered; }
            set {
                _itemPercentDelivered = value;
                RaisePropertyChanged();
            }
        }

        [NotNull]
        public OrderStatus OrderStatus {
            get { return _orderStatus; }
            set {
                _orderStatus = value;
                RaisePropertyChanged();
            }
        }

        public Byte[] Signature {
            get { return _signature; }
            set {
                _signature = value;
                RaisePropertyChanged();
            }
        }

        [NotNull]
        public Double Total {
            get { return _total; }
            set {
                _total = value;
                RaisePropertyChanged();
            }
        }

        [NotNull]
        public Double TotalDelivered {
            get { return _TotalDelivered; }
            set {
                _TotalDelivered = value;
                RaisePropertyChanged();
            }
        }

    }
}
