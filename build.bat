@echo off
echo Building Steam Connection Blocker...
echo.

REM Check if dotnet is installed
where dotnet >nul 2>nul
if %errorlevel% neq 0 (
    echo ERROR: .NET SDK not found!
    echo Please install .NET SDK from https://dotnet.microsoft.com/download
    pause
    exit /b 1
)

REM Build the project
echo Building in Release mode...
dotnet build SteamConnectionBlocker.sln -c Release

if %errorlevel% equ 0 (
    echo.
    echo ========================================
    echo Build completed successfully!
    echo ========================================
    echo.
    echo Output location:
    echo SteamConnectionBlocker\bin\Release\net8.0-windows\
    echo.
) else (
    echo.
    echo ========================================
    echo Build failed!
    echo ========================================
    echo.
)

pause
