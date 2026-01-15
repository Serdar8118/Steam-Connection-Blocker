# UI Design Specification

This document describes the visual design and layout of Steam Connection Blocker.

## Color Scheme

Based on Steam's official branding with modern dark theme:

| Element | Color Code | Usage |
|---------|------------|-------|
| Primary Background | `#1B2838` | Main window background |
| Secondary Background | `#2A475E` | Card/panel backgrounds |
| Accent Color | `#66C0F4` | Buttons, links, highlights |
| Success Color | `#4CAF50` | Success messages, active status |
| Error Color | `#F44336` | Error messages, blocked status |
| Warning Color | `#FF9800` | Warning messages |
| Text Primary | `#FFFFFF` | Main text |
| Text Secondary | `#B8B8B8` | Helper text, descriptions |

## Layout Structure

```
┌─────────────────────────────────────────────────┐
│  ███████████████████████████████████████████  │ ← Header (SecondaryColor)
│  █  Steam Connection Blocker             █  │   
│  █  Aile kütüphanesinde aynı anda oynayın █  │
│  ███████████████████████████████████████████  │
├─────────────────────────────────────────────────┤
│                                                 │
│  ┌─────────────────────────────────────────┐  │
│  │ Durum                                   │  │ ← Status Card
│  │ ● Steam bağlantısı AÇIK                │  │
│  │ Steam konumu: C:\...\Steam.exe         │  │
│  └─────────────────────────────────────────┘  │
│                                                 │
│  ┌─────────────────────────────────────────┐  │
│  │ Kontroller                              │  │ ← Control Card
│  │                                         │  │
│  │  ┌───────────────────────────────────┐ │  │
│  │  │  Steam Bağlantısını Engelle      │ │  │ ← Main Toggle Button
│  │  └───────────────────────────────────┘ │  │
│  │                                         │  │
│  │  [Kurulum Yap]  [Kaldır]              │  │ ← Action Buttons
│  └─────────────────────────────────────────┘  │
│                                                 │
│  ┌─────────────────────────────────────────┐  │
│  │ Sorun Giderme                           │  │ ← Troubleshooting Card
│  │  🔄 Steam'i Yeniden Başlat             │  │
│  │  🔍 Firewall Kurallarını Kontrol Et   │  │
│  │  📂 Steam Konumunu Yeniden Algıla     │  │
│  └─────────────────────────────────────────┘  │
│                                                 │
│  ┌─────────────────────────────────────────┐  │
│  │ ℹ️ Nasıl Kullanılır?                    │  │ ← Info Card
│  │ 1. "Kurulum Yap" ile...                │  │
│  │ 2. "Steam Bağlantısını Engelle"...    │  │
│  │ [...]                                   │  │
│  └─────────────────────────────────────────┘  │
│                                                 │
├─────────────────────────────────────────────────┤
│  Bildirim mesajı burada görünür              │ ← Footer
└─────────────────────────────────────────────────┘
```

## Component Specifications

