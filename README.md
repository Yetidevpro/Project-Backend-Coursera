### Project_Backend

## Overview

This is a backend application built using ASP.NET Core that provides API endpoints for managing users and books. The application stores data in JSON files and uses a repository pattern for data access.

## Features

CRUD operations for users and books

Assign books to users

Return books from users

Swagger API documentation

JSON file-based storage

## Technologies Used

ASP.NET Core

C#

Newtonsoft.Json

Dependency Injection

## Setup Instructions

# Prerequisites

.NET 6 or later

Visual Studio or VS Code

# Installation

Clone the repository:

git clone https://github.com/your-username/your-repo.git

Navigate to the project folder:

cd Project_Backend

Restore dependencies:

dotnet restore

Run the application:

dotnet run

Access Swagger UI at:

http://localhost:5157/swagger

## API Endpoints

# Users API

GET /api/users - Get all users

POST /api/users - Create a new user

PUT /api/users/{id} - Edit a user

DELETE /api/users/{id} - Delete a user

POST /api/users/{userId}/books - Assign a book to a user

DELETE /api/users/{userId}/books/{bookId} - Remove a book from a user

# Books API

GET /api/books - Get all books

POST /api/books - Add a new book

PUT /api/books/{id} - Edit a book

DELETE /api/books/{id} - Delete a book

# Project Structure

Project_Backend/
├── Controllers/
│   ├── BooksController.cs
│   ├── UsersController.cs
├── Models/
│   ├── Book.cs
│   ├── User.cs
├── Repositories/
│   ├── IJsonFileRepository.cs
│   ├── JsonFileRepository.cs
├── Program.cs
├── appsettings.json
├── launchSettings.json

# JSON File Storage

The application uses users.json and books.json to store data persistently.
