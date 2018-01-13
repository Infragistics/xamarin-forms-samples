
#pragma warning disable 4014

namespace Moo2U.View {
    using System;
    using System.Threading.Tasks;
    using Moo2U.Infrastructure;
    using Moo2U.Services;
    using Prism.Navigation;
    using Prism.Services;

    public class SignOrderPageViewModel : FormViewModelBase {

        String _customerName;
        Int32 _orderId;
        readonly IOrderService _orderService;

        public String CustomerName {
            get { return _customerName; }
            set {
                _customerName = value;
                RaisePropertyChanged();
            }
        }

        public SignOrderPageViewModel(IOrderService orderService, IDeviceService deviceService, INavigationService navigationService, IPageDialogService pageDialogService)
            : base(deviceService, navigationService, pageDialogService) {
            if (orderService == null) {
                throw new ArgumentNullException(nameof(orderService));
            }
            _orderService = orderService;
        }

        public async Task NoSignatureFound() {
            await base.DisplayDialog("Signature Required", "Please sign the signature pad.");
        }

        public override async void OnNavigatingTo(NavigationParameters parameters) {
            base.OnNavigatingTo(parameters);
            Int32 id;
            if (parameters.ContainsKey(Constants.Key) && Int32.TryParse(parameters[Constants.Key].ToString(), out id)) {
                _orderId = id;
            } else {
                await base.DisplayDialog("Navigation Error", "Unable to navigate to the Signature Page page, invalid order id parameter.");
                await base.GoBack();
                return;
            }

            if (parameters.ContainsKey(Constants.CustomerName)) {
                this.CustomerName = $"{parameters[Constants.CustomerName]}'s Order";
            } else {
                await base.DisplayDialog("Navigation Error", "Unable to navigate to the Signature Page page, customer name not passed.");
                await base.GoBack();
            }
        }

        public async Task SignatureComplete(Byte[] signature) {
            await base.InvokeMethodAsync(
                () => _orderService.CompleteOrderAsync(_orderId, signature),
                () => {
                    base.GoBack();
                });
        }

    }
}
