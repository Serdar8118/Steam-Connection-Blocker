# Sık Sorulan Sorular (FAQ)

## Genel Sorular

### Bu uygulama ne işe yarar?

Steam Connection Blocker, Steam'in internet bağlantısını Windows Firewall üzerinden engelleyerek Steam'i çevrimdışı modda çalıştırmanızı sağlar. Bu sayede aile kütüphanesi paylaşımı yapan arkadaşınız çevrimiçi oynarken, siz de aynı kütüphanedeki tek oyunculu/hikaye modlu oyunları oynayabilirsiniz.

### Yasal mı?

Evet, tamamen yasal. Uygulama sadece bilgisayarınızın Windows Firewall ayarlarını değiştirir. Steam'in kendisine veya dosyalarına müdahale etmez.

### Güvenli mi?

Evet. Uygulama açık kaynak kodludur ve sadece Windows Firewall API'sini kullanır. Hiçbir veri toplamaz, internet bağlantısı gerektirmez (Steam algılama haricinde).

## Kurulum ve Kullanım

### Neden yönetici izni gerekiyor?

Windows Firewall kuralları oluşturmak ve değiştirmek için yönetici (Administrator) hakları gerekir. Bu bir Windows güvenlik özelliğidir.

### Steam'i algılamıyor, ne yapmalıyım?

1. Steam'in yüklü olduğundan emin olun
2. Steam'i bir kere çalıştırıp kapatın
3. "Steam Konumunu Yeniden Algıla" butonuna tıklayın
4. Hala bulamazsa, Steam'in yüklü olduğu konumu kontrol edin (genellikle `C:\Program Files (x86)\Steam`)

### Kurulum yapıldı ama çalışmıyor?

1. "Firewall Kurallarını Kontrol Et" butonuna tıklayın
2. Windows Firewall'un açık olduğundan emin olun
3. Windows Defender veya başka bir antivirüs uygulaması bloklayabilir
4. Uygulamayı yönetici olarak çalıştırdığınızdan emin olun

### Steam kapanmıyor?

Bazen Steam arka planda çalışmaya devam eder:
1. "Steam'i Yeniden Başlat" butonunu kullanın
2. Eğer hala kapanmazsa, Task Manager (Görev Yöneticisi - Ctrl+Shift+Esc) açıp "Steam" processini manuel kapatın

## Teknik Sorular

### Hangi oyunlarda çalışır?

✅ **Çalışır:**
- Tek oyunculu (single-player) oyunlar
- Hikaye modlu oyunlar
- Çevrimdışı oynanabilen oyunlar
- Co-op local oyunlar

❌ **Çalışmaz:**
- Çevrimiçi çok oyunculu (online multiplayer) oyunlar
- Always-online oyunlar
- Steam Cloud senkronizasyonu gereken oyunlar

### Aile kütüphanesi olmadan kullanabilir miyim?

Teknik olarak evet, ancak normal kullanımda Steam'in çevrimdışı moduna geçmek daha pratiktir. Bu uygulama özellikle aile kütüphanesi senaryosu için tasarlanmıştır.

### Birden fazla Steam hesabı varsa ne olur?

Uygulama bilgisayardaki tüm Steam.exe processlerini etkiler. Tüm Steam hesapları çevrimdışı olur.

### Steam güncellemesi olursa ne olur?

Firewall kuralı Steam.exe dosyasına bağlıdır. Steam kendini güncellese bile aynı .exe dosyasını kullandığı için kural çalışmaya devam eder.

### Oyun açılırken "Steam'e bağlanılamıyor" hatası alıyorum?

Bu normaldir ve beklenen davranıştır. Steam engelliyken:
1. Steam'i offline modda açın
2. Oyunu Steam'den başlatın
3. Çoğu oyun sorunsuz çalışacaktır
4. Bazı oyunlar ilk açılışta internet kontrolü yapabilir - bunlar çalışmayabilir

## Sorun Giderme

### "Firewall kuralı oluşturulamadı" hatası

1. Uygulamayı yönetici olarak çalıştırın
2. Windows Firewall'un açık ve çalışır durumda olduğunu kontrol edin
3. PowerShell çalıştırma politikasını kontrol edin
4. Antivirüs yazılımınızın PowerShell'i bloklamadığından emin olun

