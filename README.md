# Blazor Book Shop

> A way to manage books as a web application using Blazor WASM

![visitors](https://vistr.dev/badge?repo=johnmcraig.blazor-book-shop)
![stars](https://img.shields.io/github/stars/johnmcraig/blazor-book-shop?style=flat-square&cacheSeconds=604800)
![last commit](https://img.shields.io/github/last-commit/johnmcraig/blazor-book-shop?style=flat-square&cacheSeconds=86400)
![pull requests](https://img.shields.io/github/issues-pr/johnmcraig/blazor-book-shop?color=0088ff)

## Scope

A full-stack web application to manage books and their authors with clean architecture using C#/.Net Core, Blazor, Dapper & SQL, HTML, and CSS.

### Architecture

This application uses hexagonal/onion style architecture for separation of core functionality and modularity to use or try out different technologies as it grows in scope.

The key projects are contained within the `Core`, `Infrastructure`, and `Api` projects with the client within the `UI` directory.

### Technologies

The application was built using:

- ASP.Net Core 3.1
- Blazor WebAssembly (Client)
- SQL + Dapper for database transactions
- Bootstrap 4

### Additional Built-in Tools

The API project contains the following:

- Swagger - Api/resource documentation testing from the Swashbuckler package for the client.

- NLog - for logging request from the Api and any additional errors that are saved as text documents for review.

### Database Setup

For database setup, it is recommended to use Sqlite for development and is currently an installed package with the application. Navigate to the Infrastructure project and find the `ApiDbContext.cs` class in the `..\Data` directory and find the method that configures which database to use:

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseSqlite(_config.GetConnectionString("sqlite"));
}
```

Change the above `optionsBuilder` method to which ever database package you prefer, such as MySql with: `.UseMySql()`. Configure the connection to that database in the `appsettings.Development.json` file in the Api project, then pass it in the `GetConnectionString` extension.

PostgreSQL is also already setup for database configuration, change the `optionsBuilder` method to use `.UseNpgsql()` and setup the connection string in the `application.Development.json` file.

### Future Features

The Book Store does not yet contain login/logout, registration, and authentication, but will be needed once the project grows in scope to actually sell books online.

Paging and filtering is currently in the pipeline and should be implemented within the year (*maybe*... this is a guesstimate).
