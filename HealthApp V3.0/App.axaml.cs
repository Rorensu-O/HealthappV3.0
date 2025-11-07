using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Themes.Fluent;

namespace HealthApp_V3._0;

public partial class App : Application
{
    public override void Initialize()
    {
        // Load styles manually instead of failing on XAML precompilation
        Styles.Add(new FluentTheme());
        
        // Try to load XAML, but don't crash if it fails
        try
        {
            AvaloniaXamlLoader.Load(this);
        }
        catch
        {
            // XAML precompilation failed - continue with manual setup
            // This is fine, we already added FluentTheme above
        }
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            try
            {
                // Use code-based window to avoid XAML precompilation issues
                desktop.MainWindow = new MainWindowCodeBased();
            }
            catch (System.Exception ex)
            {
                // If main window fails, show error details
                var errorWindow = new Avalonia.Controls.Window
                {
                    Title = "Health App - Initialization Error",
                    Width = 700,
                    Height = 500,
                    WindowStartupLocation = Avalonia.Controls.WindowStartupLocation.CenterScreen,
                    Content = new Avalonia.Controls.StackPanel
                    {
                        Margin = new Avalonia.Thickness(20),
                        Spacing = 10,
                        Children =
                        {
                            new Avalonia.Controls.TextBlock
                            {
                                Text = "❌ Health App - Initialization Error",
                                FontSize = 18,
                                FontWeight = Avalonia.Media.FontWeight.Bold,
                                Foreground = Avalonia.Media.Brushes.Red
                            },
                            new Avalonia.Controls.TextBlock
                            {
                                Text = $"Error: {ex.Message}",
                                FontSize = 14,
                                TextWrapping = Avalonia.Media.TextWrapping.Wrap
                            },
                            new Avalonia.Controls.TextBlock
                            {
                                Text = "Stack Trace:",
                                FontSize = 12,
                                FontWeight = Avalonia.Media.FontWeight.Bold,
                                Margin = new Avalonia.Thickness(0, 10, 0, 5)
                            },
                            new Avalonia.Controls.ScrollViewer
                            {
                                Height = 300,
                                Content = new Avalonia.Controls.TextBlock
                                {
                                    Text = ex.StackTrace ?? "No stack trace available",
                                    FontSize = 10,
                                    FontFamily = "Consolas",
                                    TextWrapping = Avalonia.Media.TextWrapping.Wrap
                                }
                            }
                        }
                    }
                };
                desktop.MainWindow = errorWindow;
            }
        }

        base.OnFrameworkInitializationCompleted();
    }
}

