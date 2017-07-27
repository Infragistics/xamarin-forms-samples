using Moo2U.Controls;
using Moo2U.Droid.Renderers;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(RoundedBoxView), typeof(RoundedBoxViewRenderer))]

namespace Moo2U.Droid.Renderers {
    using System;
    using Android.Graphics;
    using Moo2U.Controls;
    using Xamarin.Forms.Platform.Android;

    // Learned a lot from this post: https://forums.xamarin.com/discussion/17792/video-on-making-custom-renderers
    public class RoundedBoxViewRenderer : BoxRenderer {

        public RoundedBoxViewRenderer() {
            // ReSharper disable once VirtualMemberCallInConstructor
            this.SetWillNotDraw(false);
        }

        public override void Draw(Canvas canvas) {
            var rbv = (RoundedBoxView)this.Element;

            using (var rc = new Rect()) {
                GetDrawingRect(rc);

                using (var interior = rc) {
                    interior.Inset((Int32)rbv.StrokeThickness, (Int32)rbv.StrokeThickness);

                    using (var p = new Paint {Color = rbv.Color.ToAndroid(), AntiAlias = true,}) {
                        canvas.DrawRoundRect(new RectF(interior), (float)rbv.CornerRadiusX, (float)rbv.CornerRadiusY, p);
                        p.Color = rbv.Stroke.ToAndroid();
                        p.StrokeWidth = (float)rbv.StrokeThickness;
                        p.SetStyle(Paint.Style.Stroke);
                        canvas.DrawRoundRect(new RectF(rc), (float)rbv.CornerRadiusX, (float)rbv.CornerRadiusY, p);
                    }
                }
            }
        }

    }
}
