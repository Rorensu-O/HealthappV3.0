using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HealthApp_V3._0.Models;

namespace HealthApp_V3._0.Services;

public interface IWearableDeviceService
{
    Task<ObservableCollection<WearableDevice>> GetConnectedDevicesAsync();
    Task<ObservableCollection<WearableDevice>> GetAvailableDevicesAsync();
    Task<bool> ConnectDeviceAsync(string deviceId);
    Task<bool> DisconnectDeviceAsync(string deviceId);
    Task<bool> SyncDeviceAsync(string deviceId);
    Task ScanForDevicesAsync();
}

public class WearableDeviceService : IWearableDeviceService
{
    private readonly ObservableCollection<WearableDevice> _connectedDevices = new();
    private readonly ObservableCollection<WearableDevice> _availableDevices = new();

    public async Task<ObservableCollection<WearableDevice>> GetConnectedDevicesAsync()
    {
        // Simulate getting connected devices
        await Task.Delay(100);
        return _connectedDevices;
    }

    public async Task<ObservableCollection<WearableDevice>> GetAvailableDevicesAsync()
    {
        // Simulate getting available devices
        await Task.Delay(100);
        return _availableDevices;
    }

    public async Task<bool> ConnectDeviceAsync(string deviceId)
    {
        // Simulate connecting to a device
        await Task.Delay(500);
        
        var device = _availableDevices.FirstOrDefault(d => d.Id == deviceId);
        if (device != null)
        {
            device.IsConnected = true;
            device.LastSyncTime = DateTime.Now;
            _availableDevices.Remove(device);
            _connectedDevices.Add(device);
            return true;
        }
        return false;
    }

    public async Task<bool> DisconnectDeviceAsync(string deviceId)
    {
        // Simulate disconnecting from a device
        await Task.Delay(300);
        
        var device = _connectedDevices.FirstOrDefault(d => d.Id == deviceId);
        if (device != null)
        {
            device.IsConnected = false;
            _connectedDevices.Remove(device);
            _availableDevices.Add(device);
            return true;
        }
        return false;
    }

    public async Task<bool> SyncDeviceAsync(string deviceId)
    {
        // Simulate syncing device data
        await Task.Delay(1000);
        
        var device = _connectedDevices.FirstOrDefault(d => d.Id == deviceId);
        if (device != null)
        {
            device.LastSyncTime = DateTime.Now;
            return true;
        }
        return false;
    }

    public async Task ScanForDevicesAsync()
    {
        // Simulate scanning for devices
        await Task.Delay(2000);
        
        // Add some mock devices
        _availableDevices.Clear();
        _availableDevices.Add(new WearableDevice
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Smartwatch Pro",
            Type = DeviceType.Smartwatch,
            Manufacturer = "TechBrand",
            IsConnected = false,
            BatteryLevel = 85,
            FirmwareVersion = "2.1.5"
        });
        
        _availableDevices.Add(new WearableDevice
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Fitness Band X",
            Type = DeviceType.FitnessBand,
            Manufacturer = "FitTech",
            IsConnected = false,
            BatteryLevel = 70,
            FirmwareVersion = "1.8.2"
        });
    }
}

