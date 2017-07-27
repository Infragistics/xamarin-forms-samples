namespace Moo2U.View {
    using System;
    using Xamarin.Forms;

    public partial class CrashPage : ContentPage {

        public String ExceptionMessage { get; } = String.Empty;

        public CrashPage() {
            InitializeComponent();
            this.BindingContext = this;
        }

        public CrashPage(String exceptionMessage) {
            InitializeComponent();
            this.BindingContext = this;
            this.ExceptionMessage = exceptionMessage;
        }

    }
}
