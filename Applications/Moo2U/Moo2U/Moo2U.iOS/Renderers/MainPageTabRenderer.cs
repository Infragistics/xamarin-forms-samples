using Moo2U.iOS.Renderers;
using Moo2U.View;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(MainPage), typeof(MainPageTabRenderer))]

namespace Moo2U.iOS.Renderers {
    using System;
    using UIKit;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.iOS;

    public class MainPageTabRenderer : TabbedRenderer {

        public MainPageTabRenderer() {
        }

        void UpdateItem(UITabBarItem item, String icon) {
            if (item == null || String.IsNullOrWhiteSpace(icon)) {
                return;
            }
            try {
                if (!icon.Contains("_active")) {
                    item.Image = UIImage.FromBundle(icon).
                                         ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
                } else {
                    item.Image = UIImage.FromBundle(icon.Replace("_active", String.Empty)).
                                         ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
                }
            } catch (Exception ex) {
                Console.WriteLine("Unable to set normal icon: " + ex);
            }
        }

        public override void ViewWillAppear(Boolean animated) {
            if (this.TabBar?.Items == null) {
                return;
            }

            if (this.Element is TabbedPage tabs) {
                for (var i = 0; i < this.TabBar.Items.Length; i++) {
                    UpdateItem(this.TabBar.Items[i], tabs.Children[i].Icon);
                }
            }
            base.ViewWillAppear(animated);
        }

    }
}
