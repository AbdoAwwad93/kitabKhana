# Bookstore

**Bookstore** is a learning project built with ASP.NET Core MVC, Entity Framework Core, and Stripe integration. It simulates an online bookstore where users can browse books and authors, manage a shopping cart, register and manage their profiles, and complete purchases with online payment. The project is designed for educational purposes to demonstrate full-stack web development with .NET technologies.

---

## Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Database](#database)
- [Setup & Installation](#setup--installation)
- [Usage](#usage)
- [Learning Goals](#learning-goals)
- [License](#license)

---

## Features

- **User Authentication & Profile**
  - Register, login, and logout
  - Edit profile and view purchase history
  - Username availability check

- **Book Management**
  - Browse all books
  - View book details, related books, and user comments
  - Add comments and ratings to books

- **Author Management**
  - Browse all authors
  - View author details and related authors

- **Shopping Cart**
  - Add and remove books from cart
  - View cart contents

- **Checkout & Payment**
  - Delivery options (Standard, Express, Same Day)
  - Stripe integration for secure online payments
  - Order success and cancellation pages

- **Admin/Repository Pattern**
  - Generic repository for data access abstraction

---

## Tech Stack

- **Backend:** ASP.NET Core MVC (.NET 9.0)
- **ORM:** Entity Framework Core (with SQL Server)
- **Authentication:** ASP.NET Core Identity
- **Payment:** Stripe API
- **Frontend:** Razor Views (CSHTML), Bootstrap, jQuery
- **Other:** Repository Pattern, ViewModels, Partial Views

---

## Project Structure

```
Bookstore/
│
├── Controllers/         # MVC controllers (Account, Book, Author, Cart, Payment, Home)
├── Models/              # Entity models (Book, Author, Customer, Cart, Comment, etc.)
├── ViewModel/           # View models for passing data to views
├── Views/               # Razor views (organized by feature)
│   ├── Shared/          # Layouts and shared partials
│   ├── Book/            # Book-related views
│   ├── Author/          # Author-related views
│   ├── Account/         # Authentication and profile views
│   ├── Cart/            # Cart view
│   ├── Payment/         # Payment and delivery views
│   └── Home/            # Home and privacy pages
├── Configuration/       # Entity Framework model configurations
├── DBContext/           # AppDBContext for EF Core
├── Migrations/          # EF Core migrations
├── wwwroot/             # Static files (CSS, JS, images)
├── appsettings.json     # App configuration (connection strings, Stripe keys)
├── Bookstore.csproj     # Project file
└── Program.cs           # Application entry point
```

---

## Database

- **SQL Server** is used as the database.
- **Entity Framework Core** handles migrations and data access.
- **Main Entities:** Book, Author, Customer, Cart, Comment, Category, BookAuthor, Delivery

---

## Setup & Installation

1. **Clone the repository:**
   ```bash
   git clone <repository-url>
   cd Bookstore
   ```

2. **Configure the database:**
   - Update the `ConnectionStrings` section in `appsettings.json` with your SQL Server details.

3. **Configure Stripe:**
   - Add your Stripe `PublishableKey` and `SecretKey` in `appsettings.json` under the `Stripe` section.

4. **Apply migrations:**
   ```bash
   dotnet ef database update
   ```

5. **Run the application:**
   ```bash
   dotnet run
   ```
   The app will be available at `https://localhost:5001` (or as configured).

---

## Usage

- Register a new account or log in.
- Browse books and authors.
- Add books to your cart and proceed to checkout.
- Choose a delivery method and pay securely via Stripe.
- View your profile and purchase history.

---

## Learning Goals

This project is intended for educational purposes and demonstrates:

- Building a full-stack web application with ASP.NET Core MVC
- Implementing authentication and authorization with Identity
- Using Entity Framework Core for data access and migrations
- Applying the repository pattern for clean architecture
- Integrating third-party payment providers (Stripe)
- Structuring a real-world MVC project with best practices

---

## License

This project is for learning and demonstration purposes only. Feel free to use, modify, and share for educational use.

---

**Note:** This is a learning project and not intended for production use. Security, scalability, and advanced error handling are simplified for clarity. 