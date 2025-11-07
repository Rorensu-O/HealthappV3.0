@echo off
echo ============================================
echo Health App Diagnostic Tool
echo ============================================
echo.

cd /d "%~dp0"

echo Checking if app is running...
tasklist | findstr /i "HealthApp"
if %errorlevel%==0 (
    echo.
    echo App IS RUNNING! Look for window on your screen.
    echo Press Alt+Tab to find it.
    echo.
) else (
    echo App is NOT running.
    echo.
)

echo.
echo Starting app with visible console...
echo.

start /wait "" "published\HealthApp V3.0.exe"

echo.
echo App has closed.
echo.
pause

