using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia;
using System;

namespace HealthApp_V3._0.Views;

public class SettingsView : UserControl
{
    private readonly bool _isDarkMode;
    private readonly Action<bool> _onThemeChanged;
    private readonly Action<string> _onLanguageChanged;
    
    // Light mode colors
    private readonly Color _lightBg = Colors.White;
    private readonly Color _lightText = Color.Parse("#1F2937");
    private readonly Color _lightTextSecondary = Color.Parse("#6B7280");
    private readonly Color _lightBorder = Color.Parse("#E5E7EB");
    
    // Dark mode colors
    private readonly Color _darkBg = Color.Parse("#1E293B");
    private readonly Color _darkText = Color.Parse("#F1F5F9");
    private readonly Color _darkTextSecondary = Color.Parse("#94A3B8");
    private readonly Color _darkBorder = Color.Parse("#334155");
    
    // Accent color
    private readonly Color _accentColor = Color.Parse("#00D9A5");

    public SettingsView(bool isDarkMode, Action<bool> onThemeChanged, Action<string> onLanguageChanged)
    {
        _isDarkMode = isDarkMode;
        _onThemeChanged = onThemeChanged;
        _onLanguageChanged = onLanguageChanged;
        BuildUI();
    }

    private void BuildUI()
    {
        var panel = new StackPanel
        {
            Margin = new Thickness(20),
            Spacing = 24
        };

        // Header
        var header = new TextBlock
        {
            Text = "⚙️ Settings",
            FontSize = 28,
            FontWeight = FontWeight.Bold,
            Foreground = new SolidColorBrush(_isDarkMode ? _darkText : _lightText)
        };
        panel.Children.Add(header);

        // Appearance Section
        var appearanceCard = CreateAppearanceSection();
        panel.Children.Add(appearanceCard);

        // Language Section
        var languageCard = CreateLanguageSection();
        panel.Children.Add(languageCard);

        // About Section
        var aboutCard = CreateAboutSection();
        panel.Children.Add(aboutCard);

        Content = new ScrollViewer { Content = panel };
    }

    private Border CreateAppearanceSection()
    {
        var card = new Border
        {
            Background = new SolidColorBrush(_isDarkMode ? _darkBg : _lightBg),
            BorderBrush = new SolidColorBrush(_isDarkMode ? _darkBorder : _lightBorder),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(12),
            Padding = new Thickness(20)
        };

        var stack = new StackPanel { Spacing = 16 };

        // Section title
        stack.Children.Add(new TextBlock
        {
            Text = "🎨 Appearance",
            FontSize = 18,
            FontWeight = FontWeight.SemiBold,
            Foreground = new SolidColorBrush(_isDarkMode ? _darkText : _lightText)
        });

        // Theme selector
        var themePanel = new StackPanel { Spacing = 12 };

        themePanel.Children.Add(new TextBlock
        {
            Text = "Theme",
            FontSize = 14,
            Foreground = new SolidColorBrush(_isDarkMode ? _darkTextSecondary : _lightTextSecondary)
        });

        // Theme buttons
        var themeButtons = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing = 12
        };

        var lightButton = CreateThemeButton("☀️ Light Mode", !_isDarkMode, () => _onThemeChanged(false));
        var darkButton = CreateThemeButton("🌙 Dark Mode", _isDarkMode, () => _onThemeChanged(true));

        themeButtons.Children.Add(lightButton);
        themeButtons.Children.Add(darkButton);

        themePanel.Children.Add(themeButtons);
        stack.Children.Add(themePanel);

