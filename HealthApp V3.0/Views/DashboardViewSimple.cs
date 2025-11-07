using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia;

namespace HealthApp_V3._0.Views;

// Simple code-based dashboard view with dark mode support
public class DashboardViewSimple : UserControl
{
    private readonly bool _isDarkMode;
    
    // Light mode colors
    private readonly Color _lightBg = Colors.White;
    private readonly Color _lightCardBg = Color.Parse("#F9FAFB");
    private readonly Color _lightText = Color.Parse("#1F2937");
    private readonly Color _lightTextSecondary = Color.Parse("#6B7280");
    private readonly Color _lightTextCaption = Color.Parse("#9CA3AF");
    private readonly Color _lightBorder = Color.Parse("#E5E7EB");
    
    // Dark mode colors
    private readonly Color _darkBg = Color.Parse("#1E293B");
    private readonly Color _darkCardBg = Color.Parse("#0F172A");
    private readonly Color _darkText = Color.Parse("#F1F5F9");
    private readonly Color _darkTextSecondary = Color.Parse("#94A3B8");
    private readonly Color _darkTextCaption = Color.Parse("#64748B");
    private readonly Color _darkBorder = Color.Parse("#334155");
    
    // Accent color (same for both)
    private readonly Color _accentColor = Color.Parse("#00D9A5");

    public DashboardViewSimple(bool isDarkMode = false)
    {
        _isDarkMode = isDarkMode;
        BuildUI();
    }

    private void BuildUI()
    {
        var panel = new StackPanel
        {
            Margin = new Thickness(20),
            Spacing = 20
        };

        // Header
        var header = new TextBlock
        {
            Text = "📊 Dashboard",
            FontSize = 28,
            FontWeight = FontWeight.Bold,
            Foreground = new SolidColorBrush(_isDarkMode ? _darkText : _lightText)
        };
        panel.Children.Add(header);

        // Today section
        var todayCard = CreateCard("Today", "Your health metrics for today");
        panel.Children.Add(todayCard);

        // Metrics grid
        var metricsGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("*,*"),
            RowDefinitions = new RowDefinitions("Auto,Auto"),
            ColumnSpacing = 12,
            RowSpacing = 12
        };

        metricsGrid.Children.Add(CreateMetricCard("👣 Steps", "0", "Goal: 10,000", 0, 0));
        metricsGrid.Children.Add(CreateMetricCard("🔥 Calories", "0 kcal", "Goal: 2,000 kcal", 0, 1));
        metricsGrid.Children.Add(CreateMetricCard("❤️ Heart Rate", "0 bpm", "Avg today", 1, 0));
        metricsGrid.Children.Add(CreateMetricCard("🏃 Distance", "0.00 km", "Today's distance", 1, 1));

        panel.Children.Add(metricsGrid);

        // Weekly section
        var weekCard = CreateCard("This Week", "Steps: 0  |  Distance: 0 km  |  Calories: 0");
        panel.Children.Add(weekCard);

        // Theme indicator
        var themeIndicator = new TextBlock
        {
            Text = _isDarkMode ? "🌙 Dark Mode Active" : "☀️ Light Mode Active",
            FontSize = 12,
            Foreground = new SolidColorBrush(_isDarkMode ? _darkTextCaption : _lightTextCaption),
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 10, 0, 0)
        };
        panel.Children.Add(themeIndicator);

        Content = new ScrollViewer { Content = panel };
    }

    private Border CreateCard(string title, string subtitle)
    {
        var card = new Border
        {
            Background = new SolidColorBrush(_isDarkMode ? _darkBg : _lightBg),
            BorderBrush = new SolidColorBrush(_isDarkMode ? _darkBorder : _lightBorder),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(12),
            Padding = new Thickness(20)
        };

        var stack = new StackPanel { Spacing = 8 };
        
        stack.Children.Add(new TextBlock
        {
            Text = title,
            FontSize = 18,
            FontWeight = FontWeight.SemiBold,
            Foreground = new SolidColorBrush(_isDarkMode ? _darkText : _lightText)
        });

        stack.Children.Add(new TextBlock
        {
            Text = subtitle,
            FontSize = 14,
            Foreground = new SolidColorBrush(_isDarkMode ? _darkTextSecondary : _lightTextSecondary)
        });

        card.Child = stack;
        return card;
    }

    private Border CreateMetricCard(string label, string value, string subtitle, int row, int col)
    {
        var card = new Border
        {
            Background = new SolidColorBrush(_isDarkMode ? _darkCardBg : _lightCardBg),
            BorderBrush = new SolidColorBrush(_isDarkMode ? _darkBorder : _lightBorder),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(8),
            Padding = new Thickness(16)
        };

        var stack = new StackPanel { Spacing = 8 };

        stack.Children.Add(new TextBlock
        {
            Text = label,
            FontSize = 14,
            Foreground = new SolidColorBrush(_isDarkMode ? _darkTextSecondary : _lightTextSecondary)
        });

        stack.Children.Add(new TextBlock
        {
            Text = value,
            FontSize = 24,
            FontWeight = FontWeight.Bold,
            Foreground = new SolidColorBrush(_accentColor)
        });

        stack.Children.Add(new TextBlock
        {
            Text = subtitle,
            FontSize = 12,
            Foreground = new SolidColorBrush(_isDarkMode ? _darkTextCaption : _lightTextCaption)
        });

        card.Child = stack;
        Grid.SetRow(card, row);
        Grid.SetColumn(card, col);
        
        return card;
    }
}

