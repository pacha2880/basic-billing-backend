# BasicBilling — Development Plan

## Block Status

| Block | Description | Status |
|-------|-------------|--------|
| 0 | Design, folder structure, .NET solution created, build passing | ✅ Done |
| 1 | Backend core: entities, DB, seed, repositories, 5 endpoints + auth | 🔄 In Progress |
| 2 | Backend refactor: MediatR handlers, AutoMapper, JWT middleware | ⏳ Pending |
| 3 | Tests: XUnit unit tests with Moq, OData on GET endpoints | ⏳ Pending |
| 4 | Flutter frontend (separate project, done after backend is stable) | ⏳ Pending |
| 5 | Delivery: README, cleanup, final verification | ⏳ Pending |

## Block 1 — Next steps in order

1. Domain entities + enums
2. Repository interfaces
3. AppDbContext + EF relationships
4. DataSeeder
5. Initial migration + auto-apply on startup
6. Repository implementations
7. POST /api/auth/token (JWT mock)
8. POST /api/bills
9. POST /api/payments (validate client exists, bill exists and is Pending)
10. GET /api/clients/{id}/pending-bills
11. GET /api/clients/{id}/payment-history
12. CORS configuration
13. Swagger configuration

## Decisions Already Made
- Architecture: DDD with Domain / Application / Infrastructure / API layers
- MediatR and AutoMapper introduced in Block 2 (not Block 1)
- Auth: JWT mock — POST /api/auth/token returns a token given a clientId, no real user validation needed yet
- API prefix: /api/ on all endpoints
- Frontend: Flutter web (separate project, built after backend is complete and verified)