# Software Requirements Specification v1.0
===============================================================================

1. Introduction
    1.1 Purpose
    1.2 Scope
    1.3 Definitions
    1.4 References

===============================================================================

2. Overall Description
    2.1 Product Perspective
    2.2 Product Functions
    2.3 User Classes
    2.4 Operating Environment
    2.5 Design Constraints
    2.6 Assumptions and Dependencies

===============================================================================

3. Functional Requirements

     # FR-1 Product Catalog
    Derived from: URS-1

    ## FR-1.1 Display Products
    The system shall display all available products.

    ## FR-1.2 Product Categories
    The system shall organize products into categories.

    ## FR-1.3 Browse by Category
    The system shall allow users to browse products by category.



    # FR-2 Product Discovery
    Derived from: URS-2
    
    ## FR-2.1 Search Products
    The system shall allow users to search products by name.

    ## FR-2.2 Filter Products
    The system shall allow users to filter products by available product attributes.

    ## FR-2.3 Sort Products
    The system shall allow users to sort products using predefined criteria.

    ## FR-2.4 Paginate Results
    The system shall display search results using pagination.


    # FR-3 Product Details
    Derived from: URS-3

    ## FR-3.1 Display Product Information
    The system shall display detailed information about a selected product.

    ## FR-3.2 Display Product Availability
    The system shall display product availability status.


    # FR-4 Customer Accounts 
    Derived from: USR-4

    ## FR-4.1 Register User
    The system shall allow users to create a personal account.

    ## FR-4.2 Authenticate User
    The system shall allow users to log in to their account.

    ## FR-4.3 Logout User
    The system shall allow users to log out of their account.

    ## FR-4.4 Password Recovery
    The system shall allow users to recover their password.

    ## FR-4.5 Profile Management
    The system shall allow authenticated users to view and update their profile information.

    ## FR-4.6 Credential Management
    The system shall allow users to change account credentials such as password.


    # FR-5 Shopping Cart
    Derived from: URS-5 
    
    ## FR-5.1 Add Products to Cart
    The system shall allow users to add products to the shopping cart.

    ## FR-5.2 Update Product Quantity
    The system shall allow users to change the quantity of products in the shopping cart.

    ## FR-5.3 Calculate Item Total Price
    The system shall calculate and display the total price for each product based on quantity.

    ## FR-5.4 Calculate Cart Total Price
    The system shall calculate and display the total price of all items in the cart.

    ## FR-5.5 Remove Products from Cart
    The system shall allow users to remove products from the shopping cart.


    # FR-6 Checkout
    Dericed from: USR-6

    ## FR-6.1 Initiate Checkout
    The system shall allow users to initiate the checkout process.

    ## FR-6.2 Collect Shipping Information
    The system shall collect shipping information from the user.

    ## FR-6.3 Validate Cart
    The system shall validate the shopping cart before order creation.

    ## FR-6.4 Create Order
    The system shall create an order from the shopping cart.


    # FR-7 Orders
    Dericed from: URS-7

    ## FR-7.1 View Orders
    The system shall allow users to view their orders.

    ## FR-7.2 View Order Details
    The system shall display detailed information about a selected order.

    ## FR-7.3 View Order Status
    The system shall display the current status of an order.

    ## FR-7.4 Cancel Order
    The system shall allow users to cancel eligible orders.
 
    

    # FR-8 Administration
    Dericed from: URS-8
    
    ## FR-8.1 Product Management
    The system shall allow administrators to create, update, deactivate, and delete products.

    ## FR-8.2 Category Management
    The system shall allow administrators to create, update, and delete categories.

    ## FR-8.3 Order Management
    The system shall allow administrators to view, update, and manage customer orders.

    ## FR-8.4 Customer Management
    The system shall allow administrators to view and manage customer accounts.

===============================================================================

4.  Access Control Rules
    # ACR-1
    Only authenticated users shall be allowed to access shopping cart functionality.

    # ACR-2
    Only authenticated users shall be allowed to perform checkout operations.

    # ACR-3
    Only authenticated users shall be allowed to view and manage orders.

    # ACR-4
    Only administrators shall be allowed to access product, category, order, and customer management features.

    # ACR-5
    Unauthenticated users shall be allowed to browse product catalog and view product details.

===============================================================================

5. Non-Functional Requirements
    # NFR-1 Performance
    The system shall load product catalog pages within 2 seconds under normal load.

    # NFR-2 Security
    The system shall store user passwords using secure hashing algorithms.
    The system shall protect sensitive user data from unauthorized access.

    # NFR-3 Reliability
    The system shall ensure that no confirmed order is lost in case of system failure.

    # NFR-4 Maintainability
    The system shall be structured in a layered architecture to allow independent evolution of components.

    # NFR-5 Usability
    The system shall provide a simple and consistent user interface for browsing and purchasing products.

    # NFR-6 Scalability
    The system shall be designed to support increasing numbers of users and products without architectural changes.

===============================================================================

6. Business Rules
    # BR-1
    A Product shall belong to exactly one Category.

    # BR-2
    A Customer shall own exactly one active Shopping Cart.

    # BR-3
    A Shopping Cart shall contain zero or more CartItems.

    # BR-4
    A CartItem shall reference exactly one Product.

    # BR-5
    An Order shall contain one or more OrderItems.

    # BR-6
    An OrderItem shall store the product price at the time of order creation.

    # BR-7
    An Order shall be immutable after cancellation or completion.

===============================================================================

7. Future Enhancements
    - Payment integration (Stripe, PayPal)
    - Order shipment tracking
    - Product reviews and ratings
    - Discounts and coupon system
    - Return and refund management
    - Email notifications

===============================================================================

8. Glossary:
    # Checkout
    The process of converting the contents of a shopping cart into a customer order by collecting required information and confirming the purchase.

    # Order
    A confirmed purchase created from a shopping cart after successful checkout.

    # Shopping Cart
    A temporary collection of products selected by a customer before order creation.

===============================================================================