# Use Cases


## UC-1 Browse Catalog

**Actors:** Guest, Customer

**Main Flow:**

1. The user opens the home page or a category page.
2. The system displays product categories and products.
3. The user selects a product.
4. The system displays product details, availability, and images.

**Result:**  
The user can view products before adding them to a cart.


## UC-2 Manage Guest Cart

**Actor:** Guest

**Main Flow:**

1. The Guest adds a product to the cart.
2. The system stores the cart in browser local storage.
3. The Guest can change quantity or remove an item.
4. The system updates the cart summary.

**Result:**  
The Guest can prepare a cart before registration or login.


## UC-3 Register and Log In

**Actor:** Guest

**Main Flow:**

1. The Guest opens the registration or login page.
2. The Guest enters valid credentials.
3. The system creates an account or authenticates the user.
4. The system creates a JWT session.
5. The user becomes an authenticated Customer.

**Extensions:**

- The email is already registered.
- The credentials are invalid.
- The entered data does not pass validation.

**Result:**  
The user can access customer features.


## UC-4 Synchronize Cart After Login

**Actor:** Customer

**Precondition:**  
The Customer has a local cart created before login.

**Main Flow:**

1. The Customer logs in.
2. Blazor sends the local cart to the API.
3. The system merges the local cart with the server cart.
4. The system removes products that are no longer available.
5. The system updates the local cart with the result.

**Result:**  
The Customer has one current server cart.


## UC-5 Create Order

**Actor:** Customer

**Preconditions:**

- The Customer is authenticated.
- The server cart is not empty.

**Main Flow:**

1. The Customer opens the checkout page.
2. The system displays the order summary.
3. The Customer enters delivery information.
4. The Customer confirms the order.
5. The system validates the cart and product stock.
6. The system creates an order.
7. The system clears the server cart.

**Extensions:**

- The cart is empty.
- A product is no longer available.
- The requested quantity is higher than the available stock.
- Delivery information is invalid.

**Result:**  
A new order is created with status `Pending`.


## UC-6 Manage Personal Orders

**Actor:** Customer

**Main Flow:**

1. The Customer opens the orders page.
2. The system displays the Customer's orders.
3. The Customer opens an order.
4. The system displays order details.
5. If the order allows it, the Customer can edit delivery information or cancel the order.

**Result:**  
The Customer can track and manage eligible personal orders.


## UC-7 Manage Categories and Products

**Actor:** Administrator

**Main Flow:**

1. The Administrator opens the administration area.
2. The Administrator creates, edits, or deletes a category or product.
3. The system validates the data.
4. The system saves the changes.
5. The updated catalog becomes available to users.

**Result:**  
The Administrator can maintain the product catalog.


## UC-8 Manage Product Images

**Actor:** Administrator

**Precondition:**  
The product already exists.

**Main Flow:**

1. The Administrator opens the product editor.
2. The system displays existing product images.
3. The Administrator selects new WebP images or marks existing images for deletion.
4. The Administrator saves product changes.
5. The system deletes marked files and saves new files.

**Result:**  
The product gallery is updated.


## UC-9 Process Customer Order

**Actor:** Administrator

**Main Flow:**

1. The Administrator enters an order ID.
2. The system displays order details.
3. The Administrator selects an allowed new status.
4. The system validates the status change.
5. The system saves the new status.

**Extensions:**

- The order does not exist.
- The selected status transition is not allowed.

**Result:**  
The order status is updated according to domain rules.