# Todo-Management-Task

## 📋 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js & npm](https://nodejs.org/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/)

## Project Structure

    Backend/
    ├── Todo.API/          # API Controllers & Startup
    ├── Todo.Service/      # Business Logic
    ├── Todo.Core/         # Domain Models
    └── API.Infrastructure/ # EF Core & Repositories

    Frontend/
    └── Todo.Client/       # Static HTML/CSS/JS
        ├── index.html
        ├── style.css
        └── script.js

## 🛠 Setup Guide

### 1. Clone the Repository

```bash
git clone git@github.com:3brhmaan/Todo-Management-Task.git
cd Todo-Management-Task
```

### 2. Backend Setup

##### 1. Install Dependencies

```bash
    cd Todo.API
    dotnet restore
```

##### 2. Run Database Migration

```bash
    cd Todo.API
    dotnet ef database update
```

##### 3. Start the Backend

```bash
    dotnet watch run
```

### 3. Frontend Setup

##### 1. Install Dependencies

```bash
    cd Todo.Client
    npm install
```

##### 2. Start the Frontend

```bash
    npm run start
```
