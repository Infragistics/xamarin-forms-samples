namespace Moo2U.Model {
    using System;
    using System.Collections.Generic;
    using Moo2U.Behaviors;
    using Moo2U.Infrastructure;

    public class PerformanceAggregate {

        public Int32 AverageSpeed { get; set; }

        public Double BulletGraphInterval { get; set; }

        public Double BulletGraphLabelInterval { get; set; }

        public Double BulletGraphMaxValue { get; set; }

        public IEnumerable<BulletGraphRangeCreatorItem> BulletGraphRangeCreatorItems { get; set; }

        public Double ChartInterval { get; set; }

        public Int32 CountOfOrders { get; set; }

        public IList<ChartDataItem> CurrentChartDataItems { get; set; }

        public Int32 FuelEfficiency { get; set; }

        public Int32 ItemsDelivered { get; set; }

        public String ItemsDeliveredText => this.ItemsDelivered.ToKString();

        public String ItemsPerOrderText => this.CountOfOrders > 0 ? (this.ItemsDelivered / this.CountOfOrders).ToString("N0") : "0";

        public Double MaxChartDataItemValue { get; set; }

        public Int32 MilesDriven { get; set; }

        public String MilesDrivenText => this.MilesDriven.ToKString();

        public IList<ChartDataItem> PreviousChartDataItems { get; set; }

        public Double RevenueForPeriod { get; set; }

        public Double RevenueForPreviousPeriod { get; set; }

        public IList<TopCustomer> TopCustomers { get; set; }

        public PerformanceAggregate() {
        }

    }
}
