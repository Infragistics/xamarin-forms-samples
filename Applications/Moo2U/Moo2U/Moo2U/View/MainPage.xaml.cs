namespace Moo2U.View {
    using Xamarin.Forms;
    using Xamarin.Forms.PlatformConfiguration;
    using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
    public partial class MainPage : Xamarin.Forms.TabbedPage {

        public MainPage() {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.On<Android>().SetIsSwipePagingEnabled(false);
        }

    }
}
