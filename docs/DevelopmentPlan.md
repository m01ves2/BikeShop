# Development Plan

Status: Completed

## Original Goal

The original goal was to build a small e-commerce MVP in about two weeks.

The project later grew into a larger learning project. It took about six weeks.

## Development Approach

BikeShop was developed step by step:

1. Plan the project and define the MVP scope.
2. Create the Domain model and database design.
3. Build the API, Application, Domain, and Infrastructure layers.
4. Add the Blazor Server UI.
5. Test, refactor, and improve the UI.
6. Update the documentation.

Git branches were used to isolate larger parts of the work.

## Phase Results

| Phase | Status | Result |
|---|---|---|
| Planning | Completed | Initial requirements, domain model, architecture, database design, and UI sketches were created. |
| Project Setup | Completed | The solution was split into Domain, Application, Infrastructure, API, and Blazor projects. |
| Catalog | Completed | Categories, products, product details, availability, and image galleries were implemented. |
| Authentication | Completed | Registration, login, JWT authentication, and Customer/Admin roles were implemented. |
| Cart | Completed | Guest local cart, Customer server cart, and cart synchronization were implemented. |
| Checkout | Completed | Delivery information, stock validation, and order creation were implemented. |
| Customer Orders | Completed | Order history, details, editing, and cancellation were implemented. |
| Administration | Completed | Category, product, product image, and order status management were implemented. |
| UI and Refactoring | Completed | The Blazor UI was reorganized, styled, and checked at desktop, tablet, and mobile widths. |
| Documentation | Completed | Planning documents were updated to reflect the final BikeShop MVP. |

## Important Changes During Development

The project changed from the original plan in several ways:

- SQL Server LocalDB was used instead of SQLite.
- Blazor Server was chosen instead of Razor Pages.
- A separate ASP.NET Core Web API was added.
- ASP.NET Core Identity, JWT authentication, and roles were added.
- The cart became more complex because Guests and Customers use different storage.
- Product image galleries and admin image management were added.
- The UI received more work than originally planned.

## Deferred Features

The following features were intentionally moved to a future Marketplace project:

- Product search, filters, sorting, and pagination.
- Online payments.
- Discounts and coupons.
- Reviews, ratings, and wishlists.
- Password recovery and customer profile management.
- Customer management in the admin area.
- Production-grade image storage.
- Email notifications and shipment tracking.

## Final Result

BikeShop is a complete e-commerce MVP with a layered backend, Blazor Server UI, authentication, roles, carts, orders, administration pages, responsive styles, and product image management.

The project is ready to be used as a learning and portfolio example before the next steps: BootstrapLab and DeployLab.