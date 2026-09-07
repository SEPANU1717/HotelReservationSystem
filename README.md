# Hotel Reservation System

Hotel Reservation System is a Windows Forms desktop application for managing hotel operations such as room reservations, guest records, check-in/check-out, billing, and user access.

## Features

- **User authentication and access control**
  - Login flow with role-based menu visibility (admin and non-admin access).
  - Forgot-password flow with reset code verification via email.

- **Dashboard**
  - Quick overview of room statistics:
    - Total rooms
    - Available rooms
    - Occupied rooms
    - Reserved rooms
  - Reservation summary display.

- **Room management**
  - Room listing and room status handling.
  - Support for room type/rate categorization.

- **Customer management**
  - Add, update, search, and manage customer records.
  - Connect customer selection directly to reservation flow.

- **Reservation management**
  - Create and manage reservations.
  - Date validation and reservation status handling.
  - Reservation receipt support.

- **Check-in / Check-out**
  - Guest check-in and check-out workflows.
  - Companion/guest companion handling.
  - Check-in receipt generation.

- **Billing and invoicing**
  - Billing computation and invoice generation/printing.
  - Checkout-related billing support.

- **Service module (Food)**
  - Food stock management.
  - Food ordering and order confirmation flow.
  - Running total for placed food orders.

- **Database-backed architecture**
  - Layered solution with separate projects for UI (Presenter), Domain, Data, and Database.
  - SQL Server database project with schema scripts for rooms, reservations, customers, billing, users, services, and more.

## Tech Stack

- **.NET Framework 4.8**
- **C# Windows Forms**
- **Entity Framework 6**
- **SQL Server Database Project**

