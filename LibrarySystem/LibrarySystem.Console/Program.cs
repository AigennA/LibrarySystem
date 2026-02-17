using LibrarySystem.Core.Models;
using LibrarySystem.Core.Services;

class Program
{
    static Library library = new Library();

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        InitializeTestData();

        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║      BIBLIOTEKSSYSTEM - MENY         ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");
            Console.WriteLine("  1. Visa alla böcker");
            Console.WriteLine("  2. Sök bok");
            Console.WriteLine("  3. Låna bok");
            Console.WriteLine("  4. Returnera bok");
            Console.WriteLine("  5. Visa medlemmar");
            Console.WriteLine("  6. Statistik");
            Console.WriteLine("  7. Försenade lån och avgifter");
            Console.WriteLine("  0. Avsluta\n");
            Console.Write("  Välj: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    ShowAllBooks();
                    break;
                case "2":
                    SearchBooks();
                    break;
                case "3":
                    BorrowBook();
                    break;
                case "4":
                    ReturnBook();
                    break;
                case "5":
                    ShowMembers();
                    break;
                case "6":
                    ShowStatistics();
                    break;
                case "7":
                    ShowOverdueLoansWithFees();
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("\n  Tack för att du använde bibliotekssystemet!");
                    Console.WriteLine("  Hejdå!\n");
                    break;
                default:
                    Console.WriteLine("\n  ✗ Ogiltigt val! Försök igen.");
                    break;
            }

            if (running && choice != "0")
            {
                Console.WriteLine("\n  Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }
    }

    static void InitializeTestData()
    {
        library.AddBook(new Book("978-91-0-012345-6", "Sagan om ringen", "J.R.R. Tolkien", 1954));
        library.AddBook(new Book("978-91-0-012345-7", "Hobbiten", "J.R.R. Tolkien", 1937));
        library.AddBook(new Book("978-91-0-012345-8", "Harry Potter och de vises sten", "J.K. Rowling", 1997));
        library.AddBook(new Book("978-91-0-012345-9", "1984", "George Orwell", 1949));
        library.AddBook(new Book("978-91-0-012346-0", "Brott och straff", "Fjodor Dostojevskij", 1866));

        library.AddMember(new Member("M001", "Anna Andersson", "anna@email.com"));
        library.AddMember(new Member("M002", "Bob Bengtsson", "bob@email.com"));
        library.AddMember(new Member("M003", "Cecilia Carlsson", "cecilia@email.com"));
    }

    static void ShowAllBooks()
    {
        Console.WriteLine("\n╔══════════════════════════════════════╗");
        Console.WriteLine("║          ALLA BÖCKER                 ║");
        Console.WriteLine("╚══════════════════════════════════════╝\n");

        var books = library.SortBooksByTitle();

        if (!books.Any())
        {
            Console.WriteLine("  Inga böcker finns i systemet.");
            return;
        }

        for (int i = 0; i < books.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {books[i].GetInfo()}");
        }

        Console.WriteLine($"\n  Totalt: {books.Count} böcker");
    }

    static void SearchBooks()
    {
        Console.WriteLine("\n╔══════════════════════════════════════╗");
        Console.WriteLine("║           SÖK BOK                    ║");
        Console.WriteLine("╚══════════════════════════════════════╝\n");
        Console.Write("  Sökterm (titel/författare/ISBN): ");
        string searchTerm = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            Console.WriteLine("\n  ✗ Söktermen kan inte vara tom.");
            return;
        }

        var results = library.SearchBooks(searchTerm);
        Console.WriteLine($"\n  Sökresultat ({results.Count} st):\n");

        if (!results.Any())
        {
            Console.WriteLine("  Inga böcker hittades.");
            return;
        }

        for (int i = 0; i < results.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {results[i].GetInfo()}");
        }
    }

    static void BorrowBook()
    {
        Console.WriteLine("\n╔══════════════════════════════════════╗");
        Console.WriteLine("║          LÅNA BOK                    ║");
        Console.WriteLine("╚══════════════════════════════════════╝\n");
        Console.Write("  Ange ISBN: ");
        string isbn = Console.ReadLine() ?? "";
        Console.Write("  Ange medlems-ID: ");
        string memberId = Console.ReadLine() ?? "";

        try
        {
            var loan = library.BorrowBook(isbn, memberId);
            Console.WriteLine($"\n  ✓ Boken \"{loan.Book.Title}\" har lånats ut till {loan.Member.Name}.");
            Console.WriteLine($"  Lånedatum: {loan.LoanDate:yyyy-MM-dd}");
            Console.WriteLine($"  Återlämningsdatum: {loan.DueDate:yyyy-MM-dd}");
            Console.WriteLine($"  Låneperiod: 14 dagar");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n  ✗ Fel: {ex.Message}");
        }
    }

