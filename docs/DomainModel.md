# Domain Model
Entities

## Product
Represents a product available for purchase, such as a bicycle, bicycle part, or accessory.

Examples:
Bicycles
Components
Accessories

Responsibilities:
- Stores product information.
- Belongs to a category.
- Can be added to a shopping cart.
- Can appear in order items.


Relationships:
- Belongs to one Category.
- Can be referenced by many CartItems.
- Can be referenced by many OrderItems.


## Category
Represents a logical group of products used for organizing the catalog.

Examples:
Mountain Bikes
Road Bikes
Gravel Bikes
City Bikes
BMX
Electric Bikes
Kids Bikes

Responsibilities:
- Organizes products.
- Provides navigation through the catalog.

Relationships:
- Contains many Products.

## Cart
Description:
Represents the customer's current shopping cart.

Responsibilities:
- Belongs to one Customer.
- Contains CartItems.
- Calculates the total price.
- Can be converted into an Order.


## CartItem
Description:
Represents a product stored in a shopping cart.

Responsibilities:
- References one product.
- Stores the selected quantity.
- Calculates the subtotal.

Relationships:
- Belongs to one Cart.
- References one Product.

## Order
Description:
Represents a completed purchase created by a customer.

Examples:
- Order 1001
- Order 1002

Responsibilities:
- Belongs to one Customer.
- Contains purchased items.
- Stores shipping information.
- Stores the total order price.
- Represents a completed checkout.

Relationships:
- Belongs to one Customer.
- Contains many OrderItems.

## OrderItem
Description:
Represents a product included in an order.

Responsibilities:
- References one Product.
- Stores the purchased quantity.
- Stores the product price at the time of purchase.
- Calculates the subtotal.

Relationships:
- Belongs to one Order.
- References one Product.

## Customer
Description:
Represents a registered customer of the online store who performs purchases.

Responsibilities:
- Browses the product catalog.
- Adds products to the shopping cart.
- Places orders.
- Views order history.

Relationships:
- Owns one Cart.
- Can place many Orders.
- Associated with an Identity account (authentication handled externally by infrastructure layer).

Notes:
- Customer is a domain concept and is independent from authentication/authorization system.
- Roles such as Administrator or Owner are not part of this entity and belong to system infrastructure.



Customer
   │
   ├──────── owns ───────► Cart
   │                         │
   │                         ▼
   │                     CartItem ─────► Product
   │                                        ▲
   │                                        │
   └──────── places ─────► Order            │
                              │             │
                              ▼             │
                         OrderItem ─────────┘

Category
    │
    ▼
Product