namespace Moo2U.Controls {
    using System;
    using Infragistics.XamarinForms;
    using Infragistics.XamarinForms.Controls.Gauges;
    using Xamarin.Forms;

    public class RoundProgressIndicator : XamRadialGauge {

        public const String DefaultRangeColor = "#eddcd4";

        public static readonly BindableProperty ProgressValueProperty = BindableProperty.Create(nameof(ProgressValue), typeof(Double), typeof(RoundProgressIndicator), 0d, BindingMode.TwoWay, null, OnProgressValueChanged);

        public Double ProgressValue {
            get { return (Double)GetValue(ProgressValueProperty); }
            set { SetValue(ProgressValueProperty, value); }
        }

        public static readonly BindableProperty ProgressBrushColorProperty = BindableProperty.Create(nameof(ProgressBrushColor), typeof(Color), typeof(RoundProgressIndicator), Color.Transparent);

        public Color ProgressBrushColor {
            get { return (Color)GetValue(ProgressBrushColorProperty); }
            set { SetValue(ProgressBrushColorProperty, value); }
        }


        public RoundProgressIndicator() {
            this.RadiusMultiplier = 1.2d;
            this.BackingBrush = new SolidColorBrush(Color.Transparent);
            this.BackingOutline = new SolidColorBrush(Color.Transparent);
            this.BackgroundColor = Color.Transparent;
            this.FontBrush = new SolidColorBrush(Color.Transparent);
            this.NeedleBrush = new SolidColorBrush(Color.Transparent);
            this.NeedleOutline = new SolidColorBrush(Color.Transparent);
            this.NeedlePivotBrush = new SolidColorBrush(Color.Transparent);
            this.NeedlePivotOutline = new SolidColorBrush(Color.Transparent);
            this.MinorTickBrush = new SolidColorBrush(Color.Transparent);
            this.ScaleEndAngle = 269.9d;
            this.ScaleEndExtent = 0.6d;
            this.ScaleStartAngle = 270d;
            this.TickBrush = new SolidColorBrush(Color.Transparent);
            Reset();
        }

        Color GetRangeBrushForValue(Double newProgressValue) {
            if(newProgressValue <= 25) {
                return Color.FromHex("#d02c16");
            }
            if(newProgressValue <= 50) {
                return Color.FromHex("#FB7421");
            }
            if(newProgressValue <= 75) {
                return Color.FromHex("#fba821");
            }
            if(newProgressValue <= 99) {
                return Color.FromHex("#BACB00");
            }
            return Color.FromHex("#009b6a");
        }

        static void OnProgressValueChanged(BindableObject bindable, Object oldValue, Object newValue) {
            var rgi = (RoundProgressIndicator)bindable;

            if(newValue == null) {
                rgi.Reset();
                return;
            }

            var newProgressValue = (Double)newValue;
            if(Math.Abs(newProgressValue) < Double.Epsilon) {
                rgi.Reset();
                return;
            }

            rgi.SetRangesAndRangeBrushes(newProgressValue);
        }

        void Reset() {
            this.ProgressBrushColor = Color.FromHex("#70504d");
            this.Ranges = new RadialGaugeRangeCollection { new RadialGaugeRange { StartValue = 0d, EndValue = 100d } };
            this.RangeBrushes = new BrushCollection { new SolidColorBrush(Color.FromHex(DefaultRangeColor)) };
        }

        void SetRangesAndRangeBrushes(Double newProgressValue) {
            this.ProgressBrushColor = GetRangeBrushForValue(newProgressValue);
            this.Ranges = new RadialGaugeRangeCollection { new RadialGaugeRange { StartValue = 0d, EndValue = newProgressValue }, new RadialGaugeRange { StartValue = newProgressValue, EndValue = 100d } };
            this.RangeBrushes = new BrushCollection { new SolidColorBrush(this.ProgressBrushColor), new SolidColorBrush(Color.FromHex(DefaultRangeColor)) };
        }

    }
}
