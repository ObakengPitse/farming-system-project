# Agri-Energy Connect Web Application Prototype

## Overview
This is a functional prototype of the **Agri-Energy Connect** system built with ASP.NET MVC and Entity Framework. The system facilitates collaboration between South African farmers and green energy providers.

## Features
- Secure login for Farmers and Employees
- Farmers can add and manage products
- Employees can register new farmers
- Employees can view and filter farmers' product data
- Relational database integration with sample data
- Responsive Razor-based UI

## Technologies Used
- ASP.NET MVC 5
- Entity Framework Core
- Razor Pages
- SQL Server (LocalDb)/ AzureSQL
- Bootstrap (for UI styling)

## Setup Instructions

### Prerequisites
- [.NET SDK 7.0+](https://dotnet.microsoft.com/en-us/download)
- [SQL Server Express LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) with ASP.NET and web development workloads

### Clone the Repository
```bash
git clone https://github.com/your-repo/agri-energy-connect.git
cd agri-energy-connect
```

### Database Setup
1. Open the solution in Visual Studio.
2. Open the **Package Manager Console** (Tools > NuGet Package Manager > Package Manager Console).
3. Run the following commands:
```bash
Update-Database
```
This will apply migrations and seed the database with sample data.

### Running the Application
1. Press **F5** or click **Start Debugging** in Visual Studio.
2. The application will launch in your browser at `https://localhost:xxxx`.

### Default Sample Users
- **Farmer**
  - Email: `john@example.com`
  - Password: `SecurePass123` 

- **Employee**
  - Email: `bob@example.com`
  - Password: `SafePwd321`

## Functional Areas

### Farmer Role
- Login as farmer
- View list of own products
- Add new products (name, category, production date)

### Employee Role
- Login as employee
- Register new farmers
- View product listings by specific farmers
- Filter products by date range and category

## Project Structure
- **Models/** - Entity classes: Farmer, Product, Employee
- **Controllers/** - Account, Farmer, and Employee logic
- **Views/** - Razor views for all pages
- **Data/** - `AppDBContext.cs` database context with seed data

## Notes
- Passwords are currently stored as plaintext for demonstration. Use hashing in production (e.g., BCrypt).
- Authentication is role-based using simple session logic or ASP.NET Identity if extended.
- For a more scalable version, consider using API + React frontend.

## License
This prototype is for academic/demonstration purposes and not intended for production without further security and scalability enhancements.

---
© 2025 Agri-Energy Connect