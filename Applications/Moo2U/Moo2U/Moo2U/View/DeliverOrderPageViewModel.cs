
#pragma warning disable 4014

namespace Moo2U.View {
    using System;
    using System.Linq;
    using Moo2U.Infrastructure;
    using Moo2U.Model;
    using Moo2U.Services;
    using Prism.Commands;
    using Prism.Navigation;
    using Prism.Services;

    public class DeliverOrderPageViewModel : FormViewModelBase {

        String _completeButtonText = Constants.CompleteAllButtonText;

        DelegateCommand _completeOrderCommand;
        DeliverOrder _deliverOrder;
        DelegateCommand<DeliverOrderItem> _itemCanceledCommand;
        DelegateCommand<DeliverOrderItem> _itemDamagedCommand;
        DelegateCommand<DeliverOrderItem> _itemDeliveredCommand;
        readonly IOrderService _orderService;
        DelegateCommand _signOrderCommand;

        public String CompleteButtonText {
            get { return _completeButtonText; }
            set {
                _completeButtonText = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand CompleteOrderCommand => _completeOrderCommand ?? (_completeOrderCommand = new DelegateCommand(CompleteOrderCommandExecute));

        public DeliverOrder DeliverOrder {
            get { return _deliverOrder; }
            set {
                _deliverOrder = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand<DeliverOrderItem> ItemCanceledCommand => _itemCanceledCommand ?? (_itemCanceledCommand = new DelegateCommand<DeliverOrderItem>(ItemCanceledCommandExecute));

        public DelegateCommand<DeliverOrderItem> ItemDamagedCommand => _itemDamagedCommand ?? (_itemDamagedCommand = new DelegateCommand<DeliverOrderItem>(ItemDamagedCommandExecute));

        public DelegateCommand<DeliverOrderItem> ItemDeliveredCommand => _itemDeliveredCommand ?? (_itemDeliveredCommand = new DelegateCommand<DeliverOrderItem>(ItemDeliveredCommandExecute));

        public DelegateCommand SignOrderCommand => _signOrderCommand ?? (_signOrderCommand = new DelegateCommand(SignOrderCommandExecute));

        public DeliverOrderPageViewModel(IOrderService orderService, IDeviceService deviceService, INavigationService navigationService, IPageDialogService pageDialogService)
            : base(deviceService, navigationService, pageDialogService) {
            if (orderService == null) {
                throw new ArgumentNullException(nameof(orderService));
            }
            _orderService = orderService;
        }

        void CompleteOrderCommandExecute() {
            foreach (var item in this.DeliverOrder.DeliverOrderItems.Where(x => x.OrderItemStatus == OrderItemStatus.NotDelivered)) {
                ItemDeliveredCommandExecute(item);
            }
        }

        void ItemCanceledCommandExecute(DeliverOrderItem deliverOrderItem) {
            if (deliverOrderItem == null) {
                return;
            }
            deliverOrderItem.OrderItemStatus = OrderItemStatus.Canceled;
            SetFormState(deliverOrderItem);
        }

        void ItemDamagedCommandExecute(DeliverOrderItem deliverOrderItem) {
            if (deliverOrderItem == null) {
                return;
            }
            deliverOrderItem.OrderItemStatus = OrderItemStatus.Damaged;
            SetFormState(deliverOrderItem);
        }

        void ItemDeliveredCommandExecute(DeliverOrderItem deliverOrderItem) {
            if (deliverOrderItem == null) {
                return;
            }
            deliverOrderItem.OrderItemStatus = OrderItemStatus.Delivered;
            _deliverOrder.TotalDelivered += deliverOrderItem.ExtendedPrice;
            SetFormState(deliverOrderItem);
        }

        void LoadData(Int32 id) {
            InvokeMethodAsync(
                () => _orderService.GetDeliverOrderAsync(id),
                r => {
                    this.DeliverOrder = r;
                    SetFormState(null);
                },
                ex => {
                    GoBack();
                });
        }

        public override async void OnNavigatingTo(NavigationParameters parameters) {
            OnNavigatedTo(parameters);

            if (parameters.GetNavigationMode() == NavigationMode.Back && this.DeliverOrder != null) {
                LoadData(this.DeliverOrder.Id);
                return;
            }

            Int32 id;
            if (parameters.ContainsKey(Constants.Key) && Int32.TryParse(parameters[Constants.Key].ToString(), out id)) {
                LoadData(id);
            } else {
                await DisplayDialog("Navigation Error", "Unable to navigate to the Deliver Order page.");
                await GoBack();
            }
        }

        void SetFormState(DeliverOrderItem deliverOrderItem) {
            if (deliverOrderItem != null) {
                try {
                    _orderService.UpdateDeliverOrderItem(deliverOrderItem.Id, deliverOrderItem.OrderItemStatus);
                    this.DeliverOrder.SetOrderStatus();
                    _orderService.UpdateDeliverOrder(this.DeliverOrder.Id, this.DeliverOrder.OrderStatus, this.DeliverOrder.ItemPercentDelivered, this.DeliverOrder.TotalDelivered);
                } catch (Exception ex) {
                    DisplayDialog("Update Error", ex.GetBaseException().Message);
                    GoBack();
                }
            }

            if (this.DeliverOrder.OrderStatus == OrderStatus.Partial && this.CompleteButtonText != Constants.CompleteRemainingButtonText) {
                this.CompleteButtonText = Constants.CompleteRemainingButtonText;
            }
        }

        void SignOrderCommandExecute() {
            var p = new NavigationParameters {{Constants.Key, this.DeliverOrder.Id}, {Constants.CustomerName, this.DeliverOrder.CustomerName}};
            NavigateToUri(typeof(SignOrderPage).Name, p);
        }

    }
}
