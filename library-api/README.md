# Gelismis Kutuphane Uygulamasi Mimari Notlari

- Uygulamada 3 ana katman bulunmaktadir

## Controller (Presentation Layer)

- Kullanici isteklerini alir ve uygun servis metodunu cagirir.
- HTTP isteklerini isleyerek JSON formatinda veri alir ve gonderir.

## Service (Business Logic Layer)

- Controller'dan gelen istekleri isleyerek gerekli islemleri yapar.
- Veritabanina erisim, is kurallari ve veri islemleri burada gerceklestirilir.
- Service katmani, Controller'dan bagimsiz olarak tasarlanir, bu sayede test edilebilirlik ve yeniden kullanilabilirlik artar.

## Repository (Data Access Layer)

- Veritabanina erisim ve veri islemlerini gerceklestiren katmandir.
- Veritabanindan veri cekme, ekleme, guncelleme ve silme islemleri burada yapilir.
- Repository, Service katmanindan bagimsiz olarak tasarlanir, bu sayede veri erisim katmanini degistirmek kolaylasir (ornegin, veritabanini
  degistirmek).

### Acikalamalar

- Her katmanda birbirinden bagimsiz olarak hazirlanan somut siniflar bulunur, ancak soyut siniflar (Interface) katmanlardan bagimsiz olusuturulur.
- Olusturulan Interface'ler iligi katmandaki ilgili sinifa implement edilir. Boylece o somut sinifi kullancak olan diger katmanlar sadece o
  interface'i taniyarak islem baslatir
- Bu siniflari diger katmanlarda kullanabilmek icin Dependency Injection (DI) kullanilir.
    - DI, siniflar arasindaki bagimliligi azaltarak daha esnek ve test edilebilir bir mimari olusturur.
    - DI sayesinde, bir sinifin ihtiyaci olan diger siniflarin nesneleri otomatik olarak olusturulur ve enjekte edilir, bu da kodun daha temiz ve
      bakimi kolay hale gelmesini saglar.
    - DI yapmak icin Program.cs dosyasinda ilgili interface ve somu siniflar arasindaki baglanti AddScoped ile tanimlanir
    - Ornegin;
```csharp
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
```
- Bu enjeksyion islemi sayesinde, Controller katmaninda sadece IBookService interface'ini kullanarak BookService sinifina erisebiliriz, ve Service katmaninda
  sadece IBookRepository interface'ini kullanarak BookRepository sinifina erisebiliriz. Bu sayede, Controller ve Service katmanlari birbirinden bagimsiz hale gelir ve
  kodun bakimi ve test edilmesi kolaylasir.


### YAPILACAKLAR (TODOS)
- [ ] DTO'larin olusturulmasi
- [ ] Controller'larda olusturulan endpointlerin yukardaki mimari kurallarina gore revize edilmesi, ve ya bastan tekrar yazilmasi. 
