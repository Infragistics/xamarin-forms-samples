namespace Moo2U.Controls {
    using Xamarin.Forms;

    public class AlternatingRowColorListView : ListView {

        public static readonly BindableProperty AlternateRowColorProperty = BindableProperty.Create(nameof(AlternateRowColor), typeof(Color), typeof(AlternatingRowColorListView), Color.Transparent);

        public Color AlternateRowColor {
            get { return (Color)GetValue(AlternateRowColorProperty); }
            set { SetValue(AlternateRowColorProperty, value); }
        }

        public AlternatingRowColorListView(ListViewCachingStrategy strategy)
            : base(strategy) {
        }

        public AlternatingRowColorListView() {
        }

        protected override void SetupContent(Cell content, int index) {
            base.SetupContent(content, index);

            if (index % 2 == 0) {
                var currentViewCell = content as ViewCell;
                if (currentViewCell != null) {
                    currentViewCell.View.BackgroundColor = this.AlternateRowColor;
                }
            }
        }

    }
}
