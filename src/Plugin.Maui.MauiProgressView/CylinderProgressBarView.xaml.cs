using SkiaSharp.Views.Maui;
using SkiaSharp;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Plugin.Maui.MauiProgressView;

    public partial class CylinderProgressBarView : ContentView
    {
        // Bindable property for Orientation
        public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create(nameof(Orientation), typeof(TestTubeOrientation), typeof(CylinderProgressBarView), TestTubeOrientation.Vertical);

        public TestTubeOrientation Orientation
        {
            get => (TestTubeOrientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }


    // Bindable property for Minimum
    public static readonly BindableProperty MinimumProperty =
        BindableProperty.Create(nameof(Minimum), typeof(float), typeof(CylinderProgressBarView), 0f, propertyChanged: OnProgressChanged);

    // Bindable property for Maximum
    public static readonly BindableProperty MaximumProperty =
        BindableProperty.Create(nameof(Maximum), typeof(float), typeof(CylinderProgressBarView), 100f, propertyChanged: OnProgressChanged);

    public float Minimum
    {
        get => (float)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    public float Maximum
    {
        get => (float)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }




    // Bindable property for Progress
    public static readonly BindableProperty ProgressProperty =
            BindableProperty.Create(nameof(Progress), typeof(float), typeof(CylinderProgressBarView), 0f, propertyChanged: OnProgressChanged);

    public float Progress
    {
        get => (float)GetValue(ProgressProperty);
        set
        {
            float clamped = Math.Clamp(value, Minimum, Maximum);
            SetValue(ProgressProperty, clamped);
        }
    }


    // Bindable property for Tube Color
    public static readonly BindableProperty TubeColorProperty =
            BindableProperty.Create(nameof(TubeColor), typeof(Color), typeof(CylinderProgressBarView), Colors.LightBlue);

        public Color TubeColor
        {
            get => (Color)GetValue(TubeColorProperty);
            set => SetValue(TubeColorProperty, value);
        }

        // Bindable property for Fill Color
        public static readonly BindableProperty FillColorProperty =
            BindableProperty.Create(nameof(FillColor), typeof(Color), typeof(CylinderProgressBarView), Colors.Green);

        public Color FillColor
        {
            get => (Color)GetValue(FillColorProperty);
            set => SetValue(FillColorProperty, value);
        }

        // Bindable property for Bottom Rect Color
        public static readonly BindableProperty BottomRectColorProperty =
            BindableProperty.Create(nameof(BottomRectColor), typeof(Color), typeof(CylinderProgressBarView), Colors.Gray);

        public Color BottomRectColor
        {
            get => (Color)GetValue(BottomRectColorProperty);
            set => SetValue(BottomRectColorProperty, value);
        }

        // Bindable property for Bottom Rect Visibility
        public static readonly BindableProperty IsBottomRectVisibleProperty =
            BindableProperty.Create(nameof(IsBottomRectVisible), typeof(bool), typeof(CylinderProgressBarView), true);

        public bool IsBottomRectVisible
        {
            get => (bool)GetValue(IsBottomRectVisibleProperty);
            set => SetValue(IsBottomRectVisibleProperty, value);
        }

        // Bindable property for Tube Corner Radius
        public static readonly BindableProperty TubeCornerRadiusProperty =
            BindableProperty.Create(nameof(TubeCornerRadius), typeof(float), typeof(CylinderProgressBarView), 10f);

        public float TubeCornerRadius
        {
            get => (float)GetValue(TubeCornerRadiusProperty);
            set => SetValue(TubeCornerRadiusProperty, value);
        }

        private Timer _animationTimer;
        private float _waveOffset; // Offset for the wave animation

        public CylinderProgressBarView()
        {
            InitializeComponent();
            // Initialize the timer in the constructor
            _animationTimer = new Timer(50); // Set the timer interval (50 ms)
            _animationTimer.Elapsed += OnAnimationTick;
        }

        private void StartAnimation()
        {
            _animationTimer.Start(); // Start the animation timer
        }

        private void StopAnimation()
        {
            _animationTimer.Stop(); // Stop the animation timer
        }

        private void OnAnimationTick(object sender, ElapsedEventArgs e)
        {
            _waveOffset += 0.1f; // Change wave offset
            canvasView.InvalidateSurface(); // Redraw the surface
        }

        private static void OnProgressChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CylinderProgressBarView)bindable;
            control.canvasView.InvalidateSurface(); // Trigger a redraw when progress changes

            if (newValue is float newProgress && newProgress > 0f)
            {
                control.StartAnimation(); // Start the wave animation
            }
            else
            {
                control.StopAnimation(); // Stop the animation when progress is 0
            }
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.White); // Clear the canvas

            // Define the dimensions
            float width = e.Info.Width;
            float height = e.Info.Height;

            // Call the appropriate drawing method based on orientation
            if (Orientation == TestTubeOrientation.Vertical)
            {
                DrawVerticalTestTube(canvas, width, height);
            }
            else
            {
                DrawHorizontalTestTube(canvas, width, height);
            }
        }

        private void DrawVerticalTestTube(SKCanvas canvas, float width, float height)
        {
            float tubeWidth = width * 0.2f; // Width of the tube
            float tubeHeight = height * 0.8f; // Height of the tube
            float tubeX = (width - tubeWidth) / 2; // Center the tube
            float tubeY = (height - tubeHeight) / 2; // Center the tube

            // Draw the tube
            var tubeRect = new SKRect(tubeX, tubeY, tubeX + tubeWidth, tubeY + tubeHeight);
            canvas.DrawRoundRect(tubeRect, TubeCornerRadius, TubeCornerRadius, new SKPaint { Color = TubeColor.ToSKColor(), Style = SKPaintStyle.Fill });

            // Draw the filled portion based on progress
            float filledHeight = tubeHeight * Progress; // Use Progress directly (0 to 1)
            var fillRect = new SKRect(tubeX, tubeY + (tubeHeight - filledHeight), tubeX + tubeWidth, tubeY + tubeHeight);

            // Create a gradient paint for the fill color to simulate water with wave effect
            using (var paint = new SKPaint())
            {
                paint.Shader = SKShader.CreateLinearGradient(
                    new SKPoint(fillRect.Left, fillRect.Top),
                    new SKPoint(fillRect.Left, fillRect.Bottom),
                    new SKColor[] {
                        new SKColor((byte)(FillColor.Red * 255), (byte)(FillColor.Green * 255), (byte)(FillColor.Blue * 255), (byte)(0.1f * 255)), // Transparent top
                        new SKColor((byte)(FillColor.Red * 255), (byte)(FillColor.Green * 255), (byte)(FillColor.Blue * 255), 255)  // Opaque bottom
                    },
                    null,
                    SKShaderTileMode.Clamp);

                canvas.DrawRoundRect(fillRect, TubeCornerRadius, TubeCornerRadius, paint);
            }

            // Draw the wave effect above the fill
          //  DrawWaterWave(canvas, fillRect, filledHeight, tubeX, tubeWidth);

            // Draw the bottom of the test tube only if visible
            if (IsBottomRectVisible)
            {
                var bottomRect = new SKRect(tubeX, tubeY + tubeHeight, tubeX + tubeWidth, tubeY + tubeHeight + 20);
                canvas.DrawRect(bottomRect, new SKPaint { Color = BottomRectColor.ToSKColor(), Style = SKPaintStyle.Fill });
            }
        }

        private void DrawWaterWave(SKCanvas canvas, SKRect fillRect, float filledHeight, float tubeX, float tubeWidth)
        {
            // Create a paint for the wave with a different color (e.g., Colors.Blue)
            using (var paint = new SKPaint { Color = SKColors.Blue, IsAntialias = true }) // Change here to test wave color
            {
                float waveHeight = 15f; // Height of the wave
                float frequency = 0.1f; // Frequency of the wave

                // Draw the wave across the fill rectangle width
                for (int i = 0; i < fillRect.Width; i += 5) // Smoother wave
                {
                    // Calculate the wave's Y position based on the filled height
                    float waveY = fillRect.Top + filledHeight - waveHeight * (float)Math.Sin((i * frequency + _waveOffset));
                    canvas.DrawLine(fillRect.Left + i, waveY, fillRect.Left + i + 5, waveY, paint);
                }
            }
        }


        private void DrawHorizontalTestTube(SKCanvas canvas, float width, float height)
        {
            float tubeWidth = width * 0.8f; // Width of the tube
            float tubeHeight = height * 0.2f; // Height of the tube
            float tubeX = (width - tubeWidth) / 2; // Center the tube
            float tubeY = (height - tubeHeight) / 2; // Center the tube

            // Draw the tube
            var tubeRect = new SKRect(tubeX, tubeY, tubeX + tubeWidth, tubeY + tubeHeight);
            canvas.DrawRoundRect(tubeRect, TubeCornerRadius, TubeCornerRadius, new SKPaint { Color = TubeColor.ToSKColor(), Style = SKPaintStyle.Fill });

            // Draw the filled portion based on progress
            float filledWidth = tubeWidth * Progress; // Use Progress directly (0 to 1)
            var fillRect = new SKRect(tubeX, tubeY, tubeX + filledWidth, tubeY + tubeHeight);

            // Create a gradient paint for the fill color to simulate water with wave effect
            using (var paint = new SKPaint())
            {
                paint.Shader = SKShader.CreateLinearGradient(
                    new SKPoint(fillRect.Left, fillRect.Top),
                    new SKPoint(fillRect.Right, fillRect.Top),
                    new SKColor[] {
                        new SKColor((byte)(FillColor.Red * 255), (byte)(FillColor.Green * 255), (byte)(FillColor.Blue * 255), (byte)(0.1f * 255)), // Transparent left
                        new SKColor((byte)(FillColor.Red * 255), (byte)(FillColor.Green * 255), (byte)(FillColor.Blue * 255), 255)  // Opaque right
                    },
                    null,
                    SKShaderTileMode.Clamp);

                canvas.DrawRoundRect(fillRect, TubeCornerRadius, TubeCornerRadius, paint);
            }

            // Draw the wave effect above the fill
            DrawHorizontalWaterWave(canvas, fillRect, filledWidth, tubeY, tubeHeight);

            // Draw the bottom of the test tube only if visible
            if (IsBottomRectVisible)
            {
                var bottomRect = new SKRect(tubeX, tubeY + tubeHeight, tubeX + tubeWidth, tubeY + tubeHeight + 20);
                canvas.DrawRect(bottomRect, new SKPaint { Color = BottomRectColor.ToSKColor(), Style = SKPaintStyle.Fill });
            }
        }

        private void DrawHorizontalWaterWave(SKCanvas canvas, SKRect fillRect, float filledWidth, float tubeY, float tubeHeight)
        {
            // Create a paint for the wave with a different color (e.g., Colors.Blue)
            using (var paint = new SKPaint { Color = SKColors.Blue, IsAntialias = true }) // Change here to test wave color
            {
                float waveHeight = 5f; // Height of the wave
                float frequency = 0.1f; // Frequency of the wave

                // Draw the wave across the fill rectangle height
                for (int i = 0; i < fillRect.Width; i += 5) // Smoother wave
                {
                    // Calculate the wave's Y position based on the filled width
                    float waveY = fillRect.Top + waveHeight * (float)Math.Sin((i * frequency + _waveOffset));
                    canvas.DrawLine(fillRect.Left + i, waveY, fillRect.Left + i + 5, waveY, paint);
                }
            }
        }

    }

    public enum TestTubeOrientation
    {
        Vertical,
        Horizontal
    }

