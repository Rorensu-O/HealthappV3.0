using System;

namespace HealthApp_V3._0.Models;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public double Height { get; set; } // in cm
    public double Weight { get; set; } // in kg
    public Gender Gender { get; set; }
    public string? ProfileImagePath { get; set; }
    public UserPreferences Preferences { get; set; } = new();
}

public enum Gender
{
    NotSpecified,
    Male,
    Female,
    Other
}

public class UserPreferences
{
    public int DailyStepGoal { get; set; } = 10000;
    public double DailyCalorieGoal { get; set; } = 2000;
    public int DailyActiveMinutesGoal { get; set; } = 30;
    public string PreferredLanguage { get; set; } = "en"; // "en" or "nl"
    public bool UseMetricSystem { get; set; } = true;
    public bool EnableNotifications { get; set; } = true;
}

