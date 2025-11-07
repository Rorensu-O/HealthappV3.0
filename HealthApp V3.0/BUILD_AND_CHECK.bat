@echo off
cd /d "%~dp0"

echo Cleaning...
dotnet clean

echo.
echo Restoring...
dotnet restore

echo.
echo Building...
dotnet build > build_output.txt 2>&1

echo.
echo Build complete. Checking for errors...
echo.

findstr /i "error" build_output.txt

if %errorlevel%==0 (
    echo.
    echo ERRORS FOUND! Full output:
    echo ==========================================
    type build_output.txt
    echo ==========================================
) else (
    echo.
    echo BUILD SUCCEEDED!
    echo.
    echo Running app...
    start "" dotnet run
)

pause

