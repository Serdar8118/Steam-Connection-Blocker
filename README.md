# Steam Connection Blocker

Windows 10/11 için Steam bağlantısını engelleyerek aile kütüphanesindeki oyunları aynı anda oynamanızı sağlayan modern bir masaüstü uygulaması.

## 🎮 Özellikler

- **Otomatik Steam Algılama**: Steam'in kurulum konumunu otomatik olarak bulur
- **Kolay Engelleme/Açma**: Tek tıkla Steam'in internet bağlantısını engelleyin veya açın
- **Güvenli Firewall Yönetimi**: Windows Firewall kurallarını güvenli bir şekilde yönetir
- **Modern Arayüz**: Basit ve kullanıcı dostu tasarım
- **Sorun Giderme Araçları**: Otomatik sorun tespit ve çözüm önerileri
- **Animasyonlar**: Akıcı geçişler ve yükleme animasyonları

## 📋 Gereksinimler

- Windows 10 veya Windows 11
- .NET 8.0 Runtime (veya SDK geliştirme için)
- Yönetici (Administrator) izinleri
- Steam yüklü olmalıdır

## 🚀 Kurulum

### Kullanıcılar için

1. En son sürümü [Releases](../../releases) sayfasından indirin
2. İndirilen dosyayı çalıştırın (Yönetici olarak çalıştırmanız gerekir)
3. Windows SmartScreen uyarısı çıkarsa "Daha fazla bilgi" > "Yine de çalıştır" seçin
4. Uygulama açıldığında "Kurulum Yap" butonuna tıklayın

### Geliştiriciler için

```bash
# Repository'yi klonlayın
git clone https://github.com/Serdar8118/Steam-Connection-Blocker.git
cd Steam-Connection-Blocker

# Projeyi derleyin
dotnet build

# Uygulamayı çalıştırın (Yönetici olarak)
dotnet run --project SteamConnectionBlocker
```

## 📖 Kullanım

### İlk Kurulum

1. Uygulamayı **yönetici olarak** çalıştırın
2. Uygulama Steam'i otomatik olarak algılayacaktır
3. **"Kurulum Yap"** butonuna tıklayarak Windows Firewall kuralını oluşturun
4. Kurulum tamamlandığında yeşil onay mesajı göreceksiniz

### Steam Bağlantısını Engelleme

1. **Steam'de açık oyun varsa kapatın** - Uygulama kontrol edecek
2. **"Steam Bağlantısını Engelle"** butonuna tıklayın
3. Onay penceresinde "Evet" seçin
4. Steam şimdi çevrimdışı modda çalışıyor - aile kütüphanesinden oyun oynayabilirsiniz!

### Bağlantıyı Tekrar Açma

1. **"Bağlantıyı Aç"** butonuna tıklayın
2. Steam normal moda dönecek ve internet bağlantısı kurulacak

### Kaldırma

1. **"Kaldır"** butonuna tıklayın
2. Onay penceresinde "Evet" seçin
3. Windows Firewall kuralı tamamen kaldırılacak

## 🛠️ Sorun Giderme

### Steam Bulunamadı

- Steam'in yüklü olduğundan emin olun
- **"Steam Konumunu Yeniden Algıla"** butonunu kullanın
- Manuel olarak kontrol edin: `C:\Program Files (x86)\Steam\Steam.exe`

### Firewall Kuralı Çalışmıyor

- **"Firewall Kurallarını Kontrol Et"** butonuna tıklayın
- Uygulamayı yönetici olarak çalıştırdığınızdan emin olun
- Windows Firewall'un aktif olduğundan emin olun

### Steam Kapanmıyor

- **"Steam'i Yeniden Başlat"** butonunu kullanın
- Gerekirse Task Manager'dan (Görev Yöneticisi) Steam'i manuel kapatın

## 🔧 Teknik Detaylar

### Nasıl Çalışır?

Uygulama Windows Firewall'da giden (outbound) bir kural oluşturur:

```powershell
New-NetFirewallRule -DisplayName "Steam Connection Block" -Direction Outbound -Program "C:\Program Files (x86)\Steam\Steam.exe" -Action Block
```

Bu kural etkinleştirildiğinde:
- Steam'in tüm giden internet bağlantıları engellenir
- Yerel ağ bağlantıları etkilenmez
- Steam çevrimdışı modda çalışır
- Aile kütüphanesi oyunları oynamanıza izin verir

### Oyun Kontrolü

Engelleme yapılmadan önce:
1. Uygulama çalışan Steam oyunlarını kontrol eder
2. Açık oyun varsa, kullanıcıya hangi oyunların açık olduğunu gösterir
3. Kullanıcı oyunları kapatana kadar engelleme yapılmaz
4. Bu, oyun ilerlemesinin kaybolmasını önler

## 🔐 Güvenlik

- Uygulama Windows Firewall API'sini kullanır
- Sadece Steam.exe için kural oluşturur
- Sistem dosyalarına dokunmaz
- Tüm işlemler geri alınabilir

## ⚠️ Uyarılar

- Bu uygulama sadece aile kütüphanesi paylaşımı için tasarlanmıştır
- Çevrimiçi çok oyunculu oyunlar çalışmayacaktır
- Steam Market, Workshop ve diğer çevrimiçi özellikler kullanılamaz
- Her kullanımdan sonra bağlantıyı açmayı unutmayın

## 🏗️ Geliştirme

### Proje Yapısı

```
SteamConnectionBlocker/
├── App.xaml              # Uygulama kaynakları ve stiller
├── App.xaml.cs           # Uygulama başlangıç kodu
├── MainWindow.xaml       # Ana pencere UI
├── MainWindow.xaml.cs    # Ana pencere mantığı
├── SteamManager.cs       # Steam ve Firewall yönetimi
└── app.manifest          # Yönetici izinleri
```

### Build

```bash
# Debug build
dotnet build

# Release build
dotnet build -c Release

# Publish (tek dosya)
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

## 📝 Lisans

Bu proje MIT lisansı altında lisanslanmıştır.

## 🤝 Katkıda Bulunma

1. Bu repository'yi fork edin
2. Yeni bir branch oluşturun (`git checkout -b feature/amazing-feature`)
3. Değişikliklerinizi commit edin (`git commit -m 'Add amazing feature'`)
4. Branch'inizi push edin (`git push origin feature/amazing-feature`)
5. Bir Pull Request açın

## 📧 İletişim

Sorular veya öneriler için issue açabilirsiniz.

## ⭐ Teşekkürler

Bu projeyi beğendiyseniz yıldız vermeyi unutmayın!