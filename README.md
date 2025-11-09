# 💚 Health & Fitness App v3.0

A modern, GDPR-compliant health tracking application built with Avalonia UI and .NET 9.0, featuring Samsung Health-inspired design, dark mode, and multi-language support.

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4)](https://dotnet.microsoft.com/)
[![Avalonia](https://img.shields.io/badge/Avalonia-11.3.6-purple)](https://avaloniaui.net/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

![Health App Dark Mode](docs/screenshots/dark-mode-dashboard.png)

## 🌟 Features

### 📊 **Health Tracking**
- **Daily Metrics**: Track steps, calories, heart rate, and distance
- **Weekly Summaries**: View aggregated health data over time
- **Progress Goals**: Set and monitor fitness targets
- **Visual Dashboard**: Samsung Health-inspired metric cards

### 🎨 **Modern UI**
- **Dark Mode Default**: Premium dark theme with light mode option
- **Theme Switching**: Instant theme changes without restart
- **Responsive Design**: Adapts to different window sizes
- **Professional Styling**: Clean, modern Samsung Health-inspired interface

### 📱 **Device Management**
- **Wearable Integration**: Connect fitness trackers and smartwatches
- **Data Synchronization**: Sync health data from connected devices
- **Device Status**: Monitor battery and connection status
- **Multi-device Support**: Manage multiple wearable devices

### 👥 **Data Sharing**
- **Healthcare Sharing**: Share data with doctors, physiotherapists, and trainers
- **Granular Permissions**: Control what data is shared
- **Revocable Access**: Remove sharing permissions anytime
- **GDPR Compliant**: Full European data protection compliance

### 🌍 **Multi-Language Support**
- **English**: Full English interface
- **Dutch (Nederlands)**: Complete Dutch translation
- **Auto-Detection**: Automatically detects Netherlands/Belgium region
- **Easy Switching**: Change language in Settings anytime

### 🔒 **Privacy & Security**
- **GDPR Compliance**: Full compliance with European data regulations
- **Privacy Consent**: Required consent dialog on first launch
- **Data Sharing Consent**: Separate consent for sharing with healthcare providers
- **Audit Trail**: IP address and timestamp logging
- **Data Rights**: Export and delete options

### ⚙️ **Settings & Preferences**
- **Theme Selection**: Choose between Light and Dark modes
- **Language Options**: English or Nederlands
- **About Section**: App version and information
- **User Preferences**: Centralized configuration

## 🚀 Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [JetBrains Rider](https://www.jetbrains.com/rider/) (recommended)
- Windows 10/11, macOS, or Linux

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/Rorensu-O/HealthappV3.0.git
   cd HealthappV3.0
   ```

2. **Navigate to project folder**
   ```bash
   cd "HealthApp V3.0"
   ```

3. **Restore dependencies**
   ```bash
   dotnet restore
   ```

4. **Build the project**
   ```bash
   dotnet build
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

### Quick Start Scripts

**Windows:**
- Double-click `RUN_APP.bat` to launch the app
- Use `BUILD_AND_CHECK.bat` to build and test

**Command Line:**
```bash
cd "HealthApp V3.0"
dotnet run
```

## 📱 Usage

### Navigation

The app features a sidebar with four main sections:

- **📊 Dashboard**: View your daily and weekly health metrics
- **📱 Devices**: Manage connected wearable devices
- **👥 Sharing**: Share data with healthcare professionals
- **⚙️ Settings**: Configure theme and language preferences

### Changing Theme

1. Click **⚙️ Settings** in the sidebar
2. Go to **🎨 Appearance** section
3. Choose between:
   - **☀️ Light Mode**: Clean, bright interface
   - **🌙 Dark Mode**: Premium dark interface (default)

### Changing Language

1. Click **⚙️ Settings** in the sidebar
2. Go to **🌍 Language** section
3. Choose between:
   - **🇬🇧 English**: English interface
   - **🇳🇱 Nederlands**: Dutch interface

### Health Data

The Dashboard displays:
- **Steps**: Daily step count with goal progress
- **Calories**: Calorie burn with daily target
- **Heart Rate**: Current, max, and min BPM
- **Distance**: Distance traveled in kilometers
- **Weekly Summary**: Aggregated weekly statistics

## 🏗️ Architecture

### Technology Stack

- **Framework**: .NET 9.0
- **UI Framework**: Avalonia UI 11.3.6
- **Architecture**: MVVM (Model-View-ViewModel)
- **Language**: C# 13.0
- **Reactive Extensions**: System.Reactive, ReactiveUI
- **MVVM Toolkit**: CommunityToolkit.Mvvm 8.2.2

### Project Structure

```
HealthApp V3.0/
├── Models/              # Data models
│   ├── HealthData.cs
│   ├── WearableDevice.cs
│   ├── ConsentModels.cs
│   └── SharedAccess.cs
├── ViewModels/          # MVVM ViewModels
│   ├── MainWindowViewModel.cs
│   ├── DashboardViewModel.cs
│   ├── DevicesViewModel.cs
│   └── SharingViewModel.cs
├── Views/               # UI Views
│   ├── DashboardViewSimple.cs
│   ├── SettingsView.cs
│   └── (XAML views)
├── Services/            # Business logic
│   ├── HealthDataService.cs
│   ├── LocalizationService.cs
│   ├── ConsentService.cs
│   └── WearableDeviceService.cs
├── MainWindowCodeBased.cs  # Main window
├── App.axaml.cs         # Application entry
└── Program.cs           # Main entry point
```

### Design Patterns

- **MVVM**: Separation of UI and business logic
- **Dependency Injection**: Service-oriented architecture
- **Observer Pattern**: Reactive programming with Rx
- **Command Pattern**: User interactions via ICommand
- **Repository Pattern**: Data access abstraction

## 🎨 Design

### Color Scheme

**Dark Mode (Default):**
- Primary Background: `#0F172A` (Deep navy)
- Card Background: `#1E293B` (Dark slate)
- Text: `#F1F5F9` (Nearly white)
- Accent: `#00D9A5` (Samsung green)

**Light Mode:**
- Primary Background: `#F5F7FA` (Soft gray)
- Card Background: `#FFFFFF` (Pure white)
- Text: `#1F2937` (Dark gray)
- Accent: `#00D9A5` (Samsung green)

### Typography

- **Font Family**: Inter (Avalonia.Fonts.Inter)
- **Header**: 28px, Bold
- **Subheader**: 18px, SemiBold
- **Body**: 14px, Regular
- **Caption**: 12px, Regular

### UI Components

- **Cards**: Rounded corners (12px), subtle borders
- **Buttons**: Rounded (8px), hover effects
- **Metric Cards**: Progress bars, goal tracking
- **Navigation**: Sidebar with icons and labels

## 🔧 Development

### Building from Source

```bash
# Clean build
dotnet clean
dotnet restore
dotnet build

# Run in debug mode
dotnet run

# Publish release version
dotnet publish -c Release -r win-x64 --self-contained false
```

### Code-Based UI

This project uses **code-based UI** instead of XAML for several components to avoid XAML precompilation issues:

- `MainWindowCodeBased.cs` - Main window
- `DashboardViewSimple.cs` - Dashboard view
- `SettingsView.cs` - Settings page

This approach provides:
- ✅ No XAML compilation errors
- ✅ Better IDE support
- ✅ Easier debugging
- ✅ Type safety

### Testing

The app includes several test/diagnostic scripts:

- `RUN_APP.bat` - Quick launcher
- `BUILD_AND_CHECK.bat` - Build with error checking
- `DIAGNOSE_APP.bat` - Diagnostic tool

## 📋 Requirements

### Minimum System Requirements

- **OS**: Windows 10 (1809+), macOS 10.15+, or Linux
- **RAM**: 512 MB minimum
- **.NET**: .NET 9.0 Runtime
- **Display**: 900x600 minimum resolution

### Recommended

- **OS**: Windows 11, macOS 12+, or Ubuntu 20.04+
- **RAM**: 2 GB
- **.NET**: .NET 9.0 SDK (for development)
- **Display**: 1200x700 or higher

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

### Development Setup

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Coding Standards

- Follow C# naming conventions
- Use MVVM pattern for new features
- Add comments for complex logic
- Test on both light and dark themes
- Ensure GDPR compliance for data features

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- **Avalonia UI** - Cross-platform UI framework
- **Samsung Health** - Design inspiration
- **.NET Foundation** - Runtime and libraries
- **JetBrains** - Rider IDE support

## 📧 Contact

**Project Link**: [https://github.com/Rorensu-O/HealthappV3.0](https://github.com/Rorensu-O/HealthappV3.0)

**University Project**: Created as part of software development coursework

> **📌 For Teacher Review**: This repository is public for academic evaluation. See [MAKE_REPOSITORY_PUBLIC.md](MAKE_REPOSITORY_PUBLIC.md) for visibility settings.

## 🎓 University Demonstration

This project was created as a university assignment demonstrating:

- ✅ Modern .NET development
- ✅ MVVM architecture implementation
- ✅ UI/UX design principles
- ✅ GDPR compliance
- ✅ Internationalization
- ✅ Accessibility features
- ✅ Problem-solving skills
- ✅ Professional documentation

### Features Demonstrated

1. **Technical Skills**: .NET 9.0, Avalonia UI, C# 13
2. **Architecture**: MVVM, Services, Dependency Injection
3. **UI/UX**: Theme switching, responsive design, accessibility
4. **Compliance**: GDPR data protection implementation
5. **Localization**: Multi-language support (EN/NL)
6. **Professional Practices**: Git, documentation, testing

## 📊 Project Statistics

- **Lines of Code**: 5000+
- **Files**: 40+
- **Languages**: C# (primary), XAML
- **UI Components**: 15+ custom views
- **Services**: 6 core services
- **ViewModels**: 6 MVVM ViewModels
- **Models**: 5 data models
- **Supported Languages**: 2 (English, Dutch)
- **Theme Options**: 2 (Light, Dark)

## 🚧 Roadmap

### Planned Features

- [ ] Real-time device synchronization
- [ ] Cloud data backup
- [ ] Advanced analytics and insights
- [ ] Social features (challenges, sharing)
- [ ] Integration with popular fitness APIs
- [ ] Mobile app version (iOS/Android)
- [ ] Export to PDF/CSV
- [ ] Custom goal setting
- [ ] Medication tracking
- [ ] Sleep tracking

### Known Issues

- Language switching updates immediately (full implementation in progress)
- Device synchronization is simulated (real integration planned)
- Health data is currently static (database integration planned)

## 📚 Documentation

For more detailed documentation, see:

- [Dark Mode & Settings Guide](DARK_MODE_DEFAULT_SETTINGS.md)
- [GDPR Compliance](GDPR_COMPLIANCE.md)
- [Dutch Language Support](DUTCH_LANGUAGE_SUPPORT.md)
- [Build Troubleshooting](BUILD_TROUBLESHOOTING.md)
- [Security Summary](SECURITY_SUMMARY.md)

---

**Made with 💚 for a healthier lifestyle**

*Version 3.0 | November 2025 | .NET 9.0 | Avalonia UI 11.3*