    static void ReturnBook()
    {
        Console.WriteLine("\n╔══════════════════════════════════════╗");
        Console.WriteLine("║        RETURNERA BOK                 ║");
        Console.WriteLine("╚══════════════════════════════════════╝\n");
        Console.Write("  Ange ISBN: ");
        string isbn = Console.ReadLine() ?? "";

        try
        {
            library.ReturnBook(isbn);
            Console.WriteLine("\n  ✓ Boken har returnerats!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n  ✗ Fel: {ex.Message}");
        }
    }

    static void ShowMembers()
    {
        Console.WriteLine("\n╔══════════════════════════════════════╗");
        Console.WriteLine("║         ALLA MEDLEMMAR               ║");
        Console.WriteLine("╚══════════════════════════════════════╝\n");

        var members = library.GetAllMembers();

        if (!members.Any())
        {
            Console.WriteLine("  Inga medlemmar finns i systemet.");
            return;
        }

        foreach (var member in members)
        {
            Console.WriteLine($"  {member.GetMemberInfo()}");
            Console.WriteLine("  " + new string('-', 40));
        }

        Console.WriteLine($"\n  Totalt: {members.Count} medlemmar");
    }

    static void ShowStatistics()
    {
        Console.WriteLine("\n╔══════════════════════════════════════╗");
        Console.WriteLine("║           STATISTIK                  ║");
        Console.WriteLine("╚══════════════════════════════════════╝\n");

        Console.WriteLine($"  📚 Totalt antal böcker: {library.GetTotalBooks()}");
        Console.WriteLine($"  📖 Antal tillgängliga böcker: {library.GetTotalBooks() - library.GetBorrowedBooksCount()}");
        Console.WriteLine($"  📕 Antal utlånade böcker: {library.GetBorrowedBooksCount()}");

        var mostActive = library.GetMostActiveBorrower();
        if (mostActive != null)
        {
            var activeLoansCount = mostActive.GetActiveLoans().Count();
            Console.WriteLine($"  ⭐ Mest aktiv låntagare: {mostActive.Name} ({activeLoansCount} aktiva lån)");
        }

        var overdueLoans = library.GetOverdueLoans();
        Console.WriteLine($"  ⚠️  Antal försenade lån: {overdueLoans.Count}");

        if (overdueLoans.Any())
        {
            decimal totalFees = overdueLoans.Sum(l => l.CalculateLateFee());
            Console.WriteLine($"  💰 Total förseningsavgift: {totalFees:C}");
        }
    }

    static void ShowOverdueLoansWithFees()
    {
        Console.WriteLine("\n╔══════════════════════════════════════╗");
        Console.WriteLine("║    FÖRSENADE LÅN OCH AVGIFTER        ║");
        Console.WriteLine("╚══════════════════════════════════════╝\n");

        var overdueLoans = library.GetOverdueLoans();

        if (!overdueLoans.Any())
        {
            Console.WriteLine("  ✓ Inga försenade lån just nu!");
            return;
        }

        Console.WriteLine($"  Totalt {overdueLoans.Count} försenade lån:\n");

        decimal totalFees = 0m;
        foreach (var loan in overdueLoans.OrderByDescending(l => l.GetDaysOverdue()))
        {
            var fee = loan.CalculateLateFee();
            totalFees += fee;

            Console.WriteLine($"  • \"{loan.Book.Title}\"");
            Console.WriteLine($"    Låntagare: {loan.Member.Name}");
            Console.WriteLine($"    Förfallodatum: {loan.DueDate:yyyy-MM-dd}");
            Console.WriteLine($"    Dagar försenad: {loan.GetDaysOverdue()}");
            Console.WriteLine($"    Förseningsavgift: {fee:C}");
            Console.WriteLine("    " + new string('-', 40));
        }

        Console.WriteLine($"\n  💰 Total förseningsavgift: {totalFees:C}");
        Console.WriteLine($"  📊 Genomsnittlig avgift per lån: {(totalFees / overdueLoans.Count):C}");
    }
}