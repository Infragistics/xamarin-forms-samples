namespace Moo2U.Infrastructure {
    using System;
    using Prism.Mvvm;
    using Prism.Services;

    /// <summary>
    /// Class ViewModelBase.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public abstract class ViewModelBase : BindableBase {

        Boolean _isBusy;

        /// <summary>
        /// Gets the device service.
        /// </summary>
        /// <value>The device service.</value>
        protected IDeviceService DeviceService { get; }

        /// <summary>
        /// Gets or sets the is busy.
        /// </summary>
        /// <value>The is busy.</value>
        public Boolean IsBusy {
            get { return _isBusy; }
            set {
                _isBusy = value;
                RaisePropertyChanged();
                OnIsBusyChanged();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        /// <param name="deviceService">The device service.</param>
        /// <exception cref="System.ArgumentNullException">deviceService</exception>
        protected ViewModelBase(IDeviceService deviceService) {
            if (deviceService == null) {
                throw new ArgumentNullException(nameof(deviceService));
            }
            this.DeviceService = deviceService;
        }

        /// <summary>
        /// Clears the is busy flag on the device main thread.
        /// </summary>
        protected void ClearIsBusy() {
            this.DeviceService.BeginInvokeOnMainThread(() => this.IsBusy = false);
        }

        /// <summary>
        /// Invoked when IsBusy changes.
        /// </summary>
        protected virtual void OnIsBusyChanged() {
        }

        /// <summary>
        /// Sets the is busy flag on the device main thread.
        /// </summary>
        protected void SetIsBusy() {
            this.DeviceService.BeginInvokeOnMainThread(() => this.IsBusy = true);
        }

    }
}
