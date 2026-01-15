# Logo Ekleme Rehberi

Bu rehber, Steam Connection Blocker uygulamasına özel logonuzu nasıl ekleyeceğinizi açıklar.

## 📁 Gerekli Dosyalar

### 1. logo.png
- **Boyut**: 800x800 piksel (kare)
- **Format**: PNG (şeffaf arka plan önerilir)
- **Kullanım Yerleri**:
  - Uygulama başlığı (80x80 olarak gösterilir)
  - Dokümantasyon
  
### 2. logo.ico
- **Format**: ICO (Windows icon)
- **İçermesi Gereken Boyutlar**:
  - 16x16 piksel (taskbar küçük görünüm)
  - 32x32 piksel (taskbar normal görünüm)
  - 48x48 piksel (klasör görünümü)
  - 256x256 piksel (yüksek çözünürlük)
- **Kullanım Yerleri**:
  - Windows taskbar
  - Pencere başlık çubuğu
  - Uygulama kısayolu
  - Alt+Tab penceresi

## 🔧 ICO Dosyası Oluşturma

### Yöntem 1: Online Araç (Kolay)
1. https://convertio.co/png-ico/ adresine gidin
2. logo.png dosyanızı yükleyin
3. "Convert" butonuna tıklayın
4. İndirilen dosyayı `logo.ico` olarak kaydedin

### Yöntem 2: Windows Paint
1. logo.png dosyasını Paint'te açın
2. Dosya > Farklı Kaydet > BMP resmi
3. Dosya uzantısını `.bmp`'den `.ico`'ya değiştirin
4. Kaydet

### Yöntem 3: ImageMagick (Gelişmiş)
```bash
# Birden fazla boyutu içeren ICO oluşturma
convert logo.png -define icon:auto-resize=256,48,32,16 logo.ico
```

### Yöntem 4: GIMP (Ücretsiz Profesyonel)
1. logo.png'yi GIMP'te açın
2. Image > Scale Image > 256x256 yapın
3. File > Export As > logo.ico
4. "Microsoft Windows Icon" formatı seçin
5. Tüm boyutları seçin (16, 32, 48, 256)

## 📂 Dosyaları Yerleştirme

Dosyaları şu konuma kopyalayın:
```
Steam-Connection-Blocker/
└── SteamConnectionBlocker/
    └── Resources/
        ├── logo.png (800x800 PNG)
        └── logo.ico (multi-size ICO)
```

### Adım Adım:
1. Proje klasörünü açın
2. `SteamConnectionBlocker` klasörüne girin
3. `Resources` klasörünü açın
4. `logo.png` dosyanızı buraya kopyalayın
5. `logo.ico` dosyanızı buraya kopyalayın

## 🏗️ Projeyi Derleme

Logoyu ekledikten sonra:

```bash
# Proje dizinine gidin
cd Steam-Connection-Blocker

# Projeyi yeniden derleyin
dotnet build -c Release

# Veya build.bat dosyasını çalıştırın
build.bat
```

## ✅ Kontrol Listesi

Logoyu eklemeden önce kontrol edin:

- [ ] logo.png dosyası 800x800 piksel
- [ ] logo.png PNG formatında
- [ ] logo.ico dosyası oluşturuldu
- [ ] logo.ico içinde 16, 32, 48, 256 boyutları var
- [ ] Her iki dosya da Resources klasöründe
- [ ] Dosya isimleri tam olarak "logo.png" ve "logo.ico"
- [ ] Proje yeniden derlendi

## 🎨 Logoda Görünüm

### Uygulama Başlığı
- Logo 80x80 piksel olarak görünür
- Uygulama adının üstünde merkezde
- Yüksek kalite ölçekleme ile keskin görünüm

### Pencere İkonu
- Taskbar'da görünür
- Alt+Tab'de görünür
- Pencere başlık çubuğunda görünür
- Uygulama kısayolunda görünür

## 🐛 Sorun Giderme

### "Logo görünmüyor"
1. Dosya isimlerinin doğru olduğundan emin olun
2. Dosyaların Resources klasöründe olduğunu kontrol edin
3. Projeyi tamamen yeniden derleyin: `dotnet clean && dotnet build`

### "İkon bulanık görünüyor"
1. ICO dosyasının birden fazla boyut içerdiğinden emin olun
2. Her boyut için ayrı ayrı optimize edilmiş görüntüler kullanın
3. 256x256 boyutunu yüksek kalitede hazırlayın

### "Build hatası"
1. Dosya yollarının doğru olduğunu kontrol edin
2. `.csproj` dosyasında Resources bölümünün olduğunu doğrulayın
3. Visual Studio'yu yeniden başlatın

## 📝 Logo Tasarım İpuçları

### Öneriler:
- **Basit tasarım**: Küçük boyutlarda da net görünür
- **Yüksek kontrast**: Koyu ve açık tema ile uyumlu
- **Kare format**: 1:1 oran en iyi sonucu verir
- **Şeffaf arka plan**: PNG'de alfa kanalı kullanın
- **Vektör kaynak**: Mümkünse SVG/AI'dan export edin

### Kaçınılması Gerekenler:
- Çok ince çizgiler (16x16'da kaybolur)
- Küçük yazılar (okunamaz olur)
- Çok fazla detay (karmaşık görünür)
- Dikdörtgen format (kesilir)

## 🚀 Sonuç

Logoyu ekledikten ve projeyi derledikten sonra:
1. Uygulamayı çalıştırın
2. Başlıkta logonun göründüğünü kontrol edin
3. Taskbar'da ikon'un göründüğünü kontrol edin
4. Her şey yolundaysa, release build yapabilirsiniz!

---

**Not**: Bu rehber içindeki placeholder logo.png dosyası 1x1 piksel şeffaf bir PNG'dir. Gerçek logonuzla değiştirmelisiniz.
