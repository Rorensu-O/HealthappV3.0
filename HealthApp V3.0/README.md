
### Planned Features
- [ ] Data export functionality (JSON/CSV)
- [ ] Granular sharing permissions
- [ ] Consent expiration dates
- [ ] Privacy dashboard
- [ ] Two-factor authentication
- [ ] Data anonymization
- [ ] Activity charts and graphs
- [ ] Goal setting and achievements
- [ ] Notifications and reminders
- [ ] Dark mode support

### Real Integration
- [ ] Google Fit integration
- [ ] Samsung Health SDK integration
- [ ] Health Connect API
- [ ] Cloud synchronization
- [ ] Database persistence
- [ ] Authentication system

## 📝 License

[Your License Here]

## 👥 Contributing

Contributions are welcome! Please read the contributing guidelines before submitting pull requests.

## 📞 Support

For questions or issues:
- Open an issue on GitHub
- Email: support@healthapp.com
- Privacy concerns: privacy@healthapp.com
- GDPR requests: gdpr@healthapp.com

## ⚖️ Legal

This application complies with:
- **GDPR** (General Data Protection Regulation)
- **ePrivacy Directive**
- Best practices for health data processing

See [GDPR_COMPLIANCE.md](GDPR_COMPLIANCE.md) for detailed compliance information.

---

**Built with ❤️ and Avalonia UI**
# Health & Fitness App - GDPR Compliant

A comprehensive health and fitness tracking application built with Avalonia UI, featuring Samsung Health-inspired aesthetics and full GDPR compliance.

## 🎯 Features

### Core Functionality
- **Dashboard**: View daily and weekly health summaries with beautiful card-based UI
- **Device Management**: Connect and sync data from wearable devices (smartwatches, fitness bands)
- **Data Sharing**: Securely share health data with doctors, physiotherapists, or personal trainers
- **Bilingual Support**: Full support for English and Dutch languages

### Health Metrics Tracked
- Steps
- Calories burned
- Heart rate (average, min, max)
- Distance traveled
- Active minutes
- Sleep duration

### 🔒 GDPR Compliance & Security

#### Privacy Consent on First Launch
- **Mandatory privacy policy acceptance** before app usage
- Comprehensive privacy policy in English and Dutch
- Explicit checkbox consent required
- Records consent with timestamps, IP address, and policy version

#### Separate Data Sharing Consent
- **Explicit consent required** for each data sharing action
- Clear disclosure of what data types will be shared
- Recipients are identified by name, email, and role
- Users can revoke access at any time

#### User Rights (GDPR)
- ✅ Right to access data (Article 15)
- ✅ Right to rectification (Article 16)
- ✅ Right to erasure / "Right to be forgotten" (Article 17)
- ✅ Right to data portability (Article 20)
- ✅ Right to withdraw consent (Article 7(3))
- ✅ Right to object to processing (Article 21)

See [GDPR_COMPLIANCE.md](GDPR_COMPLIANCE.md) for detailed documentation.

## 🎨 Design

