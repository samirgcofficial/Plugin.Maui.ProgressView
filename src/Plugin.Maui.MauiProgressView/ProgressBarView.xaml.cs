using SkiaSharp;
using SkiaSharp.Views.Maui;

namespace Plugin.Maui.MauiProgressView;

public enum ProgressType
{
    Bar
}

public partial class ProgressBarView : ContentView
{
    // Existing Bindable properties...
    public static readonly BindableProperty ProgressProperty = BindableProperty.Create(
        nameof(Progress),
        typeof(double),
        typeof(ProgressBarView),
        0.0,
        propertyChanged: OnProgressChanged);

    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
        nameof(CornerRadius),
        typeof(float),
        typeof(ProgressBarView),
        0f);

    public static readonly BindableProperty BarHeightProperty = BindableProperty.Create(
        nameof(BarHeight),
        typeof(float),
        typeof(ProgressBarView),
        20f);

    public static readonly BindableProperty ProgressColorProperty = BindableProperty.Create(
        nameof(ProgressColor),
        typeof(Color),
        typeof(ProgressBarView),
        Colors.Blue); // Default value

    public static readonly BindableProperty ProgressTypeProperty = BindableProperty.Create(
        nameof(ProgressType),
        typeof(ProgressType),
        typeof(ProgressBarView),
        ProgressType.Bar); // Default value

    public static readonly BindableProperty SegmentCountProperty = BindableProperty.Create(
        nameof(SegmentCount),
        typeof(int),
        typeof(ProgressBarView),
        10); // Default segment count

    // New Bindable property for Outline visibility
    public static readonly BindableProperty OutlineProperty = BindableProperty.Create(
        nameof(Outline),
        typeof(bool),
        typeof(ProgressBarView),
        false); // Default to no outline

    // New Bindable property for Outline color
    public static readonly BindableProperty OutlineColorProperty = BindableProperty.Create(
        nameof(OutlineColor),
        typeof(Color),
        typeof(ProgressBarView),
        Colors.Black); // Default outline color

    public static readonly BindableProperty MinimumProperty = BindableProperty.Create(
    nameof(Minimum),
    typeof(double),
    typeof(ProgressBarView),
    0.0, // Default minimum value
    propertyChanged: OnProgressChanged);

    public static readonly BindableProperty MaximumProperty = BindableProperty.Create(
        nameof(Maximum),
        typeof(double),
        typeof(ProgressBarView),
        100.0, // Default maximum value
        propertyChanged: OnProgressChanged);

    // Properties for Minimum and Maximum
    public double Minimum
    {
        get => (double)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    public double Maximum
    {
        get => (double)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }


    // Properties...
    public double Progress
    {
        get => (double)GetValue(ProgressProperty);
        set => SetValue(ProgressProperty, value);
    }

    public float CornerRadius
    {
        get => (float)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public float BarHeight
    {
        get => (float)GetValue(BarHeightProperty);
        set => SetValue(BarHeightProperty, value);
    }

    public Color ProgressColor
    {
        get => (Color)GetValue(ProgressColorProperty);
        set => SetValue(ProgressColorProperty, value);
    }

    public ProgressType ProgressType
    {
        get => (ProgressType)GetValue(ProgressTypeProperty);
        set => SetValue(ProgressTypeProperty, value);
    }

    public int SegmentCount
    {
        get => (int)GetValue(SegmentCountProperty);
        set => SetValue(SegmentCountProperty, value);
    }

    public bool Outline
    {
        get => (bool)GetValue(OutlineProperty);
        set => SetValue(OutlineProperty, value);
    }

    public Color OutlineColor
    {
        get => (Color)GetValue(OutlineColorProperty);
        set => SetValue(OutlineColorProperty, value);
    }
    // New Bindable property for Outline thickness
    public static readonly BindableProperty OutlineThicknessProperty = BindableProperty.Create(
        nameof(OutlineThickness),
        typeof(float),
        typeof(ProgressBarView),
        2f); // Default outline thickness

    // Property for Outline thickness
    public float OutlineThickness
    {
        get => (float)GetValue(OutlineThicknessProperty);
        set => SetValue(OutlineThicknessProperty, value);
    }

    public ProgressBarView()
    {
        InitializeComponent();
    }

    private static void OnProgressChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (ProgressBarView)bindable;
        double progress = (double)newValue;

        // Clamp progress between Minimum and Maximum using Math.Clamp
        control.Progress = Math.Clamp(progress, control.Minimum, control.Maximum);

        control.canvasView.InvalidateSurface();
    }



    private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        canvas.Clear();

        float width = e.Info.Width;
        float height = e.Info.Height;

        switch (ProgressType)
        {
            case ProgressType.Bar:
                DrawProgressBar(canvas, width, height);
                break;
        }
    }
    private void DrawProgressBar(SKCanvas canvas, float width, float height)
    {
        // Normalize progress between Minimum and Maximum
        double normalizedProgress = (Progress - Minimum) / (Maximum - Minimum);

        // Draw the background bar
        using (var paint = new SKPaint())
        {
            paint.Color = SKColors.LightGray;
            paint.IsAntialias = true;
            paint.StrokeCap = SKStrokeCap.Round;
            paint.Style = SKPaintStyle.Fill;

            var rect = new SKRect(0, height / 2 - BarHeight / 2, width, height / 2 + BarHeight / 2);
            canvas.DrawRoundRect(rect, CornerRadius, CornerRadius, paint);
        }

        // Draw the progress bar
        using (var paint = new SKPaint())
        {
            paint.Color = ProgressColor.ToSKColor();
            paint.IsAntialias = true;
            paint.StrokeCap = SKStrokeCap.Round;
            paint.Style = SKPaintStyle.Fill;

            float progressWidth = (float)normalizedProgress * width;
            var progressRect = new SKRect(0, height / 2 - BarHeight / 2, progressWidth, height / 2 + BarHeight / 2);
            canvas.DrawRoundRect(progressRect, CornerRadius, CornerRadius, paint);
        }

        // Draw the outline if enabled
        if (Outline)
        {
            using (var outlinePaint = new SKPaint())
            {
                outlinePaint.Color = OutlineColor.ToSKColor();
                outlinePaint.IsAntialias = true;
                outlinePaint.Style = SKPaintStyle.Stroke;
                outlinePaint.StrokeWidth = OutlineThickness; // Use OutlineThickness

                var outlineRect = new SKRect(0, height / 2 - BarHeight / 2, width, height / 2 + BarHeight / 2);
                canvas.DrawRoundRect(outlineRect, CornerRadius, CornerRadius, outlinePaint);
            }
        }
    }


}