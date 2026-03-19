FILE: docs/requirements-backend-structured.md

# Backend Requirements — Structured Reference

## Project Setup
- Framework: .NET 8 Web API
- Command to create: dotnet new webapi -n BasicBilling.API
- Database: SQLite with Entity Framework Core (Code-First + migrations)
- Nullable reference types: enabled
- HTTPS: enabled

## Seed Data

### Clients
| ID  | Name             |
|-----|------------------|
| 100 | Joseph Carlton   |
| 200 | Maria Juarez     |
| 300 | Albert Kenny     |
| 400 | Jessica Phillips |
| 500 | Charles Johnson  |

### Bills
- Generate 2 months of bills for every client and every service type
- All bills start with status Pending
- Service types: Water, Electricity, Sewer
- Total: 5 clients × 3 services × 2 months = 30 bills

## API Endpoints

### POST /bills
- Purpose: create a new bill (administrative/testing)
- Body: ClientId, ServiceType, BillingPeriod (YYYYMM), Amount
- Validations: all fields required, client must exist, period format YYYYMM, amount positive
- Response: created bill with status Pending

### POST /payments
- Purpose: process a bill payment
- Body: ClientId, ServiceType, BillingPeriod
- Validations: all fields required, client must exist, period format valid, matching bill must exist and be Pending
- Response: confirmation with updated bill status Paid

### GET /clients/{id}/pending-bills
- Purpose: retrieve all unpaid bills for a client
- Response: list of pending bills for the given client

### GET /clients/{id}/payment-history
- Purpose: retrieve all paid bills for a client
- Response: chronological list of paid bills

## Technical Requirements (Required)
- ASP.NET Core built-in dependency injection
- JSON response format
- HTTPS enabled
- Nullable reference types enabled
- Domain-Driven Design: separate entities, services, repositories, clean domain models
- AutoMapper: map between domain models and DTOs
- MediatR: request handling and decoupling logic
- Middleware: exception handling, request logging
- CORS: configure allowed origins
- ASP.NET Core Authorization (roles/claims not required yet)
- Swagger / OpenAPI integrated
- OData for querying/filtering endpoints
- JWT Authorization headers

## Technical Requirements (Testing)
- Unit tests: XUnit or NUnit, no DB access, mock dependencies with Moq
- Integration tests: validate API endpoints with in-memory database

## Tech Stack Summary
- .NET 8 LTS
- Entity Framework Core (SQLite + migrations)
- Domain-Driven Design
- MediatR
- AutoMapper
- OData
- JWT
- ASP.NET Core Dependency Injection
- Swagger / OpenAPI
- XUnit or NUnit

## Evaluation Criteria
- Implementation quality
- Adherence to best practices
- Completeness of features
- Code organization, readability, maintainability

## Delivery
- GitHub repository
- README.md explaining:
  - API functionality
  - How to build: dotnet build
  - How to run: dotnet run
  - Any unimplemented features with reasons