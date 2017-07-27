namespace Moo2U.View {
    using System.ComponentModel;
    using Moo2U.Model;
    using Xamarin.Forms;

    public partial class DeliverOrderPage : ContentPage {

        public DeliverOrderPage() {
            InitializeComponent();
        }

        void BindableObject_OnPropertyChanged(object sender, PropertyChangedEventArgs e) {
            // this is required because iOS does not disable the ViewCell like Android does.
            // after the item is delivered, remove the ContextMenu items.
            if (Device.RuntimePlatform == Device.iOS && e.PropertyName == "IsEnabled") {
                if (sender is ViewCell viewCell) {
                    if (viewCell.BindingContext is DeliverOrderItem item) {
                        if (item.OrderItemStatus != OrderItemStatus.NotDelivered) {
                            viewCell.ContextActions.Clear();
                        }
                    }
                }
            }
        }

    }
}
