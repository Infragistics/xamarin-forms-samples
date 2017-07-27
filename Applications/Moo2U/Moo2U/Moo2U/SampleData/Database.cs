namespace Moo2U.SampleData {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Moo2U.Model;
    using Moo2U.Services;
    using SQLite.Net;

    public class Database : IDatabase {

        readonly SQLiteConnection _cn;
        readonly IProductService _productService;
        readonly ICustomerService _customerService;
        readonly IOrderService _orderService;
        readonly IDeliveryHistoryService _deliveryHistoryService;
        readonly IDeliveryStopService _deliveryStopService;

        public Database(ISQLite sqLite, IProductService productService, ICustomerService customerService, IOrderService orderService, IDeliveryHistoryService deliveryHistoryService, IDeliveryStopService deliveryStopService) {
            if (sqLite == null) {
                throw new ArgumentNullException(nameof(sqLite));
            }
            if (productService == null) {
                throw new ArgumentNullException(nameof(productService));
            }
            if (customerService == null) {
                throw new ArgumentNullException(nameof(customerService));
            }
            if (orderService == null) {
                throw new ArgumentNullException(nameof(orderService));
            }
            if (deliveryHistoryService == null) {
                throw new ArgumentNullException(nameof(deliveryHistoryService));
            }
            if (deliveryStopService == null) {
                throw new ArgumentNullException(nameof(deliveryStopService));
            }

            _cn = sqLite.GetConnection();
            _productService = productService;
            _customerService = customerService;
            _orderService = orderService;
            _deliveryHistoryService = deliveryHistoryService;
            _deliveryStopService = deliveryStopService;
        }

        public Boolean IsDatabasePopulated() {
            if (!_cn.Table<Order>().Any()) {
                return false;
            }
            return true;
        }

        public async Task Seed() {
            var task = new Task(SeedDatabase);
            task.Start();
            await task;
        }

        void SeedDatabase() {
            var dg = new DataGenerator();
            _cn.BeginTransaction();

            var p = new Product();
            p.Description = "Fresh Organic Raw Milk";
            p.Price = 5.99d;
            p.Icon = "milk.png";
            p.UnitOfMeasure = "gal";
            _productService.Insert(p);

            p = new Product();
            p.Description = "Light Buffalo Mozzarella";
            p.Price = 6.75;
            p.Icon = "mozzarella.png";
            p.UnitOfMeasure = "lbs";
            _productService.Insert(p);

            p = new Product();
            p.Description = "Organic Unsalted Butter";
            p.Price = 4.99;
            p.Icon = "butter.png";
            p.UnitOfMeasure = "lbs";
            _productService.Insert(p);

            p = new Product();
            p.Description = "Free-range Eggs";
            p.Price = 3.99;
            p.Icon = "eggs.png";
            p.UnitOfMeasure = "dz";
            _productService.Insert(p);

            for (var i = 0; i < 20; i++) {
                var c = new Customer();
                c.Address = $"{dg.GetInteger(10, 1000)} {dg.GetStreet()}";
                c.CityStateZip = "Cranbury, NJ 08520";

                if (i % 2 == 0) {
                    c.AddressType = Constants.Residence;
                    c.Name = dg.GetFullName();
                } else {
                    c.AddressType = Constants.Business;
                    c.Name = dg.GetCompanyName();

                }
                _customerService.Insert(c);
            }

            var products = _productService.GetAll();
            var customers = _customerService.GetAll();
            var currentCustomer = 0;

            for (var i = -735; i < 0; i++) {
                var hourDelivered = 8;
                for (var j = 0; j < 9; j++) {

                    var itemsDelivered = 0;
                    var revenue = 0.0d;
                    var milesDriven = 0;

                    var o = new Order();
                    o.CustomerId = customers[currentCustomer].Id;
                    o.DateOrdered = DateTime.Today.AddDays(i - 7);
                    o.DateScheduledDelivery = DateTime.Today.AddDays(i);
                    o.DateDelivered = new DateTime(o.DateScheduledDelivery.Year, o.DateScheduledDelivery.Month, o.DateScheduledDelivery.Day, j + 8, hourDelivered, dg.GetInteger(0, 59));
                    hourDelivered += 1;

                    o.OrderStatus = OrderStatus.Completed;

                    var items = new List<OrderItem>();
                    for (var k = 0; k < 4; k++) {
                        var oo = new OrderItem();
                        oo.ProductId = products[k].Id;
                        if (customers[currentCustomer].AddressType == Constants.Business) {
                            oo.Quantity = dg.GetInteger(5, 40);
                            if (i < -365) {
                                oo.Quantity = oo.Quantity - dg.GetInteger(0, 5);
                                if (oo.Quantity < 1) {
                                    oo.Quantity = 1;
                                }
                            }
                        } else {
                            oo.Quantity = dg.GetInteger(1, 5);
                            if (i < -365) {
                                oo.Quantity = oo.Quantity - dg.GetInteger(0, 2);
                                if (oo.Quantity < 1) {
                                    oo.Quantity = 1;
                                }
                            }
                        }
                        oo.ExtendedPrice = oo.Quantity * products[0].Price;
                        oo.OrderItemStatus = OrderItemStatus.Delivered;
                        items.Add(oo);

                        itemsDelivered += oo.Quantity;
                        revenue += oo.ExtendedPrice;
                        milesDriven += dg.GetInteger(5, 12);

                    }
                    var averageSpeed = dg.GetInteger(35, 50);
                    var fuelEfficiency = dg.GetInteger(25, 30);

                    _orderService.Insert(o, items);

                    var dh = new DeliveryHistory();
                    dh.OrderId = o.Id;
                    dh.CustomerId = o.CustomerId;
                    dh.AverageSpeed = averageSpeed;
                    dh.DateDelivered = o.DateDelivered.Value;
                    dh.FuelEfficiency = fuelEfficiency;
                    dh.ItemsDelivered = itemsDelivered;
                    dh.MileDriven = milesDriven;
                    dh.Revenue = revenue;
                    _deliveryHistoryService.Insert(dh);

                    currentCustomer += 1;
                    if (currentCustomer > customers.Count - 1) {
                        currentCustomer = 0;
                    }
                }

            }

            var ht = new HashSet<Int32>();
            var maxCustomerId = customers.Count - 1;

            var routeStopNumber = 1;
            for (var i = 0; i < 11; i++) {
                var customerIndex = -1;
                while (customerIndex == -1) {
                    var test = dg.GetInteger(0, maxCustomerId);
                    if (!ht.Contains(test)) {
                        customerIndex = test;
                        ht.Add(test);
                    }
                }

                var o = new Order();
                o.CustomerId = customers[customerIndex].Id;
                o.DateOrdered = DateTime.Today.AddDays(-7);
                o.DateScheduledDelivery = DateTime.Today;
                o.OrderStatus = OrderStatus.New;

                var items = new List<OrderItem>();
                for (var k = 0; k < 4; k++) {
                    var oo = new OrderItem();
                    oo.ProductId = products[k].Id;
                    if (customers[customerIndex].AddressType == Constants.Business) {
                        oo.Quantity = dg.GetInteger(5, 25);
                    } else {
                        oo.Quantity = dg.GetInteger(1, 5);
                    }
                    oo.ExtendedPrice = oo.Quantity * products[0].Price;
                    oo.OrderItemStatus = OrderItemStatus.NotDelivered;
                    items.Add(oo);
                }
                _orderService.Insert(o, items);

                var ds = new DeliveryStop();
                ds.DateDelivery = o.DateScheduledDelivery;
                ds.MileageFromPreviousStop = dg.GetInteger(5, 25);
                ds.OrderId = o.Id;
                ds.RouteStopNumber = routeStopNumber;

                _deliveryStopService.Insert(ds);
                routeStopNumber += 1;
            }

            _cn.Commit();

        }
    }
}