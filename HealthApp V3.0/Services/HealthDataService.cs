using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthApp_V3._0.Models;

namespace HealthApp_V3._0.Services;

public interface IHealthDataService
{
    Task<List<HealthData>> GetHealthDataAsync(DateTime startDate, DateTime endDate);
    Task<DailyHealthSummary> GetDailySummaryAsync(DateTime date);
    Task<WeeklyHealthSummary> GetWeeklySummaryAsync(DateTime weekStart);
    Task<bool> SaveHealthDataAsync(HealthData data);
    Task<List<DailyHealthSummary>> GetWeekDailySummariesAsync(DateTime weekStart);
}

public class HealthDataService : IHealthDataService
{
    private readonly List<HealthData> _healthDataStore = new();

    public HealthDataService()
    {
        // Initialize with mock data for demonstration
        InitializeMockData();
    }

    private void InitializeMockData()
    {
        var random = new Random();
        for (int i = 0; i < 7; i++)
        {
            var date = DateTime.Today.AddDays(-i);
            for (int j = 0; j < 24; j++)
            {
                _healthDataStore.Add(new HealthData
                {
                    Id = Guid.NewGuid(),
                    Timestamp = date.AddHours(j),
                    Steps = random.Next(100, 800),
                    Distance = random.NextDouble() * 0.5,
                    Calories = random.Next(50, 150),
                    HeartRate = random.Next(60, 100),
                    ActiveMinutes = random.Next(0, 10),
                    DeviceId = "device-001",
                    DeviceName = "Samsung Galaxy Watch 6"
                });
            }
        }
    }

    public async Task<List<HealthData>> GetHealthDataAsync(DateTime startDate, DateTime endDate)
    {
        await Task.Delay(100);
        return _healthDataStore
            .Where(d => d.Timestamp >= startDate && d.Timestamp <= endDate)
            .OrderBy(d => d.Timestamp)
            .ToList();
    }

    public async Task<DailyHealthSummary> GetDailySummaryAsync(DateTime date)
    {
        await Task.Delay(100);
        var dayData = _healthDataStore
            .Where(d => d.Timestamp.Date == date.Date)
            .ToList();

        if (!dayData.Any())
        {
            return new DailyHealthSummary { Date = date };
        }

        return new DailyHealthSummary
        {
            Date = date,
            TotalSteps = dayData.Sum(d => d.Steps),
            TotalDistance = dayData.Sum(d => d.Distance),
            TotalCalories = dayData.Sum(d => d.Calories),
            AvgHeartRate = (int)dayData.Average(d => d.HeartRate),
            MaxHeartRate = dayData.Max(d => d.HeartRate),
            MinHeartRate = dayData.Min(d => d.HeartRate),
            ActiveMinutes = dayData.Sum(d => d.ActiveMinutes),
            SleepDuration = dayData.FirstOrDefault()?.SleepDuration
        };
    }

    public async Task<WeeklyHealthSummary> GetWeeklySummaryAsync(DateTime weekStart)
    {
        await Task.Delay(100);
        var weekEnd = weekStart.AddDays(7);
        var weekData = _healthDataStore
            .Where(d => d.Timestamp >= weekStart && d.Timestamp < weekEnd)
            .ToList();

        if (!weekData.Any())
        {
            return new WeeklyHealthSummary { WeekStart = weekStart, WeekEnd = weekEnd };
        }

        var daysWithData = weekData.GroupBy(d => d.Timestamp.Date).Count();

        return new WeeklyHealthSummary
        {
            WeekStart = weekStart,
            WeekEnd = weekEnd,
            TotalSteps = weekData.Sum(d => d.Steps),
            TotalDistance = weekData.Sum(d => d.Distance),
            TotalCalories = weekData.Sum(d => d.Calories),
            AvgHeartRate = (int)weekData.Average(d => d.HeartRate),
            TotalActiveMinutes = weekData.Sum(d => d.ActiveMinutes),
            DaysActive = daysWithData
        };
    }

    public async Task<bool> SaveHealthDataAsync(HealthData data)
    {
        await Task.Delay(50);
        _healthDataStore.Add(data);
        return true;
    }

    public async Task<List<DailyHealthSummary>> GetWeekDailySummariesAsync(DateTime weekStart)
    {
        var summaries = new List<DailyHealthSummary>();
        for (int i = 0; i < 7; i++)
        {
            var date = weekStart.AddDays(i);
            summaries.Add(await GetDailySummaryAsync(date));
        }
        return summaries;
    }
}

