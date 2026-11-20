# RaceDay System
### Part 1
## System Overview
The RaceDay system is an event management platform for organizing races, managing participant entries, and publishing event results.

## System Roles
- **Organiser**: Manages race events, creates categories, and uploads overall event results.
- **Participant**: Browses available race events, registers/enrols for categories, and views individual race timings.

## CI/CD Pipeline Status
CI/CD Build Screenshot(docs/cicd-build-screenshot.png)
*Note: Workflow configured in .github/workflows/validate-docs.yml to automatically validate system documentation files on push fails due to account issues

### Part 2
## YouTube Link
https://youtu.be/750Pc54bVws

---

## Tech Stack & Architecture

* **Framework:** ASP.NET Core Web API (.NET 8)
* **ORM & Database:** Entity Framework Core (Code-First) & SQL Server
* **Authentication & Security:** Custom `IAuthService` using ASP.NET Core `PasswordHasher<User>`
* **Data Transfer & Validation:** DTO pattern backed by `System.ComponentModel.DataAnnotations`
* **API Documentation:** Swashbuckle / Swagger OpenAPI integrated with XML comments
* **Error Handling:** Custom Global Exception Handling Middleware (`ExceptionMiddleware`)
* **Configuration & Policy:** Parameterized SQL connections via `IDesignTimeDbContextFactory` and global CORS (`AllowAllPolicy`)

---

## API Endpoints Summary

- **Authentication (`/api/auth`):** Account registration (`/register`) and login (`/login`).
- **Events (`/api/events`):** Fetch all racing events and create new event entries.
- **Enrolments (`/api/enrolments`):** Register participants for events with duplicate entry validation.
- **Results (`/api/results`):** Record race completion times and positions for enrolled participants.
- **Categories (`/api/categories`):** Manage event classifications (e.g., 5km, 10km, Half Marathon).
- **Roles (`/api/roles`):** Retrieve and create user access roles (`Admin`, `Organizer`, `Participant`).

---

## Getting Started & Database Setup

1. Restore Dependencies & Build:
   powershell
   dotnet restore
   dotnet build

## References
Microsoft. 2024. ASP.NET Core Web API Overview and Controller Actions. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/aspnet/core/web-api/

Microsoft. 2024. Action Return Types in ASP.NET Core Web API. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/aspnet/core/web-api/action-return-types

Microsoft. 2024. Entity Framework Core Overview and Code-First Migrations. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/ef/core/

Microsoft. 2024. Querying and Saving Data in Entity Framework Core. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/ef/core/querying/

Microsoft. 2024. Loading Related Data and Eager Loading in Entity Framework Core. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/ef/core/querying/related-data/eager

Microsoft. 2024. Data Seeding in Entity Framework Core. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding

Microsoft. 2024. Design-Time DbContext Creation with IDesignTimeDbContextFactory. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/ef/core/cli/dbcontext-creation

Microsoft. 2024. PasswordHasher Class in Microsoft.AspNetCore.Identity. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.passwordhasher-1

Microsoft. 2024. Model Validation in ASP.NET Core MVC and Web APIs. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation

Microsoft. 2024. Write Custom ASP.NET Core Middleware. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/write-custom

Microsoft. 2024. Handle Errors in ASP.NET Core Web APIs. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/aspnet/core/web-api/handle-errors

Microsoft. 2024. Enable Cross-Origin Requests (CORS) in ASP.NET Core. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/aspnet/core/security/cors

Microsoft. 2024. Role-based Authorization in ASP.NET Core. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/aspnet/core/security/authorization/roles

Microsoft. 2024. Get Started with Swashbuckle and ASP.NET Core. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle

Microsoft. 2024. Dependency Injection and Service Scopes in ASP.NET Core. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection

GitHub. (2025). Workflow syntax for GitHub Actions. GitHub Documentation. Available at: https://docs.github.com/en/actions/writing-workflows/workflow-syntax-for-github-actions

Microsoft Learn. (2025). Unit testing best practices and tools with .NET. Microsoft Documentation. Available at: https://learn.microsoft.com/en-us/dotnet/core/testing/

xUnit.net. (2025). Getting Started with xUnit.net. xUnit Framework Documentation. Available at: https://xunit.net/docs/getting-started/netcore/cmd