### Samsung Health-Inspired Aesthetics
- Clean, modern card-based interface
- Green primary color (#00D9A5)
- Smooth rounded corners (16-20px)
- Subtle shadows and depth
- Clear typography hierarchy
- Professional health app look and feel

### Responsive Layout
- Sidebar navigation
- Adaptive content area
- Scrollable sections
- Modal dialogs for consents

## 📁 Project Structure

```
HealthApp V3.0/
├── Models/
│   ├── HealthData.cs          # Health metrics data models
│   ├── User.cs                # User profile and preferences
│   ├── WearableDevice.cs      # Device information
│   ├── SharedAccess.cs        # Data sharing models
│   └── Consent.cs             # GDPR consent models
├── Services/
│   ├── HealthDataService.cs        # Health data management
│   ├── WearableDeviceService.cs    # Device connectivity
│   ├── SharingService.cs           # Data sharing logic
│   ├── LocalizationService.cs      # Multi-language support
│   └── ConsentService.cs           # GDPR consent management
├── ViewModels/
│   ├── MainWindowViewModel.cs           # Main app navigation
│   ├── DashboardViewModel.cs            # Dashboard logic
│   ├── DevicesViewModel.cs              # Device management
│   ├── SharingViewModel.cs              # Data sharing logic
│   ├── PrivacyConsentViewModel.cs       # Privacy policy consent
│   └── DataSharingConsentViewModel.cs   # Data sharing consent
├── Views/
│   ├── DashboardView.axaml         # Dashboard UI
│   ├── DevicesView.axaml           # Device management UI
│   ├── SharingView.axaml           # Data sharing UI
│   ├── PrivacyConsentView.axaml    # Privacy consent dialog
│   └── DataSharingConsentDialog.axaml  # Data sharing consent dialog
├── Styles/
│   └── HealthAppStyles.axaml      # Samsung Health-inspired styles
├── MainWindow.axaml               # Main application window
├── App.axaml                      # Application resources
└── Program.cs                     # Entry point
```

## 🚀 Getting Started

### Prerequisites
- .NET 9.0 SDK
- JetBrains Rider or Visual Studio 2022

### Installation

1. Clone the repository:
```bash
git clone <repository-url>
cd AvaloniaApplication3
```

2. Restore NuGet packages:
```bash
cd "HealthApp V3.0"
dotnet restore
```

3. Build the project:
```bash
dotnet build
```

4. Run the application:
```bash
dotnet run
```

### First Launch
On first launch, you'll be prompted to:
1. **Accept the Privacy Policy** - Required to use the app
2. Choose your preferred language (English or Dutch)

## 📱 Usage

### Dashboard
- View your daily health statistics
- Track progress toward your goals
- See weekly summaries

### Connecting Devices
1. Navigate to **Devices** tab
2. Click **Scan for Devices**
3. Select your device and click **Connect**
4. Click **Sync** to import your health data

### Sharing Data
1. Navigate to **Sharing** tab
2. Click **Add Recipient**
3. Enter recipient details (name, email, type)
4. Click **Save**
5. **Review and accept the Data Sharing Consent** (REQUIRED)
6. Data will be shared after explicit consent

### Revoking Access
- Go to **Sharing** tab
- Find the recipient in the list
- Click **Revoke** button
- Access is immediately terminated

## 🌍 Internationalization

### Supported Languages
- 🇬🇧 **English**
- 🇳🇱 **Nederlands** (Dutch)

### Switching Languages
- Use the language selector in the sidebar
- All UI elements update immediately
- Privacy policy and consent dialogs adapt to selected language

## 🔧 Architecture

### MVVM Pattern
- **Models**: Data structures
- **Views**: UI (AXAML)
- **ViewModels**: Logic and data binding

### Services
- Interface-based design for testability
- Mock implementations for demonstration
- Ready for real API integration

### Data Flow
```
User Action → ViewModel Command → Service → Data Update → UI Update
```

## 🔐 Security Features

### Consent Management
- All consents tracked with timestamps
- Version control for privacy policies
- Audit trail for compliance
- Easy revocation mechanisms

### Data Protection
- End-to-end encryption (mentioned in policies)
- Read-only access for shared data
- Secure storage practices
- User maintains full control

### Audit Logging
- Consent actions logged
- IP address tracking
- User agent information
- Policy version tracking

## 🛠 Technologies Used

- **Avalonia UI 11.3.6** - Cross-platform UI framework
- **CommunityToolkit.Mvvm 8.2.2** - MVVM helpers
- **System.Reactive 6.0.0** - Reactive programming
- **.NET 9.0** - Latest .NET framework

## 📊 Mock Data

The app includes mock data for demonstration:
- 7 days of health data
- 2 sample wearable devices (Samsung Galaxy Watch 6, Fitbit Charge 5)
- Randomized health metrics

### Connecting to Real Devices

To connect real devices, you'll need to integrate:
- **Google Fit API** for Android devices
- **Samsung Health SDK** for Samsung wearables
- **Android Health Connect API** for modern Android
- **Bluetooth Low Energy (BLE)** for direct device communication

See `WearableDeviceService.cs` for integration points.

## 🎯 Future Roadmap

