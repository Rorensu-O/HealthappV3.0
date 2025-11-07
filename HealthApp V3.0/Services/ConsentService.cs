using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthApp_V3._0.Models;

namespace HealthApp_V3._0.Services;

public interface IConsentService
{
    Task<bool> HasUserAcceptedPrivacyPolicyAsync(Guid userId);
    Task<bool> SavePrivacyConsentAsync(Guid userId, bool accepted);
    Task<bool> HasUserAcceptedDataSharingConsentAsync(Guid userId, Guid recipientId);
    Task<bool> SaveDataSharingConsentAsync(Guid userId, Guid recipientId, bool accepted);
    Task<List<UserConsent>> GetUserConsentsAsync(Guid userId);
    Task<bool> RevokeConsentAsync(Guid consentId);
    Task<PrivacyPolicy> GetPrivacyPolicyAsync(string language);
    Task<DataSharingConsent> GetDataSharingConsentAsync(string language);
}

public class ConsentService : IConsentService
{
    private readonly List<UserConsent> _consents = new();
    private readonly PrivacyPolicy _privacyPolicy = new();
    private readonly DataSharingConsent _dataSharingConsent = new();

    public async Task<bool> HasUserAcceptedPrivacyPolicyAsync(Guid userId)
    {
        await Task.Delay(50);
        var consent = _consents
            .Where(c => c.UserId == userId && c.Type == ConsentType.PrivacyPolicy)
            .OrderByDescending(c => c.GrantedDate)
            .FirstOrDefault();
        
        return consent?.IsGranted == true && consent.RevokedDate == null;
    }

    public async Task<bool> SavePrivacyConsentAsync(Guid userId, bool accepted)
    {
        await Task.Delay(100);
        
        var consent = new UserConsent
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Type = ConsentType.PrivacyPolicy,
            IsGranted = accepted,
            GrantedDate = accepted ? DateTime.Now : null,
            Version = _privacyPolicy.Version,
            IpAddress = "127.0.0.1", // In real app, get actual IP
            UserAgent = "HealthApp/3.0"
        };
        
        _consents.Add(consent);
        return true;
    }

    public async Task<bool> HasUserAcceptedDataSharingConsentAsync(Guid userId, Guid recipientId)
    {
        await Task.Delay(50);
        var consent = _consents
            .Where(c => c.UserId == userId && c.Type == ConsentType.DataSharing)
            .OrderByDescending(c => c.GrantedDate)
            .FirstOrDefault();
        
        return consent?.IsGranted == true && consent.RevokedDate == null;
    }

    public async Task<bool> SaveDataSharingConsentAsync(Guid userId, Guid recipientId, bool accepted)
    {
        await Task.Delay(100);
        
        var consent = new UserConsent
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Type = ConsentType.DataSharing,
            IsGranted = accepted,
            GrantedDate = accepted ? DateTime.Now : null,
            Version = "1.0",
            IpAddress = "127.0.0.1",
            UserAgent = "HealthApp/3.0"
        };
        
        _consents.Add(consent);
        return true;
    }

    public async Task<List<UserConsent>> GetUserConsentsAsync(Guid userId)
    {
        await Task.Delay(50);
        return _consents.Where(c => c.UserId == userId).ToList();
    }

    public async Task<bool> RevokeConsentAsync(Guid consentId)
    {
        await Task.Delay(50);
        var consent = _consents.FirstOrDefault(c => c.Id == consentId);
        if (consent != null)
        {
            consent.IsGranted = false;
            consent.RevokedDate = DateTime.Now;
            return true;
        }
        return false;
    }

    public async Task<PrivacyPolicy> GetPrivacyPolicyAsync(string language)
    {
        await Task.Delay(50);
        return _privacyPolicy;
    }

    public async Task<DataSharingConsent> GetDataSharingConsentAsync(string language)
    {
        await Task.Delay(50);
        return _dataSharingConsent;
    }
}

