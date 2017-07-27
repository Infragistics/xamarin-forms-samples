namespace Moo2U.Model {
    using System;

    public class DeliveryListItem : DeliveryStop {

        Double _itemPercentDelivered;

        public String Address { get; set; }

        public String AddressType { get; set; }

        public String CityStateZip { get; set; }

        public String CompletedAndUnsignedIcon {
            get {
                if (this.OrderStatus == OrderStatus.ReadyForSignature) {
                    return "unsigned.png";
                }
                if (this.OrderStatus == OrderStatus.Completed) {
                    return "complete.png";
                }
                return null;
            }
        }

        public String Icon => this.AddressType == Constants.Business ? Constants.BusinessIcon : Constants.ResidenceIcon;

        public Boolean IsCompletedAndUnsigned => this.OrderStatus == OrderStatus.ReadyForSignature || this.OrderStatus == OrderStatus.Completed;

        public Double ItemPercentDelivered {
            get { return _itemPercentDelivered; }
            set {
                _itemPercentDelivered = value;
                RaisePropertyChanged();
            }
        }

        public String Name { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DeliveryListItem() {
        }

    }
}
