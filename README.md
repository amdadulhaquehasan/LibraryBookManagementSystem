# 📚 LibraryBookManagementSystem
 
## Book Catalog Module (ASP.NET Core MVC)

---

## 📌 Project Overview

This project is a layered ASP.NET Core MVC application implementing a **Book Management Module** for a library system.

It demonstrates:

- Layered Architecture
- Repository Pattern
- Dependency Injection
- Entity Framework Core (Code First)
- Custom Exception Handling (Local & Global)
- Validation (Client & Server Side)
- Professional UI with Bootstrap

---

## 🏗️ Architecture

The project follows a **3-Layer Architecture**:

### 🔹 Project Structure

```
LibraryBookManagementSystem
│
├── Library.App.Web
│   ├── Connected Services
│   ├── Dependencies
│   ├── Properties
│   ├── wwwroot
│   │
│   ├── Controllers
│   │   ├── BookController.cs
│   │   ├── ErrorController.cs
│   │   └── HomeController.cs
│   │
│   ├── Models
│   │
│   ├── Views
│   │   ├── Book
│   │   │   ├── Create.cshtml
│   │   │   ├── Details.cshtml
│   │   │   ├── Edit.cshtml
│   │   │   └── Index.cshtml
│   │   │
│   │   ├── Error
│   │   │   └── ServerError.cshtml
│   │   │
│   │   ├── Home
│   │   │
│   │   ├── Shared
│   │   ├── _ViewImports.cshtml
│   │   └── _ViewStart.cshtml
│   │
│   ├── appsettings.json
│   └── Program.cs
│
├── Library.Domain
│   ├── Dependencies
│   │
│   ├── Entity
│   │   └── Book.cs
│   │
│   ├── Enum
│   │   └── Genre.cs
│   │
│   └── Exceptions
│       ├── BookNotFoundException.cs
│       ├── DuplicateIsbnException.cs
│       └── InvalidPublicationYearException.cs
│
├── Library.Infrastructure
│   ├── Dependencies
│   │
│   ├── Data
│   │   └── LibraryBookDbContext.cs
│   │
│   ├── Migrations
│   │
│   └── Repositories
│       ├── Interface
│       │   └──IBookRepository.cs
│       │
│       └── BookRepository.cs
│
└── LibraryBookManagementSystem.sln

```


### 🔹 1. Library.AppWeb
- ASP.NET Core MVC
- Controllers & Views
- Global Exception Handling
- UI with Bootstrap

### 🔹 2. Library.Domain
- Domain Entities
- Enums
- Custom Domain Exceptions

### 🔹 3. Library.Infrastructure
- EF Core DbContext
- Repository Implementation
- Database Configuration

---

## 📖 Main Entity: Book

```csharp
public class Book
{
    public int Id { get; set; }

    [Required]
    [StringLength(150, MinimumLength = 3)]
    public string Title { get; set; }

    [Required]
    [StringLength(13, MinimumLength = 10)]
    public string ISBN { get; set; }

    [Range(1500, 2100)]
    public int PublicationYear { get; set; }

    [Range(1, int.MaxValue)]
    public int Pages { get; set; }

    public Genre Genre { get; set; }

    public bool IsAvailable { get; set; } = true;

    public DateTime AddedDate { get; set; }

    public DateTime? LastUpdated { get; set; }
}
```
---

## 📂 Genre Enum

```csharp
public enum Genre
{
    Fiction,
    NonFiction,
    Mystery,
    SciFi,
    Biography,
    Other
}
```
---

## ✅ Implemented Functional Requirements

- [x]  List all books
- [x] Search by Title
- [x] Search by ISBN
- [x] Filter by Genre
- [x] View book details
- [x] Add new book
- [x] Edit existing book
- [x] Delete book
- [x] Friendly success messages
- [x] Client-side & server-side validation
- [x] Global error handling
---

## 🗂️ Repository Pattern

Interface:

```csharp
public interface IBookRepository
{
    Task<Book?> GetByIdAsync(int id);
    Task<IEnumerable<Book>> GetAllAsync();
    Task<IEnumerable<Book>> SearchAsync(string? searchTerm = null, Genre? genre = null);
    Task AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(int id);
    Task<bool> IsIsbnUniqueAsync(string isbn, int? excludeId = null);
}
```

Implementation is done in BookRepository using Entity Framework Core and LINQ.

---

## 💉 Dependency Injection

Registered in Program.cs:

```csharp
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```
---

## ⚠️ Exception Handling
### 🔹 Local Exception Handling

Custom exceptions:

- DuplicateIsbnException

- BookNotFoundException

- InvalidPublicationYearException

Handled inside controller actions using try-catch blocks.

---

### 🔹 Global Exception Handling

Configured in Program.cs:

```csharp
app.UseExceptionHandler("/Error/ServerError");
```

Features:

- Logs exception using ILogger

- Returns HTTP 500

- Shows friendly error page

- Displays RequestId

- Does not expose stack trace in production

---

## 🎨 UI Features

- Bootstrap 5

- Responsive design

- Client-side validation

- Confirmation before delete

- Success alert messages using TempData

- Professional table layout
---

## 🔎 Search Functionality

Search supports:

- Partial Title match

- Partial ISBN match

- Genre filter

- Combined search + genre filter

Implemented using LINQ:

```csharp
query = query.Where(b =>
    b.Title.Contains(searchTerm) ||
    b.ISBN.Contains(searchTerm));
```
---

## 🧪 Validation Rules

| Field           | Rule                   |
| --------------- | ---------------------- |
| Title           | 3–150 characters       |
| ISBN            | 10 or 13 characters    |
| PublicationYear | 1500 – CurrentYear + 5 |
| Pages           | Greater than 0         |

Validation is enforced using:

- Data Annotations

- ModelState

- Client-side jQuery validation
---

## 🚀 How to Run

1. Clone the repository

2. Update connection string in appsettings.json

3. Run:

```csharp
Add-Migration InitialCreate
Update-Database
```

4. Start the project
---

## 🎯 Technologies Used

- ASP.NET Core MVC

- Entity Framework Core

- SQL Server

- Bootstrap 5

- C#

- LINQ
---

## 📌 Conclusion

This project demonstrates clean architecture principles, proper separation of concerns, secure coding practices, and professional error handling in ASP.NET Core MVC.

It follows industry-level best practices suitable for real-world applications.