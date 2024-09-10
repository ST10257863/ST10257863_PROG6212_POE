---

# Contract Monthly Claim System (CMCS)

## Overview
The Contract Monthly Claim System (CMCS) is a .NET-based web application designed to streamline the submission and approval of monthly claims for independent contractor lecturers. The system offers an intuitive, user-friendly interface and ensures that the claim process is efficient and secure.

## Features
- **Simple and intuitive GUI**: Feature-forward design with all necessary functionalities directly accessible to the user.
- **Grayscale design**: Minimalist interface to avoid confusion, with a clean and professional appearance.
- **Admin-controlled user registration**: Enhances security by restricting access to the system only to authorized users.
- **Claim submission and approval**: Users can submit monthly claims, and academic managers or coordinators can review and approve them.
- **Role-based functionality**: Different roles (Lecturer, Coordinator, Academic Manager) inherit common functionality but also have distinct features as per their responsibilities.

## System Design
- **Frontend**: The user interface was designed to be simple, professional, and easy to navigate. It avoids complex menu structures to keep navigation straightforward.
- **Database Structure**: The system's database structure is streamlined, storing users, claims, and associated documents. The database is designed to ensure consistency and ease of management, as reflected in the provided UML diagram.

## Requirements
- **.NET Core**
- **SQL Database** for storing users, claims, and documents.
- **ASP.NET Core MVC** (for web-based interface).
- **Visual Studio 2022** (or later).

## Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/ST10257863/ST10257863_PROG6212_POE.git
   ```
2. Open the solution in **Visual Studio**.
3. Restore NuGet packages:
   ```bash
   dotnet restore
   ```
4. Run the database migrations to set up the database:
   ```bash
   dotnet ef database update
   ```
5. Run the project:
   ```bash
   dotnet run
   ```

## Usage
- **Admin**: Can create and manage user accounts, and review claims.
- **Lecturer**: Can submit claims for work completed during the month.
- **Coordinator/Manager**: Can review, approve, or reject claims.

## Assumptions
- User registration is controlled by an admin, so there is no public-facing registration form.
- Security measures have been taken to prevent unauthorized access and DDoS attacks.

## UML Diagram
The system's architecture and database schema are illustrated in the provided UML diagram, which shows the relationships between the user roles and their claims.

## Contributing
If you'd like to contribute to the project:
1. Fork the repository.
2. Create a new branch for your feature/bug fix.
3. Submit a pull request with a detailed explanation of the changes made.

## License
This project is licensed under the MIT License. See the LICENSE file for more details.

## References
- Microsoft. (2023). Introduction to ASP.NET Core. Retrieved from: [https://learn.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-8.0](https://learn.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-8.0) [Accessed: 10 September 2024].

---
