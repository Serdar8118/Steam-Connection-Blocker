# Güvenlik Politikası / Security Policy

## Desteklenen Sürümler

Şu anda desteklenen sürümler:

| Sürüm | Destek Durumu |
| ------- | ------------------ |
| 1.0.x   | :white_check_mark: |

## Güvenlik Açığı Bildirme

Güvenlik açığı bulduysanız, lütfen **herkese açık issue açmayın**.

### Bildirme Adımları

1. **Özel olarak bildirin**: GitHub'ın Security Advisory özelliğini kullanın veya proje sahibine doğrudan ulaşın
2. **Detaylı açıklama**: Güvenlik açığını tekrar oluşturmak için gerekli adımları açıklayın
3. **Etki analizi**: Güvenlik açığının ne gibi etkileri olabileceğini belirtin

### Ne Bekleyebilirsiniz?

- **24 saat içinde**: İlk yanıt
- **7 gün içinde**: Güvenlik açığının değerlendirilmesi
- **30 gün içinde**: Yamaya (patch) veya geçici çözüme

### Sorumlu Açıklama

Güvenlik açığını bildirdikten sonra:
- Yama yayınlanana kadar açığı halka açıklamayın
- Açığı kötüye kullanmayın
- Makul test sınırları içinde kalın

## Güvenlik Özellikleri

Steam Connection Blocker'ın güvenlik yaklaşımı:

### ✅ Yaptığımız Güvenlik Önlemleri

- **Açık Kaynak**: Tüm kod GitHub'da açık ve incelenebilir
- **Minimal İzinler**: Sadece gerekli Windows Firewall API'sine erişir
- **Veri Toplama Yok**: Hiçbir kullanıcı verisi toplanmaz veya gönderilmez
- **Yerel İşlem**: Tüm işlemler bilgisayarınızda yapılır
- **Resmi API'ler**: Sadece Microsoft'un resmi Windows API'lerini kullanır
- **Yönetici İzni**: Firewall değişiklikleri için Windows'un güvenlik mekanizmasını kullanır

### 🔒 Kullanıcı Sorumlulukları

- Uygulamayı sadece **resmi GitHub releases** sayfasından indirin
- Uygulamayı yönetici haklarıyla çalıştırırken dikkatli olun
- Firewall kurallarını düzenli olarak kontrol edin
- Şüpheli davranış görürseniz bildirin

### 🚨 Güvenlik Açığı Kategorileri

Aşağıdaki kategorilerde güvenlik açığı bildirimi kabul edilir:

**Yüksek Öncelik**:
- Ayrıcalık yükseltme (privilege escalation)
- Uzaktan kod çalıştırma (remote code execution)
- Kritik veri sızıntısı

**Orta Öncelik**:
- Yerel ayrıcalık yükseltme
- Bilgi sızıntısı
- Servis reddi (DoS)

**Düşük Öncelik**:
- UI/UX güvenlik sorunları
- Konfigürasyon zayıflıkları

### ❌ Kapsam Dışı

Aşağıdakiler güvenlik açığı olarak kabul edilmez:

- Uygulamanın yönetici hakları gerektirmesi (tasarım gereği)
- Windows Firewall'un devre dışı bırakılabilmesi (Windows özelliği)
- Steam'in firewall kurallarını manuel değiştirebilme (Windows özelliği)
- Sosyal mühendislik saldırıları
- Fiziksel erişim gerektiren saldırılar

## Güvenlik Güncellemeleri

Güvenlik güncellemeleri:
- CHANGELOG.md'de açıklanır
- GitHub Security Advisory olarak yayınlanır
- Releases sayfasında belirtilir

## İletişim

Güvenlik sorunları için:
- GitHub Security Advisory (önerilen)
- GitHub Issues (kritik olmayan sorunlar için)

---

Güvenli kullanımlar! 🔒
