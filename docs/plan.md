# BasicBilling — Development Plan

## Block Status

| Block | Description | Status |
|-------|-------------|--------|
| 0 | Design, folder structure, .NET solution created, build passing | ✅ Done |
| 1 | Backend core: entities, DB, seed, repositories, 5 endpoints + auth | ✅ Done |
| 2 | Backend refactor: MediatR handlers, AutoMapper, JWT middleware | ⏳ Next |
| 3 | Tests: XUnit unit tests with Moq, OData on GET endpoints | ⏳ Pending |
| 4 | Flutter frontend (separate project, done after backend is stable) | ⏳ Pending |
| 5 | Delivery: README, cleanup, final verification | ⏳ Pending |

## Block 2 — Next steps in order

1. Install MediatR package in BasicBilling.Application
2. Create IRequest and IRequestHandler for each use case:
   - CreateBillCommand + CreateBillHandler
   - ProcessPaymentCommand + ProcessPaymentHandler
   - GetPendingBillsQuery + GetPendingBillsHandler
   - GetPaymentHistoryQuery + GetPaymentHistoryHandler
3. Move business logic from BillingService and PaymentService into the corresponding handlers
4. Update controllers to use IMediator instead of the service classes directly
5. Fix AutoMapper vulnerability: upgrade to AutoMapper 13+
6. Create MappingProfile: Bill -> BillDto, Payment -> PaymentHistoryDto
7. Replace manual mapping in handlers with AutoMapper IMapper
8. Add global exception handling middleware (ExceptionHandlingMiddleware)
9. Add request logging middleware
10. Register MediatR, AutoMapper and middleware in Program.cs

## Decisions Already Made
- Architecture: DDD with Domain / Application / Infrastructure / API layers
- MediatR and AutoMapper introduced in Block 2
- Auth: JWT mock — POST /api/auth/token returns a token given a clientId
- API prefix: /api/ on all endpoints
- Frontend: Flutter web (separate project, built after backend is stable)

## Decisions Already Made
- Architecture: DDD with Domain / Application / Infrastructure / API layers
- MediatR and AutoMapper introduced in Block 2 (not Block 1)
- Auth: JWT mock — POST /api/auth/token returns a token given a clientId, no real user validation needed yet
- API prefix: /api/ on all endpoints
- Frontend: Flutter web (separate project, built after backend is complete and verified)