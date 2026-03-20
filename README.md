# BasicBilling API

.NET 8 REST API for managing utility bill payments built as a technical test for NEXION.

**Frontend repository:** https://github.com/pacha2880/basic-billing-frontend

## Tech Stack
- .NET 8 Web API
- Entity Framework Core + SQLite (Code-First)
- Domain-Driven Design (Domain / Application / Infrastructure / API)
- MediatR — request handling and decoupling
- AutoMapper — entity to DTO mapping
- JWT Authentication
- Global exception handling middleware
- OData — filtering and sorting on GET endpoints
- Swagger / OpenAPI
- XUnit + Moq — unit testing

## How to Build

```bash
dotnet build
```

## How to Run

```bash
dotnet run --project BasicBilling.API/BasicBilling.API.csproj
```

Swagger available at http://localhost:5214/swagger

Migrations and seed data are applied automatically on startup.
To reset the database: delete the `.db` file and run again.

## How to Run Tests

```bash
dotnet test
```

## Seed Data

| ID  | Name             |
|-----|------------------|
| 100 | Joseph Carlton   |
| 200 | Maria Juarez     |
| 300 | Albert Kenny     |
| 400 | Jessica Phillips |
| 500 | Charles Johnson  |

30 bills generated (5 clients x 3 services x 2 periods: 202501, 202502) — all Pending.

## API Endpoints

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| POST | /api/auth/token | No | Get JWT token given clientId |
| POST | /api/bills | Yes | Create a new bill |
| POST | /api/payments | Yes | Pay a pending bill |
| GET | /api/clients/{id}/pending-bills | Yes | Get pending bills (OData) |
| GET | /api/clients/{id}/payment-history | Yes | Get payment history (OData) |

### Auth

```
POST /api/auth/token
Body: { "clientId": 100 }
Returns: { "token": "eyJ..." }
```

### OData Query Examples

```
# Filter by service type
GET /api/clients/100/pending-bills?$filter=serviceType eq 'Water'

# Sort by amount
GET /api/clients/100/pending-bills?$orderby=amount desc

# Top results
GET /api/clients/100/pending-bills?$top=2

# Sort history by date
GET /api/clients/100/payment-history?$orderby=paidAt desc
```

## Architecture

```
Domain           Entities, Enums, Repository Interfaces
Application      MediatR Commands, Queries, Handlers, DTOs, AutoMapper Profile
Infrastructure   EF Core DbContext, Repository Implementations, DataSeeder
API              Controllers, Middleware, JWT Auth, OData, Swagger
Tests            XUnit Unit Tests with Moq
```

## Unit Tests
- ProcessPaymentHandler: valid payment, client not found, bill not found
- CreateBillHandler: valid creation, client not found
- GetPendingBillsHandler: valid query, client not found

## Development Process
- [Development Plan](docs/plan.md)
- [Architecture Reference](docs/architecture.md)
- [Decision Log](docs/decisions.md)

## What I would add with more time
- Integration tests with in-memory database
- OData on additional fields (amount range, date range)
- Refresh token mechanism
- Pagination support

## Not Implemented
- Integration tests (deferred due to time constraints)
