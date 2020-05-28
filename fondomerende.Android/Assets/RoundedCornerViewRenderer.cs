using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using fondomerende.Droid.Assets;
using fondomerende.Main.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundedCornerView), typeof(RoundedCornerViewRenderer))]
namespace fondomerende.Droid.Assets
{
    public class RoundedCornerViewRenderer : ViewRenderer
    {
        public RoundedCornerViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
        }

        protected override bool DrawChild(Canvas canvas, global::Android.Views.View child, long drawingTime)
        {
            if (Element == null) return false;

            RoundedCornerView rcv = (RoundedCornerView)Element;
            SetClipChildren(true);
            rcv.Padding = new Thickness(0, 0, 0, 0);

            int radius = (int)(rcv.RoundedCornerRadius);
            // Check if make circle is set to true. If so, then we just use the width and   
            // height of the control to calculate the radius. RoundedCornerRadius will be ignored   
            // in this case.   
            if (rcv.MakeCircle)
            {
                radius = Math.Min(Width, Height) / 2;
            }

            // When we create a round rect, we will have to double the radius since it is not   
            // the same as creating a circle.   
            radius *= 2;

            try
            {
                var path = new Path();
                path.AddRoundRect(
                    new RectF(0, 0, Width, Height),
                    new float[] { radius, radius, radius, radius, radius, radius, radius, radius },
                    Path.Direction.Ccw
                    );
                canvas.Save();
                canvas.ClipPath(path);
                var result = base.DrawChild(canvas, child, drawingTime);
                canvas.Restore();

                // If a border is specified, we use the same path created above to stroke  
                // with the border color.
                if (rcv.BorderWidth > 0)
                {
                    var paint = new Paint
                    {
                        AntiAlias = true,
                        StrokeWidth = rcv.BorderWidth
                    };
                    paint.SetStyle(Paint.Style.Stroke);
                    paint.Color = rcv.BorderColor.ToAndroid();
                    canvas.DrawPath(path, paint);
                    paint.Dispose();
                }

                path.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return base.DrawChild(canvas, child, drawingTime);
        }
    }
}