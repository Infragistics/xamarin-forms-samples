namespace Moo2U.Droid {
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Microsoft.Practices.Unity;
    using Moo2U.Services;
    using Prism.Unity;

    [Activity(Label = "Moo2U", Icon = "@drawable/logo", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {

        protected override void OnCreate(Bundle bundle) {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }

    }

    public class AndroidInitializer : IPlatformInitializer {

        public void RegisterTypes(IUnityContainer container) {
            container.RegisterInstance<ISQLite>(new SqliteService());
        }

    }
}
