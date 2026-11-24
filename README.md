# YMH219 - Speech App (Speech-to-Text ve Text-to-Speech)

Ses ile metin arasında iki yönlü dönüşüm yapan bir Windows Forms uygulaması. **Text-to-Speech** bölümünde yazılan metin sesli okunur ve okunan kelime metin üzerinde anlık olarak vurgulanır. **Speech-to-Text** bölümünde mikrofondan alınan konuşma metne dönüştürülür; her sonuç zaman damgası ve güven skoruyla listelenir.

Bu proje, YMH 219 Nesne Tabanlı Programlama dersi ek uygulama ödevlerinin **6. ödevidir**.

## Kullanılan Teknolojiler

| Teknoloji | Açıklama |
|---|---|
| C# | Programlama dili |
| .NET 8 (`net8.0-windows10.0.19041.0`) | Hedef çatı (Windows 10 2004 ve üzeri) |
| Windows Forms | Masaüstü arayüzü |
| [`System.Speech`](https://www.nuget.org/packages/System.Speech) 8.0.0 (NuGet) | `SpeechSynthesizer` ile metin okuma, `SpeechRecognitionEngine` ile SAPI konuşma tanıma |
| `Windows.Media.SpeechRecognition` (Windows SDK) | SAPI tanıyıcısı olmayan Windows 11 sürümlerinde dikte tanıma |
| GDI+ (`System.Drawing`) | Yuvarlak butonlar, kartlar ve durum göstergesi simgelerinin çizimi |
| Visual Studio 2022 / 2026 | Geliştirme ortamı |

## Kurulum ve Çalıştırma

### Gereksinimler
- Windows 10 (2004+) / Windows 11
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) veya daha yeni bir SDK
- Visual Studio 2022 (17.8+) veya Visual Studio 2026, **.NET masaüstü geliştirme** iş yüküyle
- Hoparlör/kulaklık (TTS) ve mikrofon (STT)

> Proje `RollForward=Major` ayarına sahiptir. Bilgisayarda .NET 8 çalışma zamanı yoksa daha yeni bir .NET masaüstü çalışma zamanında (ör. .NET 10) da çalışır. NuGet paketleri ilk derlemede otomatik indirilir.

### Visual Studio ile
1. Depoyu klonlayın:
   ```bash
   git clone https://github.com/bakibasbay/YMH219-SpeechApp.git
   ```
2. `YMH219-SpeechApp.sln` dosyasını Visual Studio ile açın.
3. **Derle > Çözümü Derle** (`Ctrl+Shift+B`), ardından **F5** ile çalıştırın.

### Komut satırı ile
```bash
cd YMH219-SpeechApp
dotnet run --project YMH219-SpeechApp
```

### Konuşma tanıma için Windows ayarları
Windows'un yerleşik tanıyıcıları **Türkçeyi desteklemez**. Speech-to-Text için desteklenen bir dilin (ör. İngilizce - ABD) konuşma paketi kurulu olmalıdır:

1. **Ayarlar > Saat ve dil > Dil ve bölge** bölümünde *İngilizce (Amerika Birleşik Devletleri)* dilini ekleyin (yoksa).
2. Dilin yanındaki **… > Dil seçenekleri > Konuşma tanıma** paketini indirin.
3. Windows (OneCore) altyapısı kullanılacaksa **Ayarlar > Gizlilik ve güvenlik > Konuşma > Çevrimiçi konuşma tanıma** seçeneğini açın.
4. **Ayarlar > Gizlilik ve güvenlik > Mikrofon** bölümünde *Masaüstü uygulamalarının mikrofonunuza erişmesine izin verin* seçeneğinin açık olduğundan emin olun.
5. Uygulamayı yeniden başlatın; dil, **Tanıma dili** listesinde görünür.

## Kullanım

### Text-to-Speech
1. **Text-to-Speech** sekmesinde metni yazın veya **Aç (.txt)** ile bir dosyadan yükleyin.
2. Sağ panelden sesi seçin; **Hız** ve **Ses düzeyi** kaydırıcılarını ayarlayın.
3. **▶ Oku** ile okumayı başlatın. Okunan kelime sarı zeminle vurgulanır, sağda kelime ve ilerleme yüzdesi görünür.
4. **❚❚ Duraklat / ▶ Devam Et**, **■ Durdur** ve **✕ Temizle** ile okumayı yönetin; **Kaydet (.txt)** ile metni kaydedin.

