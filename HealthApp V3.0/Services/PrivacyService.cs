using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthApp_V3._0.Models;

namespace HealthApp_V3._0.Services;

public interface IPrivacyService
{
    Task<bool> HasUserAcceptedPrivacyPolicyAsync(Guid userId);
    Task<bool> SavePrivacyConsentAsync(Guid userId, bool accepted, string consentText);
    Task<bool> SaveDataSharingConsentAsync(Guid userId, Guid sharedAccessId, string recipientName, string recipientEmail, RecipientType recipientType, string consentText);
    Task<bool> WithdrawDataSharingConsentAsync(Guid sharedAccessId, string reason);
    Task<PrivacySettings?> GetPrivacySettingsAsync(Guid userId);
    Task<bool> UpdatePrivacySettingsAsync(PrivacySettings settings);
    Task<List<PrivacyConsent>> GetUserConsentsAsync(Guid userId);
    Task<List<DataSharingConsent>> GetDataSharingConsentsAsync(Guid userId);
    Task<bool> RequestDataExportAsync(Guid userId);
    Task<bool> RequestDataDeletionAsync(Guid userId);
    string GetPrivacyPolicyText(string language);
    string GetDataSharingConsentText(string language);
}

public class PrivacyService : IPrivacyService
{
    private readonly List<PrivacyConsent> _privacyConsents = new();
    private readonly List<DataSharingConsent> _dataSharingConsents = new();
    private readonly Dictionary<Guid, PrivacySettings> _privacySettings = new();
    
    private const string PrivacyPolicyVersion = "1.0";

    public async Task<bool> HasUserAcceptedPrivacyPolicyAsync(Guid userId)
    {
        await Task.Delay(50);
        var consent = _privacyConsents
            .Where(c => c.UserId == userId && c.Type == ConsentType.PrivacyPolicy)
            .OrderByDescending(c => c.ConsentDate)
            .FirstOrDefault();
        
        return consent?.IsGranted == true && consent.WithdrawnDate == null;
    }

    public async Task<bool> SavePrivacyConsentAsync(Guid userId, bool accepted, string consentText)
    {
        await Task.Delay(100);
        
        var consent = new PrivacyConsent
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Type = ConsentType.PrivacyPolicy,
            IsGranted = accepted,
            ConsentDate = DateTime.Now,
            Version = PrivacyPolicyVersion,
            IpAddress = "127.0.0.1", // In real app, get actual IP
            ConsentText = consentText
        };
        
        _privacyConsents.Add(consent);
        
        // Update or create privacy settings
        if (!_privacySettings.ContainsKey(userId))
        {
            _privacySettings[userId] = new PrivacySettings { UserId = userId };
        }
        
        var settings = _privacySettings[userId];
        settings.PrivacyPolicyAcceptedDate = DateTime.Now;
        settings.PrivacyPolicyVersion = PrivacyPolicyVersion;
        settings.HasCompletedOnboarding = accepted;
        settings.DataCollectionConsent = accepted;
        settings.DataProcessingConsent = accepted;
        
