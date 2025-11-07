@echo off
echo.
echo ================================================
echo    HEALTH ^& FITNESS APP LAUNCHER
echo ================================================
echo.

cd /d "%~dp0"

echo [1/3] Checking build...
if not exist "bin\Debug\net9.0\HealthApp V3.0.dll" (
    echo Build not found. Building now...
    dotnet build
    if errorlevel 1 (
        echo.
        echo ERROR: Build failed!
        pause
        exit /b 1
    )
)

echo [2/3] Starting Health App...
echo.

start "" "dotnet" "run"

echo [3/3] App launched!
echo.
echo The Health ^& Fitness window should appear shortly.
echo If you don't see it, press Alt+Tab to find it.
echo.
echo This window will close in 5 seconds...
timeout /t 5 /nobreak >nul

exit

