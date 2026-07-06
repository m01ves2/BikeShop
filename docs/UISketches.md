# UI Sketches

## 1. Home Page
Purpose

Landing page of the application.

Main Elements

- Navigation bar
- Product categories
- Featured products
- Search bar
- Footer

## 2. Product Catalog
Purpose

Browse products.

Main Elements

- Search
- Filters
- Sorting
- Product grid
- Pagination

## 3. Product Details
Purpose

Display detailed product information.

Main Elements

- Product images
- Description
- Price
- Availability
- Add to Cart button

## 4. Shopping Cart
Purpose

Review selected products before checkout.

Main Elements

- Product list
- Quantity controls
- Remove button
- Total price
- Checkout button

## 5. Checkout
Purpose

Collect shipping information and confirm the purchase.

Main Elements

- Shipping information
- Order summary
- Confirm Order button

## 6. Login / Registration

## 7. Customer Profile
Purpose

Manage customer account.

Main Elements

- Personal information
- Change password
- Order history shortcut

## 8. Orders
Purpose

Display customer orders.

Main Elements

- Order list
- Order details
- Order status
- Cancel Order button (when allowed)

## 9. Administration
Purpose

Manage the application.

Main Elements

- Product management
- Category management
- Customer management
- Order management



# Navigation Structure

## Guest(Public)
Home
 ├── Catalog
 │     └── Product Details
 ├── Login
 └── Register


## Authenticated Customer
Home
 ├── Catalog
 │     └── Product Details
 ├── Cart
 ├── Checkout
 ├── Orders
        └── Order Details
 └── Profile


 ## Administrator
Dashboard
 ├── Products
 ├── Categories
 ├── Product Editor
 ├── Orders
 └── Customers



# Pages appearence

## 0. Layout skeleton
+------------------------------------------------------+
| HEADER (Logo | Search | User | Cart)                |
+------------------------------------------------------+
| SIDEBAR (optional: categories / admin menu)         |
+------------------------------------------------------+
| MAIN CONTENT                                         |
|                                                      |
|                                                      |
+------------------------------------------------------+
| FOOTER                                               |
+------------------------------------------------------+

 ## 1. Home Page
Purpose
- Landing page of the application.

Main Elements

- Navigation bar
- Product categories
- Featured products
- Search bar
- Footer

Wireframe

+------------------------------------------------------+
| Logo | Search..................... | Login | Cart(2) |
+------------------------------------------------------+
| Categories                                         ▼ |
+------------------------------------------------------+
|                 Featured Products                    |
|  +--------+  +--------+  +--------+                  |
|  |        |  |        |  |        |                  |
|  | Image  |  | Image  |  | Image  |                  |
|  | Name   |  | Name   |  | Name   |                  |
|  | Price  |  | Price  |  | Price  |                  |
|  +--------+  +--------+  +--------+                  |
+------------------------------------------------------+
| Footer                                               |
+------------------------------------------------------+

 ## 2. Catalog
 +------------------------------------------------------+
| Logo | Search | Login | Cart                          |
+-------------------------------------------------------+
| Filters: Category | Price | Sort                      |
+-------------------------------------------------------+
|                 Product Grid                          |
|  +--------+  +--------+  +--------+                   |
|  |        |  |        |  |        |                   |
|  | Image  |  | Image  |  | Image  |                   |
|  | Name   |  | Name   |  | Name   |                   |
|  | Price  |  | Price  |  | Price  |                   |
|  +--------+  +--------+  +--------+                   |
+-------------------------------------------------------+
| Pagination                                            |
+-------------------------------------------------------+

## 3. PRODUCT DETAILS
+------------------------------------------------------+
| Logo | Search | Login | Cart                         |
+------------------------------------------------------+
| Product Images        | Product Info                 |
| [image]               | Name                         |
| [image]               | Price                        |
|                       | Description                  |
|                       | Add to Cart [button]         |
+------------------------------------------------------+
| Footer                                               |
+------------------------------------------------------+

## 4. Cart
+------------------------------------------------------+
| Logo | Search | Login | Cart                         |
+------------------------------------------------------+
| Cart Items                                           |
| [Product] Qty [-][+] Remove                          |
| [Product] Qty [-][+] Remove                          |
+------------------------------------------------------+
| Total: XXX                                           |
| Checkout Button                                      |
+------------------------------------------------------+

## 5. CHECKOUT
+------------------------------------------------------+
| Logo | Search | Login | Cart                         |
+------------------------------------------------------+
| Shipping Form                                        |
| Name                                                 |
| Address                                              |
| City / Zip                                           |
+------------------------------------------------------+
| Order Summary                                        |
| Items + Total                                        |
+------------------------------------------------------+
| Place Order Button                                   |
+------------------------------------------------------+

## 6. ORDERS
+------------------------------------------------------+
| Logo | Search | Login | Cart                         |
+------------------------------------------------------+
| Order List                                           |
| #1234 | Date | Status | View                       |
| #1235 | Date | Status | View                       |
+------------------------------------------------------+

## 7. LOGIN / REGISTER
+------------------------------------------------------+
| Logo                                                 |
+------------------------------------------------------+
| Login Form                                           |
| Email                                                |
| Password                                             |
| [Login Button]                                       |
+------------------------------------------------------+
| Register Link                                        |
+------------------------------------------------------+

## 8. ADMIN DASHBOARD
+------------------------------------------------------+
| Admin Panel                                          |
+------------------------------------------------------+
| Sidebar:                                             |
| - Products                                           |
| - Categories                                         |
| - Orders                                             |
| - Customers                                          |
+------------------------------------------------------+
| Main Content                                         |
| Table / Forms                                        |
+------------------------------------------------------+