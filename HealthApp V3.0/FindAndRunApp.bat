@echo off
echo Searching for HealthApp V3.0.exe...
echo.

cd /d "C:\Users\LAURE\RiderProjects\AvaloniaApplication3\HealthApp V3.0"

echo Building the project...
dotnet build -c Debug
echo.

if exist "bin\Debug\net9.0\HealthApp V3.0.exe" (
    echo SUCCESS! Found the executable at:
    echo C:\Users\LAURE\RiderProjects\AvaloniaApplication3\HealthApp V3.0\bin\Debug\net9.0\HealthApp V3.0.exe
    echo.
    echo File details:
    dir "bin\Debug\net9.0\HealthApp V3.0.exe"
    echo.
    echo Opening the folder...
    explorer "bin\Debug\net9.0"
    echo.
    echo Do you want to run the app? (Y/N)
    set /p choice=
    if /i "%choice%"=="Y" (
        start "" "bin\Debug\net9.0\HealthApp V3.0.exe"
    )
) else (
    echo ERROR: HealthApp V3.0.exe was NOT created!
    echo.
    echo Checking what files exist:
    dir "bin\Debug\net9.0\HealthApp*.*"
)

pause