### Window Properties
- **Title**: "Steam Connection Blocker"
- **Size**: 500x600 pixels
- **Resize**: CanMinimize only (fixed size)
- **Position**: Center screen
- **Background**: PrimaryColor (#1B2838)

### Header Section
- **Height**: Auto
- **Background**: SecondaryColor (#2A475E)
- **Padding**: 20px all sides
- **Title Font**: 24pt, Bold, White
- **Subtitle Font**: 12pt, Regular, TextSecondaryColor

### Status Card
- **Background**: SecondaryColor
- **Border Radius**: 10px
- **Padding**: 20px
- **Margin**: 15px bottom
- **Components**:
  - Title: "Durum", 16pt, Bold
  - Status Indicator: 12px circle (color changes with status)
  - Status Text: 14pt, changes with state
  - Path Text: 12pt, TextSecondaryColor, word wrap

### Control Card
- **Background**: SecondaryColor
- **Border Radius**: 10px
- **Padding**: 20px
- **Margin**: 15px bottom
- **Components**:
  - Title: "Kontroller", 16pt, Bold, 15px bottom margin
  - Toggle Button:
    - Padding: 30px horizontal, 15px vertical
    - Font: 16pt, Bold
    - Border Radius: 10px
    - Background: Changes (Red when blocking, Green when active)
    - Hover effect: 90% opacity
  - Action Buttons:
    - Padding: 20px horizontal, 10px vertical
    - Font: 14pt, Medium
    - Border Radius: 5px
    - Background: AccentColor
    - Grid: 2 columns, 5px gap

### Troubleshooting Card
- **Background**: SecondaryColor
- **Border Radius**: 10px
- **Padding**: 20px
- **Margin**: 15px bottom
- **Buttons**: Standard ModernButton style
  - 10px vertical margin between buttons
  - Full width
  - Emoji + text labels

### Info Card
- **Background**: SecondaryColor
- **Border Radius**: 10px
- **Padding**: 15px
- **Margin**: 15px bottom
- **Title**: 14pt, Bold, AccentColor
- **Content**: 11pt, TextSecondaryColor, 16px line height

### Loading Panel
- **Position**: Below main content
- **Visibility**: Collapsed by default
- **Components**:
  - Spinner: 40px circle, AccentColor, rotating animation
  - Text: TextSecondaryColor, 10px top margin

### Footer
- **Background**: SecondaryColor
- **Padding**: 10px
- **Text**: 12pt, TextSecondaryColor, centered, word wrap

## Button States

### Toggle Button (Main)
```
┌─────────────────────────────────────────┐
│  State: Not Installed                   │
│  Text: "Steam Bağlantısını Engelle"   │
│  Background: Gray (disabled)            │
│  Enabled: False                         │
└─────────────────────────────────────────┘

┌─────────────────────────────────────────┐
│  State: Installed & Not Blocked         │
│  Text: "Steam Bağlantısını Engelle"   │
│  Background: Red (#F44336)              │
│  Enabled: True                          │
└─────────────────────────────────────────┘

┌─────────────────────────────────────────┐
│  State: Blocked                         │
│  Text: "Bağlantıyı Aç"                │
│  Background: Green (#4CAF50)            │
│  Enabled: True                          │
└─────────────────────────────────────────┘
```

### Status Indicator Colors
- **● Green** (#4CAF50): Connection open, working normally
- **● Red** (#F44336): Connection blocked, offline mode
- **● Orange** (#FF9800): Not installed, setup needed

## Animations

### 1. Fade-In (Page Load)
```
Duration: 0.5 seconds
Effect: Opacity 0 → 1
Target: MainContent StackPanel
Trigger: Window loaded
```

### 2. Loading Spinner
```
Duration: 1 second (infinite loop)
Effect: Rotation 0° → 360°
Target: LoadingEllipse
Visual: Dashed stroke (8,4 pattern)
Trigger: ShowLoading() called
```

### 3. Button Hover
```
Duration: Instant
Effect: Opacity 100% → 80%
Target: All buttons
Trigger: Mouse hover
```

### 4. Notification Auto-Hide
```
Duration: 5 seconds
Effect: Text fade to empty
Target: NotificationText
Trigger: ShowNotification() called
```

## Notification Types

Visual feedback with color coding:

```
┌─────────────────────────────────────────────┐
│ ✓ Firewall kuralı başarıyla oluşturuldu!  │  ← Green (#4CAF50)
└─────────────────────────────────────────────┘

┌─────────────────────────────────────────────┐
│ ✗ Steam konumu bulunamadı!                 │  ← Red (#F44336)
└─────────────────────────────────────────────┘

┌─────────────────────────────────────────────┐
│ ⚠ Firewall kuralı mevcut ancak devre dışı │  ← Orange (#FF9800)
└─────────────────────────────────────────────┘

┌─────────────────────────────────────────────┐
│ ℹ Lütfen yönetici olarak çalıştırın       │  ← Blue (#66C0F4)
└─────────────────────────────────────────────┘
```

## Responsive Behavior

### ScrollViewer
- Activates when content exceeds window height
- Vertical scrolling only
- Auto-show scrollbar

### Button States
```
Loading Mode:
- All buttons disabled
- Loading spinner visible
- Text: "İşlem yapılıyor..."

Ready Mode:
- Buttons enabled based on install state
- No loading spinner
- Status updated

Error Mode:
- Buttons enabled for retry
- Error message in footer
- Status shows problem
```

## Typography

| Element | Font Family | Size | Weight | Color |
|---------|-------------|------|--------|-------|
| Window Title | Segoe UI | 24pt | Bold | White |
| Header Subtitle | Segoe UI | 12pt | Regular | TextSecondary |
| Card Titles | Segoe UI | 16pt | Bold | White |
| Body Text | Segoe UI | 14pt | Regular | White |
| Helper Text | Segoe UI | 12pt | Regular | TextSecondary |
| Info Text | Segoe UI | 11pt | Regular | TextSecondary |
| Buttons | Segoe UI | 14-16pt | Medium/Bold | White |

## Accessibility

- **High Contrast**: All text has minimum 4.5:1 contrast ratio
- **Font Sizes**: Readable at 1080p resolution
- **Button Sizes**: Minimum 44x44px touch targets
- **Status Indicators**: Both color AND text (not color-only)
- **Keyboard Navigation**: Tab order follows logical flow

## User Interaction Flow

```
1. Window Opens
   ↓
   [Fade-in animation]
   ↓
2. Status Detection
   ↓
   [Loading spinner]
   ↓
3. Display Status
   ↓
   [Status card updates with colors]
   ↓
4. User Clicks Button
   ↓
   [Button disabled, loading shown]
   ↓
5. Operation Completes
   ↓
   [Notification appears in footer]
   ↓
   [Auto-hide after 5 seconds]
   ↓
6. Return to Ready State
```

## Mobile/Tablet (Future)
Currently desktop only, but designed with responsive principles:
- Fixed width prevents awkward scaling
- Scrollable content area
- Touch-friendly button sizes
- Clear visual hierarchy

---

**Note**: All measurements and colors are defined in `App.xaml` as resources for easy theme customization.
