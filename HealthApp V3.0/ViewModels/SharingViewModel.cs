using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthApp_V3._0.Models;
using HealthApp_V3._0.Services;

namespace HealthApp_V3._0.ViewModels;

public partial class SharingViewModel : ObservableObject
{
    private readonly ISharingService _sharingService;
    private readonly IPrivacyService _privacyService;
    private readonly ILocalizationService _localizationService;
    private readonly Guid _userId;

    [ObservableProperty]
    private ObservableCollection<SharedAccess> _sharedAccesses = new();

    [ObservableProperty]
    private bool _isAddingRecipient;

    [ObservableProperty]
    private string _newRecipientName = string.Empty;

    [ObservableProperty]
    private string _newRecipientEmail = string.Empty;

    [ObservableProperty]
    private RecipientType _newRecipientType = RecipientType.Doctor;

    [ObservableProperty]
    private bool _isLoading;
    
    public event EventHandler<DataSharingConsentViewModel>? ShowConsentDialog;

    public SharingViewModel(
        ISharingService sharingService, 
        IPrivacyService privacyService,
        ILocalizationService localizationService,
        Guid userId)
    {
        _sharingService = sharingService;
        _privacyService = privacyService;
        _localizationService = localizationService;
        _userId = userId;
    }

    [RelayCommand]
    private async Task LoadSharedAccessesAsync()
    {
        IsLoading = true;
        try
        {
            var accesses = await _sharingService.GetSharedAccessesAsync();
            SharedAccesses.Clear();
            foreach (var access in accesses)
            {
                SharedAccesses.Add(access);
            }
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private void ShowAddRecipient()
    {
        IsAddingRecipient = true;
        NewRecipientName = string.Empty;
        NewRecipientEmail = string.Empty;
        NewRecipientType = RecipientType.Doctor;
    }

    [RelayCommand]
    private async Task SaveNewRecipientAsync()
    {
        // Generate shared access ID
        var sharedAccessId = Guid.NewGuid();
        
        // Create the consent view model
        var consentViewModel = new DataSharingConsentViewModel(
            _privacyService,
            _localizationService,
            _userId,
            sharedAccessId,
            NewRecipientName,
            NewRecipientEmail,
            NewRecipientType
        );
        
        // Create a TaskCompletionSource to wait for consent decision
        var tcs = new TaskCompletionSource<bool>();
        
        consentViewModel.ConsentGranted += (s, e) =>
        {
            tcs.TrySetResult(true);
        };
        
        consentViewModel.ConsentDeclined += (s, e) =>
        {
            tcs.TrySetResult(false);
        };
        
        // Raise event to show consent dialog (handled by view)
        ShowConsentDialog?.Invoke(this, consentViewModel);
        
        // Wait for user decision
        var consentGranted = await tcs.Task;
        
        if (consentGranted)
        {
            // Consent granted - proceed with sharing
            var access = new SharedAccess
            {
                Id = sharedAccessId,
                UserId = _userId,
                RecipientName = NewRecipientName,
                RecipientEmail = NewRecipientEmail,
                RecipientType = NewRecipientType,
                Permissions = new()
                {
                    new DataPermission { DataType = HealthDataType.All, CanView = true }
                }
            };

            var success = await _sharingService.CreateSharedAccessAsync(access);
            if (success)
            {
                IsAddingRecipient = false;
                await LoadSharedAccessesAsync();
            }
        }
        else
        {
            // Consent declined - do not proceed
            IsAddingRecipient = false;
        }
    }

    [RelayCommand]
    private void CancelAddRecipient()
    {
        IsAddingRecipient = false;
    }

    [RelayCommand]
    private async Task RevokeAccessAsync(Guid accessId)
    {
        // Withdraw data sharing consent
        await _privacyService.WithdrawDataSharingConsentAsync(accessId, "User revoked access");
        
        // Revoke the shared access
        var success = await _sharingService.RevokeAccessAsync(accessId);
        if (success)
        {
            await LoadSharedAccessesAsync();
        }
    }

    public string GetLocalizedString(string key) => _localizationService.GetString(key);
}

