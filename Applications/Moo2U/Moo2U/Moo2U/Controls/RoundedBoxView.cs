namespace Moo2U.Controls {
    using System;
    using Xamarin.Forms;

    public class RoundedBoxView : BoxView {

        public static readonly BindableProperty CornerRadiusXProperty = BindableProperty.Create(nameof(CornerRadiusX), typeof(Double), typeof(RoundProgressIndicator), 0d);

        public static readonly BindableProperty CornerRadiusYProperty = BindableProperty.Create(nameof(CornerRadiusY), typeof(Double), typeof(RoundProgressIndicator), 0d);

        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(nameof(HasShadow), typeof(Boolean), typeof(RoundProgressIndicator), false);

        public static readonly BindableProperty StrokeProperty = BindableProperty.Create(nameof(Stroke), typeof(Color), typeof(RoundProgressIndicator), Color.Transparent);

        public static readonly BindableProperty StrokeThicknessProperty = BindableProperty.Create(nameof(StrokeThickness), typeof(Double), typeof(RoundProgressIndicator), 0d);

        public Double CornerRadiusX {
            get { return (Double)base.GetValue(CornerRadiusXProperty); }
            set { base.SetValue(CornerRadiusXProperty, value); }
        }

        public Double CornerRadiusY {
            get { return (Double)base.GetValue(CornerRadiusYProperty); }
            set { base.SetValue(CornerRadiusYProperty, value); }
        }

        public Boolean HasShadow {
            get { return (Boolean)GetValue(HasShadowProperty); }
            set { SetValue(HasShadowProperty, value); }
        }

        public Color Stroke {
            get { return (Color)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        public Double StrokeThickness {
            get { return (Double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        public RoundedBoxView() {
        }

    }
}
