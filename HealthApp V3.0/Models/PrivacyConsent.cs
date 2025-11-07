using System;

namespace HealthApp_V3._0.Models;

public class PrivacyConsent
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public ConsentType Type { get; set; }
    public bool IsGranted { get; set; }
    public DateTime ConsentDate { get; set; }
    public DateTime? WithdrawnDate { get; set; }
    public string Version { get; set; } = "1.0"; // Privacy policy version
    public string IpAddress { get; set; } = string.Empty;
    public string ConsentText { get; set; } = string.Empty; // Store the actual consent text
}

public enum ConsentType
{
    PrivacyPolicy,          // General privacy policy consent (required)
    DataCollection,         // Consent to collect health data
    DataProcessing,         // Consent to process health data
    DataSharing,            // Consent to share data with third parties
    Analytics,              // Consent for analytics
    Marketing               // Consent for marketing (optional)
}

public class PrivacySettings
{
    public Guid UserId { get; set; }
    public DateTime? PrivacyPolicyAcceptedDate { get; set; }
    public string PrivacyPolicyVersion { get; set; } = "1.0";
    public bool HasCompletedOnboarding { get; set; }
    public bool DataCollectionConsent { get; set; }
    public bool DataProcessingConsent { get; set; }
    public bool DataSharingConsent { get; set; }
    public bool AnalyticsConsent { get; set; }
    public bool MarketingConsent { get; set; }
    
    // GDPR Rights tracking
    public DateTime? LastDataExportDate { get; set; }
    public DateTime? LastDataDeletionRequestDate { get; set; }
}

public class DataSharingConsent
{
    public Guid Id { get; set; }
    public Guid SharedAccessId { get; set; }
    public DateTime ConsentDate { get; set; }
    public string ConsentText { get; set; } = string.Empty;
    public string RecipientName { get; set; } = string.Empty;
    public string RecipientEmail { get; set; } = string.Empty;
    public RecipientType RecipientType { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? WithdrawnDate { get; set; }
    public string WithdrawalReason { get; set; } = string.Empty;
}

