namespace Moo2U.iOS {
    using System;
    using Foundation;
    using Microsoft.Practices.Unity;
    using Moo2U.Services;
    using Prism.Unity;
    using UIKit;

    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate {

        public override Boolean FinishedLaunching(UIApplication app, NSDictionary options) {

            // navigation bar text
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes { TextColor = UIColor.White });

            // navigation bar foreground color
            UINavigationBar.Appearance.TintColor = UIColor.White;

            // navigation bar background
            UINavigationBar.Appearance.BarTintColor = new UIColor(0.00f, 0.61f, 0.42f, 1.0f);

            // tab bar background
            UITabBar.Appearance.BarTintColor = new UIColor(0.00f, 0.61f, 0.42f, 1.0f);

            // tab bar text
            UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes {TextColor = UIColor.Black}, UIControlState.Normal);
            UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes {TextColor = UIColor.White}, UIControlState.Selected);

            // selected tab bar image tint color
            UITabBar.Appearance.SelectedImageTintColor = UIColor.White;

            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App(new IOSInitializer()));
            return base.FinishedLaunching(app, options);
        }

    }

    public class IOSInitializer : IPlatformInitializer {

        public void RegisterTypes(IUnityContainer container) {
            container.RegisterInstance<ISQLite>(new SqliteService());
        }

    }
}
