using System;

namespace HealthApp_V3._0.Models;

public class WearableDevice
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DeviceType Type { get; set; }
    public string Manufacturer { get; set; } = string.Empty;
    public bool IsConnected { get; set; }
    public DateTime? LastSyncTime { get; set; }
    public int BatteryLevel { get; set; }
    public string FirmwareVersion { get; set; } = string.Empty;
}

public enum DeviceType
{
    Smartwatch,
    FitnessBand,
    SmartScale,
    HeartRateMonitor,
    Other
}

