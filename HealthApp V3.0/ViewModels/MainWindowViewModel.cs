using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthApp_V3._0.Services;

namespace HealthApp_V3._0.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly ILocalizationService _localizationService;

    [ObservableProperty]
    private object? _currentView;

    [ObservableProperty]
    private string _selectedTab = "Dashboard";

    public DashboardViewModel DashboardViewModel { get; }
    public DevicesViewModel DevicesViewModel { get; }
    public SharingViewModel SharingViewModel { get; }

    public MainWindowViewModel(
        DashboardViewModel dashboardViewModel,
        DevicesViewModel devicesViewModel,
        SharingViewModel sharingViewModel,
        ILocalizationService localizationService)
    {
        DashboardViewModel = dashboardViewModel;
        DevicesViewModel = devicesViewModel;
        SharingViewModel = sharingViewModel;
        _localizationService = localizationService;

        // Set initial view
        CurrentView = DashboardViewModel;
        
        // Load initial data
        _ = DashboardViewModel.LoadDataCommand.ExecuteAsync(null);
    }

    [RelayCommand]
    private void NavigateToDashboard()
    {
        SelectedTab = "Dashboard";
        CurrentView = DashboardViewModel;
        _ = DashboardViewModel.LoadDataCommand.ExecuteAsync(null);
    }

    [RelayCommand]
    private void NavigateToDevices()
    {
        SelectedTab = "Devices";
        CurrentView = DevicesViewModel;
    }

    [RelayCommand]
    private void NavigateToSharing()
    {
        SelectedTab = "Sharing";
        CurrentView = SharingViewModel;
        _ = SharingViewModel.LoadSharedAccessesCommand.ExecuteAsync(null);
    }

    [RelayCommand]
    private void ChangeLanguage(string languageCode)
    {
        _localizationService.SetLanguage(languageCode);
        // In a real app, you'd want to notify all views to update their text
        OnPropertyChanged(nameof(DashboardViewModel));
        OnPropertyChanged(nameof(DevicesViewModel));
        OnPropertyChanged(nameof(SharingViewModel));
    }

    public string GetLocalizedString(string key) => _localizationService.GetString(key);
}

