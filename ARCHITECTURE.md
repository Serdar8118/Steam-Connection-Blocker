# Mimari Genel Bakış / Architecture Overview

Bu belge Steam Connection Blocker uygulamasının teknik mimarisini açıklar.

## Genel Mimari

```
┌─────────────────────────────────────────────────────────┐
│                    User Interface (WPF)                  │
│  ┌───────────────────────────────────────────────────┐  │
│  │           MainWindow (XAML + Code-Behind)         │  │
│  │  - Status Display                                 │  │
│  │  - Control Buttons                                │  │
│  │  - Animations & Notifications                     │  │
│  └───────────────────────────────────────────────────┘  │
│                          ▼                               │
│  ┌───────────────────────────────────────────────────┐  │
│  │            Application Logic Layer                │  │
│  │         (MainWindow.xaml.cs)                      │  │
│  │  - Event Handlers                                 │  │
│  │  - UI State Management                            │  │
│  │  - Error Handling                                 │  │
│  └───────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────┘
                           ▼
┌─────────────────────────────────────────────────────────┐
│              Business Logic Layer                        │
│  ┌───────────────────────────────────────────────────┐  │
│  │              SteamManager Class                   │  │
│  │                                                   │  │
│  │  ┌─────────────────┐  ┌─────────────────┐       │  │
│  │  │ Steam Detection │  │ Firewall Mgmt   │       │  │
│  │  │                 │  │                 │       │  │
│  │  │ - WMI Query     │  │ - Create Rule   │       │  │
│  │  │ - Process List  │  │ - Delete Rule   │       │  │
│  │  │ - Path Fallback │  │ - Enable/Disable│       │  │
│  │  └─────────────────┘  └─────────────────┘       │  │
│  │                                                   │  │
│  │  ┌─────────────────────────────────────────────┐ │  │
│  │  │        Process Management                   │ │  │
│  │  │  - Steam Restart                            │ │  │
│  │  │  - Process Termination                      │ │  │
│  │  └─────────────────────────────────────────────┘ │  │
│  └───────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────┘
                           ▼
┌─────────────────────────────────────────────────────────┐
│              System Integration Layer                    │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐  │
│  │    WMI/CIM   │  │  PowerShell  │  │   Process    │  │
│  │              │  │              │  │   API        │  │
│  │ - Process    │  │ - Firewall   │  │              │  │
│  │   Query      │  │   Commands   │  │ - Start      │  │
│  │              │  │              │  │ - Stop       │  │
│  └──────────────┘  └──────────────┘  └──────────────┘  │
└─────────────────────────────────────────────────────────┘
                           ▼
┌─────────────────────────────────────────────────────────┐
│                    Windows OS                            │
│  ┌────────────────┐        ┌──────────────────────┐    │
│  │ Windows        │        │   Steam Application  │    │
│  │ Firewall       │◄───────│   (Steam.exe)        │    │
│  │                │ blocks │                      │    │
│  └────────────────┘        └──────────────────────┘    │
└─────────────────────────────────────────────────────────┘
```

## Katman Detayları

### 1. User Interface Layer (UI Katmanı)

**Teknolojiler**: WPF, XAML

**Bileşenler**:
- `App.xaml`: Uygulama kaynakları, stiller, renkler
- `MainWindow.xaml`: Ana pencere UI tanımı
- `MainWindow.xaml.cs`: UI event handler'ları

**Sorumluluklar**:
- Kullanıcı etkileşimlerini yakalama
- Durumu görselleştirme
- Animasyonları yönetme
- Kullanıcıya geri bildirim sağlama

### 2. Application Logic Layer (Uygulama Mantığı Katmanı)

**Dosya**: `MainWindow.xaml.cs`

**Sorumluluklar**:
- Buton tıklama event'lerini işleme
- UI durumunu güncelleme (enable/disable buttons)
- Loading göstergelerini yönetme
- Bildirimleri gösterme
- Async işlemleri koordine etme
- Hata yakalama ve kullanıcıya iletme

**Önemli Metodlar**:
- `InitializeAsync()`: Uygulama başlangıç kontrolü
- `Install_Click()`: Firewall kuralı kurulumu
- `ToggleBlock_Click()`: Engelleme/açma işlemi
- `UpdateUI()`: UI durumunu senkronize etme

### 3. Business Logic Layer (İş Mantığı Katmanı)

**Dosya**: `SteamManager.cs`

**Sorumluluklar**:
- Steam'i tespit etme
- Firewall kurallarını yönetme
- Steam processlerini yönetme
- PowerShell komutlarını çalıştırma
- WMI sorguları yapma

**Modüller**:

#### Steam Detection Module
```csharp
public static async Task<string?> DetectSteamPath()
```
- WMI query ile çalışan Steam processlerini arar
- Yoksa bilinen konumları kontrol eder
- Steam.exe yolunu döndürür

#### Firewall Management Module
```csharp
public static async Task<bool> IsFirewallRuleInstalled()
public static async Task<bool> IsFirewallRuleEnabled()
public static async Task<(bool, string)> InstallFirewallRule(string steamPath)
public static async Task<(bool, string)> UninstallFirewallRule()
public static async Task<(bool, string)> EnableBlock()
public static async Task<(bool, string)> DisableBlock()
```
- PowerShell ile Windows Firewall API'sine erişir
- Kuralları oluşturur/siler/aktifleştirir/devre dışı bırakır

#### Process Management Module
```csharp
public static async Task<(bool, string)> RestartSteam(string steamPath)
public static bool IsSteamRunning()
```
- Steam processlerini kapatır
- Steam'i yeniden başlatır

### 4. System Integration Layer (Sistem Entegrasyon Katmanı)

**Teknolojiler**: 
- `System.Management` (WMI)
- `System.Diagnostics` (Process)
- PowerShell

