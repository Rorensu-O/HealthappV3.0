﻿using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthApp_V3._0.Services;

namespace HealthApp_V3._0.ViewModels;

public partial class PrivacyConsentViewModel : ObservableObject
{
    private readonly IPrivacyService _privacyService;
    private readonly ILocalizationService _localizationService;
    private readonly Guid _userId;

    [ObservableProperty]
    private string _privacyPolicyText = string.Empty;

    [ObservableProperty]
    private bool _isAccepted;

    [ObservableProperty]
    private bool _canProceed;

    [ObservableProperty]
    private string _currentLanguage = "en";

    public event EventHandler? ConsentGranted;
    public event EventHandler? ConsentDeclined;

    public PrivacyConsentViewModel(
        IPrivacyService privacyService, 
        ILocalizationService localizationService,
        Guid userId)
    {
        _privacyService = privacyService;
        _localizationService = localizationService;
        _userId = userId;
        _currentLanguage = _localizationService.CurrentLanguage;
        
        LoadPrivacyPolicy();
    }

    [RelayCommand]
    private void LoadPrivacyPolicy()
    {
        PrivacyPolicyText = _privacyService.GetPrivacyPolicyText(CurrentLanguage);
    }

    [RelayCommand]
    private void ToggleAcceptance()
    {
        CanProceed = IsAccepted;
    }

    [RelayCommand]
    private async Task AcceptAndProceedAsync()
    {
        if (!IsAccepted)
            return;

        var success = await _privacyService.SavePrivacyConsentAsync(_userId, true, PrivacyPolicyText);
        if (success)
        {
            ConsentGranted?.Invoke(this, EventArgs.Empty);
        }
    }

    [RelayCommand]
    private async Task DeclineAsync()
    {
        await _privacyService.SavePrivacyConsentAsync(_userId, false, PrivacyPolicyText);
        ConsentDeclined?.Invoke(this, EventArgs.Empty);
    }

    [RelayCommand]
    private void ChangeLanguage(string languageCode)
    {
        _localizationService.SetLanguage(languageCode);
        CurrentLanguage = languageCode;
        LoadPrivacyPolicy();
    }

    public string GetLocalizedString(string key) => _localizationService.GetString(key);
}