### Speech-to-Text
1. **Speech-to-Text** sekmesinde **Tanıma dili** listesinden dili seçin (yalnızca kurulu tanıyıcılar listelenir).
2. **● Dinlemeyi Başlat** ile konuşmaya başlayın. Konuşurken ara sonuç "Duyulan: …" satırında görünür.
3. Kesinleşen her ifade **Tanınan Metin** alanına eklenir ve **Sonuç Listesi**'nde `Zaman – Güven – Metin` olarak listelenir. Güveni %50'nin altındaki sonuçlar turuncu gösterilir.
4. **■ Durdur** ile dinlemeyi bitirin, **Sonuçları Kaydet (.txt)** ile listeyi kaydedin, **✕ Temizle** ile sıfırlayın.

## Özellikler

- **Text-to-Speech:** oku, duraklat / devam et, durdur, temizle
- Kurulu seslerden **ses seçimi**, **hız** (-10…+10) ve **ses düzeyi** (%0…%100) ayarı
- `SpeakProgress` olayı ile **okunan kelimenin vurgulanması**, ilerleme çubuğu ve yüzde bilgisi
- Metni **.txt olarak açma / kaydetme** (UTF-8; eski Türkçe ANSI/Windows-1254 dosyalar da doğru okunur)
- **Speech-to-Text:** dinlemeyi başlat / durdur, temizle; tanınan metnin metin alanına eklenmesi
- Sonuçların **zaman damgası ve güven skoruyla** `ListView`'da listelenmesi ve `.txt` olarak kaydedilmesi
- Kurulu tanıyıcılara göre **dil seçimi**; SAPI ve Windows altyapıları otomatik listelenir
- **Belirgin durum göstergesi:** *Hazır* (gri), *Okunuyor…* (yeşil, hoparlör simgesi), *Duraklatıldı* (turuncu), *Dinleniyor…* (kırmızı, **yanıp sönen mikrofon simgesi**). Simgeler GDI+ ile kodla çizilir; durum pencere başlığında da gösterilir.
- Okuma ve dinleme aynı anda yapılmaz (hoparlör sesinin mikrofona karışmaması için); butonlar duruma göre aktif/pasif olur
- **Hata yönetimi:** mikrofon yok / izin yok, tanıyıcı veya dil kurulu değil, çevrimiçi konuşma tanıma kapalı, ses aygıtı hatası, dosya hataları (izin, kilitli dosya, 2 MB sınırı) anlaşılır `MessageBox` ile bildirilir; beklenmeyen hatalar genel yakalayıcıyla gösterilir, uygulama kapanmaz
- Servisler `IDisposable`'dır; pencere kapanırken okuma durdurulur, mikrofon ve ses kaynakları serbest bırakılır

## Proje Yapısı ve Sınıf Tasarımı

```
YMH219-SpeechApp/
├── YMH219-SpeechApp.sln
├── README.md
├── screenshots/
└── YMH219-SpeechApp/
    ├── Program.cs                     # Giriş noktası, genel hata yakalama
    ├── MainForm.cs                    # Arayüz olayları ve durum yönetimi
    ├── MainForm.Designer.cs           # Kontrollerin yerleşimi
    ├── Models/
    │   ├── RecognitionResultEntry.cs  # Tanıma sonucu: metin, zaman, güven
    │   ├── VoiceOption.cs             # Ses listesi öğesi
    │   ├── LanguageOption.cs          # Dil listesi öğesi (dil + altyapı)
    │   ├── RecognizerKind.cs          # Sapi / Windows
    │   └── AppStatus.cs               # Hazır / Okunuyor / Duraklatıldı / Dinleniyor
    ├── Services/
    │   ├── TextToSpeechService.cs     # SpeechSynthesizer sarmalayıcısı
    │   ├── SpeechToTextService.cs     # Tanıma servisi (motor seçimi, olaylar)
    │   ├── IRecognizerEngine.cs       # Tanıma motorları için ortak arayüz
    │   ├── SapiRecognizerEngine.cs    # System.Speech (SAPI) motoru
    │   ├── WindowsRecognizerEngine.cs # Windows.Media.SpeechRecognition motoru
    │   ├── FileService.cs             # .txt kaydet / aç
    │   ├── ServiceException.cs        # Kullanıcıya gösterilecek hata
    │   └── WordProgressEventArgs.cs   # Okunan kelime bilgisi
    ├── Controls/
    │   ├── ModernButton.cs            # Yuvarlak köşeli buton
    │   ├── CardPanel.cs               # Kart görünümlü panel
    │   └── StatusIndicator.cs         # Renkli durum göstergesi
    └── UI/
        └── Theme.cs                   # Renkler ve çizim yardımcıları
```

