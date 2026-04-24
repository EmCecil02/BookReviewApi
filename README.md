# 📚 Book Review API

## 🚀 Overview

The Book Review API is a RESTful web service built with ASP.NET Core that allows users to manage books and submit reviews. The application demonstrates backend development concepts including RESTful API design, relational database management, Entity Framework Core integration, and service-layer architecture.

This project was built as a full-stack backend application that communicates through HTTP requests and persists data using SQL Server.

---

## ✨ Features

### 📖 Book Management
- Create books
- Retrieve all books
- Retrieve a specific book by ID
- Delete books

### 👤 User Management
- Create users
- Retrieve users
- Delete users

### ⭐ Review System
- Users can submit reviews for books
- Reviews include ratings from 1–5
- Retrieve reviews for books

### 🏷️ Genre Classification
- Create genres
- Assign genres to books
- Support for many-to-many relationships between books and genres

---

## 🛠️ Technologies Used

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger/OpenAPI
- xUnit (for unit testing)

---

## 🧱 Architecture

The project follows a layered architecture:

- Controllers → handle HTTP requests
- Services → contain business logic
- Data Layer → Entity Framework Core DbContext
- DTOs → separate API contracts from database models

---

## 🔗 Database Relationships

### One-to-Many Relationships
- One User can create many Reviews
- One Book can have many Reviews

### Many-to-Many Relationship
- Books can belong to multiple Genres
- Genres can contain multiple Books

---

## 🌐 API Endpoints

### Books
| Method | Endpoint |
|---|---|
| GET | `/api/books` |
| GET | `/api/books/{id}` |
| POST | `/api/books` |
| DELETE | `/api/books/{id}` |

---

### Users
| Method | Endpoint |
|---|---|
| GET | `/api/users` |
| GET | `/api/users/{id}` |
| POST | `/api/users` |
| DELETE | `/api/users/{id}` |

---

### Reviews
| Method | Endpoint |
|---|---|
| GET | `/api/reviews` |
| GET | `/api/reviews/{id}` |
| POST | `/api/reviews` |
| DELETE | `/api/reviews/{id}` |

---

### Genres
| Method | Endpoint |
|---|---|
| GET | `/api/genres` |
| GET | `/api/genres/{id}` |
| POST | `/api/genres` |
| POST | `/api/genres/assign/{bookId}/{genreId}` |
| DELETE | `/api/genres/{id}` |

---

## ▶️ Running the Application

### Clone the repository

```bash
git clone <your-repository-url>
```

---

### Run the project

```bash
dotnet run
```

---

### Open Swagger

```text
https://localhost:<port>/swagger
```

---

## 🗄️ Database Setup

### Apply migrations

```bash
dotnet ef database update
```

---

## 🧪 Running Tests

```bash
dotnet test
```

---

## 📌 Future Improvements

- Authentication & Authorization
- Search and filtering
- Frontend client application
- Average book ratings
- Image upload support for book covers

---

## 👨‍💻 Author

Emma Cecil