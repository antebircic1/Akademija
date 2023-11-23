**King ICT Akademija 2023**

Projekt u sklopu predavanja KING ICT Akademije - ".NET od 0 do Produkcije".

Tehnologije:

 - .NET7
 - MS SQL
 - ntity Framework Core (Code First)
 - FluentValidator (https://docs.fluentvalidation.net/en/latest/)
 - AutoMapper (https://automapper.org/)
 - MediatR (https://github.com/jbogard/MediatR)
 - Swagger (https://swagger.io/)

Pokretanje projekta
Preduvjeti:

Instaliran .NET 7
Instaliran MS SQL Server
Nakon što smo klonirali projekt potrebno je:

Provjeriti konekcijski string na bazu podataka (WebApi/appsettings.json)
Provjeriti dali se projekt uredno builda i pokreće
U Visual Studiu otvoriti "Package Manager Console" i pokrenuti migracije na bazi naredbom "Update-Database"
Provjeriti da se kreirala baza podataka "Akademija2023" i da sadrži tablice "dbo._EFMigrationsHistory" i "dbo.Test"