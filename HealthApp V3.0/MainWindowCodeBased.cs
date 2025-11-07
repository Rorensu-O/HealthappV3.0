using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using HealthApp_V3._0.Services;
using HealthApp_V3._0.ViewModels;
using HealthApp_V3._0.Views;

namespace HealthApp_V3._0;

// Code-only MainWindow with Dark Mode support
public class MainWindowCodeBased : Window
{
    private readonly Guid _currentUserId = Guid.NewGuid();
    private ContentControl _contentArea = null!;
    private MainWindowViewModel _viewModel = null!;
    private Border _sidebar = null!;
    private bool _isDarkMode = true; // Dark mode is now default
    
    // Light mode colors
    private readonly Color _lightBg = Color.Parse("#F5F7FA");
    private readonly Color _lightCardBg = Colors.White;
    private readonly Color _lightText = Color.Parse("#1F2937");
    private readonly Color _lightTextSecondary = Color.Parse("#6B7280");
    private readonly Color _lightBorder = Color.Parse("#E5E7EB");
    
    // Dark mode colors
    private readonly Color _darkBg = Color.Parse("#0F172A");
    private readonly Color _darkCardBg = Color.Parse("#1E293B");
    private readonly Color _darkText = Color.Parse("#F1F5F9");
    private readonly Color _darkTextSecondary = Color.Parse("#94A3B8");
    private readonly Color _darkBorder = Color.Parse("#334155");
    
    // Accent color (same for both)
    private readonly Color _accentColor = Color.Parse("#00D9A5");

    public MainWindowCodeBased()
    {
        Title = "Health & Fitness";
        Width = 1200;
        Height = 700;
        MinWidth = 900;
        MinHeight = 600;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;

        InitializeServices();
        BuildUI();
        ApplyTheme();
    }

    private void InitializeServices()
    {
        try
        {
            var localizationService = new LocalizationService();
            var healthDataService = new HealthDataService();
            var deviceService = new WearableDeviceService();
            var sharingService = new SharingService();

            var dashboardViewModel = new DashboardViewModel(healthDataService, localizationService);
            var devicesViewModel = new DevicesViewModel(deviceService, healthDataService, localizationService);
            var sharingViewModel = new SharingViewModel(sharingService, new PrivacyService(), localizationService, _currentUserId);

            _viewModel = new MainWindowViewModel(
                dashboardViewModel,
                devicesViewModel,
                sharingViewModel,
                localizationService
            );
        }
        catch (Exception ex)
        {
            Content = new TextBlock
            {
                Text = $"Service Initialization Error:\n\n{ex.Message}",
                Margin = new Thickness(20),
                TextWrapping = TextWrapping.Wrap,
                Foreground = Brushes.Red
            };
        }
    }

    private void BuildUI()
    {
        if (_viewModel == null) return;

        var mainGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("260,*")
        };

        // Sidebar
        _sidebar = CreateSidebar();
        Grid.SetColumn(_sidebar, 0);
        mainGrid.Children.Add(_sidebar);

        // Main content area
        _contentArea = new ContentControl
        {
            Content = new DashboardViewSimple(_isDarkMode),
            Margin = new Thickness(20)
        };
        Grid.SetColumn(_contentArea, 1);
        mainGrid.Children.Add(_contentArea);

