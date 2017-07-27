using Moo2U.Controls;
using Moo2U.iOS.Renderers;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(RoundedBoxView), typeof(RoundedBoxViewRenderer))]

namespace Moo2U.iOS.Renderers {
    using System;
    using CoreGraphics;
    using UIKit;
    using Xamarin.Forms.Platform.iOS;

    public class RoundedBoxViewRenderer : BoxRenderer {

        public RoundedBoxViewRenderer() {
        }

        public override void Draw(CGRect rect) {
            var rbv = (Moo2U.Controls.RoundedBoxView)this.Element;
            using (var context = UIGraphics.GetCurrentContext()) {
                context.SetFillColor(rbv.Color.ToCGColor());
                context.SetStrokeColor(rbv.Stroke.ToCGColor());
                context.SetLineWidth((float)rbv.StrokeThickness);

                var rc = this.Bounds.Inset((Int32)rbv.StrokeThickness, (Int32)rbv.StrokeThickness);
                var radius = (nfloat)rbv.CornerRadiusX;
                radius = (nfloat)Math.Max(0, Math.Min(radius, Math.Max(rc.Height / 2, rc.Width / 2)));

                var path = CGPath.FromRoundedRect(rc, radius, radius);
                context.AddPath(path);
                context.DrawPath(CGPathDrawingMode.FillStroke);
            }
        }

    }
}
