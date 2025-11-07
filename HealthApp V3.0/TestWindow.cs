using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;

namespace HealthApp_V3._0;

public class TestWindow : Window
{
    public TestWindow()
    {
        Title = "Health App - Test Window";
        Width = 800;
        Height = 600;
        Background = new SolidColorBrush(Color.Parse("#F5F7FA"));
        
        var panel = new StackPanel
        {
            Margin = new Thickness(40),
            Spacing = 20,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        
        var title = new TextBlock
        {
            Text = "🎉 Health & Fitness App",
            FontSize = 32,
            FontWeight = FontWeight.Bold,
            Foreground = new SolidColorBrush(Color.Parse("#00D9A5")),
            TextAlignment = TextAlignment.Center
        };
        
        var subtitle = new TextBlock
        {
            Text = "If you see this window, Avalonia UI is working!",
            FontSize = 18,
            Foreground = new SolidColorBrush(Color.Parse("#6B7280")),
            TextAlignment = TextAlignment.Center
        };
        
        var button = new Button
        {
            Content = "Open Main App",
            FontSize = 16,
            Padding = new Thickness(24, 12),
            HorizontalAlignment = HorizontalAlignment.Center,
            Background = new SolidColorBrush(Color.Parse("#00D9A5")),
            Foreground = Brushes.White
        };
        
        button.Click += OpenMainWindow;
        
        panel.Children.Add(title);
        panel.Children.Add(subtitle);
        panel.Children.Add(button);
        
        Content = panel;
    }
    
    private async void OpenMainWindow(object? sender, RoutedEventArgs e)
    {
        try
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        catch (System.Exception ex)
        {
            var error = new TextBlock
            {
                Text = $"Error opening main window:\n{ex.Message}",
                Foreground = Brushes.Red,
                Margin = new Thickness(20)
            };
            Content = error;
        }
    }
}

