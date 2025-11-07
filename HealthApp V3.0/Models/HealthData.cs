using System;

namespace HealthApp_V3._0.Models;

public class HealthData
{
    public Guid Id { get; set; }
    public DateTime Timestamp { get; set; }
    public int Steps { get; set; }
    public double Distance { get; set; } // in kilometers
    public double Calories { get; set; }
    public int HeartRate { get; set; } // beats per minute
    public double ActiveMinutes { get; set; }
    public int? SleepDuration { get; set; } // in minutes
    public string? DeviceId { get; set; }
    public string? DeviceName { get; set; }
}

public class DailyHealthSummary
{
    public DateTime Date { get; set; }
    public int TotalSteps { get; set; }
    public double TotalDistance { get; set; }
    public double TotalCalories { get; set; }
    public int AvgHeartRate { get; set; }
    public int MaxHeartRate { get; set; }
    public int MinHeartRate { get; set; }
    public double TotalActiveMinutes { get; set; }
    public double ActiveMinutes { get; set; }
    public int? SleepDuration { get; set; }
    
    // Progress tracking
    public int StepGoal { get; set; } = 10000;
    public double CalorieGoal { get; set; } = 2000;
    public double StepProgress => StepGoal > 0 ? (TotalSteps / (double)StepGoal) * 100 : 0;
    public double CalorieProgress => CalorieGoal > 0 ? (TotalCalories / CalorieGoal) * 100 : 0;
}

public class WeeklyHealthSummary
{
    public DateTime WeekStartDate { get; set; }
    public DateTime WeekStart { get; set; }
    public DateTime WeekEnd { get; set; }
    public int TotalSteps { get; set; }
    public double TotalDistance { get; set; }
    public double TotalCalories { get; set; }
    public int AvgHeartRate { get; set; }
    public double TotalActiveMinutes { get; set; }
    public int DaysActive { get; set; }
}

