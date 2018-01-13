namespace Moo2U.Services {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Moo2U.Behaviors;
    using Moo2U.Infrastructure;
    using Moo2U.Model;
    using SQLite;

    public class DeliveryHistoryService : IDeliveryHistoryService {

        readonly SQLiteConnection _cn;

        public DeliveryHistoryService(ISQLiteConnectionService sqLiteConnectionService) {
            if (sqLiteConnectionService == null) {
                throw new ArgumentNullException(nameof(sqLiteConnectionService));
            }
            _cn = sqLiteConnectionService.GetConnection();
        }

        public IList<DeliveryHistory> GetAll() {
            return _cn.Query<DeliveryHistory>("SELECT * FROM DeliveryHistory");
        }

        IList<DeliveryHistoryItem> GetAllForDateRange(DateTime starDateTime, DateTime endDateTime) {
            return _cn.Query<DeliveryHistoryItem>("SELECT DH.*, C.Name AS CustomerName FROM DeliveryHistory DH INNER JOIN Customer C on DH.CustomerId = C.Id WHERE DateDelivered BETWEEN ? AND ?", starDateTime, endDateTime);
        }

        public async Task<PerformanceAggregate> GetPerformanceAggregateAsync(Period period) {
            if (!Enum.IsDefined(typeof(Period), period)) {
                throw new ArgumentOutOfRangeException(nameof(period), "Value should be defined in the Period enum.");
            }
            var task = new Task<PerformanceAggregate>(() => LoadPerformanceAggregateData(period));
            task.Start();
            return await task;
        }

        public Int32 Insert(DeliveryHistory deliveryHistory) {
            if (deliveryHistory == null) {
                throw new ArgumentNullException(nameof(deliveryHistory));
            }
            return _cn.Insert(deliveryHistory);
        }

        PerformanceAggregate LoadPerformanceAggregateData(Period period) {
            _cn.BeginTransaction();

            var currentStart = 0d;
            var currentEnd = 0d;
            var previousStart = 0d;
            var previousEnd = 0d;

            switch (period) {
                case Period.Today:
                    currentStart = 0;
                    currentEnd = 0;
                    previousStart = -1;
                    previousEnd = -1;
                    break;
                case Period.Week:
                    currentStart = -6;
                    currentEnd = 0;
                    previousStart = -14;
                    previousEnd = -8;
                    break;
                case Period.Month:
                    currentStart = -30;
                    currentEnd = 0;
                    previousStart = -61;
                    previousEnd = -31;
                    break;
                case Period.Year:
                    currentStart = -365;
                    currentEnd = 0;
                    previousStart = -731;
                    previousEnd = -366;
                    break;
            }

            var stats = GetAllForDateRange(DateTime.Today.AddDays(currentStart), DateTime.Today.AddDays(currentEnd).AddHours(23d).AddMinutes(59d).AddSeconds(59d));
            var previousStats = GetAllForDateRange(DateTime.Today.AddDays(previousStart), DateTime.Today.AddDays(previousEnd).AddHours(23d).AddMinutes(59d).AddSeconds(59d));

            if (period == Period.Today) {
                var hour = 8;
                for (var i = 0; i < 10; i++) {
                    var dateDelivered = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, 0, 0);
                    stats.Add(new DeliveryHistoryItem {DateDelivered = dateDelivered});
                    previousStats.Add(new DeliveryHistoryItem {DateDelivered = dateDelivered.AddDays(-1)});
                    hour += 1;
                }
            }

            if (stats == null || stats.Count == 0) {
                return null;
            }
            if (previousStats == null || previousStats.Count == 0) {
                return null;
            }

            var totals = from d in stats
                where d.ItemsDelivered > 0
                group d by 1
                into da
                select new PerformanceAggregate {
                    FuelEfficiency = (Int32)da.Average(x => x.FuelEfficiency),
                    AverageSpeed = (Int32)da.Average(x => x.AverageSpeed),
                    ItemsDelivered = da.Sum(x => x.ItemsDelivered),
                    MilesDriven = da.Sum(x => x.MileDriven),
                    RevenueForPeriod = da.Sum(x => x.Revenue)
                };

            var performanceAggregate = totals.SingleOrDefault();
            if (performanceAggregate == null) {
                performanceAggregate = new PerformanceAggregate();
            } else {
                performanceAggregate.CountOfOrders = stats.Count(x => x.ItemsDelivered > 0);

                var topCustomers = from c in stats
                    group c by c.CustomerName
                    into cg
                    select new TopCustomer {
                        Revenue = cg.Sum(x => x.Revenue),
                        Name = cg.Key,
                        Quantity = cg.Sum(x => x.ItemsDelivered)
                    };

                performanceAggregate.TopCustomers = topCustomers.OrderByDescending(x => x.Revenue).Take(3).ToList();
            }

            performanceAggregate.RevenueForPreviousPeriod = previousStats.Sum(x => x.Revenue);

            if (period == Period.Year) {
                var currentChartDataItems = from c in stats
                    group c by new DateTime(c.DateDelivered.Year, c.DateDelivered.Month, 1)
                    into cg
                    select new ChartDataItem(DataItemKind.Current) {
                        Value = cg.Sum(x => x.Revenue),
                        Date = cg.Key
                    };

                performanceAggregate.CurrentChartDataItems = currentChartDataItems.OrderBy(x => x.Date).ToList();

                var previousChartDataItems = from c in previousStats
                    group c by new DateTime(c.DateDelivered.Year, c.DateDelivered.Month, 1)
                    into cg
                    select new ChartDataItem(DataItemKind.Previous) {
                        Value = cg.Sum(x => x.Revenue),
                        Date = cg.Key
                    };

                performanceAggregate.PreviousChartDataItems = previousChartDataItems.OrderBy(x => x.Date).ToList();
            } else if (period == Period.Today) {
                var currentChartDataItems = from c in stats
                    group c by c.DateDelivered.Hour
                    into cg
                    orderby cg.Key
                    select new ChartDataItem(DataItemKind.Current) {
                        Value = cg.Sum(x => x.Revenue),
                        HourLabel = cg.Key.ToString()
                    };

                performanceAggregate.CurrentChartDataItems = currentChartDataItems.OrderBy(x => x.Date.Hour).ToList();

                var previousChartDataItems = from c in previousStats
                    group c by c.DateDelivered.Hour
                    into cg
                    orderby cg.Key
                    select new ChartDataItem(DataItemKind.Previous) {
                        Value = cg.Sum(x => x.Revenue),
                        HourLabel = cg.Key.ToString()
                    };

                performanceAggregate.PreviousChartDataItems = previousChartDataItems.OrderBy(x => x.Date.Hour).ToList();
            } else {
                var currentChartDataItems = from c in stats
                    group c by new DateTime(c.DateDelivered.Year, c.DateDelivered.Month, c.DateDelivered.Day)
                    into cg
                    select new ChartDataItem(DataItemKind.Current) {
                        Value = cg.Sum(x => x.Revenue),
                        Date = cg.Key
                    };

                performanceAggregate.CurrentChartDataItems = currentChartDataItems.OrderBy(x => x.Date).ToList();

                var previousChartDataItems = from c in previousStats
                    group c by new DateTime(c.DateDelivered.Year, c.DateDelivered.Month, c.DateDelivered.Day)
                    into cg
                    select new ChartDataItem(DataItemKind.Previous) {
                        Value = cg.Sum(x => x.Revenue),
                        Date = cg.Key
                    };

                performanceAggregate.PreviousChartDataItems = previousChartDataItems.OrderBy(x => x.Date).ToList();

                // this occurs on the first of the month or when no deliveries have been posted for the day.
                // we need to insert a phantom result record so that both collections have the same number of records
                // when using multiple series with the Infragistics XamDataChart, the collections bound to the ItemsSource need to have the same number of items.
                // there are two techniques for ensuring the collections have the same number of items.
                // first - you can have a data object that has properties for each series.
                // second - Moo2U uses two different collections, this below code ensures that both collections have the same number of items.
                if (performanceAggregate.CurrentChartDataItems.Count < performanceAggregate.PreviousChartDataItems.Count) {
                    performanceAggregate.CurrentChartDataItems.Add(new ChartDataItem(DataItemKind.Current){ Date = DateTime.Now});
                }
            }

            var currentMax = 0d;
            if (performanceAggregate.CurrentChartDataItems.Count > 0) {
                currentMax = performanceAggregate.CurrentChartDataItems.Max(x => x.Value);
            }
            var previousMax = performanceAggregate.PreviousChartDataItems.Max(x => x.Value);
            var modifiedDataItemMax = Math.Max(currentMax, previousMax) + Math.Max(currentMax, previousMax) * .08d;
            performanceAggregate.MaxChartDataItemValue = modifiedDataItemMax.RoundUpProportionally();
            performanceAggregate.ChartInterval = (performanceAggregate.MaxChartDataItemValue / 4).RoundUpProportionally();

            var maxValue = Math.Max(performanceAggregate.RevenueForPeriod, performanceAggregate.RevenueForPreviousPeriod);
            var modifiedMaxValue = maxValue + maxValue * .15;
            var rangeOneEndValue = maxValue - maxValue * .20;

            performanceAggregate.BulletGraphMaxValue = modifiedMaxValue.RoundUpProportionally();
            var list = new List<BulletGraphRangeCreatorItem>();
            list.Add(new BulletGraphRangeCreatorItem {StartValue = 0d, EndValue = rangeOneEndValue});
            list.Add(new BulletGraphRangeCreatorItem {StartValue = rangeOneEndValue, EndValue = performanceAggregate.BulletGraphMaxValue});
            performanceAggregate.BulletGraphRangeCreatorItems = list;

            performanceAggregate.BulletGraphLabelInterval = performanceAggregate.BulletGraphMaxValue / 4;
            performanceAggregate.BulletGraphInterval = performanceAggregate.BulletGraphLabelInterval / 2;

            _cn.Commit();

            return performanceAggregate;
        }

    }
}