### Steam yeniden başlatılıyor ama çevrimiçi açılıyor

1. Önce engellemeyi etkinleştirin
2. Steam'i manuel kapatın (veya "Steam'i Yeniden Başlat" butonu)
3. 2-3 saniye bekleyin
4. Steam'i açın - artık çevrimdışı modda açılmalı

### Oyun başlatılmıyor

1. Steam'in offline modda olduğundan emin olun
2. Oyunun single-player/offline oynanabilir olduğunu kontrol edin
3. Oyun dosyalarını Steam'de "Verify Integrity" ile kontrol edin
4. Gerekirse bir kere engellemeyi kaldırıp oyunu güncelleyin

### Engellemeyi kaldırdım ama Steam hala çevrimdışı

1. "Bağlantıyı Aç" butonuna tıklayın
2. Steam'i kapatıp açın
3. Steam'de "Go Online" seçeneğini kullanın
4. "Firewall Kurallarını Kontrol Et" ile kuralın devre dışı olduğunu doğrulayın

## İleri Düzey

### Manuel olarak firewall kuralını nasıl kontrol ederim?

1. Windows Firewall with Advanced Security açın
2. Sol menüden "Outbound Rules" seçin
3. "Steam Connection Block" kuralını bulun
4. Sağ tıklayıp "Properties" ile detayları görebilirsiniz

### Komut satırından nasıl kontrol ederim?

PowerShell'i yönetici olarak açıp şu komutları kullanabilirsiniz:

```powershell
# Kuralı kontrol et
Get-NetFirewallRule -DisplayName "Steam Connection Block"

# Kuralı aktif et
Set-NetFirewallRule -DisplayName "Steam Connection Block" -Enabled True

# Kuralı devre dışı bırak
Set-NetFirewallRule -DisplayName "Steam Connection Block" -Enabled False

# Kuralı sil
Remove-NetFirewallRule -DisplayName "Steam Connection Block"
```

### Başka bir konumdaki Steam için nasıl kullanırım?

Uygulama otomatik algılama yapar, ancak manuel olarak eklemek isterseniz:

1. PowerShell'i yönetici olarak açın
2. Şu komutu kendi Steam konumunuzla çalıştırın:
```powershell
New-NetFirewallRule -DisplayName "Steam Connection Block" -Direction Outbound -Program "D:\Games\Steam\Steam.exe" -Action Block
```

### Portable Steam versiyonu ile çalışır mı?

Evet, Steam.exe dosyası neredeyse oradan algılar. Sadece en az bir kere çalıştırılmış olması gerekir.

## Diğer Sorular

### Neden bazen "İşlem yapılıyor..." mesajı uzun sürüyor?

- Steam'i yeniden başlatırken: 5-10 saniye normal
- Firewall kuralı oluştururken: 2-5 saniye normal
- Steam algılanırken (çalışmıyorsa): 3-5 saniye normal

Eğer 30 saniyeden fazla sürüyorsa, uygulamayı kapatıp yeniden (yönetici olarak) açmayı deneyin.

### Uygulamayı kaldırmak istersem ne yapmalıyım?

1. Önce "Kaldır" butonuna tıklayın (firewall kuralını siler)
2. Uygulamayı kapatın
3. Program dosyasını silin
4. İsterseniz Windows Firewall'dan "Steam Connection Block" kuralını manuel kontrol edin

### Kaynak kodu nerede?

GitHub'da: https://github.com/Serdar8118/Steam-Connection-Blocker

### Hata bildirmek veya öneride bulunmak istiyorum

GitHub'da Issue açabilirsiniz veya Pull Request gönderebilirsiniz. CONTRIBUTING.md dosyasını okuyun.

### Bu uygulamayı başka diller için nasıl çevirebilirim?

Şu anda sadece Türkçe destekleniyor. İngilizce veya başka dil desteği eklemek için:
1. `MainWindow.xaml` dosyasındaki text'leri kaynak dosyaya taşıyın
2. `App.xaml` içinde çoklu dil desteği ekleyin
3. Pull Request gönderin!

---

**Sorunuz burada yanıtlanmadı mı?** GitHub'da Issue açın!
