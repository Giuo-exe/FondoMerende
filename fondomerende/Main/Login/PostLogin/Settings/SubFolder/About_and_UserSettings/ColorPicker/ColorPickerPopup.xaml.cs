using System;
using System.Collections.Generic;
using ColorPicker.TouchTracking;
using fondomerende.Main.Login.PostLogin.AllSnack.Page;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.About_and_UserSettings.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.AddSnack.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditSnack.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.History.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.LogOut.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.Settaggio.View;
using fondomerende.Main.Utilities;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ColorPicker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ColorPickerPopup : PopupPage {

	    private static readonly List<ColorPick> ColorPicks;
	    private static bool _colorPicksInitialized;
	    private static ColorPick _pickedColor;

	    private const int ColorsPerRow = 5;
	    private const int CanvasPadding = 5;
	    private bool _colorChanged;

	    private readonly SKPaint _clrPickPaint = new SKPaint {
	        Style = SKPaintStyle.Fill,
	        IsAntialias = true
	    };

	    private readonly SKPaint _pickedClrPaint = new SKPaint {
	        Style = SKPaintStyle.Stroke,
            StrokeWidth = 5,
            IsAntialias = true,
	    };

        static ColorPickerPopup() {
	        ColorPicks = new List<ColorPick> {

	            new ColorPick("#25c5db"),
	            new ColorPick("#0098a6"),
	            new ColorPick("#0e47a1"),
	            new ColorPick("#1665c1"),
	            new ColorPick("#039be6"),

	            new ColorPick("#64b5f6"),
	            new ColorPick("#ff7000"),
	            new ColorPick("#ff9f00"),
                new ColorPick("#FFBF18"),
                new ColorPick("#ffb200"),

                new ColorPick("#cf9702"),
                new ColorPick("#84830b"),
                new ColorPick("#8c6e63"),
	            new ColorPick("#6e4c42"),
                new ColorPick("#9B212A"),

                new ColorPick("#d52f31"),
	            new ColorPick("#ff1643"),
	            new ColorPick("#f44236"),
                new ColorPick("#ec407a"),
	            new ColorPick("#ad1457"),

                new ColorPick("#6a1b9a"),
                new ColorPick("#904b93"),
                new ColorPick("#ab48bf"),
	            new ColorPick("#b968c7"),
                new ColorPick("#00695b"),

                new ColorPick("#00887a"),
	            new ColorPick("#4cb6ac"),
                new ColorPick("#0bb6b0"),
                new ColorPick("#307c32"),
                new ColorPick("#43a047"),


                new ColorPick("#64dd16"),
	            new ColorPick("#0886c7"),
	            new ColorPick("#5f7c8c"),
	            new ColorPick("#b1bec6"),
	            new ColorPick("#465a65"),

            };
	    }


        public ColorPickerPopup () {
			InitializeComponent ();
		}

	    public event EventHandler<ColorChangedEventArgs> ColorChanged;


        private async void OnClose(object sender, EventArgs e) {
	        await Navigation.PopPopupAsync();
        }

        private async void CanvasView_OnPaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {

	        var info = e.Info;
	        var surface = e.Surface;
	        var canvas = surface.Canvas;

	        canvas.Clear();

	        if (!_colorPicksInitialized) {
	            InitializeColorPicks(info.Width);
	        }

            // draw the colors
            foreach (var cp in ColorPicks)
            {
	            _clrPickPaint.Color = cp.Color.ToSKColor();
	            canvas.DrawCircle(cp.Position.X, cp.Position.Y, cp.Radius, _clrPickPaint);                
	        }

	        // check if there is a selected color
	        if (_pickedColor == null) { return; }

            // draw the highlight for the picked color
	        _pickedClrPaint.Color = _pickedColor.Color.ToSKColor();
            canvas.DrawCircle(_pickedColor.Position.X, _pickedColor.Position.Y, _pickedColor.Radius + 10, _pickedClrPaint);

	        if (_colorChanged) {
	            ColorChanged?.Invoke(this, new ColorChangedEventArgs(_pickedColor.Color));
	            _colorChanged = false;
                Preferences.Set("Colore", GetHexString(_pickedColor.Color));
                MessagingCenter.Send(new ChronologyViewCell()
                {

                }, "Refresh");

                MessagingCenter.Send(new FondoFondoMerendeViewCell()
                {

                }, "Refresh");

                MessagingCenter.Send(new EditUserViewCell()
                {

                }, "Refresh");

                MessagingCenter.Send(new EditSnackViewCell()
                {

                }, "Refresh");


                MessagingCenter.Send(new EditUserInfoViewCell()
                {

                }, "Refresh");

                MessagingCenter.Send(new ChangeColorViewCell()
                {

                }, "Refresh");

                MessagingCenter.Send(new AddSnackViewCell()
                {

                }, "Refresh");

                MessagingCenter.Send(new BuySnackViewCell()
                {

                }, "Refresh");

                MessagingCenter.Send(new LogoutViewCell()
                {

                }, "Refresh");
                MessagingCenter.Send(new AllSnacksPage()
                {

                }, "Animation");

                await Navigation.PopPopupAsync();
	        }
	    }
        public static string GetHexString(Color color)
        {
            var red = (int)(color.R * 255);
            var green = (int)(color.G * 255);
            var blue = (int)(color.B * 255);
            var alpha = (int)(color.A * 255);
            var hex = $"#{alpha:X2}{red:X2}{green:X2}{blue:X2}";

            return hex;
        }

        private static void InitializeColorPicks(int skImageWidth) {

	        var contentWidth = skImageWidth - (CanvasPadding * 2);
	        var colorWidth = contentWidth / ColorsPerRow;

	        var sp = new SKPoint((colorWidth / 2) + CanvasPadding, (colorWidth / 2) + CanvasPadding);
	        var col = 1;
	        var row = 1;
	        var radius = (colorWidth / 2) - 10;

	        foreach (var cp in ColorPicks) {

	            if (col > ColorsPerRow) {
	                col = 1;
	                row += 1;
	            }

	            // calc current position
	            var x = sp.X + (colorWidth * (col - 1));
	            var y = sp.Y + (colorWidth * (row - 1));

	            cp.Radius = radius;
	            cp.Position = new SKPoint(x, y);
	            col += 1;
	        }

	        _colorPicksInitialized = true;
	    }

	    private void OnTouchEffectAction(object sender, TouchActionEventArgs args) {

	        if (args.Type == TouchActionType.Released) {
	            // get the sk point pixel
	            var pnt = ConvertToPixel(args.Location);

	            // loop through all colors
	            foreach (var cp in ColorPicks) {

	                // check if selecting a color
	                if (cp.IsTouched(pnt)) {

	                    _colorChanged = true;
	                    _pickedColor = cp;
	                    break; // get out of loop
	                }
	            }

	            canvasView.InvalidateSurface();
	        }
	    }


	    private SKPoint ConvertToPixel(Point pt) {

	        return new SKPoint((float)(canvasView.CanvasSize.Width * pt.X / canvasView.Width),
	            (float)(canvasView.CanvasSize.Height * pt.Y / canvasView.Height));
	    }


    }
}