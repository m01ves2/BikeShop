# Use Cases

## UC-1 Browse Catalog
Primary Actor: Customer, Guest
Trigger: Customer opens the catalog.
Preconditions: None.

Main Flow:
1. The system displays product categories.
2. Customer selects a category.
3. The system displays products from the selected category.
Extensions:
2a. The selected category contains no products.
    2a-1. The system displays a message that no products are available.
    2a-2. The customer selects another category.
    2a-3. Return to step 2.

Success Guarantee: The customer views the selected products.



# UC-2 View Product Details
Primary Actor: Customer, Guest
Trigger: Customer selects a product.
Preconditions: None

Main Flow:
1. Customer selects product
2. The system displays the product details.
Extensions:
2a. No product found
    2a-1. The system displays a message that no product found
    2a-2. The customer pushes button "return to catalog"
    2a-3. The system redirects the customer to the catalog page.

Success Guarantee: The customer views the selected product.



# UC-3 Register Account
Primary Actor: Guest
Trigger: Customer opens the register page
Preconditions: Customer is not authenticated

Main flow:
1. Customer opens the register page.
2. Customer enters credentials.
3. Customer submits the form.
4. The system registers the customer.
5. The system redirects the customer to the home page.
Extensions:
4a. Registration data is invalid.
    4a-1. The system displays an error message.
    4a-2. The system returns to the register form.
    4a-3. Resume at step 2.
4b. Customer already exists in system.
    4a-1. The system informs the customer that the account already exists.
    4a-2. Resume at step 2.

Success Guarantee: The customer succesfully registered



# UC-4 Log In
Primary Actor: Guest
Trigger: Customer opens the log in page
Preconditions: Customer is not authenticated 

Main Flow:
1. Customer opens the log in page.
2. Customer enters credentials.
3. Customer submits the form.
4. The system authenticates the customer.
5. The system redirects the customer to the home page.
Extensions:
4a. Invalid credentials.
    4a-1. The system displays an error message.
    4a-2. The system returns to the login form.
    4a-3. Resume at step 2.

Success Guarantee: The customer succesfully logged in



# UC-5 Add Product to Cart
Primary Actor: Customer, Guest
Trigger: Customer selects "Add to Cart" for a product.
Preconditions: The selected product is available for purchase.

Main Flow:
1. Customer initiates adding the product to the shopping cart.
2. The system adds the selected product to the shopping cart.
3. The system updates the shopping cart.
4. The system confirms that the product has been added.
Extensions:

Success Guarantee: The product appeared in shopping cart



# UC-6 Remove Product from Cart
Primary Actor: Customer, Guest
Trigger: Customer initiates the removal of a product from the shopping cart.
Preconditions: Shopping cart contains the selected product.

MainFlow
1. Customer opens the shopping cart.
2. Customer chooses to remove a product.
3. The system removes the selected product.
4. The system updates the shopping cart.
5. The system displays the updated shopping cart.
Extensions:
2a. Customer decreases the product quantity to zero.
    2a-1. The system removes the product from the shopping cart.
    2a-2. The system updates the shopping cart.
    2a-3. Resume at step 5.



# UC-7 View Shopping Cart, Guest
Primary Actor: Customer
Trigger: Customer selects "Shopping cart"
Preconditions: None

Main Flow:
1. System opens the shopping cart.
2. The system shows the actual list of added products
3. Customer can change quantity of products
4. Customer can initiate creation of order
Extensions:

Success Guarantee: Customer sees the actual shopping cart 



# UC-8 Checkout
Primary Actor: Customer
Trigger: Customer selects "Checkout".
Preconditions:
- Customer is authenticated.
- Shopping cart is not empty.

Main Flow:
1. Customer opens the checkout page.
2. System displays order summary.
3. Customer enters shipping information.
4. Customer confirms the order.
5. System validates the data.
6. System creates the order.
7. System clears the cart.
8. System displays confirmation.
Extensions:

5a. Shipping information is invalid.
    5a-1. The system displays validation errors.
    5a-2. The customer corrects the information.
    5a-3. Resume at step 4.
6a. Order creation fails.
    6a-1. The system displays an error message.
    6a-2. The customer may retry the operation.

Success Guarantee: Order has been created.


# UC-9 View Orders History
Primary Actor: Customer
Trigger: Customer selects "Orders History" in his account.
Preconditions: Customer is authenticated.

Main Flow:
1. Customer opens his account settings
2. System shows menu of options
3. Customer chooses "Orders History"
4. System shows actial list of orders

Success Guarantee: Orders history shown to Customer

# UC-10 Log Out
Primary Actor: Customer
Trigger: Customer chooses the log out control
Preconditions: Customer is authenticated 

Main Flow
1. The system ends the authenticated session.
2. The customer is redirected to the home page.
Extensions:

Success Guarantee: The customer succesfully logged out