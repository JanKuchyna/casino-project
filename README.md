# Casino

Webová aplikace pro sportovní sázky a kasino hry. ASP.NET Core MVC, .NET 10, EF Core, SQL Server.

## Struktura

- `Casino.Domain` – entity a rozhraní
- `Casino.Application` – aplikační logika
- `Casino.Infrastructure` – databáze, repozitáře, migrace
- `Casino.Web` – controllery a views

## Spuštění

```powershell
dotnet ef database update --project Casino.Infrastructure --startup-project Casino.Web
dotnet run --project Casino.Web
```

Aplikace běží na http://localhost:5147.

## Nová migrace

```powershell
dotnet ef migrations add <Nazev> --project Casino.Infrastructure --startup-project Casino.Web
```
