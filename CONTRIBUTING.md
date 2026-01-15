# Katkıda Bulunma Rehberi

Steam Connection Blocker projesine katkıda bulunmak istediğiniz için teşekkür ederiz! 

## Geliştirme Ortamı Kurulumu

### Gereksinimler

- Visual Studio 2022 (Community, Professional veya Enterprise)
- .NET 8.0 SDK veya daha yeni
- Windows 10/11
- Git

### Kurulum Adımları

1. Repository'yi fork edin
2. Fork'unuzu klonlayın:
   ```bash
   git clone https://github.com/YOUR-USERNAME/Steam-Connection-Blocker.git
   cd Steam-Connection-Blocker
   ```

3. Visual Studio'da `SteamConnectionBlocker.sln` dosyasını açın

4. **Önemli**: Projeyi yönetici haklarıyla çalıştırmanız gerekir:
   - Solution Explorer'da projeye sağ tıklayın
   - Properties > Debug > General > Open debug launch profiles UI
   - "Run as Administrator" seçeneğini işaretleyin

## Kod Standartları

### C# Kodlama Kuralları

- .NET kod standartlarına uyun
- Anlamlı değişken ve metod isimleri kullanın
- Her public metod için XML dokümantasyon yazın
- Hata yönetimi için try-catch blokları kullanın
- Async/await kullanın (UI thread'i bloklamayın)

### XAML Kuralları

- Tutarlı girinti kullanın (4 boşluk)
- Kaynakları `App.xaml` içinde tanımlayın
- Binding kullanırken property değişikliklerini düşünün

## Yeni Özellik Ekleme

1. Yeni bir branch oluşturun:
   ```bash
   git checkout -b feature/your-feature-name
   ```

2. Değişikliklerinizi yapın

3. Değişikliklerinizi test edin:
   - Uygulamayı çalıştırın
   - Tüm fonksiyonları test edin
   - Farklı senaryoları deneyin

4. Commit edin:
   ```bash
   git add .
   git commit -m "feat: Add your feature description"
   ```

5. Push edin:
   ```bash
   git push origin feature/your-feature-name
   ```

6. Pull Request açın

## Commit Mesajları

Commit mesajlarınızı şu formatta yazın:

```
type: description

[optional body]

[optional footer]
```

Tipler:
- `feat`: Yeni özellik
- `fix`: Hata düzeltme
- `docs`: Dokümantasyon
- `style`: Kod formatı
- `refactor`: Kod yeniden yapılandırma
- `test`: Test ekleme/düzeltme
- `chore`: Diğer değişiklikler

Örnekler:
```
feat: Add automatic Steam restart on block enable
fix: Fix Steam path detection for custom installations
docs: Update README with troubleshooting section
```

## Pull Request Süreci

1. Fork'unuzu güncel tutun:
   ```bash
   git remote add upstream https://github.com/Serdar8118/Steam-Connection-Blocker.git
   git fetch upstream
   git merge upstream/main
   ```

2. Pull Request açın ve şunları belirtin:
   - Ne değiştirdiniz?
   - Neden bu değişikliği yaptınız?
   - Test senaryonuz neydi?
   - Ekran görüntüsü (UI değişiklikleri için)

3. Code review sürecini bekleyin

4. Gerekli değişiklikleri yapın

## Test Etme

### Manuel Test Checklist

Yeni özellik eklerken veya hata düzeltirken şunları test edin:

- [ ] Steam yüklü değilse uygun hata mesajı gösteriliyor mu?
- [ ] Steam yüklüyse doğru konumu algılıyor mu?
- [ ] Kurulum başarılı oluyor mu?
- [ ] Block/unblock düzgün çalışıyor mu?
- [ ] Steam yeniden başlatma çalışıyor mu?
- [ ] Kaldırma işlemi başarılı oluyor mu?
- [ ] Hata durumlarında uygun mesajlar gösteriliyor mu?
- [ ] Animasyonlar düzgün çalışıyor mu?
- [ ] UI responsive mi?

### Test Senaryoları

1. **İlk Kullanım**:
   - Uygulama ilk kez açılıyor
   - Kurulum yapılıyor
   - Block etkinleştiriliyor
   - Test oyunu oynayın

2. **Normal Kullanım**:
   - Block aç/kapa
   - Steam yeniden başlat
   - Firewall kontrolü

3. **Hata Senaryoları**:
   - Steam kapalıyken block etkinleştirme
   - Yönetici hakları olmadan çalıştırma
   - Steam klasik değil, alternatif konumdaysa

## Sorun Bildirimi

Bir hata buldunuz mu? Issue açın ve şunları belirtin:

- **Başlık**: Kısa ve açıklayıcı
- **Açıklama**: Hatayı detaylı anlatın
- **Adımlar**: Hatayı nasıl tekrarlayabiliriz?
- **Beklenen**: Ne olması gerekiyordu?
- **Gerçekleşen**: Ne oldu?
- **Ekran görüntüsü**: Varsa ekleyin
- **Sistem**: Windows sürümü, .NET sürümü

## Yardım ve Sorular

- Issue açabilirsiniz
- Discussions bölümünü kullanabilirsiniz

## Davranış Kuralları

- Saygılı olun
- Yapıcı geri bildirim verin
- Başkalarının çalışmalarını takdir edin
- Topluluk odaklı düşünün

Katkılarınız için teşekkürler! 🎉
