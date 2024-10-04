using SkiaSharp.Views.Maui;
using SkiaSharp;

namespace Plugin.Maui.MauiProgressView;

public enum CircleType
{
    Full,
    Arc
}

public partial class ProgressRingView : ContentView
{
    // Bindable property for CircleType
    public static readonly BindableProperty CircleTypeProperty =
        BindableProperty.Create(nameof(CircleType), typeof(CircleType), typeof(ProgressRingView), CircleType.Full, propertyChanged: OnProgressChanged);

    public CircleType CircleType
    {
        get => (CircleType)GetValue(CircleTypeProperty);
        set => SetValue(CircleTypeProperty, value);
    }

    // Bindable property for Progress
    public static readonly BindableProperty ProgressProperty =
        BindableProperty.Create(nameof(Progress), typeof(float), typeof(ProgressRingView), 0f, propertyChanged: OnProgressChanged);

    // Bindable property for Ring Color
    public static readonly BindableProperty RingColorProperty =
        BindableProperty.Create(nameof(RingColor), typeof(Color), typeof(ProgressRingView), Colors.Blue, propertyChanged: OnProgressChanged);

    // Bindable property for Thickness
    public static readonly BindableProperty ThicknessProperty =
        BindableProperty.Create(nameof(Thickness), typeof(float), typeof(ProgressRingView), 20f, propertyChanged: OnProgressChanged);

    // Bindable property for Thumb Visibility
    public static readonly BindableProperty IsThumbVisibleProperty =
        BindableProperty.Create(nameof(IsThumbVisible), typeof(bool), typeof(ProgressRingView), true, propertyChanged: OnProgressChanged);

    // Bindable property for Thumb Color
    public static readonly BindableProperty ThumbColorProperty =
        BindableProperty.Create(nameof(ThumbColor), typeof(Color), typeof(ProgressRingView), Colors.Red, propertyChanged: OnProgressChanged);

    // Bindable property for Thumb Radius
    public static readonly BindableProperty ThumbRadiusProperty =
        BindableProperty.Create(nameof(ThumbRadius), typeof(float), typeof(ProgressRingView), 10f, propertyChanged: OnProgressChanged);

    public float Progress
    {
        get => (float)GetValue(ProgressProperty);
        set => SetValue(ProgressProperty, value);
    }

    public Color RingColor
    {
        get => (Color)GetValue(RingColorProperty);
        set => SetValue(RingColorProperty, value);
    }

    public float Thickness
    {
        get => (float)GetValue(ThicknessProperty);
        set => SetValue(ThicknessProperty, value);
    }

    public bool IsThumbVisible
    {
        get => (bool)GetValue(IsThumbVisibleProperty);
        set => SetValue(IsThumbVisibleProperty, value);
    }

    public Color ThumbColor
    {
        get => (Color)GetValue(ThumbColorProperty);
        set => SetValue(ThumbColorProperty, value);
    }

    public float ThumbRadius
    {
        get => (float)GetValue(ThumbRadiusProperty);
        set => SetValue(ThumbRadiusProperty, value);
    }

    public ProgressRingView()
    {
        InitializeComponent();
    }

    private static void OnProgressChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ProgressRingView progressRing)
        {
            progressRing.canvasView.InvalidateSurface();
        }
    }

    private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        canvas.Clear();

        int width = e.Info.Width;
        int height = e.Info.Height;

        var radius = Math.Min(width, height) / 2 - Thickness;  // Adjust radius to account for stroke thickness
        var center = new SKPoint(width / 2, height / 2);  // Center of the ring

        // Draw Background Circle or Arc
        DrawBackground(canvas, center, radius);

        // Draw Progress Arc
        DrawProgress(canvas, center, radius);

        // Draw Thumb if Visible
        if (IsThumbVisible)
        {
            DrawThumb(canvas, center, radius);
        }
    }

    private void DrawBackground(SKCanvas canvas, SKPoint center, float radius)
    {
        var backgroundPaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = Thickness,
            Color = SKColors.LightGray,
            IsAntialias = true
        };

        if (CircleType == CircleType.Full)
        {
            canvas.DrawCircle(center, radius, backgroundPaint);
        }
        else if (CircleType == CircleType.Arc)
        {
            using (var path = new SKPath())
            {
                path.AddArc(new SKRect(center.X - radius, center.Y - radius, center.X + radius, center.Y + radius), 180, 180);
                canvas.DrawPath(path, backgroundPaint);
            }
        }
    }
    private void DrawProgress(SKCanvas canvas, SKPoint center, float radius)
    {
        // Calculate the sweep angle based on the circle type
        float sweepAngle;
        if (CircleType == CircleType.Full)
        {
            sweepAngle = 360 * Progress;  // Full circle from 0 to 360 degrees
        }
        else // Arc case
        {
            sweepAngle = 180 * Progress;
        }

        var progressPaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = Thickness,
            Color = RingColor.ToSKColor(),
            IsAntialias = true,
            StrokeCap = SKStrokeCap.Round
        };

        using (var path = new SKPath())
        {
            if (CircleType == CircleType.Full)
            {
                path.AddArc(new SKRect(center.X - radius, center.Y - radius, center.X + radius, center.Y + radius), -90, sweepAngle);
            }
            else if (CircleType == CircleType.Arc)
            {
                // For arc, start from 0 degrees (which is -90 in SkiaSharp coordinates)
                path.AddArc(new SKRect(center.X - radius, center.Y - radius, center.X + radius, center.Y + radius), 180, sweepAngle);
            }

            canvas.DrawPath(path, progressPaint);
        }
    }


    private void DrawThumb(SKCanvas canvas, SKPoint center, float radius)
    {
        var thumbPosition = GetThumbPosition(center, radius);

        var thumbPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = ThumbColor.ToSKColor(),
            IsAntialias = true
        };

        canvas.DrawCircle(thumbPosition, ThumbRadius, thumbPaint);
    }

    private SKPoint GetThumbPosition(SKPoint center, float radius)
    {
        // Calculate the sweep angle based on the circle type
        float sweepAngle = CircleType == CircleType.Full ? 360 * Progress : 180 * Progress;

        // Adjust the angle based on the circle type
        float adjustedAngle = CircleType == CircleType.Full
            ? sweepAngle - 90  // Adjust for starting angle at -90 degrees (right side of circle)
            : sweepAngle + 270 - 90;  // Adjust to start from 270 degrees for the arc

        // Convert angle to radians
        var angleInRadians = Math.PI * adjustedAngle / 180.0;

        // Calculate thumb position
        float x = center.X + radius * (float)Math.Cos(angleInRadians);
        float y = center.Y + radius * (float)Math.Sin(angleInRadians);

        return new SKPoint(x, y);
    }
}
