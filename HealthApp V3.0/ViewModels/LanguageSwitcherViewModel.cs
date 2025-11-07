using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthApp_V3._0.Services;

namespace HealthApp_V3._0.ViewModels;

public partial class LanguageSwitcherViewModel : ObservableObject
{
    private readonly ILocalizationService _localizationService;

    [ObservableProperty]
    private string _currentLanguage;

    [ObservableProperty]
    private string _currentLanguageDisplayName;

    [ObservableProperty]
    private bool _isNetherlandsRegion;

    public event EventHandler? LanguageChanged;

    public LanguageSwitcherViewModel(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
        _currentLanguage = _localizationService.CurrentLanguage;
        _currentLanguageDisplayName = _currentLanguage == "nl" ? "Nederlands" : "English";
        
        UpdateDisplayName();
        CheckRegion();
    }

    [RelayCommand]
    private void SwitchLanguage(string languageCode)
    {
        // Use switch statement to handle language codes
        switch (languageCode?.ToLower())
        {
            case "nl":
            case "dutch":
            case "nederlands":
                _localizationService.SetLanguage("nl");
                CurrentLanguage = "nl";
                CurrentLanguageDisplayName = "Nederlands";
                break;

            case "en":
            case "english":
            case "engels":
                _localizationService.SetLanguage("en");
                CurrentLanguage = "en";
                CurrentLanguageDisplayName = "English";
                break;

            default:
                // Default to English if unknown
                _localizationService.SetLanguage("en");
                CurrentLanguage = "en";
                CurrentLanguageDisplayName = "English";
                break;
        }

        LanguageChanged?.Invoke(this, EventArgs.Empty);
    }

    [RelayCommand]
    private void ToggleLanguage()
    {
        // Toggle between English and Dutch
        switch (CurrentLanguage)
        {
            case "nl":
                SwitchLanguage("en");
                break;
            case "en":
                SwitchLanguage("nl");
                break;
            default:
                SwitchLanguage("en");
                break;
        }
    }

    private void UpdateDisplayName()
    {
        CurrentLanguageDisplayName = CurrentLanguage switch
        {
            "nl" => "Nederlands",
            "en" => "English",
            _ => "English"
        };
    }

    private void CheckRegion()
    {
        var detectedLanguage = _localizationService.DetectAndSetDefaultLanguage();
        IsNetherlandsRegion = detectedLanguage == "nl";
    }

    public string GetLocalizedString(string key) => _localizationService.GetString(key);
}

