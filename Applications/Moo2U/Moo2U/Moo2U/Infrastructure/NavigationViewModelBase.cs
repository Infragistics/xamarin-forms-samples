namespace Moo2U.Infrastructure {
    using System;
    using System.Threading.Tasks;
    using Prism.Commands;
    using Prism.Navigation;
    using Prism.Services;

    /// <summary>
    /// Class NavigationAwareViewModelBase.
    /// </summary>
    /// <seealso cref="ViewModelBase" />
    /// <seealso cref="Prism.Navigation.INavigationAware" />
    /// <seealso cref="Prism.Navigation.IConfirmNavigation" />
    /// <seealso cref="Prism.Navigation.IDestructible" />
    public abstract class NavigationViewModelBase : ViewModelBase, INavigationAware, IConfirmNavigation, IDestructible {

        DelegateCommand<String> _navigateAbsoluteCommand;
        DelegateCommand<String> _navigateCommand;
        DelegateCommand<String> _navigateModalCommand;
        DelegateCommand<String> _navigateNonModalCommand;

        const String ButtonTextOK = "OK";
        const String CaptionError = "Error";
        const String ParameterKey = "Key";
        const String RootUriPrependText = "/";

        /// <summary>
        /// Gets the NavigateAbsoluteCommand.
        /// </summary>
        public DelegateCommand<String> NavigateAbsoluteCommand => _navigateAbsoluteCommand ?? (_navigateAbsoluteCommand = new DelegateCommand<String>(async param => await NavigateAbsoluteCommandExecute(param), CanNavigateAbsoluteCommandExecute));

        /// <summary>
        /// Gets the navigate command.
        /// </summary>
        /// <value>The navigate command.</value>
        public DelegateCommand<String> NavigateCommand => _navigateCommand ?? (_navigateCommand = new DelegateCommand<String>(async param => await NavigateCommandExecute(param), CanNavigateCommandExecute));

        /// <summary>
        /// Gets the navigate modal command.
        /// </summary>
        /// <value>The navigate modal command.</value>
        public DelegateCommand<String> NavigateModalCommand => _navigateModalCommand ?? (_navigateModalCommand = new DelegateCommand<String>(async param => await NavigateModalCommandExecute(param), CanNavigateModalCommandExecute));

        /// <summary>
        /// Gets the NavigateNonModalCommand.
        /// </summary>
        public DelegateCommand<String> NavigateNonModalCommand => _navigateNonModalCommand ?? (_navigateNonModalCommand = new DelegateCommand<String>(async param => await NavigateNonModalCommandExecute(param), CanNavigateNonModalCommandExecute));

        /// <summary>
        /// Gets the navigation service.
        /// </summary>
        /// <value>The navigation service.</value>
        protected INavigationService NavigationService { get; }

        /// <summary>
        /// Gets the page dialog service.
        /// </summary>
        /// <value>The page dialog service.</value>
        protected IPageDialogService PageDialogService { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationViewModelBase"/> class.
        /// </summary>
        /// <param name="deviceService">The device service.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="pageDialogService">The page dialog service.</param>
        /// <exception cref="System.ArgumentNullException">The navigationService or pageDialogService was null.</exception>
        protected NavigationViewModelBase(IDeviceService deviceService, INavigationService navigationService, IPageDialogService pageDialogService)
            : base(deviceService) {
            if (navigationService == null) {
                throw new ArgumentNullException(nameof(navigationService));
            }
            if (pageDialogService == null) {
                throw new ArgumentNullException(nameof(pageDialogService));
            }
            this.NavigationService = navigationService;
            this.PageDialogService = pageDialogService;
        }

        /// <summary>
        /// Determines whether this instance accepts being navigated away from.  This method is invoked by Prism before a navigation operation and is a member of IConfirmNavigation.
        /// </summary>
        /// <param name="parameters">The navigation parameters.</param>
        /// <returns><c>True</c> if navigation can continue, <c>False</c> if navigation is not allowed to continue</returns>
        public virtual Boolean CanNavigate(NavigationParameters parameters) {
            return true;
        }

        /// <summary>
        /// Determines whether this instance can execute the NavigateAbsoluteCommand.
        /// </summary>
        /// <param name="uri">The uri.</param>
        /// <returns><c>true</c> if this instance can execute NavigateAbsoluteCommand; otherwise, <c>false</c>.</returns>
        protected virtual Boolean CanNavigateAbsoluteCommandExecute(String uri) {
            return !String.IsNullOrEmpty(uri);
        }

        /// <summary>
        /// Determines whether this instance can execute the NavigateAbsoluteCommand.
        /// </summary>
        /// <param name="uri">The uri.</param>
        /// <returns><c>true</c> if this instance can execute NavigateAbsoluteCommand; otherwise, <c>false</c>.</returns>
        protected virtual Boolean CanNavigateCommandExecute(String uri) {
            return !String.IsNullOrEmpty(uri);
        }

        /// <summary>
        /// Determines whether this instance can execute the NavigateModalCommand.
        /// </summary>
        /// <param name="uri">The uri.</param>
        /// <returns><c>true</c> if this instance can execute NavigateModalCommand; otherwise, <c>false</c>.</returns>
        protected virtual Boolean CanNavigateModalCommandExecute(String uri) {
            return !String.IsNullOrEmpty(uri);
        }

        /// <summary>
        /// Determines whether this instance can execute the NavigateNonModalCommand.
        /// </summary>
        /// <param name="uri">The uri.</param>
        /// <returns><c>true</c> if this instance can execute NavigateNonModalCommand; otherwise, <c>false</c>.</returns>
        protected virtual Boolean CanNavigateNonModalCommandExecute(String uri) {
            return !String.IsNullOrEmpty(uri);
        }

        /// <summary>
        /// <p>Invoked by Prism Navigation when the instance is removed from the navigation stack.</p>
        /// <p>Deriving class can override and perform any required clean up.</p>
        /// </summary>
        public virtual void Destroy() {
        }

        /// <summary>
        /// Displays an alert dialog.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="buttonText">The button text.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentException">The title or message or buttonText was null or white space.</exception>
        protected async Task DisplayDialog(String title, String message, String buttonText = ButtonTextOK) {
            if (String.IsNullOrWhiteSpace(title)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(title));
            }
            if (String.IsNullOrWhiteSpace(message)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(message));
            }
            if (String.IsNullOrWhiteSpace(buttonText)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(buttonText));
            }
            try {
                await this.PageDialogService.DisplayAlertAsync(title, message, buttonText);
            } catch (Exception ex) {
                await HandleException(ex);
            }
        }

        /// <summary>
        /// Displays an alert dialog.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="acceptButtonText">The accept button text.</param>
        /// <param name="cancellationButtonText">The cancellation button text.</param>
        /// <returns>Task&lt;Boolean&gt;.</returns>
        /// <exception cref="System.ArgumentException">The title or message or acceptButtonText or cancellationButtonText was null or white space.</exception>
        protected async Task<Boolean> DisplayDialog(String title, String message, String acceptButtonText, String cancellationButtonText) {
            if (String.IsNullOrWhiteSpace(title)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(title));
            }
            if (String.IsNullOrWhiteSpace(message)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(message));
            }
            if (String.IsNullOrWhiteSpace(acceptButtonText)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(acceptButtonText));
            }
            if (String.IsNullOrWhiteSpace(cancellationButtonText)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(cancellationButtonText));
            }
            try {
                return await this.PageDialogService.DisplayAlertAsync(title, message, acceptButtonText, cancellationButtonText);
            } catch (Exception ex) {
                await HandleException(ex);
                return false;
            }
        }

        /// <summary>
        /// Navigates back by invoking NavigationService.GoBackAsync.
        /// </summary>
        /// <returns>Task.</returns>
        protected virtual async Task GoBack() {
            try {
                await this.NavigationService.GoBackAsync();
            } catch (Exception ex) {
                await HandleException(ex);
            }
        }

        /// <summary>
        /// Handles the exception by clearing the IsBusy flat, then displaying an alert with the base exception message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns>Task.</returns>
        protected virtual Task HandleException(Exception ex) {
            ClearIsBusy();
            var baseException = ex.GetBaseException();
            return this.PageDialogService.DisplayAlertAsync(CaptionError, baseException.Message, ButtonTextOK);
        }

        /// <summary>
        /// Navigates to the uri after creating a new navigation root. (Effectively replacing the Application MainPage.)
        /// </summary>
        /// <param name="uri">The uri text.</param>
        /// <returns>Task.</returns>
        protected virtual async Task NavigateAbsoluteCommandExecute(String uri) {
            try {
                if (CanNavigateAbsoluteCommandExecute(uri)) {
                    SetIsBusy();
                    await NavigateToNewRootUri(uri);
                }
            } catch (Exception ex) {
                await HandleException(ex);
            } finally {
                ClearIsBusy();
            }
        }

        /// <summary>
        /// Navigates to the uri.
        /// </summary>
        /// <param name="uri">The uri text.</param>
        /// <returns>Task.</returns>
        protected virtual async Task NavigateCommandExecute(String uri) {
            try {
                if (CanNavigateCommandExecute(uri)) {
                    SetIsBusy();
                    await NavigateToUri(uri);
                }
            } catch (Exception ex) {
                await HandleException(ex);
            } finally {
                ClearIsBusy();
            }
        }

        /// <summary>
        /// Navigates to the uri using a Modal navigation.
        /// </summary>
        /// <param name="uri">The uri text.</param>
        /// <returns>Task.</returns>
        protected virtual async Task NavigateModalCommandExecute(String uri) {
            try {
                if (CanNavigateCommandExecute(uri)) {
                    SetIsBusy();
                    await NavigateModalToUri(uri);
                }
            } catch (Exception ex) {
                await HandleException(ex);
            } finally {
                ClearIsBusy();
            }
        }

        /// <summary>
        /// Navigates to the uri using a Modal navigation. The parameter is wrapped in NavigationParameters and the parameter Key is set to Key.
        /// </summary>
        /// <param name="uri">The uri text.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentException">The uri cannot be null or white space.</exception>
        /// <exception cref="System.ArgumentNullException">The parameter was null.</exception>
        protected async Task NavigateModalToUri(String uri, Object parameter) {
            if (String.IsNullOrWhiteSpace(uri)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(uri));
            }
            if (parameter == null) {
                throw new ArgumentNullException(nameof(parameter));
            }

            try {
                var navigationParameters = new NavigationParameters();
                navigationParameters.Add(ParameterKey, parameter);
                await NavigateModalToUri(uri, navigationParameters);
            } catch (Exception ex) {
                await HandleException(ex);
            }
        }

        /// <summary>
        /// Navigates to the uri using a Modal navigation.
        /// </summary>
        /// <param name="uri">The uri text.</param>
        /// <param name="navigationParameters">The navigation parameters.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentException">The uri cannot be null or white space.</exception>
        /// <exception cref="System.ArgumentNullException">The navigationParameters were null.</exception>
        protected async Task NavigateModalToUri(String uri, NavigationParameters navigationParameters) {
            if (String.IsNullOrWhiteSpace(uri)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(uri));
            }
            if (navigationParameters == null) {
                throw new ArgumentNullException(nameof(navigationParameters));
            }
            try {
                await this.NavigationService.NavigateAsync(uri, navigationParameters, true);
            } catch (Exception ex) {
                await HandleException(ex);
            }
        }

        /// <summary>
        /// Navigates to the uri using a Modal navigation.
        /// </summary>
        /// <param name="uri">The uri text.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentException">The uri cannot be null or white space.</exception>
        protected async Task NavigateModalToUri(String uri) {
            if (String.IsNullOrWhiteSpace(uri)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(uri));
            }
            try {
                await this.NavigationService.NavigateAsync(uri, useModalNavigation: true);
            } catch (Exception ex) {
                await HandleException(ex);
            }
        }

        /// <summary>
        /// Navigates to the uri using Non-Modal navigation.
        /// </summary>
        /// <param name="uri">The uri text.</param>
        /// <returns>Task.</returns>
        protected virtual async Task NavigateNonModalCommandExecute(String uri) {
            try {
                if (CanNavigateCommandExecute(uri)) {
                    SetIsBusy();
                    await this.NavigationService.NavigateAsync(uri, null, false);
                }
            } catch (Exception ex) {
                await HandleException(ex);
            } finally {
                ClearIsBusy();
            }
        }

        /// <summary>
        /// Navigates to new root uri.
        /// </summary>
        /// <param name="uri">The uri text.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentException">The uri cannot be null or white space.</exception>
        protected async Task NavigateToNewRootUri(String uri) {
            if (String.IsNullOrWhiteSpace(uri)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(uri));
            }
            await NavigateToNewRootUriInternal(uri);
        }

        /// <summary>
        /// Navigates to new root uri. The parameter is wrapped in NavigationParameters and the parameter Key is set to Key.
        /// </summary>
        /// <param name="uri">The uri text.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentException">The uri cannot be null or white space.</exception>
        /// <exception cref="System.ArgumentNullException">The parameter was null.</exception>
        protected async Task NavigateToNewRootUri(String uri, Object parameter) {
            if (String.IsNullOrWhiteSpace(uri)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(uri));
            }
            if (parameter == null) {
                throw new ArgumentNullException(nameof(parameter));
            }

            var navigationParameters = new NavigationParameters();
            navigationParameters.Add(ParameterKey, parameter);
            await NavigateToNewRootUriInternal(uri, navigationParameters);
        }

        /// <summary>
        /// Navigates to the uri after creating a new navigation root. (Effectively replacing the Application MainPage.)
        /// </summary>
        /// <param name="uri">The uri text.</param>
        /// <param name="navigationParameters">The navigation parameters.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentException">The uri cannot be null or white space.</exception>
        /// <exception cref="System.ArgumentNullException">The navigationParameters were null.</exception>
        protected async Task NavigateToNewRootUri(String uri, NavigationParameters navigationParameters) {
            if (String.IsNullOrWhiteSpace(uri)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(uri));
            }
            if (navigationParameters == null) {
                throw new ArgumentNullException(nameof(navigationParameters));
            }
            await NavigateToNewRootUriInternal(uri, navigationParameters);
        }

        async Task NavigateToNewRootUriInternal(String uri, NavigationParameters navigationParameters = null) {
            if (String.IsNullOrWhiteSpace(uri)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(uri));
            }
            try {
                if (!uri.StartsWith(RootUriPrependText)) {
                    uri = String.Concat(RootUriPrependText, uri);
                }
                await this.NavigationService.NavigateAsync(new Uri(uri, UriKind.Absolute), navigationParameters, false);
            } catch (Exception ex) {
                await HandleException(ex);
            }
        }

        /// <summary>
        /// Navigates to uri.
        /// </summary>
        /// <param name="uri">The uri text.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentException">The uri cannot be null or white space.</exception>
        protected async Task NavigateToUri(String uri) {
            if (String.IsNullOrWhiteSpace(uri)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(uri));
            }
            try {
                await this.NavigationService.NavigateAsync(uri, useModalNavigation: false);
            } catch (Exception ex) {
                await HandleException(ex);
            }
        }

        /// <summary>
        /// Navigates to uri. The parameter is wrapped in NavigationParameters and the parameter Key is set to Key.
        /// </summary>
        /// <param name="uri">The uri text.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentException">The uri cannot be null or white space.</exception>
        /// <exception cref="System.ArgumentNullException">The parameter was null.</exception>
        protected async Task NavigateToUri(String uri, Object parameter) {
            if (String.IsNullOrWhiteSpace(uri)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(uri));
            }
            if (parameter == null) {
                throw new ArgumentNullException(nameof(parameter));
            }

            try {
                var navigationParameters = new NavigationParameters();
                navigationParameters.Add(ParameterKey, parameter);
                await NavigateToUri(uri, navigationParameters);
            } catch (Exception ex) {
                await HandleException(ex);
            }
        }

        /// <summary>
        /// Navigates to uri.
        /// </summary>
        /// <param name="uri">The uri text.</param>
        /// <param name="navigationParameters">The navigation parameters.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentException">The uri cannot be null or white space.</exception>
        /// <exception cref="System.ArgumentNullException">The navigationParameters are null.</exception>
        protected async Task NavigateToUri(String uri, NavigationParameters navigationParameters) {
            if (String.IsNullOrWhiteSpace(uri)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(uri));
            }
            if (navigationParameters == null) {
                throw new ArgumentNullException(nameof(navigationParameters));
            }
            try {
                await this.NavigationService.NavigateAsync(uri, navigationParameters, false);
            } catch (Exception ex) {
                await HandleException(ex);
            }
        }

        /// <summary>
        /// Invoked by Prism after navigating away from viewmodel's page.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public virtual void OnNavigatedFrom(NavigationParameters parameters) {
        }

        /// <summary>
        /// Invoked by Prism after navigating to the viewmodel's page.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public virtual void OnNavigatedTo(NavigationParameters parameters) {
        }

        /// <summary>
        /// Invoked by Prism before navigating to the viewmodel's page. Deriving classes can use this method to invoke async loading of data instead of waiting for the OnNavigatedTo method to be invoked.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public virtual void OnNavigatingTo(NavigationParameters parameters) {
        }

    }
}