        card.Child = stack;
        return card;
    }

    private Border CreateLanguageSection()
    {
        var card = new Border
        {
            Background = new SolidColorBrush(_isDarkMode ? _darkBg : _lightBg),
            BorderBrush = new SolidColorBrush(_isDarkMode ? _darkBorder : _lightBorder),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(12),
            Padding = new Thickness(20)
        };

        var stack = new StackPanel { Spacing = 16 };

        // Section title
        stack.Children.Add(new TextBlock
        {
            Text = "🌍 Language",
            FontSize = 18,
            FontWeight = FontWeight.SemiBold,
            Foreground = new SolidColorBrush(_isDarkMode ? _darkText : _lightText)
        });

        // Language selector
        var langPanel = new StackPanel { Spacing = 12 };

        langPanel.Children.Add(new TextBlock
        {
            Text = "App Language",
            FontSize = 14,
            Foreground = new SolidColorBrush(_isDarkMode ? _darkTextSecondary : _lightTextSecondary)
        });

        // Language buttons
        var langButtons = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing = 12
        };

        var englishButton = CreateLanguageButton("🇬🇧 English", "en", () => _onLanguageChanged("en"));
        var dutchButton = CreateLanguageButton("🇳🇱 Nederlands", "nl", () => _onLanguageChanged("nl"));

        langButtons.Children.Add(englishButton);
        langButtons.Children.Add(dutchButton);

        langPanel.Children.Add(langButtons);

        // Language description
        langPanel.Children.Add(new TextBlock
        {
            Text = "The app interface will be displayed in the selected language.",
            FontSize = 12,
            Foreground = new SolidColorBrush(_isDarkMode ? _darkTextSecondary : _lightTextSecondary),
            TextWrapping = TextWrapping.Wrap,
            Margin = new Thickness(0, 8, 0, 0)
        });

        stack.Children.Add(langPanel);

        card.Child = stack;
        return card;
    }

    private Border CreateAboutSection()
    {
        var card = new Border
        {
            Background = new SolidColorBrush(_isDarkMode ? _darkBg : _lightBg),
            BorderBrush = new SolidColorBrush(_isDarkMode ? _darkBorder : _lightBorder),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(12),
            Padding = new Thickness(20)
        };

        var stack = new StackPanel { Spacing = 12 };

        stack.Children.Add(new TextBlock
        {
            Text = "ℹ️ About",
            FontSize = 18,
            FontWeight = FontWeight.SemiBold,
            Foreground = new SolidColorBrush(_isDarkMode ? _darkText : _lightText)
        });

        stack.Children.Add(new TextBlock
        {
            Text = "Health & Fitness App",
            FontSize = 16,
            FontWeight = FontWeight.Bold,
            Foreground = new SolidColorBrush(_accentColor)
        });

        stack.Children.Add(new TextBlock
        {
            Text = "Version 3.0",
            FontSize = 14,
            Foreground = new SolidColorBrush(_isDarkMode ? _darkTextSecondary : _lightTextSecondary)
        });

        stack.Children.Add(new TextBlock
        {
            Text = "A modern health tracking application with GDPR compliance, multi-language support, and Samsung Health-inspired design.",
            FontSize = 12,
            Foreground = new SolidColorBrush(_isDarkMode ? _darkTextSecondary : _lightTextSecondary),
            TextWrapping = TextWrapping.Wrap,
            Margin = new Thickness(0, 8, 0, 0)
        });

        card.Child = stack;
        return card;
    }

    private Button CreateThemeButton(string text, bool isSelected, Action onClick)
    {
        var button = new Button
        {
            Content = text,
            Padding = new Thickness(20, 12),
            FontSize = 14,
            MinWidth = 150,
            Background = isSelected ? new SolidColorBrush(_accentColor) : Brushes.Transparent,
            Foreground = isSelected ? Brushes.White : new SolidColorBrush(_isDarkMode ? _darkText : _lightText),
            BorderBrush = new SolidColorBrush(isSelected ? _accentColor : (_isDarkMode ? _darkBorder : _lightBorder)),
            BorderThickness = new Thickness(2),
            CornerRadius = new CornerRadius(8)
        };

        button.Click += (s, e) => onClick();
        return button;
    }

    private Button CreateLanguageButton(string text, string langCode, Action onClick)
    {
        var isSelected = (langCode == "en"); // Default to English for now
        
        var button = new Button
        {
            Content = text,
            Padding = new Thickness(20, 12),
            FontSize = 14,
            MinWidth = 150,
            Background = isSelected ? new SolidColorBrush(_accentColor) : Brushes.Transparent,
            Foreground = isSelected ? Brushes.White : new SolidColorBrush(_isDarkMode ? _darkText : _lightText),
            BorderBrush = new SolidColorBrush(isSelected ? _accentColor : (_isDarkMode ? _darkBorder : _lightBorder)),
            BorderThickness = new Thickness(2),
            CornerRadius = new CornerRadius(8)
        };

        button.Click += (s, e) => onClick();
        return button;
    }
}

