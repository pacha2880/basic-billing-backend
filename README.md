# BasicBilling API

REST API for managing utility bill payments (water, electricity, sewer) for 5 clients.

## Tech Stack
- .NET 8 Web API
- Entity Framework Core with SQLite (Code-First)
- Domain-Driven Design (Domain / Application / Infrastructure / API)
- JWT Authentication
- Swagger / OpenAPI

## How to Build
dotnet build

## How to Run
dotnet run --project BasicBilling.API/BasicBilling.API.csproj

The API will automatically apply migrations and seed the database on startup.
Swagger is available at http://localhost:5214/swagger

## Seed Data
5 clients are seeded automatically:
- 100: Joseph Carlton
- 200: Maria Juarez
- 300: Albert Kenny
- 400: Jessica Phillips
- 500: Charles Johnson

30 bills are generated (5 clients x 3 services x 2 billing periods: 202501 and 202502), all with Pending status.

## API Endpoints

### Auth
POST /api/auth/token
Body: { "clientId": 100 }
Returns a JWT token. Use it as: Authorization: Bearer <token>

### Bills
POST /api/bills
Create a new bill (requires JWT)
Body: { "clientId": 100, "serviceType": "Water", "billingPeriod": "202503", "amount": 75.50 }

### Payments
POST /api/payments
Pay a pending bill (requires JWT)
Body: { "clientId": 100, "serviceType": "Water", "billingPeriod": "202501" }

### Clients
GET /api/clients/{id}/pending-bills
Get all pending bills for a client (requires JWT)

GET /api/clients/{id}/payment-history
Get payment history for a client (requires JWT)

## Not Implemented (planned)
- MediatR handlers (planned Block 2)
- AutoMapper profiles (planned Block 2)
- OData filtering (planned Block 3)
- Integration tests (planned Block 3)
- Flutter frontend (separate repository)
