using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthApp_V3._0.Models;
using HealthApp_V3._0.Services;

namespace HealthApp_V3._0.ViewModels;

public partial class DataSharingConsentViewModel : ObservableObject
{
    private readonly IPrivacyService _privacyService;
    private readonly ILocalizationService _localizationService;
    private readonly Guid _userId;
    private readonly Guid _sharedAccessId;

    [ObservableProperty]
    private string _consentText = string.Empty;

    [ObservableProperty]
    private string _recipientName = string.Empty;

    [ObservableProperty]
    private string _recipientEmail = string.Empty;

    [ObservableProperty]
    private string _recipientType = string.Empty;

    [ObservableProperty]
    private bool _isAccepted;

    [ObservableProperty]
    private bool _canProceed;

    public event EventHandler? ConsentGranted;
    public event EventHandler? ConsentDeclined;

    public DataSharingConsentViewModel(
        IPrivacyService privacyService,
        ILocalizationService localizationService,
        Guid userId,
        Guid sharedAccessId,
        string recipientName,
        string recipientEmail,
        RecipientType recipientType)
    {
        _privacyService = privacyService;
        _localizationService = localizationService;
        _userId = userId;
        _sharedAccessId = sharedAccessId;
        _recipientName = recipientName;
        _recipientEmail = recipientEmail;
        _recipientType = recipientType.ToString();
        
        LoadConsentText();
    }

    [RelayCommand]
    private void LoadConsentText()
    {
        ConsentText = _privacyService.GetDataSharingConsentText(_localizationService.CurrentLanguage);
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

        var success = await _privacyService.SaveDataSharingConsentAsync(
            _userId, 
            _sharedAccessId, 
            RecipientName, 
            RecipientEmail,
            Enum.Parse<RecipientType>(RecipientType),
            ConsentText);
            
        if (success)
        {
            ConsentGranted?.Invoke(this, EventArgs.Empty);
        }
    }

    [RelayCommand]
    private void Decline()
    {
        ConsentDeclined?.Invoke(this, EventArgs.Empty);
    }

    public string GetLocalizedString(string key) => _localizationService.GetString(key);
}

