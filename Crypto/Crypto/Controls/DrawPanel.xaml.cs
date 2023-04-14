using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.CommunityToolkit.Behaviors;
using Xamarin.Forms;

namespace Crypto.Controls
{
    public partial class DrawPanel : Grid
    {
        private readonly Dictionary<long, SKPath> _inProgressPaths = new();
        private readonly List<SKPath> _completedPaths = new();

        private SKPaint _paint = new()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.White,
            StrokeWidth = 2,
            StrokeCap = SKStrokeCap.Round,
            StrokeJoin = SKStrokeJoin.Round,
        };

        public DrawPanel()
        {
            InitializeComponent();
        }

        #region -- Public properties --

        public static readonly BindableProperty BackColorProperty = BindableProperty.Create(
            propertyName: nameof(BackColor),
            returnType: typeof(Color),
            declaringType: typeof(DrawPanel),
            defaultValue: Color.FromHex("#34374C"),
            defaultBindingMode: BindingMode.TwoWay);

        public Color BackColor
        {
            get => (Color)GetValue(BackColorProperty);
            set => SetValue(BackColorProperty, value);
        }

        public static readonly BindableProperty DrawColorProperty = BindableProperty.Create(
            propertyName: nameof(DrawColor),
            returnType: typeof(Color),
            declaringType: typeof(DrawPanel),
            defaultValue: Color.FromHex("#34374C"),
            defaultBindingMode: BindingMode.TwoWay);

        public Color DrawColor
        {
            get => (Color)GetValue(DrawColorProperty);
            set => SetValue(DrawColorProperty, value);
        }

        public static readonly BindableProperty DataProperty = BindableProperty.Create(
            propertyName: nameof(Data),
            returnType: typeof(double[]),
            declaringType: typeof(DrawPanel),
            defaultBindingMode: BindingMode.TwoWay);

        public double[] Data
        {
            get => (double[])GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(Data))
            {
                canvasView.InvalidateSurface();
            }
        }

        #endregion

        #region -- Private helpers --

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            if (Data is not null)
            {
                SKCanvas canvas = args.Surface.Canvas;
                canvas.Clear(BackColor.ToSKColor());

                SKPaint paint = new SKPaint
                {
                    Color = DrawColor.ToSKColor(),
                    StrokeWidth = 2,
                    StrokeCap = SKStrokeCap.Round,
                    IsAntialias = true
                };

                var max = Data.Max();
                var min = Data.Min();                
                var count = Data.Count();
                var height = canvas.LocalClipBounds.Height;
                var width = canvas.LocalClipBounds.Width / (float)count;
                var delta = (float)height / (max - min);

                for ( var i = 1; i < count; i++ )
                {
                    float startX = (i - 1) * width;
                    float endX = i * width;
                    float startY = (float)((Data[i-1] - min) * delta);
                    float endY = (float)((Data[i] - min) * delta);

                    canvas.DrawLine(startX, startY, endX, endY, paint);
                }
            }
        }

        private SKPoint ConvertToPixel(Point pt)
        {
            return new(
                (float)(canvasView.CanvasSize.Width * pt.X / canvasView.Width),
                (float)(canvasView.CanvasSize.Height * pt.Y / canvasView.Height));
        }

        #endregion
    }
}