# Blazor Book Store

> A way to manage books as a web application using Blazor WASM

![visitors](https://vistr.dev/badge?repo=johnmcraig.blazor-book-shop)

## Scope

A full-stack web application to manage books and thier authors.

### Architecture

This application uses hexagonal/onion style architecture for separation of core functionality. The key projects are contained within the `Core`, `Infrastructure`, and `Api`projects with the client within the `UI`.

### Technologies

The application was built using:

- ASP.Net Core 3.1
- Blazor WebAssembly (WASM)
- Entity Freamework Core
- Bootstrap 4

### Tools

Book Store uses Swagger from Swashbuckle for client resource testing, NLog for logging requrst from the Api and any additional errors.

For database setup, for development it is recomended to use Sqlite, but PostgreSQL is also already setup for database configuration in the Infrastructure project.
