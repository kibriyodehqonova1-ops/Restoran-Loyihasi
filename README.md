# RestoranAt — Restoran boshqaruv tizimi

Restoran faoliyatini avtomatlashtirish uchun **ASP.NET Core MVC (.NET 9)** da yozilgan veb-ilova: taomlar menyusi, buyurtmalar, stol bron qilish, yetkazib berish (delivery), xodimlar va admin panel.

## Loyiha haqida

**RestoranAt** — restoran va kafelarning kundalik ish jarayonlarini avtomatlashtirish uchun yaratilgan zamonaviy veb-tizim. Loyiha C# tilida, ASP.NET Core texnologiyasi asosida mustaqil ishlab chiqilgan.

## Amalga oshirilgan ishlar

* **Ma'lumotlar bazasi dizayni:** taomlar, kategoriyalar, stollar, buyurtmalar va mijozlar o'rtasidagi bog'lanishlar loyihalashtirilgan.
* **Buyurtma va stol moduli:** zaldagi stollarni bron qilish, joyida ovqatlanish yoki olib ketish uchun buyurtmalar qabul qilish.
* **Kuhnya va yetkazib berish:** buyurtma holatlarini kuzatish (Tayyorlanmoqda, Yetkazilmoqda, Yakunlandi) va kuryerlik xizmati.
* **Admin panel:** menyu, xodimlar, mijozlar va buyurtmalarni boshqarish.
* **Docker:** loyihani `Dockerfile` va `docker-compose.yml` orqali bir buyruq bilan ishga tushirish.

## Texnologiyalar

C#, ASP.NET Core, Docker

## Imkoniyatlar

**Foydalanuvchi (Mijoz)**

* Menyu katalogi: qidiruv, filtrlash, kategoriyalar bo'yicha saralash
* Stolni bron qilish; onlayn tarzda bo'sh stollarni ko'rib chiqish va band qilish
* Sevimli taomlarni savatga qo'shish va buyurtma berish
* Buyurtmalar tarixi va yetkazib berish holatini kuzatish
* Profil, parolni o'zgartirish va bildirishnomalar

**Admin / Xodim (Admin, Manager, Waiter)**

* Taomlar, ingredientlar, kategoriyalar va stol raqamlarini CRUD boshqaruvi
* Kelib tushgan buyurtmalarni qabul qilish va ularning holatini o'zgartirish
* Stol bronlarini tasdiqlash yoki bekor qilish
* Dashboard statistikasi, kunlik savdo hisobotlari va moliyaviy tahlillar
* Xodimlar va kuryerlar faoliyatini nazorat qilish

**Biznes qoidalar (avtomatik servis):** mijoz faol bo'lishi, stol band qilingan vaqtda ziddiyat yuzaga kelmasligi tekshiriladi; buyurtma holatlari ketma-ketligi avtomatlashtirilgan.

## Texnologiyalar

| Qatlam | Texnologiya |
| --- | --- |
| Backend | C#, ASP.NET Core MVC (.NET 9), Areas (Admin / User) |
| Ma'lumotlar bazasi | SQL Server, Entity Framework Core 9 (Code First, migratsiyalar) |
| Autentifikatsiya | ASP.NET Core Identity, rollar: Admin, Manager, Waiter, User |
| Frontend | Razor Views, Bootstrap, JavaScript |
| Boshqa | Session (savat), `DocumentFormat.OpenXml`, Docker / docker-compose |

## Arxitektura

```
RestoranAt/
├── Areas/
│   ├── Admin/         # Admin panel: Controllers + Views
│   └── User/          # Foydalanuvchi bo'limi: Controllers + Views
├── Controllers/       # Umumiy: Home, Account (login/register)
├── Data/              # ApplicationDbContext, DbInitializer (demo ma'lumotlar)
├── Models/            # Entity'lar va ViewModel'lar
├── Services/          # BuyurtmaService, StolBronService, BildirishnomaService
├── Migrations/        # EF Core migratsiyalari
└── wwwroot/           # Statik fayllar

```



## Ishga tushirish

### 1-usul: Docker (tavsiya etiladi, istalgan tizimda)

Talab: Docker Desktop.

```bash
cp .env.example .env        # fayl ichidagi parollarni o'zgartiring
docker compose up --build

```

Ilova: [http://localhost:8080](http://localhost:8080). Birinchi ishga tushishda baza avtomatik yaratiladi (migratsiyalar) va demo ma'lumotlar yuklanadi.

### 2-usul: Lokal (Windows + SQL Server LocalDB)

Talab: .NET 9 SDK, SQL Server LocalDB (Visual Studio bilan birga o'rnatiladi).

* **Visual Studio:** `Restoran_Loyiha.sln` ni oching (papkani emas, aynan `.sln` faylni), yuqoridagi profilni **http** qilib **F5** bosing.


* **Windows:** Terminalda `dotnet run --launch-profile http` buyrug'ini bajaring.

Ilova [http://localhost:5030](http://localhost:5030) da ochiladi. Ishga tushganda migratsiyalar avtomatik qo'llanadi, Development muhitida demo ma'lumotlar ham yuklanadi.

Lokal sinash uchun admin paroli `appsettings.Development.json` da yozilgan (`Admin123!`). **Bu faqat lokal demo uchun**: serverga joylashda parolni `user-secrets` yoki environment (`AdminSettings__Password`) orqali o'zgartiring.

### Hisoblar

| Rol | Login | Parol |
| --- | --- | --- |
| Admin | `admin@restoran.uz` | lokal: `Admin123!` (Docker: `.env` dagi `ADMIN_PASSWORD`) |
| Menejer (demo) | `manager@restoran.uz` | `Manager@2025!` |
| Foydalanuvchi (demo) | `ali.karimov@client.uz` | `User@2025!` |

Demo hisoblar faqat namunaviy ma'lumotlar (`SeedDemoData`) yoqilganda yaratiladi. **Haqiqiy muhitda `SeedDemoData=false` qoldiring.**

## Ekran rasmlari

*Rasmlarni `docs/screenshots/` papkasiga qo'shing va shu yerga ulang:*

## Xavfsizlik bo'yicha eslatmalar

* Production maxfiy ma'lumotlari (admin paroli, DB paroli) repoda saqlanmaydi: `user-secrets` yoki environment o'zgaruvchilari orqali beriladi (`appsettings.Development.json` dagi parol faqat lokal demo uchun)
* POST amallarida `[ValidateAntiForgeryToken]`, rollarga asoslangan `[Authorize]`
* Parolni tiklash tokeni faqat Development muhitida to'g'ridan-to'g'ri ko'rsatiladi; productionda email xizmati ulanishi kerak (hozircha ulanmagan)

## Rivojlantirish rejasi

* [ ] Holatlarni (`"Tayyorlanmoqda"`, `"Yetkazilmoqda"` ...) `enum` ga o'tkazish
* [ ] Buyurtma mantiqini controllerlardan `OrderService` ga ko'chirish
* [ ] Stol bron qilishda bir vaqtda band qilishdan himoya (`RowVersion`)
* [ ] `DateTime.Now` → `DateTime.UtcNow`
* [ ] xUnit testlar (buyurtma va hisob-kitob qoidalari)
* [ ] Email yoki Telegram bot orqali bildirishnoma yuborish
* [ ] REST API qatlami (JWT + Swagger)
* [ ] GitHub Actions (build + test)
