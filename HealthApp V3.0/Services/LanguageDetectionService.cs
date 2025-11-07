using System;
using System.Globalization;

namespace HealthApp_V3._0.Services;

public interface ILanguageDetectionService
{
    string DetectSystemLanguage();
    string GetDefaultLanguageForRegion();
    bool IsNetherlands();
}

public class LanguageDetectionService : ILanguageDetectionService
{
    public string DetectSystemLanguage()
    {
        // Get system culture
        var currentCulture = CultureInfo.CurrentCulture;
        var currentUICulture = CultureInfo.CurrentUICulture;
        
        // Check if Dutch is the system language
        if (currentCulture.TwoLetterISOLanguageName == "nl" || 
            currentUICulture.TwoLetterISOLanguageName == "nl")
        {
            return "nl";
        }
        
        // Check if system is set to Netherlands region
        if (currentCulture.Name.StartsWith("nl-") || 
            currentCulture.Name.Contains("NL"))
        {
            return "nl";
        }
        
        // Default to English
        return "en";
    }

    public string GetDefaultLanguageForRegion()
    {
        var regionInfo = RegionInfo.CurrentRegion;
        
        // Check if user is in Netherlands, Belgium (Flanders), or Suriname
        switch (regionInfo.TwoLetterISORegionName.ToUpper())
        {
            case "NL": // Netherlands
            case "BE": // Belgium (check if Dutch-speaking region)
            case "SR": // Suriname
                return "nl";
            
            default:
                // For all other regions, detect from system language
                return DetectSystemLanguage();
        }
    }

    public bool IsNetherlands()
    {
        var regionInfo = RegionInfo.CurrentRegion;
        return regionInfo.TwoLetterISORegionName.ToUpper() == "NL" ||
               regionInfo.TwoLetterISORegionName.ToUpper() == "BE" ||
               CultureInfo.CurrentCulture.Name.StartsWith("nl-");
    }
}

