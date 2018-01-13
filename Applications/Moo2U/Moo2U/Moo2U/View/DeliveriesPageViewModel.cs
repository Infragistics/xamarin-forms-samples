
#pragma warning disable 4014

namespace Moo2U.View {
    using System;
    using System.Collections.Generic;
    using Moo2U.Infrastructure;
    using Moo2U.Model;
    using Moo2U.Services;
    using Prism;
    using Prism.Navigation;
    using Prism.Services;

    public class DeliveriesPageViewModel : FormViewModelBase, IActiveAware {

        IList<DeliveryListItem> _deliveryListItems;
        readonly IDeliveryStopService _deliveryStopService;
        Boolean _isActive;
        DeliveryListItem _selectedDeliveryListItem;

        public IList<DeliveryListItem> DeliveryListItems {
            get { return _deliveryListItems; }
            set {
                _deliveryListItems = value;
                RaisePropertyChanged();
            }
        }

        public Boolean IsActive {
            get { return _isActive; }
            set {
                _isActive = value;
                RaisePropertyChanged();
                if (_isActive) {
                    LoadData();
                }
            }
        }

        public DeliveryListItem SelectedDeliveryListItem {
            get { return _selectedDeliveryListItem; }
            set {
                _selectedDeliveryListItem = value;
                RaisePropertyChanged();
                if (_selectedDeliveryListItem != null) {
                    DeliveryListItemSelected();
                }
            }
        }

        public DeliveriesPageViewModel(IDeliveryStopService deliveryStopService, IDeviceService deviceService, INavigationService navigationService, IPageDialogService pageDialogService)
            : base(deviceService, navigationService, pageDialogService) {
            if (deliveryStopService == null) {
                throw new ArgumentNullException(nameof(deliveryStopService));
            }
            _deliveryStopService = deliveryStopService;
        }

        void DeliveryListItemSelected() {
            NavigateToUri(typeof(DeliverOrderPage).Name, this.SelectedDeliveryListItem.OrderId);
            this.SelectedDeliveryListItem = null;
        }

#pragma warning disable 67

        public event EventHandler IsActiveChanged;

#pragma warning disable 67

        async void LoadData() {
            await InvokeMethodAsync(
                () => _deliveryStopService.GetDeliveryListItemsForDateAsync(DateTime.Today),
                r => this.DeliveryListItems = r,
                ex => {
                    GoBack();
                });
        }

    }
}
