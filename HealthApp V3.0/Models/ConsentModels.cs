using System;

namespace HealthApp_V3._0.Models;


public class UserConsent
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public ConsentType Type { get; set; }
    public DateTime ConsentDate { get; set; }
    public string ConsentType { get; set; } = string.Empty; // "Privacy", "DataSharing", etc.
    public bool IsAccepted { get; set; }
    public bool IsGranted { get; set; }
    public DateTime? GrantedDate { get; set; }
    public string Version { get; set; } = "1.0";
    public string? Purpose { get; set; }
    public string? RecipientId { get; set; }
    public string? RecipientName { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime? RevokedDate { get; set; }
    public string IpAddress { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
    public string ConsentVersion { get; set; } = "1.0";
}

public class PrivacyPolicy
{
    public Guid Id { get; set; }
    public string Version { get; set; } = "1.0";
    public DateTime EffectiveDate { get; set; }
    public string Content { get; set; } = string.Empty;
    public string Language { get; set; } = "en";
    public bool IsActive { get; set; }
}

