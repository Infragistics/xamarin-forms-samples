namespace Moo2U.View {
    using System;
    using Infragistics.XamarinForms.Controls.Gauges;
    using Xamarin.Forms;

    public partial class PerformancePage : ContentPage {

        public PerformancePage() {
            InitializeComponent();
        }

        void XamBulletGraph_OnFormatLabel(Object sender, FormatLinearGraphLabelEventArgs args) {
            if (args.Value < 10000) {
                args.Label = args.Value.ToString("N0");
            } else {
                args.Label = $"{args.Value / 1000:N0}k";
            }
        }

    }
}