        return true;
    }

    public async Task<bool> SaveDataSharingConsentAsync(Guid userId, Guid sharedAccessId, string recipientName, string recipientEmail, RecipientType recipientType, string consentText)
    {
        await Task.Delay(100);
        
        // Check if user has privacy policy consent first
        if (!await HasUserAcceptedPrivacyPolicyAsync(userId))
        {
            return false; // Cannot share data without basic privacy consent
        }
        
        var consent = new DataSharingConsent
        {
            Id = Guid.NewGuid(),
            SharedAccessId = sharedAccessId,
            ConsentDate = DateTime.Now,
            ConsentText = consentText,
            RecipientName = recipientName,
            RecipientEmail = recipientEmail,
            RecipientType = recipientType,
            IsActive = true
        };
        
        _dataSharingConsents.Add(consent);
        
        // Update privacy settings
        if (_privacySettings.ContainsKey(userId))
        {
            _privacySettings[userId].DataSharingConsent = true;
        }
        
        return true;
    }

    public async Task<bool> WithdrawDataSharingConsentAsync(Guid sharedAccessId, string reason)
    {
        await Task.Delay(100);
        var consent = _dataSharingConsents.FirstOrDefault(c => c.SharedAccessId == sharedAccessId && c.IsActive);
        
        if (consent != null)
        {
            consent.IsActive = false;
            consent.WithdrawnDate = DateTime.Now;
            consent.WithdrawalReason = reason;
            return true;
        }
        
        return false;
    }

    public async Task<PrivacySettings?> GetPrivacySettingsAsync(Guid userId)
    {
        await Task.Delay(50);
        return _privacySettings.TryGetValue(userId, out var settings) ? settings : null;
    }

    public async Task<bool> UpdatePrivacySettingsAsync(PrivacySettings settings)
    {
        await Task.Delay(50);
        _privacySettings[settings.UserId] = settings;
        return true;
    }

    public async Task<List<PrivacyConsent>> GetUserConsentsAsync(Guid userId)
    {
        await Task.Delay(50);
        return _privacyConsents.Where(c => c.UserId == userId).OrderByDescending(c => c.ConsentDate).ToList();
    }

    public async Task<List<DataSharingConsent>> GetDataSharingConsentsAsync(Guid userId)
    {
        await Task.Delay(50);
        return _dataSharingConsents.Where(c => c.IsActive).ToList();
    }

    public async Task<bool> RequestDataExportAsync(Guid userId)
    {
        await Task.Delay(500); // Simulate export processing
        
        if (_privacySettings.ContainsKey(userId))
        {
            _privacySettings[userId].LastDataExportDate = DateTime.Now;
        }
        
        // In real app: generate data export file, send to user
        return true;
    }

    public async Task<bool> RequestDataDeletionAsync(Guid userId)
    {
        await Task.Delay(500); // Simulate deletion processing
        
        if (_privacySettings.ContainsKey(userId))
        {
            _privacySettings[userId].LastDataDeletionRequestDate = DateTime.Now;
        }
        
        // In real app: schedule data deletion, notify user
        return true;
    }

    public string GetPrivacyPolicyText(string language)
    {
        if (language == "nl")
        {
            return @"PRIVACYBELEID & GEBRUIKSVOORWAARDEN
Health & Fitness App - Versie 1.0
Ingangsdatum: 1 januari 2025

1. VERWERKINGSVERANTWOORDELIJKE
Deze gezondheids- en fitness-applicatie verzamelt en verwerkt uw persoonlijke gezondheidsgegevens in overeenstemming met de Algemene Verordening Gegevensbescherming (AVG).

2. VERZAMELDE GEGEVENS
Wij verzamelen de volgende categorieën gegevens:

a) Gezondheidsgegevens (Art. 9 AVG - bijzondere categorieën):
   • Stappen en bewegingsgegevens
   • Hartslagmetingen
   • Verbrande calorieën
   • Slaapgegevens en slaapkwaliteit
   • Afgelegde afstand
   • Actieve minuten

b) Persoonlijke gegevens:
   • Naam en e-mailadres
   • Geboortedatum
   • Lengte en gewicht
   • Geslacht (optioneel)

c) Technische gegevens:
   • Verbonden apparaten (wearables)
   • App-gebruiksgegevens
   • IP-adres en apparaatinformatie

3. DOEL VAN VERWERKING
Wij verwerken uw gegevens voor:
   • Het leveren van gezondheids- en fitnesstrackingdiensten
   • Synchronisatie met draagbare apparaten
   • Het genereren van gezondheidsstatistieken en inzichten
   • Het mogelijk maken van gegevens delen met zorgverleners (met uw toestemming)
   • Verbetering van app-functionaliteit

4. RECHTSGRONSLAG (Art. 6 & 9 AVG)
   • Uw uitdrukkelijke toestemming (Art. 6(1)(a) en Art. 9(2)(a))
   • Uitvoering van overeenkomst (Art. 6(1)(b))
   • Gerechtvaardigde belangen (Art. 6(1)(f))

5. GEGEVENS DELEN
Uw gezondheidsgegevens worden ALLEEN gedeeld met derden (artsen, fysiotherapeuten, personal trainers) met uw uitdrukkelijke, afzonderlijke toestemming. U kunt deze toestemming op elk moment intrekken.

Wij delen GEEN gegevens met adverteerders of derden voor marketingdoeleinden zonder uw expliciete toestemming.

6. GEGEVENSBEWARING
   • Actieve gegevens: Bewaard zolang u de dienst gebruikt
   • Na accountverwijdering: Gegevens binnen 30 dagen verwijderd
   • Back-upgegevens: Binnen 90 dagen verwijderd
   • Wettelijke bewaarplicht: Indien verplicht volgens wetgeving

