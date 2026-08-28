# Actors

## Guest

A visitor who uses the store without an account.

Permissions:

- Browse categories and products.
- View product details and images.
- Add products to a local cart.
- Change quantities, remove items, and clear the local cart.

Restrictions:

- Cannot create an order.
- Cannot view customer orders.
- Cannot access administration pages.

## Customer

A registered and authenticated user who can buy products.

Permissions:

- All Guest permissions.
- Synchronize the local cart with the server cart after login.
- Use a server cart.
- Create an order.
- View personal order history and order details.
- Edit delivery information for eligible orders.
- Cancel eligible orders.
- Log out.

Restrictions:

- Cannot access administration pages.
- Cannot view or change another customer's orders.

## Administrator

An authenticated user with the Admin role.

Permissions:

- Create, edit, and delete categories.
- Create, edit, and delete products.
- Add and remove product images.
- Find and view customer orders.
- Change order statuses according to the allowed transitions.

Restrictions:

- Cannot bypass the order status rules defined by the domain model.
