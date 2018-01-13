namespace Moo2U.Model {
    using System;
    using Prism.Mvvm;
    using SQLite;

    public class DeliveryStop : BindableBase {

        [NotNull]
        public DateTime DateDelivery { get; set; }

        [PrimaryKey, AutoIncrement]
        public Int32 Id { get; set; }

        [NotNull]
        public Int32 MileageFromPreviousStop { get; set; }

        [NotNull]
        public Int32 OrderId { get; set; }

        [NotNull]
        public Int32 RouteStopNumber { get; set; }

        public DeliveryStop() {
        }

    }
}
