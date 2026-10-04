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
└── ObjectModel/bin/Release/          # Kamu.Object.dll
```

`Kamu.Data` kaynak projesi proje referansıyla, `Kamu.Object.dll` ise göreli dosya referansıyla kullanılır. Yalnızca bu depoyu klonlamak bütün bağımlılıkları sağlamaz. Önce uyumlu ObjectModel ve Kamu.Data projelerini hazırlayın, ardından `ParselManager.sln` çözümünü açın.

Visual Studio Developer PowerShell üzerinden:

```powershell
MSBuild.exe ParselManager.sln /t:Build /p:Configuration=Release
```

Çıktı: `bin/Release/Kamu.ParselManager.dll`.

## Ana uygulamada kullanım

Ana uygulama `Dashboard.Manager` kontrolünü barındırır. Açık projenin `KamuDatabase.ConnectionInfo` nesnesiyle `SetConnection(connectionInfo)` çağrılır; ardından `FillListView(parselGlobalID, Manager.ListViewType.Tum)` seçili parseli yükler. `Clear` ilgili görünümü temizler. Veri erişimi için geçerli proje bağlantısı gereklidir.

## Kaynak dosyaları

| Dosya | Görev |
| --- | --- |
| `ParselManager.cs` | Veri yükleme, listeleme, özetler ve kontrol olayları |
| `ParselManager.Designer.cs`, `ParselManager.resx` | Windows Forms tasarımı ve kaynakları |
| `Manager.GridModel.cs` | PropertyGrid modelleri, kod listeleri ve Türkçe değer dönüştürücüleri |
| `Panel.cs` | Özet veri modeli ve `ListViewType` |
| `Properties/AssemblyInfo.cs` | Derleme kimliği ve sürüm bilgisi |

Derleme çıktıları, IDE ayarları ve yerel kimlik bilgileri depoya dahil edilmez. Veritabanı ve kullanıcı verileri bu deponun parçası değildir.
