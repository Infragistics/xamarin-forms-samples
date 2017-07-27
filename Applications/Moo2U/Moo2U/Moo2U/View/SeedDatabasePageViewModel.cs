namespace Moo2U.View {
    using System;
    using Moo2U.Infrastructure;
    using Moo2U.SampleData;
    using Prism.Navigation;
    using Prism.Services;
    using Xamarin.Forms;

    public class SeedDatabasePageViewModel : NavigationViewModelBase {

        readonly IDatabase _database;

        public SeedDatabasePageViewModel(IDatabase database, IDeviceService deviceService, INavigationService navigationService, IPageDialogService pageDialogService)
            : base(deviceService, navigationService, pageDialogService) {
            if (database == null) {
                throw new ArgumentNullException(nameof(database));
            }
            _database = database;
        }

        public override async void OnNavigatingTo(NavigationParameters parameters) {
            try {
                await _database.Seed();
                await base.NavigateToNewRootUri(Constants.DefaultUri);
            } catch (Exception ex) {
                Device.BeginInvokeOnMainThread(() => Application.Current.MainPage = new CrashPage(ex.Message));
            }
        }

    }
}
