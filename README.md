# Plugin.Maui.MauiProgressView

The `Plugin.Maui.MauiProgressView` is a .NET MAUI plugin that provides customizable progress bars for use in mobile applications. This component allows users to display progress in a visually appealing circular format, with additional features such as a thumb indicator and customizable colors.

# Inspired from dribbble 
_Refer [here](https://dribbble.com/shots/19500244-Attendance-App?utm_source=Clipboard_Shot&utm_campaign=agungmahendra15&utm_content=Attendance%20App&utm_medium=Social_Share&utm_source=Clipboard_Shot&utm_campaign=agungmahendra15&utm_content=Attendance%20App&utm_medium=Social_Share)_

<p align="center">
  <img src="https://raw.githubusercontent.com/samirgcofficial/Plugin.Maui.ProgressView/main/Images/ArcCircle.png" alt="MauiProgressView" width="45%" />
  <img src="https://raw.githubusercontent.com/samirgcofficial/Plugin.Maui.ProgressView/main/Images/FullCircle.png" alt="MauiProgressView" width="45%" />
</p>

## Features

- Display circular progress with full or partial ring options.
- Properties Supported as of V1 CircleType, Ring Color, Thumb Color,ThumbRadius, Thickness, and IsThumbVisible.

## Getting Started

* Available on NuGet: <http://www.nuget.org/packages/Plugin.Maui.MauiProgressView> [![NuGet](https://img.shields.io/nuget/v/Plugin.Maui.MauiProgressView.svg?label=NuGet)](https://www.nuget.org/packages/Plugin.Maui.MauiProgressView/)


### Installation

You can install the plugin via NuGet:

```sh
   dotnet add package Plugin.Maui.MauiProgressView --version 0.0.1 
```


## Dotnet MAUI Implementation 
```xml
 xmlns:progress="clr-namespace:Plugin.Maui.MauiProgressView;assembly=Plugin.Maui.MauiProgressView"
 ```

```xml
  <StackLayout HorizontalOptions="Center" VerticalOptions="Center">
        <progress:ProgressRingView
            CircleType="Full"
            HeightRequest="280"
            HorizontalOptions="Center"
            IsThumbVisible="True"
            Progress="{Binding Progress}"
            RingColor="#4e8fee"
            Thickness="80"
            ThumbColor="#2c5c8a"
            ThumbRadius="30"
            VerticalOptions="Center"
            WidthRequest="350" />
    </StackLayout>
```

## MauiProgram.cs using .UseSkiaSharp()

```csharp 
 public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }


```



