using LibrarySystem.Data;
using LibrarySystem.Data.Repositories;
using LibrarySystem.Web.Components;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlite("Data Source=library.db"));

// Add repositories
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();
    context.Database.EnsureCreated();

    if (!context.Books.Any())
    {
        context.Books.AddRange(
            new LibrarySystem.Core.Models.Book("978-91-0-012345-6", "Sagan om ringen", "J.R.R. Tolkien", 1954),
            new LibrarySystem.Core.Models.Book("978-91-0-012345-7", "Hobbiten", "J.R.R. Tolkien", 1937),
            new LibrarySystem.Core.Models.Book("978-91-0-012345-8", "Harry Potter och de vises sten", "J.K. Rowling", 1997),
            new LibrarySystem.Core.Models.Book("978-91-0-012345-9", "1984", "George Orwell", 1949),
            new LibrarySystem.Core.Models.Book("978-91-0-012346-0", "Brott och straff", "Fjodor Dostojevskij", 1866),
            new LibrarySystem.Core.Models.Book("978-91-29-06634-5", "Pippi Långstrump", "Astrid Lindgren", 1945),
            new LibrarySystem.Core.Models.Book("978-91-7001-765-0", "Män som hatar kvinnor", "Stieg Larsson", 2005),
            new LibrarySystem.Core.Models.Book("978-91-37-14199-5", "En man som heter Ove", "Fredrik Backman", 2012),
            new LibrarySystem.Core.Models.Book("978-91-0-056734-2", "Kallocain", "Karin Boye", 1940),
            new LibrarySystem.Core.Models.Book("978-91-0-072221-6", "Doktor Glas", "Hjalmar Söderberg", 1905)
        );

        context.Members.AddRange(
            new LibrarySystem.Core.Models.Member("M001", "Anna Andersson", "anna@email.com"),
            new LibrarySystem.Core.Models.Member("M002", "Bob Bengtsson", "bob@email.com"),
            new LibrarySystem.Core.Models.Member("M003", "Cecilia Carlsson", "cecilia@email.com"),
            new LibrarySystem.Core.Models.Member("M004", "David Danielsson", "david@email.com"),
            new LibrarySystem.Core.Models.Member("M005", "Eva Eriksson", "eva@email.com")
        );

        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
