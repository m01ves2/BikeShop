# Software Requirements Specification

## 1. Scope

BikeShop is a small online bicycle store.

The system supports three actors:

- Guest
- Customer
- Administrator

Guests can browse products and use a local cart. Customers can create and manage orders. Administrators can manage catalog data and process orders.

## 2. Functional Requirements

### FR-1 Catalog

- The system shall display product categories.
- The system shall display products in the catalog.
- The system shall allow Guests and Customers to browse products by category.
- The system shall display product details, availability, and images.

### FR-2 Authentication

- The system shall allow Guests to register an account.
- The system shall allow registered users to log in and log out.
- The system shall use JWT authentication for protected API requests.
- The system shall support Customer and Admin roles.

### FR-3 Cart

- The system shall allow Guests to use a cart stored in browser local storage.
- The system shall provide a server cart for authenticated Customers.
- The system shall allow users to add products, change quantity, and remove items from a cart.
- The system shall synchronize the Guest cart with the Customer server cart after login.
- The system shall inform the user when unavailable products are removed during synchronization.

### FR-4 Checkout

- The system shall allow authenticated Customers to create an order from the server cart.
- The system shall collect delivery date, delivery address, customer phone number, and payment method.
- The system shall validate product stock during order creation.
- The system shall adjust requested quantities when stock is lower than requested.
- The system shall clear the server cart after successful order creation.

### FR-5 Customer Orders

- The system shall allow Customers to view their own order history.
- The system shall allow Customers to view their own order details.
- The system shall allow Customers to edit delivery information when the order can be edited.
- The system shall allow Customers to cancel an order when cancellation is allowed.

### FR-6 Category Management

- The system shall allow Administrators to create categories.
- The system shall allow Administrators to edit categories.
- The system shall allow Administrators to delete categories.

### FR-7 Product and Image Management

- The system shall allow Administrators to create, edit, and delete products.
- The system shall display product image previews in administration pages.
- The system shall allow Administrators to add WebP images to an existing product.
- The system shall allow Administrators to remove product images.

### FR-8 Order Management

- The system shall allow Administrators to find an order by ID.
- The system shall allow Administrators to view order details.
- The system shall allow Administrators to change order status.
- The system shall reject status changes that are not allowed by the domain model.

## 3. Access Control Rules

- Guests can browse the catalog and use a local cart.
- Only authenticated Customers can create orders and view personal orders.
- Customers can access only their own orders.
- Only Administrators can access administration pages and endpoints.
- Administrators cannot bypass order status rules.

## 4. Business Rules

- A product belongs to one category.
- An authenticated Customer has one server cart.
- A cart cannot contain duplicate items for the same product.
- An order contains one or more order items.
- An order item stores the product price at the time of order creation.
- Stock is validated when an order is created.
- Completed and cancelled orders cannot be edited or cancelled.
- Order status changes must follow the allowed transitions.

## 5. Non-Functional Requirements

### Security

- Passwords are managed by ASP.NET Core Identity.
- Protected endpoints require JWT authentication.
- Admin endpoints and pages require the Admin role.

### Maintainability

- The system shall use a layered architecture.
- Business rules shall not depend on EF Core, HTTP controllers, or Blazor components.

### Usability

- The UI shall work on desktop, tablet, and mobile screen sizes.
- The UI shall show loading states and error messages for failed operations.

## 6. Future Features

The following features are outside the BikeShop MVP:

- Product search, filters, sorting, and pagination.
- Online payments.
- Discounts and coupons.
- Reviews and ratings.
- Password recovery.
- Email notifications.
- Production-grade image storage.