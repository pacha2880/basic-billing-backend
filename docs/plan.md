# BasicBilling — Development Plan

## Block Status

| Block | Description | Status |
|-------|-------------|--------|
| 0 | Design, folder structure, .NET solution created, build passing | ✅ Done |
| 1 | Backend core: entities, DB, seed, repositories, 5 endpoints + auth | ✅ Done |
| 2 | Flutter frontend (separate project) | ✅ Done |
| 3 | Backend refactor: MediatR handlers, AutoMapper, JWT middleware | ✅ Done |
| 4 | Tests: XUnit unit tests with Moq | ✅ Done |
| 5 | OData on GET endpoints | ⏳ Pending |
| 6 | Delivery: README, cleanup, final verification | ⏳ Pending |

## Decisions Already Made
- Architecture: DDD with Domain / Application / Infrastructure / API layers
- MediatR: handlers for all 4 use cases (CreateBill, ProcessPayment, GetPendingBills, GetPaymentHistory)
- AutoMapper: MappingProfile for Bill→BillDto and Payment→PaymentHistoryDto
- Auth: JWT mock — POST /api/auth/token returns a token given a clientId
- API prefix: /api/ on all endpoints
- Exception handling: global middleware maps exceptions to ProblemDetails
- Tests: XUnit + Moq, pure unit tests, no DB access
- Frontend: Flutter web (separate repository)
- OData deferred to Block 5
