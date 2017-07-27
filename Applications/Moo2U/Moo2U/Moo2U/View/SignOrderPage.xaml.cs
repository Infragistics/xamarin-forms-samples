namespace Moo2U.View {
    using System;
    using System.IO;
    using System.Linq;
    using Acr.XamForms.SignaturePad;
    using Xamarin.Forms;

    public partial class SignOrderPage : ContentPage {

        SignOrderPageViewModel _signOrderPageViewModel;

        SignOrderPageViewModel SignOrderPageViewModel => _signOrderPageViewModel ?? (_signOrderPageViewModel = (SignOrderPageViewModel)this.BindingContext);

        public SignOrderPage() {
            InitializeComponent();
        }

        async void CompleteButton_OnClicked(Object sender, EventArgs e) {
            if (!SignaturePadView.GetDrawPoints().Any()) {
                await this.SignOrderPageViewModel.NoSignatureFound();
            } else {
                var result = ImageStreamToBytes(SignaturePadView.GetImage(ImageFormatType.Png));
                await this.SignOrderPageViewModel.SignatureComplete(result);
            }
        }

        Byte[] ImageStreamToBytes(Stream input) {
            using (var ms = new MemoryStream()) {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

    }
}
