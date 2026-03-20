# BasicBilling — Development Plan

## Block Status

| Block | Description | Status |
|-------|-------------|--------|
| 0 | Design, folder structure, .NET solution created, build passing | ✅ Done |
| 1 | Backend core: entities, DB, seed, repositories, 5 endpoints + auth | ✅ Done |
| 2 | Flutter frontend (separate project) | ✅ Done |
| 3 | Backend refactor: MediatR handlers, AutoMapper, JWT middleware | ✅ Done |
| 4 | Tests: XUnit unit tests with Moq, OData on GET endpoints | ⏳ Pending |
| 5 | Delivery: README, cleanup, final verification | ⏳ Pending |

## Decisions Already Made
- Architecture: DDD with Domain / Application / Infrastructure / API layers
- MediatR introduced in Block 3 — handlers for all 4 use cases
- AutoMapper introduced in Block 3 — MappingProfile for Bill and Payment
- Auth: JWT mock — POST /api/auth/token returns a token given a clientId
- API prefix: /api/ on all endpoints
- Exception handling: global middleware maps exceptions to ProblemDetails
- Frontend: Flutter web (separate repository)
- OData and unit tests deferred to Block 4
