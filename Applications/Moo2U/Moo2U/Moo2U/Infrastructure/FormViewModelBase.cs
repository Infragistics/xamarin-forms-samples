namespace Moo2U.Infrastructure {
    using System;
    using System.Threading.Tasks;
    using Prism.Navigation;
    using Prism.Services;
    

    public abstract class FormViewModelBase : NavigationViewModelBase {

        protected FormViewModelBase(IDeviceService deviceService, INavigationService navigationService, IPageDialogService pageDialogService)
            : base(deviceService, navigationService, pageDialogService) {
        }

        protected async Task HandleAggregateException(AggregateException ae, Action<Exception> errorCallback, ShowDialog showDialog) {
            ClearIsBusy();
            var baseException = ae?.Flatten()?.GetBaseException() ?? new Exception(Constants.AntecedentExceptionNull);
            if (showDialog == ShowDialog.Yes) {
                await this.PageDialogService.DisplayAlertAsync(Constants.Error, baseException.Message, Constants.OK);
            }
            errorCallback?.Invoke(baseException);
        }

        protected async Task HandleException(Exception ex, Action<Exception> errorCallback, ShowDialog showDialog) {
            ClearIsBusy();
            var baseException = ex.GetBaseException();
            if (showDialog == ShowDialog.Yes) {
                await this.PageDialogService.DisplayAlertAsync(Constants.Error, baseException.Message, Constants.OK);
            }
            errorCallback?.Invoke(baseException);
        }

        protected async Task InvokeMethodAsync<T>(Func<Task<T>> method, Action<T> resultCallback, Action<Exception> errorCallback = null, ShowDialog showDialog = ShowDialog.Yes) {
            if (method == null) {
                throw new ArgumentNullException(nameof(method));
            }
            if (resultCallback == null) {
                throw new ArgumentNullException(nameof(resultCallback));
            }
            if (!Enum.IsDefined(typeof(ShowDialog), showDialog)) {
                throw new ArgumentOutOfRangeException(nameof(showDialog), "Value should be defined in the ShowDialog enum.");
            }
            try {
                SetIsBusy();
                var result = await method.Invoke();
                ClearIsBusy();
                resultCallback(result);
            } catch (AggregateException ae) {
                await HandleAggregateException(ae, errorCallback, showDialog);
            } catch (Exception ex) {
                await HandleException(ex, errorCallback, showDialog);
            }
        }

        protected async Task InvokeMethodAsync(Func<Task> method, Action resultCallback = null, Action<Exception> errorCallback = null, ShowDialog showDialog = ShowDialog.Yes) {
            if (method == null) {
                throw new ArgumentNullException(nameof(method));
            }
            if (!Enum.IsDefined(typeof(ShowDialog), showDialog)) {
                throw new ArgumentOutOfRangeException(nameof(showDialog), "Value should be defined in the ShowDialog enum.");
            }

            try {
                SetIsBusy();
                await method.Invoke();
                ClearIsBusy();
                resultCallback?.Invoke();
            } catch (AggregateException ae) {
                await HandleAggregateException(ae, errorCallback, showDialog);
            } catch (Exception ex) {
                await HandleException(ex, errorCallback, showDialog);
            }
        }

    }
}
