# Jeomatik.Kamu.ParselManager

Kamu ana uygulamasının parsel yönetim panelini sağlayan Windows Forms kontrol kitaplığıdır. Bağımsız bir çalıştırılabilir uygulama değildir; `Dashboard.Manager` kullanıcı kontrolünü ve `Kamu.ParselManager.dll` derlemesini sunar.

## İşlevler

- Seçili parselin bilgilerini ve kamulaştırma alan/bedel özetlerini gösterir.
- Malik ve mirasçıları, dava kayıtlarını, müştemilat ve mevsimlik ürünleri listeler.
- Parsel ve malik özelliklerini Türkçe kod listeleriyle PropertyGrid üzerinde düzenler.
- Veri okuma ve güncelleme işlemlerinde `Kamu.Data` içindeki `PresentationDataService` hizmetini kullanır.
- Liste çift tıklama olaylarını ana uygulamanın işlemlerine açar.

## Gereksinimler ve klasör düzeni

Windows, Visual Studio 2022 (.NET masaüstü geliştirme iş yükü) ve .NET Framework 4.7.2 hedefleme paketi gerekir. Proje aşağıdaki kardeş klasörlere bağlıdır:

```text
Kamu7/
├── Manager/                         # Bu depo
├── Kamu.Data/src/Kamu.Data/          # Kamu.Data.csproj
└── ObjectModel/                     # KamuObject.csproj (Kamu.Object)
```

`Kamu.Data` ve `Kamu.Object` kaynak projeleri göreli proje referanslarıyla kullanılır. Yalnızca bu depoyu klonlamak bütün bağımlılıkları sağlamaz. Uyumlu ObjectModel ve Kamu.Data kaynaklarını yukarıdaki konumlara yerleştirin, ardından `ParselManager.sln` çözümünü açın. Derleme çıktısı klasörlerini kaynak bağımlılığı olarak kopyalamak gerekmez.

Visual Studio Developer PowerShell üzerinden:

```powershell
MSBuild.exe ParselManager.sln /t:Build /p:Configuration=Release
```

Çıktı: `bin/Release/Kamu.ParselManager.dll`.

## Ana uygulamada kullanım

Ana uygulama `Dashboard.Manager` kontrolünü barındırır. Açık projenin `KamuDatabase.ConnectionInfo` nesnesiyle `SetConnection(connectionInfo)` çağrılır; ardından `FillListView(parselGlobalID, Manager.ListViewType.Tum)` seçili parseli yükler. `Clear` ilgili görünümü temizler. Veri erişimi için geçerli proje bağlantısı gereklidir.

```csharp
var panel = new Dashboard.Manager();
panel.SetConnection(connectionInfo);
panel.FillListView(parselGlobalID, Manager.ListViewType.Tum);
```

Kontrol işlemlerini Windows Forms UI iş parçacığında çağırın. `ListViewType.Kisi` için verilen kimlik kişi kimliğidir; bu görünüm parsel listelerini korur. `SetConnection`, eski proje seçimlerini ve kod açıklaması önbelleğini temizleyerek yeni projenin kod listelerini yükler.

## Yenileme ve düzenleme davranışı

- Liste yenilemelerinde satırlarla birlikte gruplar da temizlenir; çizim olayları tekrar bağlanmaz.
- Dava ve kamulaştırma listeleri mevcut kayıtlardaki türlere göre gruplanır. Kodların ardışık olması gerekmez; bilinmeyen kodlar hata açıklamasıyla görünür.
- Müştemilat ve mevsimlik malik sorguları bir yenileme boyunca kişi kimliğine göre paylaşılır. Kod açıklamaları bağlantı değişene kadar önbellekte tutulur.
- PropertyGrid kod seçimleri nesneler değiştirilmeden önce doğrulanır. Değiştirilmemiş eski kodlar korunur; geçersiz yeni seçimler reddedilir.
- Malik düzenlemelerinde yalnız ilgili kişi veya hisse kaydı güncellenir. Mirasçı görünümünde hisse bulunmayabilir.

## Regresyon kontrolleri

Kardeş projelerin uyumlu Release çıktıları hazırken, depo kökünden PowerShell ile:

```powershell
.\Tests\Run-RegressionTests.ps1
```

Betik Visual Studio MSBuild konumunu bulur, Manager projesini `BuildProjectReferences=false` ile Release olarak derler ve .NET Framework test programını çalıştırır. Kardeş projeleri yeniden derlemez; eksik veya eski çıktılar varsa önce çözümü normal şekilde derleyin. Test çıktıları `bin/Release` altına yazılır ve Git'e dahil edilmez.

Kontroller veritabanına bağlanmadan kişi görünümünde listelerin korunmasını, grupların temizlenmesini, başlık çizim davranışını, kod dönüştürmeyi ve doğrulama hatasında parselin kısmen değişmemesini sınar. Gerçek veritabanındaki okuma/yazma işlemleri ve ana uygulamadaki etkileşimler ayrıca entegrasyon kontrolü gerektirir.

## Kaynak dosyaları

| Dosya | Görev |
| --- | --- |
| `ParselManager.cs` | Veri yükleme, listeleme, özetler ve kontrol olayları |
| `ParselManager.Designer.cs`, `ParselManager.resx` | Windows Forms tasarımı ve kaynakları |
| `Manager.GridModel.cs` | PropertyGrid modelleri, kod listeleri ve Türkçe değer dönüştürücüleri |
| `Panel.cs` | Özet veri modeli ve `ListViewType` |
| `Properties/AssemblyInfo.cs` | Derleme kimliği ve sürüm bilgisi |
| `Tests/RegressionTests.cs`, `Tests/Run-RegressionTests.ps1` | Veritabanı gerektirmeyen regresyon kontrolleri ve çalıştırıcı |

Derleme çıktıları, IDE ayarları ve yerel kimlik bilgileri depoya dahil edilmez. Veritabanı ve kullanıcı verileri bu deponun parçası değildir.
