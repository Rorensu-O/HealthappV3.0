using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using HealthApp_V3._0.Services;
using HealthApp_V3._0.ViewModels;
using HealthApp_V3._0.Views;

namespace HealthApp_V3._0;

public partial class MainWindow : Window
{
    private readonly Guid _currentUserId = Guid.NewGuid(); // In real app, this would be from authentication
    
    public MainWindow()
    {
        InitializeComponent();
        
        // Setup services (in a production app, use proper DI container like Microsoft.Extensions.DependencyInjection)
        var localizationService = new LocalizationService();
        var healthDataService = new HealthDataService();
        var deviceService = new WearableDeviceService();
        var sharingService = new SharingService();
        var privacyService = new PrivacyService();
        
        // Check for privacy consent on startup (async void is okay for event handlers/constructors)
        _ = CheckAndShowPrivacyConsentAsync(privacyService, localizationService);
        
        // Setup ViewModels
        var dashboardViewModel = new DashboardViewModel(healthDataService, localizationService);
        var devicesViewModel = new DevicesViewModel(deviceService, healthDataService, localizationService);
        var sharingViewModel = new SharingViewModel(sharingService, privacyService, localizationService, _currentUserId);
        
        // Subscribe to data sharing consent dialog event
        sharingViewModel.ShowConsentDialog += async (s, consentViewModel) =>
        {
            var consentWindow = new DataSharingConsentWindow
            {
                DataContext = consentViewModel
            };
            await consentWindow.ShowDialog(this);
        };
        
        // Setup MainWindowViewModel
        var mainViewModel = new MainWindowViewModel(
            dashboardViewModel,
            devicesViewModel,
            sharingViewModel,
            localizationService
        );
        
        DataContext = mainViewModel;
    }
    
    private async Task CheckAndShowPrivacyConsentAsync(IPrivacyService privacyService, ILocalizationService localizationService)
    {
        // Small delay to let the main window render first
        await Task.Delay(500);
        
        // Check if user has already accepted privacy policy
        var hasConsent = await privacyService.HasUserAcceptedPrivacyPolicyAsync(_currentUserId);
        
        if (!hasConsent)
        {
            // Show privacy consent dialog
            var consentWindow = new PrivacyConsentWindow
            {
                DataContext = new PrivacyConsentViewModel(privacyService, localizationService, _currentUserId)
            };
            
            var viewModel = (PrivacyConsentViewModel)consentWindow.DataContext;
            
            // Handle consent granted
            viewModel.ConsentGranted += (s, e) =>
            {
                consentWindow.Close();
            };
            
            // Handle consent declined - close app
            viewModel.ConsentDeclined += (s, e) =>
            {
                consentWindow.Close();
                this.Close(); // Close main window if consent is declined
            };
            
            // Show modal dialog - user must accept to continue
            await consentWindow.ShowDialog(this);
        }
    }
}

