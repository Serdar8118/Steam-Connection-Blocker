# Steam Connection Blocker

**Aile Kütüphanesi için Özel Çözüm**

Steam aile kütüphanesinde paylaşılan **hikaye modlu (story)** ve **tek oyunculu (single-player)** oyunları aynı anda oynamanızı sağlayan kullanışlı masaüstü uygulaması.

## 🎯 Amaç

Bu uygulama, Steam'in aile kütüphanesi kısıtlamasını aşmak için tasarlanmıştır. Normalde bir hesap çevrimiçi oynarken, aile kütüphanesini paylaşan diğer hesaplar aynı oyun oynayamaz. Bu uygulama ile:

- **Hikaye modlu oyunlar** (Story games)
- **Tek oyunculu oyunlar** (Single-player games)
- **Çevrimdışı oynanabilen oyunlar**

...aynı anda oynanabilir hale gelir.

> ⚠️ **Önemli**: Bu uygulama sadece çevrimdışı oynanabilen oyunlar içindir. Çevrimiçi çok oyunculu oyunlar çalışmayacaktır.

## 🎮 Özellikler

- **Tek EXE Dosyası**: Kurulum gerektirmez, doğrudan çalıştırılabilir
- **Taşınabilir**: USB bellek veya herhangi bir klasörden çalışır
- **Marka Logosu**: Özel logo ile profesyonel görünüm
- **Otomatik Steam Algılama**: Steam'in kurulum konumunu otomatik olarak bulur
- **Kolay Engelleme/Açma**: Tek tıkla Steam'in internet bağlantısını engelleyin veya açın
- **Güvenli Firewall Yönetimi**: Windows Firewall kurallarını güvenli bir şekilde yönetir
- **Modern Arayüz**: Basit ve kullanıcı dostu tasarım
- **Sorun Giderme Araçları**: Otomatik sorun tespit ve çözüm önerileri
- **Animasyonlar**: Akıcı geçişler ve yükleme animasyonları

## 📋 Gereksinimler

- Windows 10 veya Windows 11
- .NET 8.0 Runtime (tek EXE sürümü için gerekli değil)
- Yönetici (Administrator) izinleri
- Steam yüklü olmalıdır

## 🚀 Kurulum

### Tek EXE Sürümü (Önerilen)

**Kurulum gerektirmez!**

1. `SteamConnectionBlocker.exe` dosyasını indirin
2. İstediğiniz bir klasöre kopyalayın (USB bellek, masaüstü, vb.)
3. Dosyaya **sağ tıklayın** ve **"Yönetici olarak çalıştır"** seçin
4. Windows SmartScreen uyarısı çıkarsa "Daha fazla bilgi" > "Yine de çalıştır" seçin
5. Uygulama açıldığında "Kurulum Yap" butonuna tıklayın

> 💡 **Not**: Uygulama taşınabilirdir (portable). Herhangi bir klasörden çalıştırılabilir, sistem dosyalarına hiçbir şey yüklemez.

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
- Aile kütüphanesindeki hikaye/tek oyunculu oyunları oynamanıza izin verir

### Oyun Kontrolü

Engelleme yapılmadan önce:
1. Uygulama çalışan Steam oyunlarını kontrol eder
2. Açık oyun varsa, kullanıcıya hangi oyunların açık olduğunu gösterir
3. Kullanıcı oyunları kapatana kadar engelleme yapılmaz
4. Bu, oyun ilerlemesinin kaybolmasını önler

### Tek EXE Nasıl Oluşturuldu?

Uygulama .NET 8.0 ile geliştirilmiş ve self-contained single-file olarak publish edilmiştir:

```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true
```

Bu sayede:
- .NET Runtime dahil edilmiştir
- Tüm bağımlılıklar tek dosyada
- Kurulum gerektirmez
- Taşınabilir (portable)

## 💡 Kullanım Senaryoları

### Senaryo 1: Aile Üyeleri
- Aileden biri Steam'de çevrimiçi oyun oynuyor
- Siz aynı aile kütüphanesinden tek oyunculu bir oyun oynamak istiyorsunuz
- Steam Connection Blocker ile bağlantınızı engelleyip oyununuzu oynayabilirsiniz

### Senaryo 2: Hikaye Modlu Oyunlar
- Arkadaşınız competitive oyun oynuyor
- Siz paylaşılan kütüphaneden hikaye modlu bir oyun oynamak istiyorsunuz
- Her ikiniz de aynı anda oynayabilirsiniz

### Senaryo 3: Ofline İçerik
- Steam Workshop veya Market'e ihtiyaç duymayan oyunlar
- Tamamen offline oynanabilen single-player oyunlar
- Co-op local oyunlar

## 🎮 Hangi Oyunlar Çalışır?

### ✅ Çalışan Oyun Türleri

- **Hikaye Modlu (Story)**: The Witcher 3, Red Dead Redemption 2, God of War
- **Tek Oyunculu RPG**: Skyrim, Fallout 4, Cyberpunk 2077
- **Strateji**: Civilization, Total War (kampanya modu)
- **Puzzle/Platform**: Portal, Hollow Knight, Celeste
- **Sandbox (Offline)**: Terraria (solo)
- **Adventure**: Tomb Raider serisi, Uncharted benzeri oyunlar

### ❌ Çalışmayan Oyun Türleri

- **Online Multiplayer**: CS:GO, Dota 2, PUBG, Valorant
- **Always-Online**: Destiny 2, The Division, Diablo IV
- **MMO**: World of Warcraft, Final Fantasy XIV
- **Live Service**: Apex Legends, Fortnite
- **DRM Korumalı**: Her zaman internet isteyen oyunlar

## 📞 Destek

Uygulama hakkında sorularınız için:
- Uygulama içindeki "Sorun Giderme" menüsünü kullanın
- Yaygın sorunlar için bu README'yi kontrol edin

## 📄 Yasal Bilgilendirme

Bu uygulama, Steam'in aile kütüphanesi özelliğini kullanarak **tek oyunculu ve hikaye modlu oyunları** aynı anda oynamak için geliştirilmiştir. Uygulama:

- Steam hesaplarına zarar vermez
- Oyun dosyalarını değiştirmez
- Sadece yerel Windows Firewall ayarlarını kullanır
- Valve Anti-Cheat (VAC) sistemini tetiklemez (çevrimdışı oyunlar için)
- Kişisel kullanım amacıyla tasarlanmıştır

**Uyarı**: Çevrimiçi/multiplayer oyunlarda kullanmayın. Steam Kullanım Şartlarını ihlal edebilir.

---

**© 2026 Steam Connection Blocker** - Aile kütüphanesi için tasarlandı.