| Sınıf | Sorumluluk |
|---|---|
| `TextToSpeechService` | `SpeechSynthesizer`'ı kapsüller: `Speak`, `Pause`, `Resume`, `Stop`, `Rate`, `Volume`, `SelectVoice`. `WordProgress`, `StateChanged`, `SpeakFinished` olaylarını yayınlar. `IDisposable`. |
| `SpeechToTextService` | Seçilen dile göre uygun motoru (`IRecognizerEngine`) oluşturur; `StartAsync`, `Stop` ve `Recognized`, `Hypothesized`, `ListeningChanged`, `ErrorOccurred` olaylarını sağlar. `IDisposable`. |
| `IRecognizerEngine` | SAPI ve Windows motorlarının ortak sözleşmesi (çok biçimlilik). |
| `SapiRecognizerEngine` | `SpeechRecognitionEngine` + `DictationGrammar` ile mikrofondan sürekli tanıma. |
| `WindowsRecognizerEngine` | `SpeechRecognizer.ContinuousRecognitionSession` ile sürekli dikte; gizlilik/mikrofon/ağ hatalarını anlaşılır mesajlara çevirir. |
| `RecognitionResultEntry` | Değiştirilemez sonuç nesnesi (`Text`, `Timestamp`, `Confidence`); `ToString()` kayıt biçimini üretir. |
| `FileService` | Metin ve sonuç dosyalarını okur/yazar; G/Ç hatalarını `ServiceException`'a çevirir. |
| `ServiceException` | Başlık + açıklama taşıyan, doğrudan `MessageBox`'ta gösterilebilen hata. |
| `MainForm` | Yalnızca arayüz: kullanıcı etkileşimini alır, servisleri çağırır, olaylara göre ekranı günceller. |

## Ekran Görüntüleri

### Text-to-Speech: okuma sırasında kelime vurgusu ve "Okunuyor…" göstergesi
![Text-to-Speech okunuyor](screenshots/screenshot1.png)

### Speech-to-Text: tanıma sonuçları zaman damgası ve güven skoruyla
![Speech-to-Text sonuç listesi](screenshots/screenshot2.png)

## Bilinen Sınırlamalar / Geliştirme Notları

- **Türkçe konuşma tanıma yoktur.** Ne SAPI (`System.Speech`) ne de Windows'un OneCore tanıyıcısı (`Windows.Media.SpeechRecognition`) Türkçe dikte desteği sunar. Uygulama İngilizce (ABD) gibi desteklenen ve kurulu bir dille kullanılmalıdır.
- **Altyapı seçimi ve gerekçesi:** Geliştirme bilgisayarında (Windows 11 26H2, Türkçe) yapılan incelemede `SpeechRecognitionEngine.InstalledRecognizers()` boş döndü; Windows'un dikte dilleri (`SpeechRecognizer.SupportedTopicLanguages`) de boştu. Windows 11 24H2 ile *Windows Konuşma Tanıma* kaldırıldığı için yeni sürümlerde SAPI masaüstü tanıyıcısı bulunmayabilir. Bu nedenle tanıma iki altyapılı tasarlandı: SAPI tanıyıcısı varsa (Windows 10 / eski Windows 11) `System.Speech`, yoksa konuşma paketi kurulduğunda Windows'un desteklenen modern API'si `Windows.Media.SpeechRecognition` kullanılır. Bu API için hedef çatı `net8.0-windows10.0.19041.0` olarak seçildi.
- Windows altyapısıyla dikte, **çevrimiçi konuşma tanıma** iznine ve internet bağlantısına ihtiyaç duyar; ayar kapalıysa uygulama nasıl açılacağını söyleyen bir uyarı gösterir.
- Bu bilgisayarda TTS için `System.Speech` yalnızca **Microsoft Zira Desktop (en-US)** sesini görebiliyor. Türkçe *Microsoft Tolga* sesi yalnızca Windows OneCore ses deposunda kurulu olduğu için SAPI tarafından listelenmez; Türkçe metinler İngilizce sesin aksanıyla okunur.
- Hız ve ses düzeyi değişiklikleri bir sonraki **Oku** işleminde uygulanır: SAPI, kuyruğa alınmış metnin ayarlarını okuma sırasında değiştirmez (okurken hız +10 yapıldığında okuma hızı değişmedi, yeni okumada yaklaşık 3 katına çıktı).
- Okuma sırasında kelime vurgusunun kaymaması için metin alanı salt okunur yapılır.
- Güven skoru tanıyıcıdan gelir; Windows altyapısı ham skor vermezse *Yüksek/Orta/Düşük* seviyesi yaklaşık yüzdeye (%90/%70/%40) çevrilir.
- Açılabilecek metin dosyası 2 MB ile sınırlıdır; kayıtlar UTF-8 (BOM'lu) olarak yazılır.
- Olası geliştirmeler: Vosk gibi çevrimdışı bir kütüphane ile Türkçe tanıma, Windows OneCore seslerinin (ör. Tolga) `Windows.Media.SpeechSynthesis` ile TTS'e eklenmesi.

## Öğrenci

**Muhammed Baki Başbay** – 250541016

YMH 219 Nesne Tabanlı Programlama – Ek Uygulama Ödevleri
