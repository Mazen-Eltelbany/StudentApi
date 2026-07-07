# StudentAPI

StudentAPI is a RESTful Web API built with **ASP.NET Core** that manages student records — including CRUD operations, filtering passed students, and calculating average grades. The project follows a layered architecture separating the API, business logic, and data access concerns.

![Language](https://img.shields.io/badge/language-C%23-239120)
![Framework](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-512BD4)
![Database](https://img.shields.io/badge/database-SQL%20Server-CC2927)

## ✨ Features

- Retrieve all students
- Retrieve only students who passed
- Calculate the average grade across all students
- Retrieve a single student by ID
- Add a new student
- Update an existing student's details
- Delete a student by ID
- Consistent, typed HTTP responses (`200`, `201`, `400`, `404`) with proper `ProducesResponseType` documentation for OpenAPI/Scalar

## 📡 API Endpoints

| Method   | Route                      | Description                          |
|----------|-----------------------------|---------------------------------------|
| `GET`    | `/api/Students/All`         | Get all students                      |
| `GET`    | `/api/Students/Passed`      | Get students who passed               |
| `GET`    | `/api/Students/AverageGrade`| Get the average grade of all students |
| `GET`    | `/api/Students/{id}`        | Get a single student by ID            |
| `POST`   | `/api/Students`             | Add a new student                     |
| `PUT`    | `/api/Students/{id}`        | Update an existing student            |
| `DELETE` | `/api/Students/{id}`        | Delete a student by ID                |

### Example: Add a Student

**Request**
```http
POST /api/Students
Content-Type: application/json

{
  "name": "Mazen Eltelbany",
  "age": 19,
  "grade": 95
}
```

**Response** — `201 Created`
```json
{
  "id": 1,
  "name": "Mazen Eltelbany",
  "age": 19,
  "grade": 95
}
```

## 🏗️ Project Structure

```
StudentAPI/
├── StudentAPIWithDB/              # API layer (controllers, startup config)
│   └── Controllers/
│       └── StudentApiController.cs
├── StudentAPIBussinessLayer/      # Business logic layer (Student entity, validation rules)
└── StudentDataAccess/             # Data access layer (StudentDTO, database operations)
```

The API follows a 3-layer architecture:
- **Presentation Layer** (`StudentAPIWithDB`) — exposes HTTP endpoints and handles request/response logic.
- **Business Layer** (`StudentAPIBussinessLayer`) — contains the `Student` class with core logic (validation, save, delete, average calculation).
- **Data Access Layer** (`StudentDataAccess`) — contains `StudentDTO` and handles direct communication with the database.

## 🛠️ Technologies Used

- **Language:** C#
- **Framework:** ASP.NET Core Web API
- **Database:** Microsoft SQL Server
- **API Documentation:** OpenAPI / Scalar
- **Tools:** Visual Studio 2022

## 📋 System Requirements

- .NET SDK (matching the project's target framework)
- SQL Server 2016 or higher
- Visual Studio 2022 (for development)

## 🚀 Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/Mazen-Eltelbany/StudentApi.git
   ```
2. Restore the `StudentsDB` database in SQL Server Management Studio.
3. Update the connection string in `appsettings.json` to point to your SQL Server instance.
4. Open the solution in Visual Studio 2022.
5. Run the project — the API will launch with Scalar/OpenAPI documentation available at `/scalar` (or `/swagger`, depending on configuration).

## 📄 License

This project is licensed under the **MIT License**.

Copyright (c) 2026 Mazen Eltelbany

## 👤 Author

**Mazen Eltelbany**