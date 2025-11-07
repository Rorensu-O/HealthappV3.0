@echo off
echo ============================================
echo Health App - ADMIN LAUNCHER
echo ============================================
echo.
echo Running as Administrator...
echo.

cd /d "%~dp0"

echo Starting app with error capture...
echo.

"published\HealthApp V3.0.exe" 2> error_log.txt

if exist error_log.txt (
    echo.
    echo Error log created. Contents:
    echo ----------------------------------------
    type error_log.txt
    echo ----------------------------------------
) else (
    echo No error log created - app may have started successfully
)

echo.
echo Press any key to exit...
pause > nul

