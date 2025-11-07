using System.Collections.Generic;
using System.Globalization;

namespace HealthApp_V3._0.Services;

public interface ILocalizationService
{
    string GetString(string key);
    void SetLanguage(string languageCode);
    string CurrentLanguage { get; }
    string DetectAndSetDefaultLanguage();
}

public class LocalizationService : ILocalizationService
{
    private Dictionary<string, string> _currentStrings = new();
    public string CurrentLanguage { get; private set; } = "en";

    private readonly Dictionary<string, Dictionary<string, string>> _localizations = new()
    {
        ["en"] = new Dictionary<string, string>
        {
            // General
            ["AppTitle"] = "Health & Fitness",
            ["Dashboard"] = "Dashboard",
            ["Activity"] = "Activity",
            ["Devices"] = "Devices",
            ["Sharing"] = "Sharing",
            ["Settings"] = "Settings",
            
            // Dashboard
            ["Today"] = "Today",
            ["ThisWeek"] = "This Week",
            ["Steps"] = "Steps",
            ["Calories"] = "Calories",
            ["HeartRate"] = "Heart Rate",
            ["Distance"] = "Distance",
            ["ActiveMinutes"] = "Active Minutes",
            ["Sleep"] = "Sleep",
            ["Goal"] = "Goal",
            
            // Devices
            ["ConnectedDevices"] = "Connected Devices",
            ["AvailableDevices"] = "Available Devices",
            ["ScanDevices"] = "Scan for Devices",
            ["Connect"] = "Connect",
            ["Disconnect"] = "Disconnect",
            ["Sync"] = "Sync",
            ["LastSync"] = "Last Sync",
            ["Battery"] = "Battery",
            
            // Sharing
            ["ShareYourData"] = "Share Your Data",
            ["SharedWith"] = "Shared With",
            ["AddRecipient"] = "Add Recipient",
            ["RecipientName"] = "Recipient Name",
            ["RecipientEmail"] = "Recipient Email",
            ["RecipientType"] = "Recipient Type",
            ["Doctor"] = "Doctor",
            ["Physiotherapist"] = "Physiotherapist",
            ["PersonalTrainer"] = "Personal Trainer",
            ["Permissions"] = "Permissions",
            ["ExpiryDate"] = "Expiry Date",
            ["Revoke"] = "Revoke",
            ["Save"] = "Save",
            ["Cancel"] = "Cancel",
            
            // Units
            ["km"] = "km",
            ["kcal"] = "kcal",
            ["bpm"] = "bpm",
            ["min"] = "min",
            ["hours"] = "hours",
            
            // Privacy & Consent
            ["PrivacyPolicy"] = "Privacy Policy",
            ["AcceptAndContinue"] = "Accept & Continue",
            ["DeclineAndExit"] = "Decline & Exit",
            ["IAccept"] = "I Accept",
            ["IConsent"] = "I Consent - Share Data",
            ["DoNotShare"] = "Do Not Share",
            ["PrivacySettings"] = "Privacy Settings",
            ["ManageConsent"] = "Manage Consent",
            ["YourRights"] = "Your Rights",
            ["DataProtection"] = "Data Protection",
            ["GDPRCompliant"] = "GDPR Compliant",
            ["ExportData"] = "Export My Data",
            ["DeleteData"] = "Delete My Data",
            ["ViewConsents"] = "View Consents",
            ["WithdrawConsent"] = "Withdraw Consent",
            
            // Additional strings
            ["Welcome"] = "Welcome",
            ["LanguageSettings"] = "Language Settings",
            ["DetectedLanguage"] = "Detected Language",
            ["CurrentRegion"] = "Current Region",
            ["Netherlands"] = "Netherlands",
            ["Loading"] = "Loading...",
            ["Scanning"] = "Scanning...",
            ["Syncing"] = "Syncing...",
            ["Connected"] = "Connected",
            ["Disconnected"] = "Disconnected",
            ["NoData"] = "No data available",
            ["Error"] = "Error",
            ["Success"] = "Success",
            ["Warning"] = "Warning",
            ["Info"] = "Information"
        },
        ["nl"] = new Dictionary<string, string>
        {
            // General
            ["AppTitle"] = "Gezondheid & Fitness",
            ["Dashboard"] = "Dashboard",
            ["Activity"] = "Activiteit",
            ["Devices"] = "Apparaten",
            ["Sharing"] = "Delen",
            ["Settings"] = "Instellingen",
            
            // Dashboard
            ["Today"] = "Vandaag",
            ["ThisWeek"] = "Deze Week",
            ["Steps"] = "Stappen",
            ["Calories"] = "Calorieën",
            ["HeartRate"] = "Hartslag",
            ["Distance"] = "Afstand",
            ["ActiveMinutes"] = "Actieve Minuten",
            ["Sleep"] = "Slaap",
            ["Goal"] = "Doel",
            
            // Devices
            ["ConnectedDevices"] = "Verbonden Apparaten",
            ["AvailableDevices"] = "Beschikbare Apparaten",
            ["ScanDevices"] = "Scan voor Apparaten",
            ["Connect"] = "Verbinden",
            ["Disconnect"] = "Verbreken",
            ["Sync"] = "Synchroniseren",
            ["LastSync"] = "Laatste Sync",
            ["Battery"] = "Batterij",
            
            // Sharing
            ["ShareYourData"] = "Deel Je Gegevens",
            ["SharedWith"] = "Gedeeld Met",
            ["AddRecipient"] = "Ontvanger Toevoegen",
            ["RecipientName"] = "Naam Ontvanger",
            ["RecipientEmail"] = "E-mail Ontvanger",
            ["RecipientType"] = "Type Ontvanger",
            ["Doctor"] = "Arts",
            ["Physiotherapist"] = "Fysiotherapeut",
            ["PersonalTrainer"] = "Persoonlijke Trainer",
            ["Permissions"] = "Rechten",
            ["ExpiryDate"] = "Vervaldatum",
            ["Revoke"] = "Intrekken",
            ["Save"] = "Opslaan",
            ["Cancel"] = "Annuleren",
            
            // Units
            ["km"] = "km",
            ["kcal"] = "kcal",
            ["bpm"] = "bpm",
            ["min"] = "min",
            ["hours"] = "uur",
            
            // Privacy & Consent
            ["PrivacyPolicy"] = "Privacybeleid",
            ["AcceptAndContinue"] = "Accepteren & Doorgaan",
            ["DeclineAndExit"] = "Weigeren & Afsluiten",
            ["IAccept"] = "Ik Accepteer",
            ["IConsent"] = "Ik Stem Toe - Gegevens Delen",
            ["DoNotShare"] = "Niet Delen",
            ["PrivacySettings"] = "Privacy Instellingen",
            ["ManageConsent"] = "Toestemming Beheren",
            ["YourRights"] = "Uw Rechten",
            ["DataProtection"] = "Gegevensbescherming",
            ["GDPRCompliant"] = "AVG Conform",
            ["ExportData"] = "Mijn Gegevens Exporteren",
            ["DeleteData"] = "Mijn Gegevens Verwijderen",
            ["ViewConsents"] = "Toestemmingen Bekijken",
            ["WithdrawConsent"] = "Toestemming Intrekken",
            
            // Additional Dutch-specific strings
            ["Welcome"] = "Welkom",
            ["LanguageSettings"] = "Taalinstellingen",
            ["DetectedLanguage"] = "Gedetecteerde Taal",
            ["CurrentRegion"] = "Huidige Regio",
            ["Netherlands"] = "Nederland",
            ["Loading"] = "Laden...",
            ["Scanning"] = "Scannen...",
            ["Syncing"] = "Synchroniseren...",
            ["Connected"] = "Verbonden",
            ["Disconnected"] = "Verbroken",
            ["NoData"] = "Geen gegevens beschikbaar",
            ["Error"] = "Fout",
            ["Success"] = "Succes",
            ["Warning"] = "Waarschuwing",
            ["Info"] = "Informatie"
        }
    };

