# Doara – Spuštění vývojového prostředí (.NET 9 + Angular)

Tento návod slouží ke spuštění celé aplikace **Doara** (backend + frontend) v lokálním prostředí pomocí SQL Serveru v Dockeru a nástrojů .NET 9 a Angular.

---

## 🔧 Předpoklady

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet) nainstalovaný
- [Node.js (verze 18+)](https://nodejs.org/) + `npm`
- Angular CLI 19 (`npm install -g @angular/cli`)
- Docker (pro SQL Server)

---

## 1. Spuštění databáze v Dockeru

```bash
docker run --name Doara.sql -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=D0ara!sBest" -p 1433:1433 -d mcr.microsoft.com/mssql/server:latest
```

Zkontroluj, že kontejner běží:

```bash
docker ps
```

---

## 2. Nastavení připojení k databázi

Otevři a uprav následující soubory:

### 📁 `Doara.DbMigrator\appsettings.json`  
### 📁 `Doara.HttpApi.Host\appsettings.json`

```json
"ConnectionStrings": {
  "Default": "Server=localhost,1433;Database=Api;User Id=sa;Password=D0ara!sBest;TrustServerCertificate=True"
}
```

---

## 3. Spuštění migrací

Ve složce `src/Doara.DbMigrator` spusť migrace databáze:

```bash
dotnet run
```

---

## 4. Nastavení backendu (pro frontend komunikaci)

Ve `Doara.HttpApi.Host\appsettings.json` uprav sekci `App` a `AuthServer`:

```json
"App": {
  "SelfUrl": "https://localhost:44346",
  "ClientUrl": "http://localhost:4200",
  "CorsOrigins": "https://*.Doara.com,http://localhost:4200",
  "RedirectAllowedUrls": "http://localhost:4200"
},
"AuthServer": {
  "Authority": "https://localhost:44346",
  "RequireHttpsMetadata": false,
  "SwaggerClientId": "Doara_Swagger"
}
```

---

## 5. Spuštění backendu

Ve složce `src/Doara.HttpApi.Host`:

```bash
dotnet run
```

Aplikace poběží na: [https://localhost:44346](https://localhost:44346)

---

## 6. Spuštění frontendu (Angular 19)

Přejdi do složky frontendové aplikace (např. `frontend/`):

```bash
npm install
ng serve
```

Frontend běží na: [http://localhost:4200](http://localhost:4200)

---

## ✅ Hotovo!

Frontend i backend jsou nyní propojené a připravené pro vývoj/testování.

---


---

## 🧪 Testování backendu

Testy jsou psány pomocí [xUnit](https://xunit.net/).

### Spouštění testů v JetBrains Rider

Testy se spouští samostatně pro každý modul. Otevři příslušný modul (např. `Doara.Ucetnictvi.Tests` nebo `Doara.Sklady.Tests`) a spusť testy:

1. Otevři soubor s testy nebo klikni pravým tlačítkem na název projektu v Solution Exploreru.
2. Zvol `Run All Tests` nebo konkrétní `Run <TestName>`.

Rider automaticky detekuje testovací framework a nabízí nástroje pro filtraci, ladění i opakování testů.

Testy jsou rozděleny dle modulů:
- `Doara.Ucetnictvi.Tests`
- `Doara.Sklady.Tests`


---

## 🧪 Testování frontendu (Angular)

Pro spuštění unit testů frontendu použij příkaz:

```bash
cd angular
ng test
```

Testy se otevřou v prohlížeči (výchozí je Karma + Jasmine). Výsledky se zobrazí živě a aktualizují se při každé změně souboru.

> Před spuštěním testů se ujisti, že máš nainstalované závislosti (`npm install`).
---

---

### 🔐 Přihlášení do Swaggeru

Pro otestování zabezpečených endpointů v Swagger UI se přihlaš pomocí následujících údajů:

- **Uživatelské jméno:** `admin`  
- **Heslo:** `1q2w3E*`  
- **Tenant:** _není potřeba vyplňovat (nechat prázdné)_

> Po přihlášení klikni vpravo nahoře na tlačítko `Authorize` a vyplň údaje. Token se automaticky přidá do všech požadavků.


## 📝 Poznámky

- Pokud používáš Visual Studio, ujisti se, že `Doara.HttpApi.Host` má nastavený SSL port `44346`.
- Defaultní login se nastavuje v ABP UI při prvním běhu (např. admin@admin.com).
- Certifikáty mohou být při prvním spuštění nedůvěryhodné – v prohlížeči je třeba je ručně povolit.