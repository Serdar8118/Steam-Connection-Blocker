@echo off
echo Publishing Steam Connection Blocker...
echo.

REM Check if dotnet is installed
where dotnet >nul 2>nul
if %errorlevel% neq 0 (
    echo ERROR: .NET SDK not found!
    echo Please install .NET SDK from https://dotnet.microsoft.com/download
    pause
    exit /b 1
)

REM Publish the project as a single file
echo Publishing as single file executable...
dotnet publish SteamConnectionBlocker.sln -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true /p:EnableCompressionInSingleFile=true

if %errorlevel% equ 0 (
    echo.
    echo ========================================
    echo Publish completed successfully!
    echo ========================================
    echo.
    echo Output location:
    echo SteamConnectionBlocker\bin\Release\net8.0-windows\win-x64\publish\
    echo.
    echo The SteamConnectionBlocker.exe file is ready to distribute!
    echo.
) else (
    echo.
    echo ========================================
    echo Publish failed!
    echo ========================================
    echo.
)

pause
