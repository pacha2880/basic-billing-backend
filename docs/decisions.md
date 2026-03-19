# BasicBilling — Decision Log

- Architecture: Domain-Driven Design (Domain / Application / Infrastructure / API)
- ORM: Entity Framework Core with SQLite, Code-First with migrations
- MediatR: introduced in Block 2 refactor, not in initial implementation
- AutoMapper: introduced in Block 2 refactor
- Auth: JWT mock endpoint, no real user table needed
- Tests: XUnit + Moq, no real DB access in unit tests
- CORS: open for localhost (Flutter web connects from browser)
- BillingPeriod: validated with regex ^\d{6}$ (YYYYMM)
- Enums: serialized as strings in JSON
- Frontend: Flutter web + flutter_bloc + Dio + go_router (separate project, done after backend)
