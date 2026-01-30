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

Sökfunktionalitet implementeras via interfacet `ISearchable` för polymorf sökning.

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

```bash
git clone https://github.com/AigennA/LibrarySystem.git
```

Öppna sedan `LibrarySystem.sln` i Visual Studio.


## Tester

Projektet innehåller **44 enhetstester** (minimikrav: 10).

Kör tester via terminal:
```bash
dotnet test
```

Testresultat:
```
Passed!  - Failed: 0, Passed: 44, Skipped: 0, Total: 44
```