**Sorumluluklar**:
- Windows API'leri ile iletişim
- Firewall komutlarını çalıştırma
- Process yönetimi

## Veri Akışı

### Uygulama Başlangıcı

```
User → Uygulama Başlat
  ↓
MainWindow_Loaded()
  ↓
InitializeAsync()
  ↓
SteamManager.DetectSteamPath()
  ↓ (WMI Query)
Windows Process List
  ↓
Steam Path Return
  ↓
SteamManager.IsFirewallRuleInstalled()
  ↓ (PowerShell)
Windows Firewall Check
  ↓
UI Update
  ↓
User Sees Status
```

### Engelleme İşlemi

```
User → "Engelle" Button Click
  ↓
ToggleBlock_Click()
  ↓
Show Confirmation Dialog
  ↓ (User confirms)
Show Loading
  ↓
SteamManager.EnableBlock()
  ↓ (PowerShell)
Set-NetFirewallRule -Enabled True
  ↓ (Success)
SteamManager.RestartSteam()
  ↓
1. Stop Steam Processes
2. Wait 2 seconds
3. Start Steam.exe
  ↓
Update UI
  ↓
Show Success Notification
```

## Güvenlik Modeli

### Privilege Elevation

```
User (Normal) 
  ↓ Start App
UAC Prompt (app.manifest requireAdministrator)
  ↓ User Accepts
App Runs as Administrator
  ↓
Can modify Windows Firewall
```

**Manifest**: `app.manifest`
- `requestedExecutionLevel level="requireAdministrator"`
- Uygulama başlangıcında UAC prompt gösterir

### Firewall Rule Security

- **Direction**: Outbound (sadece giden bağlantıları engeller)
- **Program**: Sadece Steam.exe
- **Action**: Block
- **Scope**: Local machine
- **Profile**: All (Domain, Private, Public)

## Asenkron İşlem Modeli

Tüm uzun süren işlemler async/await ile yapılır:

```csharp
// UI Thread
private async void Button_Click(...)
{
    ShowLoading();
    
    // Task.Run ile background thread'e geçer
    var result = await SteamManager.SomeOperation();
    
    // UI Thread'e geri döner
    UpdateUI(result);
    HideLoading();
}
```

**Avantajları**:
- UI donmaz (responsive)
- Kullanıcı deneyimi iyileşir
- Arka plan işlemleri UI'yi bloklamaz

## Hata Yönetimi

Her katmanda hata yakalama:

```
User Action
  ↓
try {
    Business Logic
      ↓
    try {
        System API Call
    } catch {
        Return error message
    }
} catch {
    Show user-friendly error
}
```

**Örnek**:
```csharp
public static async Task<(bool success, string message)>
```
- Tuple return ile hem sonuç hem mesaj döndürür
- UI katmanı mesajı kullanıcıya gösterir

## Dependency Yapısı

```
MainWindow
    │
    ├─> SteamManager
    │       ├─> System.Management (WMI)
    │       ├─> System.Diagnostics (Process)
    │       └─> PowerShell (Firewall)
    │
    ├─> WPF Framework
    └─> .NET Runtime
```

**External Dependencies**:
- .NET 8.0
- WPF (Microsoft.NET.Sdk)
- System.Management.dll

## Thread Model

```
Main UI Thread
    ├─> Event Handlers (sync)
    │
    └─> Async Operations
            ├─> Task.Run (background thread)
            │       └─> System API Calls
            │
            └─> await (returns to UI thread)
```

## Performans Optimizasyonları

1. **Lazy Loading**: Uygulama başlangıcında sadece gerekli kontroller
2. **Async Operations**: Hiçbir işlem UI thread'i bloklamaz
3. **Caching**: Steam path bir kere bulunur, cache'lenir
4. **Timeout**: Process kill için 5 saniye timeout

## Genişletilebilirlik

Gelecek özellikler için hazır noktalar:

### 1. Multi-Language Support
```csharp
// App.xaml içinde
<Application.Resources>
    <ResourceDictionary Source="Resources/Strings.tr.xaml"/>
</Application.Resources>
```

### 2. Settings/Configuration
```csharp
public class AppSettings
{
    public string CustomSteamPath { get; set; }
    public bool AutoStartBlocking { get; set; }
    public string Language { get; set; }
}
```

### 3. Logging
```csharp
public static class Logger
{
    public static void Log(string message) { }
    public static void LogError(Exception ex) { }
}
```

### 4. Multiple Rules
```csharp
// Diğer uygulamalar için de kural
SteamManager.CreateCustomRule("Epic Games", "Epic.exe");
```

## Test Stratejisi

### Manuel Test Noktaları

1. **Steam Detection**
   - Steam açık
   - Steam kapalı
   - Steam farklı konumda

2. **Firewall Operations**
   - Kural oluşturma
   - Kural silme
   - Enable/Disable

3. **UI Responsiveness**
   - Loading animations
   - Button states
   - Error messages

4. **Edge Cases**
   - Steam.exe bulunamadı
   - Yönetici hakları yok
   - PowerShell disabled

## Bilinen Sınırlamalar

1. **Platform**: Sadece Windows 10/11
2. **Steam**: Tek bir Steam instance desteklenir
3. **Firewall**: Windows Firewall olması gerekir
4. **Admin Rights**: Her çalıştırmada UAC prompt

## Kaynak Tüketimi

- **Memory**: ~50-100 MB (WPF framework)
- **CPU**: %0-1 (idle), %5-10 (işlem sırasında)
- **Disk**: ~10-15 MB (self-contained), ~150 MB (with .NET)
- **Network**: Yok (sadece local WMI queries)

---

Bu mimari, bakımı kolay, genişletilebilir ve performanslı bir uygulama sağlar.
