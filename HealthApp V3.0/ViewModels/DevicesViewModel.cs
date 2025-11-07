﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthApp_V3._0.Models;
using HealthApp_V3._0.Services;

namespace HealthApp_V3._0.ViewModels;

public partial class DevicesViewModel : ObservableObject
{
    private readonly IWearableDeviceService _deviceService;
    private readonly IHealthDataService _healthDataService;
    private readonly ILocalizationService _localizationService;

    [ObservableProperty]
    private ObservableCollection<WearableDevice> _availableDevices = new();

    [ObservableProperty]
    private ObservableCollection<WearableDevice> _connectedDevices = new();

    [ObservableProperty]
    private bool _isScanning;

    [ObservableProperty]
    private bool _isSyncing;

    public DevicesViewModel(
        IWearableDeviceService deviceService, 
        IHealthDataService healthDataService,
        ILocalizationService localizationService)
    {
        _deviceService = deviceService;
        _healthDataService = healthDataService;
        _localizationService = localizationService;
    }

    [RelayCommand]
    private async Task ScanDevicesAsync()
    {
        IsScanning = true;
        try
        {
            await _deviceService.ScanForDevicesAsync();
            var devices = await _deviceService.GetAvailableDevicesAsync();
            AvailableDevices.Clear();
            foreach (var device in devices)
            {
                AvailableDevices.Add(device);
            }
            await LoadConnectedDevicesAsync();
        }
        finally
        {
            IsScanning = false;
        }
    }

    [RelayCommand]
    private async Task ConnectDeviceAsync(string deviceId)
    {
        var success = await _deviceService.ConnectDeviceAsync(deviceId);
        if (success)
        {
            await LoadConnectedDevicesAsync();
        }
    }

    [RelayCommand]
    private async Task DisconnectDeviceAsync(string deviceId)
    {
        var success = await _deviceService.DisconnectDeviceAsync(deviceId);
        if (success)
        {
            await LoadConnectedDevicesAsync();
        }
    }

    [RelayCommand]
    private async Task SyncDeviceAsync(string deviceId)
    {
        IsSyncing = true;
        try
        {
            await _deviceService.SyncDeviceAsync(deviceId);
        }
        finally
        {
            IsSyncing = false;
        }
    }

    private async Task LoadConnectedDevicesAsync()
    {
        var devices = await _deviceService.GetConnectedDevicesAsync();
        ConnectedDevices.Clear();
        foreach (var device in devices)
        {
            ConnectedDevices.Add(device);
        }
    }

    public string GetLocalizedString(string key) => _localizationService.GetString(key);
}

