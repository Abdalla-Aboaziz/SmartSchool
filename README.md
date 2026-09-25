<p align="center">
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/C%23-13-239120?style=for-the-badge&logo=csharp&logoColor=white" />
  <img src="https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" />
  <img src="https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge" />
</p>

# 🎓 SmartSchool API

> A School Management REST API built from the ground up to **practice and demonstrate** Clean Architecture, CQRS, and MediatR patterns in a real-world domain.

---

## 📌 Why This Project?

This project was built as a **hands-on learning exercise** to deeply understand:

| Concept | Why it matters |
|---------|---------------|
| **Clean Architecture** | Enforces separation of concerns — the domain and business logic are completely independent of frameworks, databases, and UI |
| **CQRS (Command Query Responsibility Segregation)** | Separates read operations (Queries) from write operations (Commands), making each side independently scalable and optimizable |
| **MediatR** | Implements the Mediator pattern to decouple request handling from controllers, enabling clean pipeline behaviors like validation and logging |

Rather than building a minimal demo, I chose a **realistic domain** (School Management) with multiple entities and relationships to exercise these patterns in a meaningful context.

---

## 🏗️ Architecture Overview

The solution follows **Clean Architecture** with 4 clearly separated layers:

```
┌─────────────────────────────────────────────────────┐
│                   SmartSchool.Api                    │  ← Presentation Layer
│            (Controllers, Program.cs, DI)            │     Thin controllers that only
│                                                     │     dispatch MediatR requests
├─────────────────────────────────────────────────────┤
│              SmartSchool.Application                │  ← Application Layer
│    (Features, Commands, Queries, Handlers,          │     All business use cases live here
│     Validators, Mapping, Pipeline Behaviors)        │     No dependency on Infrastructure
├─────────────────────────────────────────────────────┤
│             SmartSchool.Infrastructure              │  ← Infrastructure Layer
│   (EF Core DbContext, Repositories, Migrations,    │     Implements abstractions defined
│    Entity Configurations, Identity)                 │     in the Application layer
├─────────────────────────────────────────────────────┤
│            SmartSchool.Domain (Data)                │  ← Domain Layer
│        (Entities, Value Objects, Domain Rules)      │     Zero external dependencies
│                                                     │     Pure C# classes
└─────────────────────────────────────────────────────┘
```

### Dependency Flow

```
Api → Application → Domain
Api → Infrastructure → Application → Domain
```

> ⚡ **Key Rule**: Dependencies always point **inward**. The Domain layer has **zero** references to any other project or framework.

---

## 🧩 CQRS Implementation

Every feature is organized using the CQRS pattern with a consistent folder structure:

```
Features/
├── Students/
│   ├── Commands/
│   │   ├── AddStudentCommand.cs            ← Request (IRequest<Response<T>>)
│   │   ├── AddStudentCommandHandler.cs     ← Handler (IRequestHandler<TReq, TRes>)
│   │   ├── EditStudentCommand.cs
│   │   ├── EditStudentCommandHandler.cs
│   │   ├── DeleteStudentCommand.cs
│   │   └── DeleteStudentCommandHandler.cs
│   ├── Queries/
│   │   ├── GetStudentByIdQuery.cs
│   │   ├── GetStudentByIdQueryHandler.cs
│   │   ├── GetStudentsListQuery.cs
│   │   ├── GetStudentsListQueryHandler.cs
│   │   ├── GetStudentPaginatedListQuery.cs
│   │   └── GetStudentPaginatedListQueryHandler.cs
│   ├── Responses/                          ← DTOs (never expose domain entities)
│   ├── Mapping/                            ← Mapster configuration
│   └── Validations/                        ← FluentValidation rules
│
├── Departments/   (same structure)
├── Instructor/    (same structure)
├── Subject/       (same structure)
└── Authentication/
    ├── Auth/      (Login, Refresh, ForgotPassword, ResetPassword)
    └── User/      (Register, Queries)
```

### How a Request Flows

```
HTTP Request
    │
    ▼
┌──────────────┐     ┌──────────────────┐     ┌──────────────────┐     ┌────────────┐
│  Controller  │────▶│  MediatR.Send()  │────▶│ ValidationBehavior│────▶│  Handler   │
│  (thin)      │     │  (dispatch)      │     │ (pipeline)        │     │ (logic)    │
└──────────────┘     └──────────────────┘     └──────────────────┘     └─────┬──────┘
                                                                             │
                                                                             ▼
                                                                      ┌────────────┐
                                                                      │ Repository │
                                                                      │ (EF Core)  │
                                                                      └────────────┘
```

