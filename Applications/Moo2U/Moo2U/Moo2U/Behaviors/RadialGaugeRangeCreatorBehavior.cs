namespace Moo2U.Behaviors {
    using System;
    using Infragistics.XamarinForms.Controls.Gauges;
    using Xamarin.Forms;

    public class RadialGaugeRangeCreatorBehavior : BehaviorBase<XamRadialGauge> {

        public static readonly BindableProperty RangeValueProperty = BindableProperty.Create(nameof(RangeValue), typeof(Double), typeof(RadialGaugeRangeCreatorBehavior), 0d, BindingMode.TwoWay, null, OnRangeValueChanged);

        public Double RangeValue {
            get { return (Double)GetValue(RangeValueProperty); }
            set { SetValue(RangeValueProperty, value); }
        }

        static void OnRangeValueChanged(BindableObject bindable, Object oldValue, Object newValue) {
            var g = (XamRadialGauge)bindable;
            g.Ranges.Clear();
            g.Ranges.Add(new RadialGaugeRange {InnerEndExtent = .5d, InnerStartExtent = .5d, OuterEndExtent = .7d, OuterStartExtent = .7d, StartValue = 0d, EndValue = (Double)newValue});
            g.Ranges.Add(new RadialGaugeRange {InnerEndExtent = .5d, InnerStartExtent = .5d, OuterEndExtent = .7d, OuterStartExtent = .7d, StartValue = (Double)newValue, EndValue = g.MaximumValue});
        }

    }
}
