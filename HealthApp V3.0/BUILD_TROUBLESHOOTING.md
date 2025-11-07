# 🔧 BUILD TROUBLESHOOTING GUIDE

**If the build failed, follow these steps to fix it:**

---

## 🎯 **Quick Fixes:**

### **Fix 1: Clean and Rebuild in Rider**
1. In Rider, press **Ctrl+Shift+F9** (Build Solution)
2. If errors appear, press **Ctrl+Shift+B** (Rebuild Solution)
3. Check the Build window at the bottom for specific errors

### **Fix 2: Clean Build from Terminal**
```bash
cd "C:\Users\LAURE\RiderProjects\AvaloniaApplication3\HealthApp V3.0"
dotnet clean
dotnet restore
dotnet build
```

### **Fix 3: Delete bin/obj and Rebuild**
```bash
cd "C:\Users\LAURE\RiderProjects\AvaloniaApplication3\HealthApp V3.0"
Remove-Item -Recurse -Force bin,obj
dotnet restore
dotnet build
```

---

## 🔍 **Common Build Errors & Solutions:**

### Error: "Using directive is not required"
**Solution:** These are just warnings, not errors. Safe to ignore.

### Error: "Cannot find type X"
**Solution:** 
- Press **Ctrl+Shift+F9** in Rider to rebuild
- Make sure all using statements are present
- Clean and rebuild

### Error: "XAML precompilation failed"
**Solution:** We're using code-based UI now, so this shouldn't happen. If it does:
- Make sure you're using `MainWindowCodeBased` not `MainWindow`
- Check `App.axaml.cs` uses `MainWindowCodeBased`
- Check `Program.cs` references `App` correctly

---

## ✅ **Verify Your Setup:**

### Check These Files Exist:
- [ ] `MainWindowCodeBased.cs`
- [ ] `Views/DashboardViewSimple.cs`
- [ ] `App.axaml.cs`
- [ ] `Program.cs`

### Check App.axaml.cs Contains:
```csharp
desktop.MainWindow = new MainWindowCodeBased();
```
NOT:
```csharp
desktop.MainWindow = new MainWindow(); // ❌ Old version
```

---

## 🚀 **If Build Succeeds But App Won't Run:**

### Check 1: Look for the Window
- Press **Alt+Tab** to find the window
- Check taskbar for "Health & Fitness"
- Look on all monitors if you have multiple

### Check 2: Look for Error Window
- If app crashes, an error window should appear
- Read the error message
- Check the stack trace

### Check 3: Run from Rider
- Press **Shift+F10** in Rider
- This shows any runtime errors in the Run window

---

## 📝 **What to Tell Me:**

If the build still fails, please tell me:

1. **What error message do you see?**
   - Copy the exact error text
   - Include the file name and line number

2. **Where does it appear?**
   - In Rider's Build window?
   - In terminal?
   - In an error dialog?

3. **What step are you on?**
   - Building?
   - Running?
   - Opening the project?

---

## 🎯 **Most Likely Issues:**

### Issue 1: Old MainWindow Reference
**Check:** `App.axaml.cs` should use `MainWindowCodeBased`

**Fix:**
```csharp
// In App.axaml.cs line ~27:
desktop.MainWindow = new MainWindowCodeBased(); // ✅ Correct
```

### Issue 2: Missing Using Statements
**Check:** Top of `MainWindowCodeBased.cs`

**Should have:**
```csharp
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using HealthApp_V3._0.Services;
using HealthApp_V3._0.ViewModels;
```

### Issue 3: Build Cache
**Fix:** Delete bin and obj folders, then rebuild

---

## 💡 **Quick Test:**

Try building the MinimalApp to verify Avalonia works:

1. In `Program.cs`, temporarily change line ~17 to:
```csharp
=> AppBuilder.Configure<MinimalApp>()
```

2. Build and run
3. If MinimalApp works, the problem is in MainWindowCodeBased
4. Change back to `<App>()` when done

---

## 🛠️ **Emergency: Start Fresh**

If nothing works, we can:
1. Create a brand new Avalonia project
2. Copy over just the working code
3. Start with MinimalApp and build up

---

## 📞 **Need Help?**

Tell me:
- ✅ The exact error message
- ✅ Which file has the error
- ✅ What line number
- ✅ Whether you're building or running

I'll fix it immediately!

---

**Created:** November 7, 2025  
**Purpose:** Troubleshooting build failures  
**Status:** Ready to help ✅