    public LocalizationService()
    {
        // Auto-detect language based on system settings
        var defaultLanguage = DetectAndSetDefaultLanguage();
        SetLanguage(defaultLanguage);
    }

    public string DetectAndSetDefaultLanguage()
    {
        // Get system culture
        var currentCulture = CultureInfo.CurrentCulture;
        var currentUICulture = CultureInfo.CurrentUICulture;
        var regionInfo = RegionInfo.CurrentRegion;

        // Use switch statement to determine language based on region and culture
        string detectedLanguage = regionInfo.TwoLetterISORegionName.ToUpper() switch
        {
            "NL" => "nl",  // Netherlands - Default to Dutch
            "BE" when currentCulture.TwoLetterISOLanguageName == "nl" => "nl",  // Belgium (Flemish)
            "SR" => "nl",  // Suriname - Dutch speaking
            _ => currentCulture.TwoLetterISOLanguageName.ToLower() switch
            {
                "nl" => "nl",  // Dutch language system
                _ => "en"  // Default to English for all other cases
            }
        };

        return detectedLanguage;
    }

    public string GetString(string key)
    {
        return _currentStrings.TryGetValue(key, out var value) ? value : key;
    }

    public void SetLanguage(string languageCode)
    {
        // Use switch statement for language selection
        switch (languageCode.ToLower())
        {
            case "nl":
            case "dutch":
            case "nederlands":
                if (_localizations.ContainsKey("nl"))
                {
                    CurrentLanguage = "nl";
                    _currentStrings = _localizations["nl"];
                }
                break;

            case "en":
            case "english":
            case "engels":
                if (_localizations.ContainsKey("en"))
                {
                    CurrentLanguage = "en";
                    _currentStrings = _localizations["en"];
                }
                break;

            default:
                // If unknown language, default to English
                if (_localizations.ContainsKey("en"))
                {
                    CurrentLanguage = "en";
                    _currentStrings = _localizations["en"];
                }
                break;
        }
    }
}

