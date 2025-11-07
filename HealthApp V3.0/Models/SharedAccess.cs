using System;
using System.Collections.Generic;

namespace HealthApp_V3._0.Models;

public class SharedAccess
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string RecipientName { get; set; } = string.Empty;
    public string RecipientEmail { get; set; } = string.Empty;
    public RecipientType RecipientType { get; set; }
    public DateTime SharedDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public List<DataPermission> Permissions { get; set; } = new();
    public bool IsActive { get; set; } = true;
}

public enum RecipientType
{
    Doctor,
    Physiotherapist,
    PersonalTrainer,
    Other
}

public class DataPermission
{
    public HealthDataType DataType { get; set; }
    public bool CanView { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public enum HealthDataType
{
    Steps,
    HeartRate,
    Calories,
    Sleep,
    Distance,
    ActiveMinutes,
    Weight,
    All
}

