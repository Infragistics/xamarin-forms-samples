namespace Moo2U.Model {
    using System;

    public class DeliverOrderItem : OrderItem {

        public String Description { get; set; }

        public String Icon { get; set; }

        public Double Price { get; set; }

        public String UnitOfMeasure { get; set; }

        public DeliverOrderItem() {
        }

    }
}
