using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace HealthApp_V3._0;

// Minimal test - no XAML, pure code
public class MinimalApp : Application
{
    public override void Initialize()
    {
        // Skip XAML loading - do everything in code
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var window = new Window
            {
                Title = "🎉 HEALTH APP TEST - Do you see this window?",
                Width = 600,
                Height = 400,
                Background = new SolidColorBrush(Color.Parse("#00D9A5")),
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Content = new TextBlock
                {
                    Text = "✅ SUCCESS!\n\nIf you see this window, Avalonia is working!\n\nThe Health App will load next.",
                    FontSize = 24,
                    Foreground = Brushes.White,
                    TextAlignment = Avalonia.Media.TextAlignment.Center,
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                    Margin = new Avalonia.Thickness(40)
                }
            };
            
            desktop.MainWindow = window;
        }

        base.OnFrameworkInitializationCompleted();
    }
}

