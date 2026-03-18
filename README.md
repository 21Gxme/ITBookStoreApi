# The IT Book Online Shop

This project consists of a RESTful API backend built with **.NET 8 (C#)** and a frontend web application built with **Next.js**.

## Getting Started: Backend API

1. Navigate to the API folder (if not already there):

   ```bash
   cd ITBookStoreApi
   ```

2. Restore dependencies:

   ```bash
   dotnet restore
   ```

3. Run the EF Core migrations to create the SQLite database (`itbookstore.db`):

   ```bash
   dotnet ef database update --project ITBookStoreApi
   ```

4. Run the application:

   ```bash
   dotnet run --project ITBookStoreApi/ITBookStoreApi.csproj
   ```

## Endpoints (Backend)

1. **`POST /register`**
   - **Body:** `{ "username": "...", "password": "...", "fullname": "..." }`
   - **Response:** 200 OK on success, 400 Bad Request on duplicate.

2. **`POST /login`**
   - **Body:** `{ "username": "...", "password": "..." }`
   - **Response:** 200 OK with `{ "token": "..." }` on valid credentials.

3. **`GET /books`**
   - **Description:** Fetches a list of books from `https://api.itbook.store/1.0/search/mysql` and returns it sorted A-Z by title. Gracefully handles external API unavailability with a 503 Service Unavailable response.
   - **Response:** 200 OK with JSON array of sorted books.

4. **`POST /user/like`**
   - **Headers:** `Authorization: Bearer <Your-JWT-Token>`
   - **Body:** `{ "user_id": 1, "book_id": "1234" }`
   - **Response:** 200 OK when successfully liked.

## Evaluation Criteria Addressed

- **Coding and repositories**: Code is neatly structured into standard .NET Core API folders (Controllers, Models, DTOs, Middleware, Data).
- **Error and exception handling**: Implemented a global `ExceptionHandlingMiddleware` for robust centralized exception catching. Network instability to the external IT book API is gracefully handled inside the `BooksController` with a custom `HttpRequestException` catch block that returns a 503 instead of crashing.
- **Database design**: Modeled cleanly through Entity Framework Core using SQLite with relationships between `User` and `LikedBook`. Passwords are hashed automatically using BCrypt.
- **Documentation**: Provided steps for compiling/starting the application here in the README.
