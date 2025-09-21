**ECommerce Product API Solution**
A scalable, well-structured RESTful API for managing e-commerce products. 
Built with .NET 8 and Clean Architecture principles,this microservice provides product CRUD operations, validation, logging, and API documentation.


**Features:**

-Complete Create, Read, Update, Delete (CRUD) operations for products
-Input validation using data annotations
-Centralized error handling with consistent error responses
-Structured logging via Serilog
-Persistence using Entity Framework Core and SQL Server
-Interactive API documentation with Swagger/OpenAPI
-Docker support with multi-stage builds
-Clean Architecture with separation of concerns
-Generic service pattern for reusable data operations


**Architecture:**
The solution follows Clean Architecture with the following layers:

-ProductApi.Api            (Presentation layer)
-ProductApi.Application    (Application layer)
-ProductApi.Domain         (Domain layer)
-ProductApi.Infrastructure (Infrastructure layer)

**Design patterns and practices:**

-Repository / Service abstraction for data access
-Dependency Injection for loose coupling
-DTOs for API surface models
-Generic service interfaces (e.g., IGenericService<T>)
-Custom mapping utilities for entity-to-DTO conversions
-Centralized exception handling and consistent response envelope


**Technical Stack:**

-.NET 8
-ASP.NET Core Web API
-Entity Framework Core 8
-SQL Server
-Swagger / OpenAPI


**Key Libraries:**

-Microsoft.EntityFrameworkCore.Tools
-Swashbuckle.AspNetCore
-Serilog.AspNetCore
-Microsoft.AspNetCore.Authentication.JwtBearer
-ClosedXML (for Excel processing)


**Development Tools:**

-Visual Studio 2022 or VS Code
-Docker (for container builds and runtime)
