# Plugin.Maui.MauiProgressView

The `Plugin.Maui.MauiProgressView` is a .NET MAUI plugin that provides customizable progress bars for use in mobile applications. This component allows users to display progress in a visually appealing circular format, with additional features such as a thumb indicator and customizable colors.

# Inspired from dribbble 
_Refer [here](https://dribbble.com/shots/19500244-Attendance-App?utm_source=Clipboard_Shot&utm_campaign=agungmahendra15&utm_content=Attendance%20App&utm_medium=Social_Share&utm_source=Clipboard_Shot&utm_campaign=agungmahendra15&utm_content=Attendance%20App&utm_medium=Social_Share)_

![MauiProgressView](https://raw.githubusercontent.com/samirgcofficial/Plugin.Maui.ProgressView/main/Images/ArcCircle.png)
![MauiProgressView](<p align="center">
  <img src="https://raw.githubusercontent.com/samirgcofficial/Plugin.Maui.ProgressView/main/Images/ArcCircle.png" alt="MauiProgressView" width="45%" />
  <img src="https://raw.githubusercontent.com/samirgcofficial/Plugin.Maui.ProgressView/main/Images/FullCircle.png" alt="MauiProgressView" width="45%" />
</p>)

<a href="https://www.buymeacoffee.com/samirgc"><img src="https://img.buymeacoffee.com/button-api/?text=1 coffee fuels this project!&emoji=&slug=samirgc&button_colour=FFDD00&font_colour=000000&font_family=Cookie&outline_colour=000000&coffee_colour=ffffff" /></a>

## Features

- Display circular progress with full or partial ring options.
- Properties Supported as of V1 CircleType, Ring Color, Thumb Color,ThumbRadius, Thickness, and IsThumbVisible.

## Getting Started

* Available on NuGet: <http://www.nuget.org/packages/Plugin.Maui.MauiProgressView> [![NuGet](https://img.shields.io/nuget/v/Plugin.Maui.MauiProgressView.svg?label=NuGet)](https://www.nuget.org/packages/Plugin.Maui.MauiProgressView/)


### Installation

You can install the plugin via NuGet:

```sh
   dotnet add package Plugin.Maui.MauiProgressView --version 0.0.2 
```


## Dotnet MAUI Implementation 
```xml
 xmlns:progress="clr-namespace:Plugin.Maui.MauiProgressView;assembly=Plugin.Maui.MauiProgressView"
 ```

```xml
  <StackLayout HorizontalOptions="Center" VerticalOptions="Center">
        <progress:ProgressRingView
                    CircleType="Arc"
                    HeightRequest="280"
                    HorizontalOptions="Center"
                    IsThumbVisible="True"
                    Maximum="1"
                    Minimum="0"
                    Progress="{Binding Progress}"
                    RingColor="#4e8fee"
                    Thickness="80"
                    ThumbColor="#2c5c8a"
                    ThumbRadius="30"
                    VerticalOptions="Center"
                    WidthRequest="350" />
    </StackLayout>
```

## Enable SkiaSharp in MauiProgram.cs
To use SkiaSharp in your MAUI project, add the .UseSkiaSharp() line in your MauiProgram.cs file. You may also need to install the SkiaSharp.Views.Maui.Controls package via NuGet.

Step 1: Add the Package
Add the following package reference to your project file (.csproj):

```xml
<PackageReference Include="SkiaSharp.Views.Maui.Controls" Version="2.88.8" />
```
Step 2: Update MauiProgram.cs
Insert .UseSkiaSharp() in the CreateMauiApp() method, as shown below:

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



