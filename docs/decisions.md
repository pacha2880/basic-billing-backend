# BasicBilling — Decision Log

- Architecture: Domain-Driven Design (Domain / Application / Infrastructure / API)
- ORM: Entity Framework Core with SQLite, Code-First with migrations
- MediatR: introduced in Block 3 refactor — handlers for all 4 use cases
- AutoMapper: introduced in Block 3 refactor — MappingProfile for Bill and Payment
- Auth: JWT mock endpoint, no real user table needed
- Tests: XUnit + Moq, no real DB access in unit tests
- CORS: open for localhost (Flutter web connects from browser)
- BillingPeriod: validated with regex `^\d{6}$` (YYYYMM)
- Enums: serialized as strings in JSON
- OData: enabled on GET endpoints with `[EnableQuery]` attribute
- Exception handling: global middleware maps exceptions to ProblemDetails JSON
- Frontend: Flutter web + flutter_bloc + Dio + go_router (separate repository)
