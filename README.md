# BasicBilling API

REST API for managing utility bill payments (water, electricity, sewer) for 5 clients.

## Tech Stack
- .NET 8 Web API
- Entity Framework Core with SQLite (Code-First migrations)
- Domain-Driven Design (Domain / Application / Infrastructure / API)
- MediatR for request handling and decoupling
- AutoMapper for entity to DTO mapping
- JWT Authentication
- Global exception handling middleware
- Swagger / OpenAPI
- XUnit + Moq for unit testing

## How to Build
dotnet build

## How to Run
dotnet run --project BasicBilling.API/BasicBilling.API.csproj

The API will automatically apply migrations and seed the database on startup.
Swagger available at http://localhost:5214/swagger

## How to Run Tests
dotnet test

## Seed Data
5 clients seeded automatically:
- 100: Joseph Carlton
- 200: Maria Juarez
- 300: Albert Kenny
- 400: Jessica Phillips
- 500: Charles Johnson

30 bills generated (5 clients x 3 services x 2 periods: 202501 and 202502), all Pending.
To reset the database: delete the .db file and run dotnet run again.

## API Endpoints

### Auth
POST /api/auth/token
Body: { "clientId": 100 }
Returns JWT token. Use as: Authorization: Bearer <token>

### Bills
POST /api/bills — Create a new bill (requires JWT)
Body: { "clientId": 100, "serviceType": "Water", "billingPeriod": "202503", "amount": 75.50 }

### Payments
POST /api/payments — Pay a pending bill (requires JWT)
Body: { "clientId": 100, "serviceType": "Water", "billingPeriod": "202501" }

### Clients
GET /api/clients/{id}/pending-bills — Get pending bills (requires JWT)
GET /api/clients/{id}/payment-history — Get payment history (requires JWT)

## Architecture
- Domain: entities (Client, Bill, Payment), enums, repository interfaces
- Application: MediatR commands/queries/handlers, DTOs, AutoMapper profile
- Infrastructure: EF Core DbContext, repository implementations, DataSeeder
- API: controllers, JWT auth, exception middleware, Swagger

## Unit Tests
Tests cover the core business logic handlers:
- ProcessPaymentHandler: valid payment, client not found, bill not found
- CreateBillHandler: valid creation, client not found
- GetPendingBillsHandler: valid query, client not found

## Not Implemented
- OData filtering (planned, deferred due to time constraints)
- Integration tests (planned, deferred due to time constraints)
