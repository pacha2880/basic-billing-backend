# BasicBilling — Development Plan

## Block Status

| Block | Description | Status |
|-------|-------------|--------|
| 0 | Design, folder structure, .NET solution created, build passing | ✅ Done |
| 1 | Backend core: entities, DB, seed, repositories, 5 endpoints + auth | ✅ Done |
| 2 | Flutter setup + core screens (Pending Bills, Pay Bill) | 🔄 Next |
| 3 | Backend refactor: MediatR handlers, AutoMapper, JWT middleware | ⏳ Pending |
| 4 | Flutter remaining screens (Create Bill, Payment History, routing) | ⏳ Pending |
| 5 | Tests: XUnit backend + OData | ⏳ Pending |
| 6 | Delivery: README, cleanup, final verification | ⏳ Pending |
| 7 | Android (optional/fun) | ⏳ Bonus |

## Block 2 — Next: Flutter setup + core screens

1. Create Flutter project: flutter create basic_billing_app inside the solution root
2. Add dependencies to pubspec.yaml: flutter_bloc, dio, go_router, equatable
3. Create folder structure: core, models, repositories, blocs, screens
4. Create models: BillModel, PaymentHistoryModel, ClientModel
5. Create ApiService with Dio and JWT interceptor
6. Create BillsBloc: events, states, and connection to API
7. Screen: Pending Bills — client selector + list of pending bills
8. Screen: Pay Bill — form + payment confirmation

## Decisions Already Made
- Architecture: DDD with Domain / Application / Infrastructure / API layers
- MediatR and AutoMapper introduced in Block 3
- Auth: JWT mock — POST /api/auth/token returns a token given a clientId
- API prefix: /api/ on all endpoints
- Backend base URL: http://localhost:5214
- Flutter state management: flutter_bloc
- Flutter HTTP client: Dio (JWT interceptor)
- Flutter navigation: go_router
- Frontend: Flutter web primary, Android optional bonus
