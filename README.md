# Plugin.Maui.MauiProgressView

The `Plugin.Maui.MauiProgressView` is a .NET MAUI plugin that provides customizable progress bars for use in mobile applications. This component allows users to display progress in a visually appealing circular format, with additional features such as a thumb indicator and customizable colors.

# Inspired from dribbble 
_Refer [here](https://dribbble.com/shots/19500244-Attendance-App?utm_source=Clipboard_Shot&utm_campaign=agungmahendra15&utm_content=Attendance%20App&utm_medium=Social_Share&utm_source=Clipboard_Shot&utm_campaign=agungmahendra15&utm_content=Attendance%20App&utm_medium=Social_Share)_

## Progress Views

### Progress Ring View

The `Progress Ring View` offers two different types of circular progress displays:

| ![ArcCircle](https://raw.githubusercontent.com/samirgcofficial/Plugin.Maui.ProgressView/main/Images/ProgressRingViewArc.gif) | ![FullCircle](https://raw.githubusercontent.com/samirgcofficial/Plugin.Maui.ProgressView/main/Images/ProgressRingViewFullCircle.gif) |
|:--:|:--:|
| **Arc Circle** | **Full Circle** |

### Progress Bar View

The `Progress Bar View` supports multiple types of progress displays:

- **Bar**  

|![Bar](https://raw.githubusercontent.com/samirgcofficial/Plugin.Maui.ProgressView/main/Images/Progressbarviewbar.gif)|
|:--:|
|**Bar** |


### ☕️ Fuel the Development with a Coffee!

If you find this plugin helpful and want to support its continuous development, consider [buying me a coffee](https://www.buymeacoffee.com/samirgc)! Your support means a lot and helps keep the project alive and growing. Every cup of coffee boosts my productivity and fuels new features and improvements.

Thank you for your support! 🙌


## Getting Started

* Available on NuGet: <http://www.nuget.org/packages/Plugin.Maui.MauiProgressView> [![NuGet](https://img.shields.io/nuget/v/Plugin.Maui.MauiProgressView.svg?label=NuGet)](https://www.nuget.org/packages/Plugin.Maui.MauiProgressView/)


### Installation

You can install the plugin via NuGet:

```sh
   dotnet add package Plugin.Maui.MauiProgressView --version 0.0.3 
```


## Dotnet MAUI Implementation 
```xml
 xmlns:progress="clr-namespace:Plugin.Maui.MauiProgressView;assembly=Plugin.Maui.MauiProgressView"
 ```

```xml
  <VerticalStackLayout HorizontalOptions="Center" VerticalOptions="Center">
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
    
      <progress:ProgressBarView
                    BarHeight="50"
                    CornerRadius="25"
                    HeightRequest="120"
                    Maximum="1"
                    Minimum="0"
                    Outline="True"
                    OutlineColor="LightGray"
                    OutlineThickness="2"
                    Progress="{Binding Progress, Mode=TwoWay}"
                    ProgressColor="CornflowerBlue"
                    ProgressType="Bar" />
   </VerticalStackLayout>
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



