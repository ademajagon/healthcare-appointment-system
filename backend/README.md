# Healthcare Appointment Management System

## Overview

This is a Healthcare Appointment Management System developed using .NET 8. The project is built following the Clean Architecture approach, ensuring a clear separation of concerns and promoting maintainability and testability.

## Project Structure

The solution is organized into the following projects:

- **Api**: This is the entry point of the application. It contains the controllers and the middleware.
- **Application**: This layer contains the business logic of the application. It includes services, DTOs (Data Transfer Objects), and interfaces.
- **Domain**: This layer includes the core entities of the application along with the interfaces and exceptions.
- **Infrastructure**: This layer contains the implementations for accessing external systems such as databases, file systems, etc.
- **Tests**: This project contains unit tests to ensure the reliability and correctness of the application.

## Clean Architecture

The project follows the Clean Architecture principles to achieve separation of concerns and independence of frameworks, UI, databases, and any external agencies.

### Layers

1. **Domain**: The core of the application. It includes:

   - Entities
   - Interfaces
   - Exceptions

2. **Application**: Contains the business logic. It includes:

   - Services
   - DTOs
   - Interfaces

3. **Infrastructure**: Implements the interfaces defined in the Application layer and interacts with external systems. It includes:

   - Repositories
   - Database context

4. **Api**: The entry point of the application. It includes:
   - Controllers
   - Middleware

## Project Setup

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio](https://visualstudio.microsoft.com/) or any other IDE

### Clone the repository

```sh
  git clone https://github.com/your-repo/healthcare-appointment-management.git
```

## Unit Testing

Unit tests are written using xUnit and Moq. The test project structure mirrors the main project structure to ensure easy navigation and maintainability.

### Running Tests

To run the unit tests, use the following command:

```sh
  dotnet test
```

This command will execute all the tests in the Tests project and display the results in the terminal.