---

## 🗃️ Domain Model

A rich domain model with encapsulated business rules:

```
┌─────────────┐       ┌──────────────┐       ┌─────────────┐
│  Department │◄──────│   Student    │──────►│   Subject   │
│             │       │              │  M:N  │             │
│ - Name      │       │ - Name       │       │ - Name      │
│ - ManagerId │       │ - Address    │       │ - Period    │
│             │       │ - Phone      │       │             │
└──────┬──────┘       └──────────────┘       └──────┬──────┘
       │                                            │
       │ 1:N                                        │ M:N
       ▼                                            │
┌──────────────┐                                    │
│  Instructor  │◄───────────────────────────────────┘
│              │
│ - Name       │
│ - Address    │
│ - Position   │
│ - Salary     │
│ - Supervisor │ ← Self-referencing (hierarchy)
└──────────────┘
```

### Domain Design Decisions

| Decision | Reason |
|----------|--------|
| **Private setters** on all properties | Enforce invariants — state can only change through domain methods |
| **Domain behavior methods** (`UpdateName()`, `AssignToDepartment()`) | Encapsulate business rules inside the entity |
| **Private constructors** for EF Core + public for domain creation | Support ORM while maintaining valid object creation |
| **`IReadOnlyCollection`** for navigation properties | Prevent external code from modifying collections directly |
| **`AuditableEntity`** base class | Automatic tracking of `CreatedBy`, `CreatedOn`, `UpdatedBy`, `UpdatedOn` |
| **Join entities** (`StudentSubject`, `InstructorSubject`, `DepartmentSubject`) | Explicit many-to-many with potential for extra payload |

---

## ⚙️ Tech Stack & Libraries

| Category | Technology | Why? |
|----------|-----------|------|
| **Framework** | .NET 10 | Latest LTS features and performance |
| **Database** | SQL Server + EF Core 10 | Industry standard ORM with migrations |
| **CQRS / Mediator** | MediatR 14 | Decouple handlers from controllers, enable pipeline behaviors |
| **Object Mapping** | Mapster | Faster than AutoMapper, compile-time mapping |
| **Validation** | FluentValidation 12 | Declarative, testable validation rules |
| **Authentication** | ASP.NET Identity + JWT Bearer | Production-grade auth with refresh tokens |
| **Localization** | `IStringLocalizer` + .resx files | Multi-language support (EN, AR, DE, FR) |
| **API Docs** | Swagger / OpenAPI | Interactive API documentation |

---

## 🔐 Authentication & Security

The API implements a complete JWT authentication flow:

```
┌────────────┐                    ┌─────────────┐
│   Client   │───── Register ────▶│             │
│            │                    │   Identity   │
│            │───── Login ───────▶│   + JWT      │──── Access Token (short-lived)
│            │                    │              │──── Refresh Token (long-lived)
│            │───── Refresh ─────▶│              │
│            │                    │              │
│            │── Forgot Password─▶│              │──── Password Reset Token (email)
│            │── Reset Password──▶│              │
└────────────┘                    └─────────────┘
```

### Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | `/api/Authetication/register` | Create a new user account |
| `POST` | `/api/Authetication/login` | Get access + refresh tokens |
| `POST` | `/api/Authetication/refresh` | Exchange refresh token for new access token |
| `POST` | `/api/Authetication/forgot-password` | Generate password reset token |
| `POST` | `/api/Authetication/reset-password` | Reset password with token |

---

## 🔄 MediatR Pipeline Behaviors

Pipeline behaviors intercept every request before it reaches the handler:

```
Request ──▶ [ValidationBehavior] ──▶ Handler
                    │
                    ├── Has validators? → Run all FluentValidation rules
                    ├── Validation failed? → Throw ValidationException
                    └── Validation passed? → Forward to handler
```

The `ValidationBehavior` automatically discovers and runs all `AbstractValidator<T>` classes registered for the incoming request type — **zero boilerplate** in the handlers.

---

## 🌍 Localization

The API supports **4 languages** out of the box:

| Language | Culture Code |
|----------|-------------|
| 🇺🇸 English | `en-US` (default) |
| 🇪🇬 Arabic | `ar-EG` |
| 🇩🇪 German | `de-DE` |
| 🇫🇷 French | `fr-FR` |

