# 🎬 Movie API

Welcome to the **Movie API**! This project is a RESTful API built with C# for managing a collection of movies. Perfect for learning, demo apps, or as a starting point for your own movie database! 🍿✨

![Movie API Banner](https://github.com/abdoshady550/Movie_Api/blob/main/Output_Example.png?raw=true) <!-- Replace with your own image if you wish -->

---

## 🚀 Features
- 🛠️ Built with **.NET** for robust and scalable APIs
- 📦 Clean project structure for easy understanding
- 📝 CRUD operations for movies (Create, Read, Update, Delete) 
- ⭐Supports genres and movie ratings 
- 🖼️Upload and manage movie posters 
- 📦 Custom API responses for consistency 
- 🛡️ Simple rate limiting middleware to prevent abuse
- 🔀 API Versioning (v1, v2) support with headers

---

## 📂 Project Structure

```
├── Controllers/           # API controllers (MovieController.cs)
│   ├── v1/                # Version 1 Controllers (MovieController.cs)
│   └── v2/                # Version 2 Controllers (MovieController.cs with updates)
├── Data/
│   └── Configrations/     # Entity configurations (MovieConfigration.cs)
├── Handler/               # Custom API responses & exceptions (APIRespone.cs, ApiException.cs)
├── Mappers/               # DTO mappers (MovieMapper.cs)
├── Middleware/            # Middleware like RateLimiterMiddleware.cs
├── Model/
│   └── Eintites/          # Entity definitions (Movie.cs)
│   └── Dtos/              # Data Transfer Objects
├── Migrations/            # Entity Framework migrations
├── Services/              # Business logic and services (MovieService.cs)
├── Movie_Api.http         # Example HTTP requests
└── ...                    # Other standard files
```

---



---

## 🛠️ Usage

1. **Clone the repo**  
   ```bash
   git clone https://github.com/abdoshady550/Movie_Api.git
   cd Movie_Api
   ```

2. **Restore dependencies**  
   ```bash
   dotnet restore
   ```

3. **Run the API**  
   ```bash
   dotnet run
   ```

4. **Try the API**  
   Use [Postman](https://www.postman.com/) or `curl` to hit endpoints like:
   - `GET /api/movie` - List all movies
   - `GET /api/movie/{id}` - Get a movie by ID
   - `POST /api/movie` - Add a new movie
   - `PUT /api/movie/{id}` - Update a movie
   - `DELETE /api/movie/{id}` - Delete a movie

---
## 🔀 API Versioning

Versioning is now supported! multiple API versions (currently v1 and v2).

### 📌 How to specify version
You can use the -Accept- header:
- Version 1:
  ```
  Accept: application/json;v=1.0
  
  ```

- Version 2:
  ```
  Accept: application/json;v=2.0

  ```
Examples:

GET /api/movie (with Accept: application/json;v=1.0) → returns movies in v1 format

GET /api/movie (with Accept: application/json;v=2.0) → returns movies in v2 format

✔️ You can test using Swagger UI or Postman by adding the Accept header.

Refer to each version’s documentation for details about the available fields and differences between versions.
---
## 📚 Notable Files

- `Controllers/MovieController.cs` - API endpoints
 - `Controllers/v1/MovieController.cs` - Version 1 API
 - `Controllers/v2/MovieController.cs` - Version 2 API
- `Services/MovieService.cs` - Business logic
- `Model/Eintites/Movie.cs` - Movie entity definition
- `Handler/APIRespone.cs` - API response wrapper
- `Middleware/RateLimiterMiddleware.cs` - Rate limiting

---

## 🤝 Contributing

Contributions welcome! Please open an issue or submit a pull request.  
Let's make movie APIs awesome together! 🚀

---

## 👤 Author

- GitHub: [abdoshady550](https://github.com/abdoshady550)



---

> _Tip: To view more code or details, check the repo here: [Movie_Api on GitHub](https://github.com/abdoshady550/Movie_Api)_
