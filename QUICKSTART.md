# Hızlı Başlangıç Rehberi

Bu rehber Steam Connection Blocker'ı ilk kez kullanacak kullanıcılar içindir.

## 📦 Kurulum (5 dakika)

### Adım 1: İndirme

1. [Releases](../../releases) sayfasına gidin
2. En son sürümü bulun
3. İşletim sisteminize göre dosyayı indirin:
   - **64-bit Windows**: `SteamConnectionBlocker-win-x64.zip`
   - **32-bit Windows**: `SteamConnectionBlocker-win-x86.zip`

> **Not**: Çoğu modern bilgisayar 64-bit'tir. Emin değilseniz x64 sürümünü indirin.

### Adım 2: Çıkarma

1. İndirilen ZIP dosyasına sağ tıklayın
2. "Tümünü Ayıkla" veya "Extract All" seçin
3. İstediğiniz bir klasöre çıkarın (örn: `C:\Programs\SteamBlocker`)

### Adım 3: İlk Çalıştırma

1. Çıkardığınız klasörde `SteamConnectionBlocker.exe` dosyasını bulun
2. Dosyaya **sağ tıklayın**
3. **"Yönetici olarak çalıştır"** seçin

> ⚠️ **Önemli**: Uygulama her zaman yönetici olarak çalıştırılmalıdır!

### Adım 4: İlk Kurulum

Uygulama açıldığında:

1. Steam otomatik olarak algılanacak
2. "Steam konumu: C:\Program Files (x86)\Steam\Steam.exe" gibi bir mesaj göreceksiniz
3. **"Kurulum Yap"** butonuna tıklayın
4. Başarılı mesajını bekleyin: "Firewall kuralı başarıyla oluşturuldu!"

✅ Kurulum tamamlandı! Artık uygulamayı kullanabilirsiniz.

## 🎮 İlk Kullanım (2 dakika)

### Senario: Aile kütüphanesinden oyun oynayacaksınız

**Durum**: Arkadaşınız Steam'de çevrimiçi oyun oynuyor ve sizin aile kütüphanesi erişiminizi engelliyor.

**Çözüm**:

1. **Açık oyunları kapatın** - Steam'de oyun varsa önce kapatın
2. Steam Connection Blocker'ı açın
3. **"Steam Bağlantısını Engelle"** butonuna tıklayın
4. Eğer hala açık oyun varsa, uygulama sizi uyaracak ve hangi oyunların açık olduğunu gösterecek
5. Oyunları kapattıktan sonra tekrar deneyin
6. Çıkan onay penceresinde **"Evet"** deyin
7. Durum "Steam bağlantısı ENGELLENDİ" olarak değişecek

🎮 Artık Steam'den istediğiniz tek oyunculu oyunu başlatabilirsiniz!

### Oyun Bittikten Sonra

1. Steam Connection Blocker'ı açın
2. **"Bağlantıyı Aç"** butonuna tıklayın
3. Durum "Steam bağlantısı AÇIK" olacak
4. Steam artık normal modda çalışıyor

## 🔧 İlk Sorun Giderme

### Sorun: "Steam bulunamadı" hatası

**Çözümler**:
1. Steam'in yüklü olduğundan emin olun
2. Steam'i bir kere açıp kapatın
3. Uygulamada **"Steam Konumunu Yeniden Algıla"** butonuna tıklayın

### Sorun: "Yönetici izni gerekli" hatası

**Çözüm**:
- Uygulamaya sağ tıklayıp **"Yönetici olarak çalıştır"** seçin
- Windows 10/11'de UAC (Kullanıcı Hesabı Denetimi) onayı isteyecek, "Evet" deyin

### Sorun: Steam yeniden başlamıyor

**Çözüm**:
1. Task Manager açın (Ctrl+Shift+Esc)
2. "Processes" sekmesinde "Steam" processlerini bulun
3. Her birini seçip "End Task" (Görevi Sonlandır) tıklayın
4. Uygulamada **"Steam'i Yeniden Başlat"** butonuna tıklayın

### Sorun: Oyun "Steam'e bağlanamıyor" diyor

**Bu normal!** Steam çevrimdışı modda:
1. Steam'i açın
2. Oyunu Steam kütüphanenizden başlatın
3. Çoğu tek oyunculu oyun sorunsuz çalışacak
4. Bazı oyunlar internet gerektirebilir - bunlar çalışmayacaktır

## 📋 Günlük Kullanım İpuçları

### Kısayol Oluşturma

Daha kolay erişim için:
1. `SteamConnectionBlocker.exe` dosyasına sağ tıklayın
2. "Kısayol Oluştur" seçin
3. Kısayolu masaüstüne taşıyın
4. Kısayola sağ tıklayın > "Properties" > "Advanced"
5. "Run as administrator" işaretleyin > OK

Artık kısayola çift tıklayarak doğrudan yönetici olarak açabilirsiniz!

### Hızlı Kontrol

Firewall durumunu kontrol etmek için:
1. Uygulamayı açın
2. **"Firewall Kurallarını Kontrol Et"** butonuna tıklayın
3. Kuralın aktif mi pasif mi olduğunu göreceksiniz

### Uygulama Başlangıçta Açılsın mı?

Steam Connection Blocker otomatik başlamaz. Bunu istiyorsanız:
1. Windows + R tuşlarına basın
2. `shell:startup` yazıp Enter
3. Uygulamanın kısayolunu bu klasöre kopyalayın

> ⚠️ Not: Yönetici olarak çalışması gerektiği için her açılışta UAC onayı isteyecektir.

## 🎯 Hangi Oyunlar Çalışır?

### ✅ Çalışan Oyun Türleri

- **Tek oyunculu RPG'ler**: Witcher 3, Skyrim, Fallout 4
- **Strateji oyunları**: Civilization, Total War (kampanya)
- **Hikaye odaklı oyunlar**: Life is Strange, Detroit: Become Human
- **Sandbox oyunlar**: Minecraft (single-player), Terraria (solo)
- **Puzzle oyunlar**: Portal, Portal 2 (kampanya)

### ❌ Çalışmayan Oyun Türleri

- **Online multiplayer**: CS:GO, Dota 2, PUBG
- **Always-online**: Destiny 2, The Division
- **MMO'lar**: WoW, FFXIV
- **Bazı DRM'li oyunlar**: Her zaman internet isteyen oyunlar

## 📚 Daha Fazla Bilgi

- Detaylı açıklamalar için: [README.md](README.md)
- Sorun giderme için: [FAQ.md](FAQ.md)
- Geliştirici bilgileri: [CONTRIBUTING.md](CONTRIBUTING.md)

## 🆘 Yardım Gerekirse

1. [FAQ.md](FAQ.md) dosyasına bakın
2. [GitHub Issues](../../issues) sayfasında arayın
3. Yeni bir issue açın

---

**Keyifli oyunlar! 🎮**
