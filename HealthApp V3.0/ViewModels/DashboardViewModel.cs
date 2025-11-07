using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthApp_V3._0.Models;
using HealthApp_V3._0.Services;

namespace HealthApp_V3._0.ViewModels;

public partial class DashboardViewModel : ObservableObject
{
    private readonly IHealthDataService _healthDataService;
    private readonly ILocalizationService _localizationService;

    [ObservableProperty]
    private DailyHealthSummary _todaySummary = new();

    [ObservableProperty]
    private WeeklyHealthSummary _weeklySummary = new();

    [ObservableProperty]
    private bool _isLoading;

    public DashboardViewModel(
        IHealthDataService healthDataService,
        ILocalizationService localizationService)
    {
        _healthDataService = healthDataService;
        _localizationService = localizationService;
    }

    [RelayCommand]
    private async Task LoadData()
    {
        IsLoading = true;
        try
        {
            TodaySummary = await _healthDataService.GetDailySummaryAsync(DateTime.Today);
            WeeklySummary = await _healthDataService.GetWeeklySummaryAsync(DateTime.Today);
        }
        catch (Exception ex)
        {
            // Log error
            Console.WriteLine($"Error loading dashboard data: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    public string GetLocalizedString(string key) => _localizationService.GetString(key);
}

