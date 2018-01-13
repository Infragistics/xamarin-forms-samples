namespace Moo2U.Behaviors {
    using System;
    using System.Collections.Generic;
    using Infragistics.XamarinForms.Controls.Gauges;
    using Xamarin.Forms;

    public class BulletGraphRangeCreatorBehavior : BehaviorBase<XamBulletGraph> {

        public static readonly BindableProperty RangesDataProperty = BindableProperty.Create(nameof(RangesData), typeof(IEnumerable<BulletGraphRangeCreatorItem>), typeof(BulletGraphRangeCreatorBehavior), null, propertyChanged: OnRangesDataChanged);

        public IEnumerable<BulletGraphRangeCreatorItem> RangesData {
            get { return (IEnumerable<BulletGraphRangeCreatorItem>)GetValue(RangesDataProperty); }
            set { SetValue(RangesDataProperty, value); }
        }

        static void OnRangesDataChanged(BindableObject bindable, Object oldValue, Object newValue) {
            var b = (XamBulletGraph)bindable;
            b.Ranges.Clear();

            var rangesData = newValue as IEnumerable<BulletGraphRangeCreatorItem>;
            if (rangesData == null) {
                return;
            }
            foreach (var item in rangesData) {
                b.Ranges.Add(new LinearGraphRange {StartValue = item.StartValue, EndValue = item.EndValue, InnerEndExtent = item.InnerEndExtent, InnerStartExtent = item.InnerStartExtent, OuterEndExtent = item.OuterEndExtent, OuterStartExtent = item.OuterStartExtent});
            }
        }

    }
}
