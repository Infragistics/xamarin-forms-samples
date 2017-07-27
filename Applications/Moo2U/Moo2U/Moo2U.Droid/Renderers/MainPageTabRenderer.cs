using Moo2U.Droid.Renderers;
using Moo2U.View;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(MainPage), typeof(MainPageTabRenderer))]

namespace Moo2U.Droid.Renderers {
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using Android.Support.Design.Widget;
    using Moo2U.Droid;
    using Xamarin.Forms.Platform.Android.AppCompat;

    /// <summary>
    /// Class MainPageTabRenderer. 
    /// </summary>
    /// <seealso cref="Xamarin.Forms.Platform.Android.AppCompat.TabbedPageRenderer" />
    public class MainPageTabRenderer : TabbedPageRenderer {

        Boolean _animationsDisabled;
        Int32 _previouslySelectedTabIndex;
        const Int32 DeliveriesTabIndex = 0;
        const Int32 MapTabIndex = 1;
        const Int32 PerformanceTabIndex = 2;

        public MainPageTabRenderer() {
            
        }

        protected override void OnElementPropertyChanged(Object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);
            if (!_animationsDisabled) {
                _animationsDisabled = true;

                // Disable animations only when UseAnimations is queried for enabling gestures
                var fieldInfo = typeof(TabbedPageRenderer).GetField("_useAnimations", BindingFlags.Instance | BindingFlags.NonPublic);
                fieldInfo?.SetValue(this, false);
            }

            if (e.PropertyName == "CurrentPage") {
                var layout = (TabLayout)this.ViewGroup.GetChildAt(1);
                if (layout == null) {
                    return;
                }

                var previousTab = layout.GetTabAt(_previouslySelectedTabIndex);
                if (previousTab == null) {
                    return;
                }
                switch (previousTab.Text) {
                    case Constants.Deliveries:
                        previousTab.SetIcon(Resource.Drawable.delivery);
                        break;
                    case Constants.Map:
                        previousTab.SetIcon(Resource.Drawable.map);
                        break;
                    case Constants.Performance:
                        previousTab.SetIcon(Resource.Drawable.performance);
                        break;
                    default:
                        return;
                }

                _previouslySelectedTabIndex = layout.SelectedTabPosition;
                var currentTab = layout.GetTabAt(layout.SelectedTabPosition);
                switch (layout.SelectedTabPosition) {
                    case DeliveriesTabIndex:
                        currentTab.SetIcon(Resource.Drawable.delivery_active);
                        break;
                    case MapTabIndex:
                        currentTab.SetIcon(Resource.Drawable.map_active);
                        break;
                    case PerformanceTabIndex:
                        currentTab.SetIcon(Resource.Drawable.performance_active);
                        break;
                    default:
                        return;
                }
            } 
        }

    }
}
