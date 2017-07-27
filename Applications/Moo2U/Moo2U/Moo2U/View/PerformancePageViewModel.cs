
#pragma warning disable 4014

namespace Moo2U.View {
    using System;
    using Moo2U.Controls;
    using Moo2U.Infrastructure;
    using Moo2U.Model;
    using Moo2U.Services;
    using Prism;
    using Prism.Commands;
    using Prism.Navigation;
    using Prism.Services;

    public class PerformancePageViewModel : FormViewModelBase, IActiveAware {

        DelegateCommand<ButtonBarState?> _buttonBarSelectedCommand;
        readonly IDeliveryHistoryService _deliveryHistoryService;
        Boolean _isActive;
        PerformanceAggregate _performanceAggregate;
        Period _period = Period.Week;

        public DelegateCommand<ButtonBarState?> ButtonBarSelectedCommand => _buttonBarSelectedCommand ?? (_buttonBarSelectedCommand = new DelegateCommand<ButtonBarState?>(ButtonBarSelectedCommandExecute));

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

        public Boolean IsNotPeriodToday => this.Period != Period.Today;

        public Boolean IsPeriodToday => this.Period == Period.Today;

        public PerformanceAggregate PerformanceAggregate {
            get { return _performanceAggregate; }
            set {
                _performanceAggregate = value;
                RaisePropertyChanged();
            }
        }

        public Period Period {
            get { return _period; }
            private set {
                _period = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(this.IsNotPeriodToday));
                RaisePropertyChanged(nameof(this.IsPeriodToday));
            }
        }

        public PerformancePageViewModel(IDeliveryHistoryService deliveryHistoryService, IDeviceService deviceService, INavigationService navigationService, IPageDialogService pageDialogService)
            : base(deviceService, navigationService, pageDialogService) {
            if (deliveryHistoryService == null) {
                throw new ArgumentNullException(nameof(deliveryHistoryService));
            }
            _deliveryHistoryService = deliveryHistoryService;
        }

        void ButtonBarSelectedCommandExecute(ButtonBarState? buttonBarState) {
            switch (buttonBarState) {
                case ButtonBarState.Today:
                    this.Period = Period.Today;
                    break;
                case ButtonBarState.Week:
                    this.Period = Period.Week;
                    break;
                case ButtonBarState.Month:
                    this.Period = Period.Month;
                    break;
                case ButtonBarState.Year:
                    this.Period = Period.Year;
                    break;
            }

            LoadData();
        }
#pragma warning disable 67

        public event EventHandler IsActiveChanged;

#pragma warning disable 67

        void LoadData() {
            this.PerformanceAggregate = null;
            InvokeMethodAsync(
                () => _deliveryHistoryService.GetPerformanceAggregateAsync(_period),
                r => this.PerformanceAggregate = r);
        }

    }
}
