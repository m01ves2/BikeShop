# Architecture Decisions

## ADR-1: Layered Architecture
Decision:
The system uses a layered architecture consisting of Web UI, API, Application, Domain, and Infrastructure layers.

Context:
We need clear separation of concerns to keep business logic independent from framework and infrastructure details.

Consequences:
- Improves maintainability and testability
- Allows independent evolution of layers
- Adds additional abstraction overhead


## ADR-2: Domain Layer is framework-agnostic
Decision:
The Domain layer does not depend on any external frameworks (EF Core, ASP.NET Core, etc.).

Context:
Business rules must remain independent from technical implementation details.

Consequences:
- Domain logic is reusable and testable
- Infrastructure changes do not affect core business rules
- Requires mapping between Domain and Infrastructure models


## ADR-3: Application Layer handles use cases
Decision:
All business workflows (e.g., Checkout, Order creation) are implemented in the Application layer.

Context:
We need a clear separation between business rules and orchestration logic.

Consequences:
- Application layer becomes coordinator of business processes
- Domain layer remains focused on rules
- Use cases are easy to locate and test


## ADR-4: EF Core is used only in Infrastructure
Decision:
Entity Framework Core is restricted to the Infrastructure layer.

Context:
Database access should not leak into business logic or application logic.

Consequences:
- Easier to replace data storage technology
- Requires repository or abstraction layer
- Adds mapping overhead between Domain and persistence models


## ADR-5: Checkout is implemented as Application Use Case
Decision:
Checkout is implemented as an Application layer use case, not inside Domain entities.

Context:
Checkout involves orchestration of multiple domain objects (Cart, Order, Product).

Consequences:
- Clear separation between process and domain logic
- Domain remains pure and reusable
- Checkout logic may grow in complexity but stays centralized


## ADR-6: Pricing and Discount logic is handled in Domain Services
Decision:
Pricing and discount calculations are implemented using Domain Services.

Context:
Discount rules involve multiple entities and business policies that do not belong to a single entity.

Consequences:
- Business rules are centralized
- Easier to extend discount logic
- Requires explicit data input from Application layer


## ADR-7: No direct dependency from Domain to Infrastructure
Decision:
Domain layer does not access Infrastructure directly, only via abstractions.

Context:
We need to avoid coupling business rules with storage or external systems.

Consequences:
- Supports clean architecture principles
- Enables easier testing and mocking
- Requires dependency inversion via interfaces