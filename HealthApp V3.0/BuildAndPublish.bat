@echo off
echo ===============================================
echo Building HealthApp V3.0 with Executable
echo ===============================================
echo.

cd /d "%~dp0"

echo Step 1: Cleaning previous builds...
dotnet clean
echo.

echo Step 2: Building the project...
dotnet build -c Debug
echo.

echo Step 3: Publishing to create standalone executable...
dotnet publish -c Debug -r win-x64 --self-contained false -o ".\published"
echo.

echo ===============================================
echo Checking for executable files...
echo ===============================================
echo.

if exist "published\HealthApp V3.0.exe" (
    echo *** SUCCESS! ***
    echo.
    echo Executable found at:
    echo %CD%\published\HealthApp V3.0.exe
    echo.
    dir "published\HealthApp V3.0.exe"
    echo.
    echo Opening folder...
    explorer "published"
    echo.
    echo Starting application automatically in 3 seconds...
    timeout /t 3 /nobreak >nul
    echo.
    echo Launching Health App...
    start "" "published\HealthApp V3.0.exe"
    echo.
    echo App is now running! You should see the Health App window.
    echo.
) else (
    echo *** EXECUTABLE NOT FOUND IN PUBLISHED FOLDER ***
    echo.
    echo Checking bin\Debug\net9.0...
    if exist "bin\Debug\net9.0\HealthApp V3.0.exe" (
        echo Found in bin folder!
        echo %CD%\bin\Debug\net9.0\HealthApp V3.0.exe
        explorer "bin\Debug\net9.0"
    ) else (
        echo NOT FOUND in bin folder either.
        echo.
        echo Files that were created:
        dir /b "bin\Debug\net9.0\HealthApp*.*"
        echo.
        echo All DLL files:
        dir /b "bin\Debug\net9.0\*.dll" | findstr /i "health"
    )
)

echo.
echo Press any key to exit...
pause > nul

