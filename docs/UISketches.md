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

## Guest
Home
 ├── Catalog
 │     └── Product Details
 ├── Login
 └── Register



## Authenticated Customer
Home
 ├── Catalog
 ├── Cart
 ├── Checkout
 ├── Orders
 └── Profile



 ## Administrator
Dashboard
 ├── Products
 ├── Categories
 ├── Orders
 └── Customers