Set the language via the `Accept-Language` header:
```http
Accept-Language: ar-EG
```

All validation messages and API responses are localized using `.resx` resource files.

---

## 📄 API Endpoints

### Students

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/students` | Get all students |
| `GET` | `/api/students/paginated` | Get students with pagination |
| `GET` | `/api/students/{id}` | Get student by ID |
| `POST` | `/api/students` | Add a new student |
| `PUT` | `/api/students` | Update a student |
| `DELETE` | `/api/students/{id}` | Delete a student |

### Departments

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/departments` | Get all departments |
| `GET` | `/api/departments/{id}` | Get department by ID |
| `POST` | `/api/departments` | Add a new department |
| `PUT` | `/api/departments` | Update a department |
| `PUT` | `/api/departments/assign-manager` | Assign a manager to department |
| `DELETE` | `/api/departments/{id}` | Delete a department |

### Instructors

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/instructors` | Get all instructors |
| `GET` | `/api/instructors/{id}` | Get instructor by ID |
| `POST` | `/api/instructors` | Add a new instructor |
| `PUT` | `/api/instructors` | Update an instructor |
| `DELETE` | `/api/instructors/{id}` | Delete an instructor |

### Subjects

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/subjects` | Get all subjects |
| `GET` | `/api/subjects/{id}` | Get subject by ID |
| `POST` | `/api/subjects` | Add a new subject |
| `PUT` | `/api/subjects` | Update a subject |
| `DELETE` | `/api/subjects/{id}` | Delete a subject |

---

## 📦 Unified Response Wrapper

Every API response follows a consistent structure:

```json
{
  "statusCode": 200,
  "succeeded": true,
  "message": "Success",
  "data": { ... },
  "errors": null,
  "meta": null
}
```

Error responses:
```json
{
  "statusCode": 422,
  "succeeded": false,
  "message": "Student Name Already Exists",
  "data": null,
  "errors": null
}
```

The `AppBaseController` translates `Response<T>` into the correct HTTP status code automatically.

---

## 🛡️ Error Handling

A global `ErrorHandlerMiddleware` catches all unhandled exceptions and maps them to proper HTTP responses:

| Exception Type | HTTP Status Code |
|---------------|-----------------|
| `ValidationException` | `422 Unprocessable Entity` |
| `KeyNotFoundException` | `404 Not Found` |
| `UnauthorizedAccessException` | `401 Unauthorized` |
| `DbUpdateException` | `400 Bad Request` |
| Unhandled `Exception` | `500 Internal Server Error` |

---

## 📐 Repository Pattern

The project uses a **Generic Repository** with specific repositories for extended queries:

```
IGenericRepositoryAsync<T>          ← Base interface (CRUD + Transactions)
    │
    ├── IStudentRepository          ← GetByIdWithDepartmentAsync()
    ├── IDepartmentRepository       ← IsDepartmentExist()
    ├── IInstructorRepository       ← Custom queries
    └── ISubjectRepository          ← Custom queries
```

Key features of the Generic Repository:
- Full async CRUD operations
- `GetTableNoTracking()` for read-only queries (better performance)
- `GetTableAsTracking()` for entities that will be modified
- Transaction support (`BeginTransaction`, `Commit`, `RollBack`)

---

## 🗄️ Database & Auditing

### Auto-Auditing via `SaveChangesAsync` Override

The `ApplicationDbContext` automatically populates audit fields on every save:

```csharp
// On INSERT → sets CreatedById from the current JWT user
// On UPDATE → sets UpdatedById and UpdatedOn
```

### Additional DB Conventions
- All cascade deletes are changed to **Restrict** (prevent accidental data loss)
- Entity configurations are applied from separate `IEntityTypeConfiguration<T>` classes
- Fluent API configurations for all entities (7 configuration files)

---

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB or full instance)

### Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/Abdalla-Aboaziz/SmartSchool.git
   cd SmartSchool
   ```

2. **Update the connection string** in `SmartSchool.Api/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=.;Database=SmartSchool;Trusted_Connection=True;Encrypt=False"
     }
   }
   ```

3. **Configure JWT settings** — add to `appsettings.json`:
   ```json
   {
     "Jwt": {
       "Key": "YourSuperSecretKeyHere_AtLeast32Characters!",
       "Issuer": "SmartSchool",
       "Audience": "SmartSchoolUsers",
       "AccessTokenMinutes": 30,
       "RefreshTokenDays": 7
     }
   }
   ```

4. **Apply migrations**
   ```bash
   cd SmartSchool.Api
   dotnet ef database update --project ../SmartSchool.Infrastructure
   ```

5. **Run the application**
   ```bash
   dotnet run --project SmartSchool.Api
   ```

6. **Open Swagger UI** at: `https://localhost:<port>/swagger`

