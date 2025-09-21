# ECommerce Product API Solution

A modern, scalable RESTful API solution for managing e-commerce products built with .NET 8 and Clean Architecture principles. This microservice provides comprehensive product management capabilities including CRUD operations, data validation, and robust error handling.

## 🚀 Features

- **Complete CRUD Operations**: Create, Read, Update, and Delete products
- **Data Validation**: Comprehensive input validation with data annotations
- **Error Handling**: Centralized exception handling with user-friendly error messages
- **Logging**: Structured logging using Serilog for better observability
- **Database Integration**: Entity Framework Core with SQL Server support
- **API Documentation**: Swagger/OpenAPI integration for interactive API documentation
- **Docker Support**: Containerized deployment with multi-stage Docker builds
- **Clean Architecture**: Separation of concerns with layered architecture
- **Generic Service Pattern**: Reusable service interfaces for consistent data operations

## 🏗️ Technical Stack

### Core Technologies
- **.NET 8.0** - Latest LTS version of .NET framework
- **ASP.NET Core Web API** - RESTful API framework
- **Entity Framework Core 8.0.7** - Object-Relational Mapping (ORM)
- **SQL Server** - Primary database
- **Swagger/OpenAPI** - API documentation and testing

### Key Libraries & Packages
- **Microsoft.EntityFrameworkCore.Tools** - EF Core tooling for migrations
- **Microsoft.VisualStudio.Azure.Containers.Tools.Targets** - Docker containerization
- **Swashbuckle.AspNetCore** - Swagger UI integration
- **Serilog.AspNetCore** - Structured logging
- **Microsoft.AspNetCore.Authentication.JwtBearer** - JWT authentication support
- **ClosedXML** - Excel file processing capabilities

### Development Tools
- **Visual Studio 2022** - Primary IDE
- **Docker Desktop** - Containerization platform
- **SQL Server Management Studio** - Database management

## 🏛️ Architecture & Design Patterns

### Clean Architecture Implementation
The solution follows Clean Architecture principles with clear separation of concerns:

```
ProductApi.Api (Presentation Layer)
    ↓
ProductApi.Application (Application Layer)
    ↓
ProductApi.Domain (Domain Layer)
    ↓
ProductApi.Infrastructure (Infrastructure Layer)
```

### Design Patterns Applied

1. **Repository Pattern**: Abstracted data access through service interfaces
2. **Dependency Injection**: IoC container for loose coupling
3. **DTO Pattern**: Data Transfer Objects for API communication
4. **Generic Service Pattern**: Reusable service interfaces (`IGenericService<T>`)
5. **Factory Pattern**: Service container for dependency registration
6. **Mapping Pattern**: Entity-DTO conversion utilities

### Project Structure

```
ECommerce.ProductApiSolution/
├── ProductApi.Api/                 # Web API layer
│   ├── Controllers/               # API controllers
│   ├── appsettings.json          # Configuration
│   └── Dockerfile                # Container configuration
├── ProductApi.Application/        # Application layer
│   ├── DTOs/                     # Data Transfer Objects
│   └── IServices/                # Service interfaces
├── ProductApi.Domain/            # Domain layer
│   └── Entities/                 # Domain entities
└── ProductApi.Infrastructure/    # Infrastructure layer
    ├── Data/                     # Database context & migrations
    ├── Services/                 # Service implementations
    └── DependencyInjection/      # DI configuration
```

## 🛠️ Setup & Installation

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB or Express)
- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/) or [VS Code](https://code.visualstudio.com/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (optional)

### Installation Steps

1. **Clone the Repository**
   ```bash
   git clone <repository-url>
   cd ECommerce.ProductApiSolution
   ```

2. **Database Setup**
   - Ensure SQL Server is running
   - Update connection string in `ProductApi.Api/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "eCommerceConnection": "Server=(local); Database=ECommerce.Product; Trusted_Connection=true; TrustServerCertificate=true;"
     }
   }
   ```

3. **Restore Dependencies**
   ```bash
   dotnet restore
   ```

4. **Run Database Migrations**
   ```bash
   cd ProductApi.Infrastructure
   dotnet ef database update --startup-project ../ProductApi.Api
   ```

5. **Build the Solution**
   ```bash
   dotnet build
   ```

6. **Run the Application**
   ```bash
   cd ProductApi.Api
   dotnet run
   ```
