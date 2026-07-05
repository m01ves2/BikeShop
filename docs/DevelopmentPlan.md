# Development Plan

## 1. Goal
The goal of this project is to build a portfolio-quality e-commerce application while learning modern ASP.NET Core development practices, software architecture, and clean engineering principles.


## 2. Development Strategy
Development follows an iterative approach.

Each feature is implemented through the following stages:

1. Planning
2. Domain Modeling
3. Implementation
4. Testing
5. Refactoring
6. Documentation

Git branches and pull requests are used to isolate changes and maintain a clean project history.


## 3. Milestones

### Phase 1 - Planning
Goal
- Define the project scope, requirements, architecture, database design, and development roadmap before implementation begins.

Deliverables
- Project Charter
- URS
- SRS
- Use Cases
- Domain Model
- Architecture
- Architecture Decisions
- Database Design
- UI Sketches
- Development Plan
- Draw.io diagrams

Completion Criteria
- All planning documents are completed.
- Architecture decisions are documented.
- Core diagrams are created.


### Phase 2 - Project Setup
Goal
- Prepare the project infrastructure and establish the solution foundation.

Deliverables
- Solution structure
- Project references
- ASP.NET Core application
- EF Core configuration
- SQLite database
- Initial migration
- Git repository configuration

Completion Criteria
- The solution builds successfully.
- Database connection is configured.
- Project structure follows the planned architecture


### Phase 3 - Catalog
Goal
- Implement the public product catalog and allow customers to discover products.

Deliverables
- Categories
- Products
- Product Details
- Search
- Filtering
- Sorting
- Pagination

Completion Criteria
- Customers can browse categories.
- Customers can browse products.
- Customers can search products.
- Customers can filter and sort products.
- Customers can view product details.

# Phase 4 - Customer Accounts
Goal
- Allow customers to create and manage personal accounts.

Deliverables
- Registration
- Authentication
- Logout
- Password Recovery
- Customer Profile

Completion Criteria
- Customers can register.
- Customers can log in and log out.
- Customers can recover their password.
- Customers can view and update their profile information.


# Phase 5 - Shopping Cart
Goal
Allow customers to collect products before placing an order.

Deliverables
- Add to Cart
- Update Quantity
- Remove Products
- Cart Summary

Completion Criteria
- Customers can add products to the cart.
- Customers can change product quantities.
- Customers can remove products from the cart.
- Cart totals are calculated correctly.


# Phase 6 - Checkout
Goal
- Convert the shopping cart into a customer order.

Deliverables
- Shipping Information
- Cart Validation
- Order Creation
 
Completion Criteria
- Customers can complete the checkout process.
- Invalid carts are rejected.
- Orders are created successfully.


# Phase 7 - Orders
Goal
- Allow customers to manage and monitor their orders.

Deliverables
- Order List
- Order Details
- Order Status
- Order Cancellation

Completion Criteria
- Customers can view their orders.
- Customers can view order details.
- Customers can track the current order status.
- Eligible orders can be cancelled.


# Phase 8 - Administration
Goal
- Provide administrative tools for managing the system.

Deliverables
- Product Management
- Category Management
- Order Management
- Customer Management

Completion Criteria
- Administrators can manage products.
- Administrators can manage categories.
- Administrators can manage customer accounts.
- Administrators can manage customer orders.


# Phase 9 - Testing & Refactoring
Goal
- Improve code quality and verify system correctness.

Deliverables
- Bug Fixes
- Refactoring
- Code Review
- Documentation Updates

Completion Criteria
- Critical defects are resolved.
- Code follows project conventions.
- Documentation reflects the implemented system.


# Phase 10 - Final Review
Goal
- Prepare the project for portfolio publication.

Deliverables
- Final README
- Screenshots
- Architecture diagrams review
- Repository cleanup

Completion Criteria
- Documentation is complete.
- Repository is well organized.
- The project is ready for demonstration and portfolio use.


## 4. Technologies
Backend
- C#
- .NET
- ASP.NET Core

Database
- SQLite
- Entity Framework Core

Frontend
- Razor Pages (or Blazor, depending on project evolution)

Tools
- Git
- GitHub
- Draw.io
- Visual Studio


## 5. Future Improvements

Possible future improvements include:
- Online payments
- Product reviews
- Wishlist
- Discount system
- Email notifications
- Image upload
- Inventory management
- Shipment tracking