---

## 📁 Solution Structure

```
SmartSchool/
│
├── SmartSchool.Api/                          # Presentation Layer
│   ├── Controllers/
│   │   ├── AppBaseController.cs              # Base controller with MediatR + Response mapping
│   │   ├── StudentController.cs
│   │   ├── DepartmentController.cs
│   │   ├── InstructorController.cs
│   │   ├── SubjectController.cs
│   │   ├── AutheticationController.cs
│   │   └── UserController.cs
│   └── Program.cs                            # App configuration & middleware pipeline
│
├── SmartSchool.Application/                  # Application Layer (Use Cases)
│   ├── Abstractions/
│   │   └── Persistence/Repositories/         # Repository interfaces (contracts)
│   ├── Common/
│   │   ├── Response.cs                       # Unified response wrapper
│   │   ├── ResponseHandler.cs                # Factory methods for responses
│   │   ├── PaginatedResult.cs                # Pagination wrapper
│   │   ├── RequestFilters.cs                 # Query filtering
│   │   └── JwtSettings.cs                    # JWT configuration POCO
│   ├── Features/
│   │   ├── Students/                         # Full CQRS (Commands/Queries/Responses/Mapping/Validations)
│   │   ├── Departments/                      # Full CQRS
│   │   ├── Instructor/                       # Full CQRS
│   │   ├── Subject/                          # Full CQRS
│   │   └── Authentication/                   # Auth + User management
│   ├── MiddleWares/
│   │   └── ErrorHandlerMiddleware.cs         # Global exception handler
│   ├── PipelineBehaviors/
│   │   └── ValidationBehavior.cs             # FluentValidation pipeline
│   ├── Services/
│   │   ├── ITokenService.cs                  # Token generation contract
│   │   └── TokenService.cs                   # JWT + Refresh token generation
│   └── Resources/                            # Localization .resx files (EN + AR)
│
├── SmartSchool.Infrastructure/               # Infrastructure Layer
│   ├── Persistence/
│   │   ├── Data/
│   │   │   └── ApplicationDbContext.cs       # EF Core context with auto-auditing
│   │   ├── Configurations/                   # 7 Fluent API entity configurations
│   │   └── Repositories/                     # Repository implementations
│   └── Migrations/                           # EF Core migrations
│
└── SmartSchool.Domain (Data)/                # Domain Layer
    └── Entities/
        ├── Student.cs                        # Rich domain model
        ├── Department.cs                     # Rich domain model
        ├── Instructor.cs                     # Rich domain model + self-referencing
        ├── Subject.cs                        # Rich domain model
        ├── StudentSubject.cs                 # Many-to-many join entity
        ├── DepartmentSubject.cs              # Many-to-many join entity
        ├── InstructorSubject.cs              # Many-to-many join entity
        ├── ApplicationUser.cs                # Identity user
        ├── RefreshToken.cs                   # Refresh token entity
        └── AuditableEntity.cs                # Base class for audit fields
```

---

## 📚 What I Learned

Building this project gave me practical experience with:

1. **Clean Architecture** — How to structure a solution so the domain is at the center and completely independent
2. **CQRS** — Why separating reads from writes makes code more maintainable and scalable
3. **MediatR** — How the mediator pattern eliminates direct dependencies between controllers and business logic
4. **Pipeline Behaviors** — Cross-cutting concerns (validation, logging) without touching handler code
5. **Rich Domain Models** — Moving business logic into entities instead of spreading it across services
6. **Repository Pattern** — Abstracting data access behind contracts for testability
7. **FluentValidation** — Declarative, composable validation with async support
8. **JWT Authentication** — Complete auth flow with access tokens, refresh tokens, and password reset
9. **EF Core Patterns** — Fluent configurations, auto-auditing, cascade delete management

---

## 📝 License

This project is open source and available under the [MIT License](LICENSE).

---

<p align="center">
  <b>⭐ If this project helped you understand Clean Architecture or CQRS, give it a star!</b>
</p>