7. UW RECHTEN ONDER DE AVG
U heeft de volgende rechten:

   a) Recht op inzage (Art. 15): Toegang tot uw persoonlijke gegevens
   b) Recht op rectificatie (Art. 16): Correctie van onjuiste gegevens
   c) Recht op vergetelheid (Art. 17): Verwijdering van uw gegevens
   d) Recht op beperking (Art. 18): Beperking van verwerking
   e) Recht op gegevensoverdraagbaarheid (Art. 20): Export van uw gegevens
   f) Recht om toestemming in te trekken (Art. 7(3)): Op elk moment
   g) Recht van bezwaar (Art. 21): Bezwaar maken tegen verwerking
   h) Recht om klacht in te dienen (Art. 77): Bij de Autoriteit Persoonsgegevens

8. BEVEILIGING VAN GEGEVENS
Wij implementeren passende technische en organisatorische maatregelen:
   • End-to-end encryptie voor gegevensoverdracht
   • Veilige opslag met toegangscontroles
   • Regelmatige beveiligingsaudits
   • Pseudonimisering waar mogelijk
   • Gegevensbescherming by design en by default

9. INTREKKING VAN TOESTEMMING
U kunt uw toestemming op elk moment intrekken zonder opgave van reden. Dit laat de rechtmatigheid van de verwerking vóór de intrekking onverlet.

10. CONTACT & KLACHTEN
   • Privacyvragen: privacy@healthapp.com
   • Uitoefenen van rechten: rights@healthapp.com
   • Klachten indienen bij: Autoriteit Persoonsgegevens (www.autoriteitpersoonsgegevens.nl)

11. WIJZIGINGEN
Wij behouden ons het recht voor dit privacybeleid te wijzigen. U wordt hiervan op de hoogte gesteld en gevraagd opnieuw toestemming te geven indien vereist.

Door op 'Accepteren' te klikken, bevestigt u dat u:
✓ Dit privacybeleid hebt gelezen en begrepen
✓ Begrijpt dat gezondheidsgegevens bijzondere categorieën zijn (Art. 9 AVG)
✓ Uitdrukkelijk toestemming geeft voor de verwerking van uw gezondheidsgegevens
✓ Weet dat u uw toestemming op elk moment kunt intrekken
✓ Begrijpt dat u rechten heeft onder de AVG";
        }
        else // English
        {
            return @"PRIVACY POLICY & TERMS OF USE
Health & Fitness App - Version 1.0
Effective Date: January 1, 2025

1. DATA CONTROLLER
This health and fitness application collects and processes your personal health data in accordance with the General Data Protection Regulation (GDPR).

2. DATA WE COLLECT
We collect the following categories of data:

a) Health Data (Art. 9 GDPR - special categories):
   • Steps and activity data
   • Heart rate measurements
   • Calories burned
   • Sleep data and sleep quality
   • Distance traveled
   • Active minutes

b) Personal Data:
   • Name and email address
   • Date of birth
   • Height and weight
   • Gender (optional)

c) Technical Data:
   • Connected devices (wearables)
   • App usage data
   • IP address and device information

3. PURPOSE OF PROCESSING
We process your data to:
   • Provide health and fitness tracking services
   • Sync with wearable devices
   • Generate health statistics and insights
   • Enable data sharing with healthcare professionals (with your consent)
   • Improve app functionality

4. LEGAL BASIS (Art. 6 & 9 GDPR)
   • Your explicit consent (Art. 6(1)(a) and Art. 9(2)(a))
   • Performance of contract (Art. 6(1)(b))
   • Legitimate interests (Art. 6(1)(f))

5. DATA SHARING
Your health data will ONLY be shared with third parties (doctors, physiotherapists, personal trainers) with your explicit, separate consent. You can withdraw this consent at any time.

We do NOT share data with advertisers or third parties for marketing purposes without your explicit consent.

6. DATA RETENTION
   • Active data: Retained while you use the service
   • After account deletion: Data deleted within 30 days
   • Backup data: Deleted within 90 days
   • Legal retention: If required by law

7. YOUR RIGHTS UNDER GDPR
You have the following rights:

   a) Right of access (Art. 15): Access to your personal data
   b) Right to rectification (Art. 16): Correction of inaccurate data
   c) Right to erasure (Art. 17): Deletion of your data ('right to be forgotten')
   d) Right to restriction (Art. 18): Restriction of processing
   e) Right to data portability (Art. 20): Export your data
   f) Right to withdraw consent (Art. 7(3)): At any time
   g) Right to object (Art. 21): Object to processing
   h) Right to lodge a complaint (Art. 77): With supervisory authority

8. DATA SECURITY
We implement appropriate technical and organizational measures:
   • End-to-end encryption for data transmission
   • Secure storage with access controls
   • Regular security audits
   • Pseudonymization where possible
   • Data protection by design and by default

