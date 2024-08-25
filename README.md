# Restaurant Management Web Application

## Project Overview
The Restaurant Management Web Application is a full-stack application designed to manage and interact with a restaurant's menu and orders. Using a C# backend with ASP.NET Core and an Angular frontend, the application provides a comprehensive platform for both customers and administrators.

### Key Features
- **Menu Management**: 
  - **Product Catalog**: View a dynamic list of menu items including appetizers, entrees, desserts, and drinks.
  - **Pagination**: Navigate through menu items with pagination and select different page sizes.
  - **Filtering**: Apply filters to view specific categories of menu items, such as vegetarian options or daily specials.
  - **Sorting**: Sort menu items by price (highest to lowest or lowest to highest) or alphabetically.
  - **Search**: Use search functionality to quickly find specific dishes or drinks.

- **Order Management**:
  - **Cart Functionality**: Add items to the cart, update quantities, and view real-time updates. The cart automatically reflects changes in the order summary.
  - **Voucher System**: Apply voucher codes for discounts on the total order value.

- **Checkout Process**:
  - **User Authentication**: Customers are required to log in to complete the checkout process.
  - **Address Management**: Users can save and manage their default delivery addresses, with Google Places autocomplete for address entry.
  - **Payment Integration**: Secure payment processing with Stripe. Test cards can be used for development purposes.
  - **Order Review**: Review order details including items, delivery address, and payment summary before finalizing the purchase.

- **Admin Functions**:
  - **Order Management**: Admins can view and manage customer orders.
  - **Menu Editing**: Admins can update the menu, including adding or removing items.

### Technology Stack
- **Backend**: C# with ASP.NET Core, using Entity Framework for database interactions and SQL Server as the database.
- **Frontend**: Angular with TypeScript, featuring Angular Material for UI components and Tailwind CSS for additional styling.
- **Payment Processing**: Stripe for handling payments, including Google Places autocomplete for address input.
- **Version Control**: Git for source control and version management.

### How to Set Up and Run the Project

#### Backend Setup (C# with ASP.NET Core)
1. **Clone the Repository**  
   Clone the repository from GitHub to your local machine:
    ```bash
    git clone https://github.com/akin11235/foodnet.git
    ```

2. **Navigate to the Backend Directory**  
   Change your working directory to the backend directory:
    ```bash
    cd Restaurant-WebApp/backend
    ```

3. **Restore Dependencies**  
   Restore the necessary packages using NuGet:
    ```bash
    dotnet restore
    ```

4. **Build the Project**  
   Build the solution to compile the backend:
    ```bash
    dotnet build
    ```

5. **Run the Backend**  
   Start the ASP.NET Core server:
    ```bash
    dotnet run
    ```

#### Frontend Setup (Angular)
1. **Navigate to the Frontend Directory**  
   Change your working directory to the frontend directory:
    ```bash
    cd Restaurant-WebApp/frontend
    ```

2. **Install Dependencies**  
   Install the required packages using npm:
    ```bash
    npm install
    ```

3. **Run the Development Server**  
   Start the Angular development server:
    ```bash
    ng serve
    ```

4. **Access the Application**  
   Open a web browser and go to `http://localhost:4200` to access the Angular frontend.

### Integration
Ensure that the frontend application is configured to communicate with the backend API. Update the API base URL in the Angular application to match the endpoint where the C# backend is running (usually `http://localhost:5000` or similar).

### Clean Up
To remove any build artifacts and temporary files:
```bash
# For Backend
dotnet clean

# For Frontend
npm cache clean --force
rm -rf node_modules
