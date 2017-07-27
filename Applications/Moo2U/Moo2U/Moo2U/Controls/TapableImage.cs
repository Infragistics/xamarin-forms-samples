namespace Moo2U.Controls {
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class TapableImage : Image {

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(Object), typeof(TapableImage));
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(TapableImage));
        public static readonly BindableProperty DimImageOnTapProperty = BindableProperty.Create(nameof(DimImageOnTap), typeof(Boolean), typeof(TapableImage), true);
        public static readonly BindableProperty TagProperty = BindableProperty.Create(nameof(Tag), typeof(String), typeof(TapableImage), String.Empty);

        public ICommand Command {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public Object CommandParameter {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public Boolean DimImageOnTap {
            get { return (Boolean)GetValue(DimImageOnTapProperty); }
            set { SetValue(DimImageOnTapProperty, value); }
        }

        public String Tag {
            get { return (String)GetValue(TagProperty); }
            set { SetValue(TagProperty, value); }
        }

        public TapableImage() {
            var gr = new TapGestureRecognizer();
            gr.Tapped += TapGestureRecognizer_Tapped;
            this.GestureRecognizers.Add(gr);
        }

        public event EventHandler OnTapped;

        async void TapGestureRecognizer_Tapped(Object sender, EventArgs e) {
            if (this.DimImageOnTap) {
                this.Opacity = .5;
                await Task.Delay(200);
                this.Opacity = 1;
            }

            this.OnTapped?.Invoke(this, EventArgs.Empty);

            if (this.Command == null) {
                return;
            }

            Object resolvedParameter = null;
            if (this.CommandParameter != null) {
                resolvedParameter = this.CommandParameter;
            }

            if (this.Command.CanExecute(resolvedParameter)) {
                this.Command.Execute(resolvedParameter);
            }
        }

    }
}
