namespace Moo2U.Model {
    using System;
    using SQLite.Net.Attributes;

    public class DeliveryHistory {

        [NotNull]
        public Int32 AverageSpeed { get; set; }

        [NotNull]
        public Int32 CustomerId { get; set; }

        [NotNull, Indexed]
        public DateTime DateDelivered { get; set; }

        [NotNull]
        public Int32 FuelEfficiency { get; set; }

        [PrimaryKey, AutoIncrement]
        public Int32 Id { get; set; }

        [NotNull]
        public Int32 ItemsDelivered { get; set; }

        [NotNull]
        public Int32 MileDriven { get; set; }

        [NotNull]
        public Int32 OrderId { get; set; }

        [NotNull]
        public Double Revenue { get; set; }

        public DeliveryHistory() {
        }

    }
}
