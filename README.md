# LibrarySystem

Detta projekt är ett konsolbaserat bibliotekssystem utvecklat i C# inom ramen för  
**Individuell Inlämningsuppgift – Del 1: OOP, Arv/Komposition & Algoritmer**.

------

## Funktionalitet

Systemet hanterar:
- Böcker
- Medlemmar
- Utlåning och återlämning

Via en konsolbaserad meny kan användaren:
- Visa och söka böcker (titel, författare, ISBN)
- Låna och returnera böcker
- Visa medlemmar
- Visa statistik (antal böcker, utlånade böcker, mest aktiva låntagare)
- Visa försenade lån och beräkna förseningsavgifter
- Avsluta applikationen

------

## Designval

Projektet använder **komposition** enligt alternativ B i uppgiften.

Ett `Library`-objekt ansvarar för att samordna:
- `BookCatalog` – hantering av böcker
- `MemberRegistry` – hantering av medlemmar
- `LoanManager` – hantering av utlåning

Sökfunktionalitet implementeras via interfacet `ISearchable`, vilket möjliggör
polymorf sökning.

Projektstrukturen justerades under utvecklingen för att uppnå en tydligare och
mer konsekvent solution-struktur.

---

## SOLID-principer

Projektet följer SOLID-principerna:

- **S**ingle Responsibility – varje klass har ett tydligt ansvar
- **O**pen/Closed – lätt att utöka via `ISearchable`
- **L**iskov Substitution – `ISearchable` kan användas polymorfiskt
- **I**nterface Segregation – interfacet är litet och fokuserat
- **D**ependency Inversion – `Library` arbetar mot abstraktioner

---

## Projektstruktur

- **LibrarySystem.Console**  
  Konsolapplikation (meny och användarinteraktion)

- **LibrarySystem.Core**  
  Affärslogik (Models och Services)

- **LibrarySystem.Tests**  
  Enhetstester med xUnit

---

## Köra applikationen

1. Öppna `LibrarySystem.sln` i Visual Studio
2. Sätt **LibrarySystem.Console** som Startup Project
3. Kör applikationen (▶)

---
## Hämta projektet

Projektet kan hämtas från GitHub på två sätt. Antingen kan projektet laddas ner som ZIP-fil genom att gå till GitHub-repot, klicka på Code → Download ZIP, packa upp filen och därefter öppna `LibrarySystem.sln` i Visual Studio.

Det går även att klona projektet via Git genom att använda följande kommando:  
git clone https://github.com/AigennA/LibrarySystem.git  

Efter kloning öppnas `LibrarySystem.sln` i Visual Studio, **LibrarySystem.Console** sätts som Startup Project och applikationen kan därefter köras.


## Tester

Projektet innehåller **44 enhetstester**, vilket överstiger minimikravet (10 tester).

Tester verifierar:
- Bok- och lånelogik
- Sökning via interface
- Statistikberäkningar
- Felhantering och edge cases

Kör tester i Visual Studio via:  
**Test → Kör alla tester**

Eller via terminal:
```bash
dotnet test