        Content = mainGrid;
        DataContext = _viewModel;
    }

    private Border CreateSidebar()
    {
        var sidebar = new Border
        {
            BorderThickness = new Thickness(0, 0, 1, 0)
        };

        var stackPanel = new StackPanel
        {
            Spacing = 8,
            Margin = new Thickness(0, 24, 0, 0)
        };

        // App Title
        var titleBlock = new TextBlock
        {
            Text = "💚 Health & Fitness",
            FontSize = 20,
            FontWeight = FontWeight.Bold,
            Foreground = new SolidColorBrush(_accentColor),
            Margin = new Thickness(24, 0, 24, 24),
            TextAlignment = TextAlignment.Center
        };
        stackPanel.Children.Add(titleBlock);

        // Navigation Buttons
        stackPanel.Children.Add(CreateNavButton("📊 Dashboard", () => NavigateToDashboard()));
        stackPanel.Children.Add(CreateNavButton("📱 Devices", () => NavigateToDevices()));
        stackPanel.Children.Add(CreateNavButton("👥 Sharing", () => NavigateToSharing()));
        stackPanel.Children.Add(CreateNavButton("⚙️ Settings", () => NavigateToSettings()));

        // Spacer to push version to bottom
        stackPanel.Children.Add(new Border { Height = 20 });

        // Version info at bottom
        var versionText = new TextBlock
        {
            Text = "v3.0",
            FontSize = 11,
            Foreground = new SolidColorBrush(_isDarkMode ? _darkTextSecondary : _lightTextSecondary),
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 0, 0, 16)
        };
        stackPanel.Children.Add(versionText);

        sidebar.Child = stackPanel;
        return sidebar;
    }

    private void NavigateToDashboard()
    {
        if (_contentArea != null)
        {
            _contentArea.Content = new DashboardViewSimple(_isDarkMode);
        }
    }

    private void NavigateToDevices()
    {
        if (_contentArea != null)
        {
            var textBlock = new TextBlock
            {
                Text = "📱 Devices\n\nDevice management coming soon!\n\nConnect your wearable devices here.",
                FontSize = 24,
                Margin = new Thickness(40),
                TextAlignment = TextAlignment.Center,
                Foreground = new SolidColorBrush(_isDarkMode ? _darkText : _lightText)
            };
            _contentArea.Content = textBlock;
        }
    }

    private void NavigateToSharing()
    {
        if (_contentArea != null)
        {
            var textBlock = new TextBlock
            {
                Text = "👥 Data Sharing\n\nShare your health data with healthcare professionals.\n\nDoctors, Physiotherapists, and Personal Trainers.",
                FontSize = 24,
                Margin = new Thickness(40),
                TextAlignment = TextAlignment.Center,
                Foreground = new SolidColorBrush(_isDarkMode ? _darkText : _lightText)
            };
            _contentArea.Content = textBlock;
        }
    }

    private void NavigateToSettings()
    {
        if (_contentArea != null)
        {
            _contentArea.Content = new SettingsView(_isDarkMode, OnThemeChanged, OnLanguageChanged);
        }
    }

    private void OnThemeChanged(bool isDark)
    {
        _isDarkMode = isDark;
        ApplyTheme();
    }

    private void OnLanguageChanged(string languageCode)
    {
        // TODO: Implement language switching with LocalizationService
        // For now, just refresh the current view
        if (_contentArea?.Content is SettingsView)
        {
            NavigateToSettings(); // Refresh settings view
        }
    }

    private void ApplyTheme()
    {
        // Update window background
        Background = new SolidColorBrush(_isDarkMode ? _darkBg : _lightBg);

        // Update sidebar
        if (_sidebar != null)
        {
            _sidebar.Background = new SolidColorBrush(_isDarkMode ? _darkCardBg : Colors.White);
            _sidebar.BorderBrush = new SolidColorBrush(_isDarkMode ? _darkBorder : _lightBorder);
        }

        // Refresh content with new theme
        if (_contentArea?.Content is DashboardViewSimple)
        {
            NavigateToDashboard();
        }
        else if (_contentArea?.Content is SettingsView)
        {
            NavigateToSettings();
        }
    }

    private Button CreateNavButton(string text, Action onClick)
    {
        var button = new Button
        {
            Content = text,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            HorizontalContentAlignment = HorizontalAlignment.Left,
            Padding = new Thickness(24, 12),
            Margin = new Thickness(12, 0),
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            FontSize = 14,
            Foreground = new SolidColorBrush(_isDarkMode ? _darkText : _lightText)
        };

        button.Click += (s, e) => onClick();
        return button;
    }
}

