# Actors

## Guest
Description:
A person who visits the online store to browse products without being authenticated.

Permissions:
- Browse Catalog
- View Product Details
- Add Product to Cart
- Remove Product from Cart
- View Shopping Cart
Restrictions:
- Cannot place orders
- Cannot view order history
- Cannot log out


## Customer
Description:
A person who visits the online store to browse products and purchase bicycles.

Role:
Primary Actor

Permissions:
- All Guest permissions
- Place orders
- View order history
- Log out

Restrictions:
- None

---


## Store Owner
Description:
The owner of the online store who provides products and receives customer orders.

Role:
Business Actor

Goals:
- Present products online
- Receive customer orders