# ASP.NET Core MVC Movie Management Demo

## Overview

This is a demo ASP.NET Core MVC project that provides a web application for managing movie categories and other movie-related data. It uses Dapper for database operations and is built with .NET 8.

## Features

- Create, Read, Update, and Delete (CRUD) operations for movie categories.
- Simple and lightweight using Dapper for data access.
- Responsive design using Bootstrap.

## Table of Contents

- [Installation](#installation)
- [Usage](#usage)

## Installation

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [Visual Studio Code](https://code.visualstudio.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Steps

1. Clone the repository:
    ```bash
    git clone https://github.com/your-username/your-repo-name.git
    cd your-repo-name
    ```

2. Restore the dependencies:
    ```bash
    dotnet restore
    ```

3. Update the database connection string in `appsettings.json`:
    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=YourDatabaseName;Trusted_Connection=True;MultipleActiveResultSets=true"
    }
    ```

4. Create the database and tables:
    ```sql
    CREATE TABLE Movies (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Movie_Name VARCHAR(50) NOT NULL,
        Movie_Price Decimal(10,2) NOT NULL,
    );
    CREATE TABLE MovieCategories (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Genre VARCHAR(50) NOT NULL,
        Description VARCHAR(255) NULL,
    );
    ```

5. Run the application:
    ```bash
    dotnet run
    ```

6. Open your browser and navigate to `https://localhost:5001`.

## Usage

### Running the Application

To run the application in development mode:
```bash
dotnet watch run
```
```Building the Application
dotnet publish --configuration Release
```

