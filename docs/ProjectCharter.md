# BikeShop Project Charter

Status: Completed

## Project Overview

BikeShop is a single-vendor online store for bicycles. It allows guests to browse the catalog and lets registered customers manage a cart and place orders. Administrators can manage products, categories, product images, and order statuses.

The project was built to practice the main parts of a modern ASP.NET Core application and to create a portfolio-quality e-commerce MVP.

## Goals

- Build a complete shopping flow, from catalog browsing to order creation.
- Apply a layered architecture and keep business rules separate from UI and database code.
- Learn ASP.NET Core Web API, Entity Framework Core, Identity, JWT authentication, and Blazor Server.
- Create a responsive interface with Bootstrap and custom CSS.
- Practice Git, debugging, refactoring, and technical documentation.

## Implemented Features

- Product catalog, categories, product details, availability, and image galleries.
- A guest cart in browser local storage and a server cart for authenticated customers.
- Cart synchronization after login.
- Registration, login, JWT authentication, and Customer/Admin roles.
- Checkout with stock validation and order creation.
- Customer order history, order details, editing, and cancellation when allowed.
- Admin pages for category management, product management, image management, and order status changes.

## Technology Stack

- C# and .NET
- ASP.NET Core Web API
- ASP.NET Core Identity and JWT bearer authentication
- Entity Framework Core with SQL Server LocalDB
- Blazor Server
- Bootstrap and CSS isolation

## Deliberately Excluded Features

The following features are outside the BikeShop MVP and are planned for a future Marketplace project:

- Product search, filters, sorting, and pagination.
- Online payments, discounts, and coupons.
- Reviews, ratings, wishlists, and recommendations.
- Password recovery and customer profile management.
- Email notifications, returns, refunds, and shipment tracking.
- Production-grade image storage and localization.

## Important Simplifications

- BikeShop represents a small single-vendor store.
- Product images are stored in the Blazor project's `wwwroot` folder.
- Image upload is designed for the local MVP. A deployed application would require durable external file storage.
- No real payment provider is connected.
- Automated tests are outside the final project scope.

## Outcome

The original plan was a two-week MVP. The project took about six weeks because the scope grew during development: it gained a separate API and Blazor client, authentication and roles, cart synchronization, customer and admin order workflows, responsive UI work, and product image management.

BikeShop is now a complete learning project that connects domain logic, application use cases, persistence, HTTP APIs, client state, and a responsive Blazor interface.
