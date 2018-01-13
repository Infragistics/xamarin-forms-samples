namespace Moo2U.iOS {
    using System;
    using Foundation;
    using Moo2U.Services;
    using Prism;
    using Prism.Ioc;
    using UIKit;

    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate {

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
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
            UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes { TextColor = UIColor.Black }, UIControlState.Normal);
            UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes { TextColor = UIColor.White }, UIControlState.Selected);

            // selected tab bar image tint color
            UITabBar.Appearance.SelectedImageTintColor = UIColor.White;

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App(new iOSInitializer()));
            return base.FinishedLaunching(app, options);
        }

    }

    public class iOSInitializer : IPlatformInitializer {

        public void RegisterTypes(IContainerRegistry containerRegistry) {
            containerRegistry.RegisterInstance<IFileHelper>(new FileHelper());
        }
    }
}
