namespace Moo2U.Controls {
    using System;
    using System.Windows.Input;
    using Prism.Commands;
    using Xamarin.Forms;

    public partial class ButtonBar : TemplatedView {

        ButtonBarState _buttonBarState = ButtonBarState.Week;
        DelegateCommand<ButtonBarState?> _labelTappedCommand;

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ButtonBar));
        public static readonly BindableProperty MonthTextProperty = BindableProperty.Create(nameof(MonthText), typeof(String), typeof(ButtonBar), "Month");
        public static readonly BindableProperty WeekTextProperty = BindableProperty.Create(nameof(WeekText), typeof(String), typeof(ButtonBar), "Week");
        public static readonly BindableProperty YearTextProperty = BindableProperty.Create(nameof(YearText), typeof(String), typeof(ButtonBar), "Year");
        public static readonly BindableProperty ActiveColorProperty = BindableProperty.Create(nameof(ActiveColor), typeof(Color), typeof(ButtonBar), Color.Transparent, BindingMode.Default, null, OnActiveColorChanged);
        public static readonly BindableProperty MonthColorProperty = BindableProperty.Create(nameof(MonthColor), typeof(Color), typeof(ButtonBar), Color.Transparent);
        public static readonly BindableProperty MonthTextColorProperty = BindableProperty.Create(nameof(MonthTextColor), typeof(Color), typeof(ButtonBar), Color.Transparent);
        public static readonly BindableProperty NotActiveColorProperty = BindableProperty.Create(nameof(NotActiveColor), typeof(Color), typeof(ButtonBar), Color.Transparent, BindingMode.Default, null, OnNotActiveColorChanged);
        public static readonly BindableProperty TodayColorProperty = BindableProperty.Create(nameof(TodayColor), typeof(Color), typeof(ButtonBar), Color.Transparent);
        public static readonly BindableProperty TodayTextColorProperty = BindableProperty.Create(nameof(TodayTextColor), typeof(Color), typeof(ButtonBar), Color.Transparent);
        public static readonly BindableProperty TodayTextProperty = BindableProperty.Create(nameof(TodayText), typeof(String), typeof(ButtonBar), "Today");
        public static readonly BindableProperty WeekColorProperty = BindableProperty.Create(nameof(WeekColor), typeof(Color), typeof(ButtonBar), Color.Transparent);
        public static readonly BindableProperty WeekTextColorProperty = BindableProperty.Create(nameof(WeekTextColor), typeof(Color), typeof(ButtonBar), Color.Transparent);
        public static readonly BindableProperty YearColorProperty = BindableProperty.Create(nameof(YearColor), typeof(Color), typeof(ButtonBar), Color.Transparent);
        public static readonly BindableProperty YearTextColorProperty = BindableProperty.Create(nameof(YearTextColor), typeof(Color), typeof(ButtonBar), Color.Transparent);

        public Color ActiveColor {
            get { return (Color)GetValue(ActiveColorProperty); }
            set { SetValue(ActiveColorProperty, value); }
        }

        public ICommand Command {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public DelegateCommand<ButtonBarState?> LabelTappedCommand => _labelTappedCommand ?? (_labelTappedCommand = new DelegateCommand<ButtonBarState?>(LabelTappedCommandExecute));

        public Color MonthColor {
            get { return (Color)GetValue(MonthColorProperty); }
            set { SetValue(MonthColorProperty, value); }
        }

        public String MonthText {
            get { return (String)GetValue(MonthTextProperty); }
            set { SetValue(MonthTextProperty, value); }
        }

        public Color MonthTextColor {
            get { return (Color)GetValue(MonthTextColorProperty); }
            set { SetValue(MonthTextColorProperty, value); }
        }

        public Color NotActiveColor {
            get { return (Color)GetValue(NotActiveColorProperty); }
            set { SetValue(NotActiveColorProperty, value); }
        }

        public Color TodayColor {
            get { return (Color)GetValue(TodayColorProperty); }
            set { SetValue(TodayColorProperty, value); }
        }

        public String TodayText {
            get { return (String)GetValue(TodayTextProperty); }
            set { SetValue(TodayTextProperty, value); }
        }

        public Color TodayTextColor {
            get { return (Color)GetValue(TodayTextColorProperty); }
            set { SetValue(TodayTextColorProperty, value); }
        }

        public Color WeekColor {
            get { return (Color)GetValue(WeekColorProperty); }
            set { SetValue(WeekColorProperty, value); }
        }

        public String WeekText {
            get { return (String)GetValue(WeekTextProperty); }
            set { SetValue(WeekTextProperty, value); }
        }

        public Color WeekTextColor {
            get { return (Color)GetValue(WeekTextColorProperty); }
            set { SetValue(WeekTextColorProperty, value); }
        }

        public Color YearColor {
            get { return (Color)GetValue(YearColorProperty); }
            set { SetValue(YearColorProperty, value); }
        }

        public String YearText {
            get { return (String)GetValue(YearTextProperty); }
            set { SetValue(YearTextProperty, value); }
        }

        public Color YearTextColor {
            get { return (Color)GetValue(YearTextColorProperty); }
            set { SetValue(YearTextColorProperty, value); }
        }

        public ButtonBar() {
            InitializeComponent();
        }

        void LabelTappedCommandExecute(ButtonBarState? buttonBarState) {
            if (!buttonBarState.HasValue) {
                return;
            }
            _buttonBarState = buttonBarState.Value;

            SetColors();

            if (this.Command == null) {
                return;
            }

            if (this.Command.CanExecute(_buttonBarState)) {
                this.Command.Execute(_buttonBarState);
            }
        }

        static void OnActiveColorChanged(BindableObject bindable, Object oldValue, Object newValue) {
            var pbb = (ButtonBar)bindable;
            pbb.SetColors();
        }

        static void OnNotActiveColorChanged(BindableObject bindable, object oldValue, object newValue) {
            var pbb = (ButtonBar)bindable;
            pbb.SetColors();
        }

        void SetColors() {
            this.TodayColor = this.NotActiveColor;
            this.TodayTextColor = this.ActiveColor;
            this.WeekColor = this.NotActiveColor;
            this.WeekTextColor = this.ActiveColor;
            this.MonthColor = this.NotActiveColor;
            this.MonthTextColor = this.ActiveColor;
            this.YearColor = this.NotActiveColor;
            this.YearTextColor = this.ActiveColor;

            switch (_buttonBarState) {
                case ButtonBarState.Today:
                    this.TodayColor = this.ActiveColor;
                    this.TodayTextColor = this.NotActiveColor;
                    break;
                case ButtonBarState.Week:
                    this.WeekColor = this.ActiveColor;
                    this.WeekTextColor = this.NotActiveColor;
                    break;
                case ButtonBarState.Month:
                    this.MonthColor = this.ActiveColor;
                    this.MonthTextColor = this.NotActiveColor;
                    break;

                case ButtonBarState.Year:
                    this.YearColor = this.ActiveColor;
                    this.YearTextColor = this.NotActiveColor;
                    break;
            }
        }

    }
}
