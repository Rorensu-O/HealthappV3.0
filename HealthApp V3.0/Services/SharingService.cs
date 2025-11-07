using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthApp_V3._0.Models;

namespace HealthApp_V3._0.Services;

public interface ISharingService
{
    Task<List<SharedAccess>> GetSharedAccessesAsync();
    Task<bool> CreateSharedAccessAsync(SharedAccess access);
    Task<bool> RevokeAccessAsync(Guid accessId);
    Task<bool> UpdateAccessAsync(SharedAccess access);
}

public class SharingService : ISharingService
{
    private readonly List<SharedAccess> _sharedAccesses = new();

    public async Task<List<SharedAccess>> GetSharedAccessesAsync()
    {
        await Task.Delay(100);
        return _sharedAccesses.Where(a => a.IsActive).ToList();
    }

    public async Task<bool> CreateSharedAccessAsync(SharedAccess access)
    {
        await Task.Delay(200);
        access.Id = Guid.NewGuid();
        access.SharedDate = DateTime.Now;
        _sharedAccesses.Add(access);
        return true;
    }

    public async Task<bool> RevokeAccessAsync(Guid accessId)
    {
        await Task.Delay(100);
        var access = _sharedAccesses.FirstOrDefault(a => a.Id == accessId);
        if (access != null)
        {
            access.IsActive = false;
            return true;
        }
        return false;
    }

    public async Task<bool> UpdateAccessAsync(SharedAccess access)
    {
        await Task.Delay(100);
        var existingAccess = _sharedAccesses.FirstOrDefault(a => a.Id == access.Id);
        if (existingAccess != null)
        {
            _sharedAccesses.Remove(existingAccess);
            _sharedAccesses.Add(access);
            return true;
        }
        return false;
    }
}

