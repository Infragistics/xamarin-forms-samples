// ReSharper disable UnusedMethodReturnValue.Local

#pragma warning disable 4014

namespace Moo2U {
    using System;
    using System.Threading.Tasks;
    using Microsoft.Practices.Unity;
    using Moo2U.Infrastructure;
    using Moo2U.Model;
    using Moo2U.SampleData;
    using Moo2U.Services;
    using Moo2U.View;
    using Prism.Unity;
    using Xamarin.Forms;

    public partial class App : PrismApplication {

        public App(IPlatformInitializer initializer = null)
            : base(initializer) {
            ThreadHelper.Initialize(Environment.CurrentManagedThreadId);

            // You can use the ThreadHelper in other classes to determine if the code is running on the main thread.
            // This is good for understanding async await & Tasks
            // var onCurrentThread = ThreadHelper.IsOnMainThread;
        }

        Boolean InitializeTablesCheckDatabaseIsPopulated() {
            // this is NOT async by design
            var sqlite = this.Container.Resolve<ISQLite>();
            sqlite.CreateDatabase();

            var cn = sqlite.GetConnection();
            cn.CreateTable<Product>();
            cn.CreateTable<Customer>();
            cn.CreateTable<DeliveryHistory>();
            cn.CreateTable<DeliveryStop>();
            cn.CreateTable<Order>();
            cn.CreateTable<OrderItem>();

            var db = this.Container.Resolve<IDatabase>();
            return db.IsDatabasePopulated();
        }

        protected override async void OnInitialized() {
            InitializeComponent();

            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            try {
                var uri = InitializeTablesCheckDatabaseIsPopulated() ? Constants.DefaultUri : Constants.SeedDatabasePage;
                await this.NavigationService.NavigateAsync(uri);
            } catch (Exception ex) {
                ShowCrashPage(ex);
            }
        }

        protected override void RegisterTypes() {
            // services
            this.Container.RegisterType<IDataGenerator, DataGenerator>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<ICustomerService, CustomerService>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IDeliveryStopService, DeliveryStopService>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IDeliveryHistoryService, DeliveryHistoryService>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IOrderService, OrderService>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IProductService, ProductService>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IDatabase, Database>(new ContainerControlledLifetimeManager());

            // framework
            this.Container.RegisterTypeForNavigation<NavigationPage>();

            // tab pages
            this.Container.RegisterTypeForNavigation<DeliveriesPage, DeliveriesPageViewModel>();
            this.Container.RegisterTypeForNavigation<MapPage>();
            this.Container.RegisterTypeForNavigation<PerformancePage, PerformancePageViewModel>();

            // pages
            this.Container.RegisterTypeForNavigation<DeliverOrderPage, DeliverOrderPageViewModel>();
            this.Container.RegisterTypeForNavigation<SignOrderPage>();
            this.Container.RegisterTypeForNavigation<SeedDatabasePage, SeedDatabasePageViewModel>();
            this.Container.RegisterTypeForNavigation<MainPage>();
        }

        void ShowCrashPage(Exception ex = null) {
            Device.BeginInvokeOnMainThread(() => this.MainPage = new CrashPage(ex?.Message));

            //TODO: Add logging and handle this the way you want
            //Logging would be a good idea too so you can correct the problem.
        }

        void TaskScheduler_UnobservedTaskException(Object sender, UnobservedTaskExceptionEventArgs e) {
            if (!e.Observed) {
                // prevents the app domain from being torn down
                e.SetObserved();

                // show the crash page
                ShowCrashPage(e.Exception.Flatten().GetBaseException());
            }
        }

    }
}
