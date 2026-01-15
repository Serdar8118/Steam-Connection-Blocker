# Resources Folder

Bu klasör uygulama kaynaklarını içerir.

## Gerekli Dosyalar

### logo.png
- Boyut: 800x800 piksel
- Format: PNG
- Kullanım: Uygulama başlığında ve yükleme ekranında

### logo.ico
- Format: ICO (multi-size: 16x16, 32x32, 48x48, 256x256)
- Kullanım: Windows uygulama ikonu (taskbar, başlık çubuğu)

## Dosyaları Ekleme

1. `logo.png` dosyanızı bu klasöre kopyalayın
2. `logo.ico` dosyanızı bu klasöre kopyalayın
3. Projeyi yeniden derleyin

## ICO Dosyası Oluşturma

PNG'den ICO'ya dönüştürmek için:
- Windows: Paint > Farklı Kaydet > ICO formatı
- Online: https://convertio.co/png-ico/
- ImageMagick: `convert logo.png -define icon:auto-resize=256,48,32,16 logo.ico`
