# ECommerce API

A .NET 10 Web API built with Clean Architecture, CQRS, and JWT authentication.

## Architecture

Four-layer Clean Architecture — dependencies point inward toward the Domain.
ECommerce.API            → Controllers, middleware, Program.cs
ECommerce.Application    → Commands, queries, handlers, DTOs, validators
ECommerce.Infrastructure → DbContext, repositories, Identity, JWT
ECommerce.Domain         → Entities, enums, repository interfaces

## Tech Stack

- .NET 10 / ASP.NET Core
- Entity Framework Core 10 (SQL Server LocalDB)
- MediatR — CQRS
- AutoMapper — DTO mapping
- FluentValidation — pipeline validation
- ASP.NET Core Identity + JWT Bearer
- Scalar — API documentation

## Getting Started

### Prerequisites
- .NET 10 SDK
- SQL Server LocalDB

### Run

```bash
git clone https://github.com/<olofs22>/ecommerce-clean-architecture.git
cd ecommerce-clean-architecture

dotnet dev-certs https --trust (if ssl issues appear)

cd ECommerce.API
dotnet user-secrets init
dotnet user-secrets set "Jwt:Key" "your-dev-key-at-least-32-characters-long"
cd ..

dotnet run --project ECommerce.API
```

Open `https://localhost:<port>/scalar/v1` for the API docs.

Migrations apply and a default admin is seeded on first run:
admin@ecommerce.local / Admin123!

## Endpoints

| Method | Route | Auth |
|---|---|---|
| POST | `/api/Auth/register` | Public |
| POST | `/api/Auth/login` | Public |
| GET | `/api/Products` | Public |
| GET | `/api/Products/{id}` | Public |
| POST/PUT/DELETE | `/api/Products` | Admin |
| GET | `/api/Categories` | Public |
| POST | `/api/Categories` | Admin |
| GET | `/api/Orders/my` | User |
| GET | `/api/Orders/{id}` | Owner or Admin |
| POST | `/api/Orders` | User |

## Testing

The repo includes `ECommerce.API/api-tests.http` — open in Visual Studio or VS Code (REST Client) and run the requests top to bottom. Tokens and IDs chain automatically.

## Project Layout
src/
├── ECommerce.Domain/         # Entities, interfaces — no dependencies
├── ECommerce.Application/    # Use cases (CQRS handlers, validators, mappings)
├── ECommerce.Infrastructure/ # EF Core, repositories, JWT, Identity
└── ECommerce.API/            # Controllers, middleware, Program.cs

