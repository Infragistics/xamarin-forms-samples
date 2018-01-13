[assembly: Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]

namespace Moo2U {
    using System;
    using System.Threading.Tasks;
    using Moo2U.Infrastructure;
    using Moo2U.Model;
    using Moo2U.SampleData;
    using Moo2U.Services;
    using Moo2U.View;
    using Prism;
    using Prism.Ioc;
    using Prism.Unity;
    using Xamarin.Forms;

    public partial class App : PrismApplication {

        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App()
            : this(null) {
        }

        public App(IPlatformInitializer initializer)
            : base(initializer) {
            // You can use the ThreadHelper in other classes to determine if the code is running on the main thread.
            // This is good for understanding async await & Tasks
            // var onCurrentThread = ThreadHelper.IsOnMainThread;

            ThreadHelper.Initialize(Environment.CurrentManagedThreadId);
        }

        Boolean InitializeTablesCheckDatabaseIsPopulated() {
            // this is NOT async by design
            var sqlite = this.Container.Resolve<ISQLiteConnectionService>();

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

        protected override void RegisterTypes(IContainerRegistry containerRegistry) {
            // DO NOT REGISTER THE FileHelper service in this project.
            // It must be registered in the Android and iOS projects.

            // services
            containerRegistry.RegisterSingleton<ISQLiteConnectionService, SqliteConnectionService>();
            containerRegistry.RegisterSingleton<IDatabase, Database>();
            containerRegistry.RegisterSingleton<IDataGenerator, DataGenerator>();
            containerRegistry.RegisterSingleton<ICustomerService, CustomerService>();
            containerRegistry.RegisterSingleton<IDeliveryStopService, DeliveryStopService>();
            containerRegistry.RegisterSingleton<IDeliveryHistoryService, DeliveryHistoryService>();
            containerRegistry.RegisterSingleton<IOrderService, OrderService>();
            containerRegistry.RegisterSingleton<IProductService, ProductService>();

            // framework
            containerRegistry.RegisterForNavigation<NavigationPage>();

            // tab pages
            containerRegistry.RegisterForNavigation<DeliveriesPage, DeliveriesPageViewModel>();
            containerRegistry.RegisterForNavigation<MapPage>();
            containerRegistry.RegisterForNavigation<PerformancePage, PerformancePageViewModel>();

            // pages
            containerRegistry.RegisterForNavigation<DeliverOrderPage, DeliverOrderPageViewModel>();
            containerRegistry.RegisterForNavigation<SignOrderPage>();
            containerRegistry.RegisterForNavigation<SeedDatabasePage, SeedDatabasePageViewModel>();
            containerRegistry.RegisterForNavigation<MainPage>();
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
