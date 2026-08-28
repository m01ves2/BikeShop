# Architecture Decisions

## ADR-1: Layered Architecture

**Decision:**  
BikeShop uses Domain, Application, Infrastructure, API, and Blazor layers.

**Reason:**  
The layers separate business rules, use cases, database code, HTTP endpoints, and UI code.

**Result:**  
The project is easier to navigate and change. The structure also adds some extra interfaces and mapping code.


## ADR-2: Framework-Free Domain Layer

**Decision:**  
The Domain layer does not depend on ASP.NET Core, EF Core, or Blazor.

**Reason:**  
Business rules should not depend on a database, HTTP, or UI framework.

**Result:**  
Domain entities can be tested and changed without changing technical layers.


## ADR-3: Application Layer Contains Use Cases

**Decision:**  
Commands, queries, and their handlers are placed in the Application layer.

**Reason:**  
Use cases often coordinate several domain entities and repositories. This work does not belong to controllers or Razor components.

**Result:**  
Controllers and Blazor pages stay small. The main business workflows are easy to find.


## ADR-4: EF Core Stays in Infrastructure

**Decision:**  
`DbContext`, EF Core configuration, and repository implementations are placed in Infrastructure.

**Reason:**  
Database code is a technical detail. Domain and Application should not depend directly on EF Core.

**Result:**  
Application code works with repository interfaces instead of database queries.


## ADR-5: Blazor Server and API Are Separate Applications

**Decision:**  
BikeShop uses a Blazor Server application for the UI and a separate ASP.NET Core Web API for business operations.

**Reason:**  
Blazor Server is responsible for pages, UI state, and user interaction. The API is responsible for HTTP endpoints, authorization, and application use cases. This creates a clear boundary between presentation and backend logic.

The API can later be used by another client, for example Blazor WebAssembly or a mobile application. It also gives practical experience with real client-server communication, JWT authentication, and HTTP error handling.

**Result:**  
The solution has more projects and requires HTTP calls even when both applications run locally. In return, the backend is reusable and does not depend on the Blazor UI.


## ADR-6: JWT Authentication and Roles

**Decision:**  
BikeShop uses ASP.NET Core Identity for users and roles. The API creates JWT tokens after login.

**Reason:**  
The Blazor application and API are separate applications. JWT allows Blazor to call protected API endpoints.

**Result:**  
The API can identify the current user and apply Customer or Admin authorization rules.


## ADR-7: Guest Cart and Server Cart

**Decision:**  
Guests use a local cart in browser local storage. Authenticated customers use a server cart.

**Reason:**  
Guests should be able to add products before registration or login. Orders still require an authenticated customer and a server cart.

**Result:**  
The application synchronizes the local cart with the server cart after login.


## ADR-8: Order Rules Belong to the Domain Model

**Decision:**  
The Order entity controls status changes, editing rules, and cancellation rules.

**Reason:**  
These are business rules. They must work the same way regardless of the API endpoint or UI page that calls them.

**Result:**  
Customer and admin use cases use the same domain rules.


## ADR-9: Simple File-Based Product Images

**Decision:**  
Product images are stored as WebP files in `BikeShop.Blazor/wwwroot/images/products/{productId}`.

**Reason:**  
BikeShop is a local MVP. A database image model, upload API, and cloud storage would add too much complexity for the current scope.

**Result:**  
The Blazor UI can show galleries and admins can manage images. This approach is not suitable for a production deployment because published files may be replaced and storage may be read-only.