9. WITHDRAWAL OF CONSENT
You can withdraw your consent at any time without giving a reason. This does not affect the lawfulness of processing based on consent before its withdrawal.

10. CONTACT & COMPLAINTS
   • Privacy questions: privacy@healthapp.com
   • Exercise your rights: rights@healthapp.com
   • File complaints with: Data Protection Authority in your country

11. CHANGES
We reserve the right to modify this privacy policy. You will be notified and asked to provide consent again if required.

By clicking 'Accept', you confirm that you:
✓ Have read and understood this privacy policy
✓ Understand that health data is a special category (Art. 9 GDPR)
✓ Explicitly consent to the processing of your health data
✓ Know you can withdraw your consent at any time
✓ Understand your rights under GDPR";
        }
    }

    public string GetDataSharingConsentText(string language)
    {
        if (language == "nl")
        {
            return @"TOESTEMMING VOOR DELEN VAN GEZONDHEIDSGEGEVENS

Door akkoord te gaan, geeft u uitdrukkelijke toestemming voor het delen van uw gezondheids- en fitnessgegevens met de opgegeven ontvanger.

WELKE GEGEVENS WORDEN GEDEELD:
✓ Stappen en activiteitsgegevens
✓ Hartslagmetingen
✓ Verbrande calorieën
✓ Slaapduur en slaapkwaliteit
✓ Afgelegde afstand
✓ Actieve minuten
✓ Algemene gezondheidsstatistieken

RECHTSGRONSLAG: Art. 9(2)(a) AVG - Uitdrukkelijke toestemming

UW RECHTEN:
• Toestemming op elk moment intrekken
• Beperken welke gegevens worden gedeeld
• Vervaldatum instellen voor gegevenstoegang
• Verwijdering van gedeelde gegevens aanvragen
• Inzage in wie toegang heeft tot uw gegevens

TOEGANGSNIVEAU:
De ontvanger krijgt alleen-lezen toegang tot uw gegevens voor de gespecificeerde periode. Zij kunnen uw gegevens NIET wijzigen of verwijderen.

BEVEILIGING:
• Gegevens worden verzonden met end-to-end encryptie (TLS 1.3)
• Veilig opgeslagen met AES-256 encryptie
• Toegangslogboeken worden bijgehouden
• Regelmatige beveiligingsaudits

VERANTWOORDELIJKHEID ONTVANGER:
De ontvanger is ook gebonden aan de AVG en moet uw gegevens beschermen volgens dezelfde standaarden.

INTREKKING:
U kunt deze toestemming op elk moment intrekken zonder opgave van reden. Na intrekking:
• Toegang wordt onmiddellijk geblokkeerd
• Gedeelde gegevens worden binnen 7 dagen verwijderd bij ontvanger
• U ontvangt een bevestiging van intrekking

Door op 'Ik Stem Toe' te klikken, bevestigt u dat:
✓ U begrijpt welke gegevens worden gedeeld
✓ U vrijwillig toestemming geeft zonder dwang
✓ U weet dat u de toestemming op elk moment kunt intrekken
✓ U de ontvanger vertrouwt met uw gezondheidsgegevens";
        }
        else // English
        {
            return @"CONSENT FOR SHARING HEALTH DATA

By proceeding, you explicitly consent to share your health and fitness data with the specified recipient.

WHAT DATA WILL BE SHARED:
✓ Steps and activity data
✓ Heart rate measurements
✓ Calories burned
✓ Sleep duration and quality
✓ Distance traveled
✓ Active minutes
✓ General health statistics

LEGAL BASIS: Art. 9(2)(a) GDPR - Explicit Consent

YOUR RIGHTS:
• Withdraw consent at any time
• Restrict which data types are shared
• Set expiration date for data access
• Request deletion of shared data
• View who has access to your data

ACCESS LEVEL:
The recipient receives read-only access to your data for the specified time period. They CANNOT modify or delete your data.

SECURITY:
• Data transmitted using end-to-end encryption (TLS 1.3)
• Securely stored with AES-256 encryption
• Access logs are maintained
• Regular security audits

RECIPIENT RESPONSIBILITY:
The recipient is also bound by GDPR and must protect your data according to the same standards.

WITHDRAWAL:
You can withdraw this consent at any time without giving a reason. Upon withdrawal:
• Access is immediately blocked
• Shared data deleted within 7 days at recipient
• You receive confirmation of withdrawal

By clicking 'I Consent', you confirm that:
✓ You understand what data will be shared
✓ You consent voluntarily without coercion
✓ You know you can withdraw consent at any time
✓ You trust the recipient with your health data";
        }
    }
}

