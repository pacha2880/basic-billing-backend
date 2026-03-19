# BasicBilling — Architecture Reference

## Solution Structure

BasicBilling/
├── BasicBilling.API/
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── BillsController.cs
│   │   ├── PaymentsController.cs
│   │   └── ClientsController.cs
│   ├── Middleware/
│   │   └── ExceptionHandlingMiddleware.cs
│   ├── Program.cs
│   └── appsettings.json
├── BasicBilling.Application/
│   ├── Bills/Commands/
│   │   ├── CreateBillCommand.cs
│   │   └── CreateBillHandler.cs
│   ├── Payments/Commands/
│   │   ├── ProcessPaymentCommand.cs
│   │   └── ProcessPaymentHandler.cs
│   ├── Clients/Queries/
│   │   ├── GetPendingBillsQuery.cs
│   │   ├── GetPendingBillsHandler.cs
│   │   ├── GetPaymentHistoryQuery.cs
│   │   └── GetPaymentHistoryHandler.cs
│   ├── DTOs/
│   │   ├── BillDto.cs
│   │   ├── CreateBillRequest.cs
│   │   ├── PaymentRequest.cs
│   │   ├── PaymentHistoryDto.cs
│   │   └── ClientDto.cs
│   └── Mapping/
│       └── MappingProfile.cs
├── BasicBilling.Domain/
│   ├── Entities/
│   │   ├── Client.cs
│   │   ├── Bill.cs
│   │   └── Payment.cs
│   ├── Enums/
│   │   ├── ServiceType.cs
│   │   └── BillStatus.cs
│   └── Interfaces/
│       ├── IClientRepository.cs
│       ├── IBillRepository.cs
│       └── IPaymentRepository.cs
├── BasicBilling.Infrastructure/
│   ├── Data/
│   │   ├── AppDbContext.cs
│   │   └── DataSeeder.cs
│   ├── Repositories/
│   │   ├── ClientRepository.cs
│   │   ├── BillRepository.cs
│   │   └── PaymentRepository.cs
│   └── Migrations/
└── BasicBilling.Tests/
    ├── Unit/
    │   ├── ProcessPaymentHandlerTests.cs
    │   └── CreateBillHandlerTests.cs
    └── Integration/
        └── BillsEndpointTests.cs

## Domain Entities

### Client
- Id (int): 100, 200, 300, 400, 500
- Name (string)
- Bills (ICollection<Bill>)

### Bill
- Id (int)
- ClientId (int) → FK to Client
- ServiceType (enum): Water, Electricity, Sewer
- BillingPeriod (string): format YYYYMM
- Amount (decimal)
- Status (enum): Pending, Paid
- CreatedAt (DateTime)
- Payment (Payment?) → optional navigation

### Payment
- Id (int)
- BillId (int) → FK to Bill
- PaidAt (DateTime)
- AmountPaid (decimal)

## Repository Interfaces

IClientRepository: GetByIdAsync(int id), ExistsAsync(int id)
IBillRepository: GetPendingBillAsync(clientId, serviceType, period), GetPendingBillsByClientAsync(clientId), AddAsync(bill), SaveChangesAsync()
IPaymentRepository: GetPaymentHistoryByClientAsync(clientId), AddAsync(payment), SaveChangesAsync()

## API Endpoints

POST   /api/auth/token          → Body: { clientId } → returns JWT token (mock)
POST   /api/bills               → Body: CreateBillRequest → returns BillDto (201)
POST   /api/payments            → Body: PaymentRequest → returns PaymentHistoryDto (200)
GET    /api/clients/{id}/pending-bills    → returns BillDto[]
GET    /api/clients/{id}/payment-history → returns PaymentHistoryDto[]

All endpoints except /auth/token require Authorization: Bearer <token> header.

## DTOs

CreateBillRequest: ClientId (int), ServiceType, BillingPeriod (regex ^\d{6}$), Amount (> 0)
PaymentRequest: ClientId (int), ServiceType, BillingPeriod (regex ^\d{6}$)
BillDto: Id, ClientId, ServiceType (string), BillingPeriod, Amount, Status (string), CreatedAt
PaymentHistoryDto: BillId, ServiceType (string), BillingPeriod, AmountPaid, PaidAt, Status ("Paid")

## Seed Data

Clients: 100 Joseph Carlton, 200 Maria Juarez, 300 Albert Kenny, 400 Jessica Phillips, 500 Charles Johnson
Periods: 202501 and 202502
Services: Water, Electricity, Sewer
Total: 30 bills (5 × 3 × 2), all Pending, amounts random $20–$150

## Key Rules
- Never expose domain entities directly — always use DTOs
- Controllers must be thin — no business logic inside them
- dotnet run from scratch must apply migrations and seed automatically
- CORS must be open for localhost (Flutter web frontend will connect later)
- Enums serialized as strings in JSON responses
