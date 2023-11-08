# ProductCatalog - Clean Architecture for ASP.NET Core 7

ProductCatalog is a praticing test ASP.NET Core 7 web application that tries to follow the Clean Architecture pattern. It incorporates several modern development practices and tools to ensure maintainability, testability, and flexibility. 
This project is designed for praticing the development of a enterprise-level web applications with a focus on separation of concerns and clean code principles, and it can be improved a lot in several ways, like adding error handling pratices, logging, adding more unit tests, etc.

## Features

- **Clean Architecture**: ProductCatalog is structured following the Clean Architecture pattern, ensuring a clear separation of concerns among the layers: Presentation (WebUI and API), Application, Domain,  Infrastructure (Data and Ioc) and Tests.

- **MVC pattern**: The project uses ASP.NET Core 7 MVC for the web presentation layer, offering a user-friendly and responsive interface.
- **API**: The project also uses ASP.NET Core 7 API for the web presentation layer..

- **CQRS pattern**: It follows the Command-Query Responsibility Segregation (CQRS) pattern. It uses Dapper for query operations, which provides fast and efficient read-only access to the database. For command operations, Entity Framework Core (EF Core) is used.

- **Automapper**: Automapper is integrated to simplify the mapping of data between DTOs, Domain Entities, and Database Models.

- **Unit Testing**: Unit tests are implemented using NUnit, ensuring the reliability and correctness of the code.

- **Authentication and Authorization**: The project uses Identity and JWT for authentication.

- **SOLID Principles**: The codebase tries to adheres to SOLID principles, enhancing code maintainability and extensibility.

## Getting Started

To get started with ProductCatalog, follow these steps:

1. Clone the repository.
2. Set up your database connection in appsettings.json to match your environment.
3. Run the database migrations to create the necessary database schema
4. Update the database

License
This project is licensed under the MIT License.
