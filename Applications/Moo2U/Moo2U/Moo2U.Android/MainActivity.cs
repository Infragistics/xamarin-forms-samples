namespace Moo2U.Droid {
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Moo2U.Services;
    using Prism;
    using Prism.Ioc;

    [Activity(Label = "Moo2U", Icon = "@drawable/logo", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {

        protected override void OnCreate(Bundle bundle) {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }

    }

    public class AndroidInitializer : IPlatformInitializer {

        public void RegisterTypes(IContainerRegistry containerRegistry) {
            containerRegistry.RegisterInstance<IFileHelper>(new FileHelper());
        }
    }
}
