# ManagementInvoices

A comprehensive invoice management system built with .NET 9, React, and MySQL. This project follows Clean Architecture principles with CQRS pattern using MediatR.

## 🏗️ Architecture

The solution is structured using Clean Architecture with the following layers:

- **Domain**: Core business entities and domain logic
- **Application**: Application services, commands, queries, and business rules
- **Infrastructure**: Data access, external services, and infrastructure concerns
- **API**: Web API layer with REST endpoints
- **Frontend**: React TypeScript application

## 🚀 Features

- **Product Management**: CRUD operations for products
- **Clean Architecture**: Separation of concerns with domain-driven design
- **CQRS Pattern**: Command Query Responsibility Segregation using MediatR
- **Entity Framework Core**: ORM with MySQL database
- **FluentValidation**: Input validation
- **Swagger/OpenAPI**: API documentation
- **React Frontend**: Modern UI with TypeScript

## 📋 Prerequisites

Before you begin, ensure you have the following installed:

- **.NET 9 SDK** - [Download here](https://dotnet.microsoft.com/download/dotnet/9.0)
- **Node.js** (v18 or higher) - [Download here](https://nodejs.org/)
- **MySQL** (v8.0 or higher) - [Download here](https://dev.mysql.com/downloads/)
- **Git** - [Download here](https://git-scm.com/)

## 🛠️ Setup Instructions

### 1. Clone the Repository

```bash
git clone <repository-url>
cd ManagementInvoices
```

### 2. Database Setup

1. Install MySQL Server on your local machine
2. Ensure MySQL is running and accessible with the default credentials (root/root)
3. The database will be created automatically when running migrations

### 3. Backend Setup

#### Navigate to the API project
```bash
cd ManagementInvoices.API
```

#### Install dependencies and run migrations
```bash
# Restore NuGet packages
dotnet restore

# Run Entity Framework migrations (this will create the database automatically)
dotnet ef database update -p ../ManagementInvoices.Infrastructure -s ManagementInvoices.API

# Run the API
dotnet run
```

The API will be available at:
- **API**: https://localhost:7001
- **Swagger UI**: https://localhost:7001/swagger

### 4. Frontend Setup

#### Navigate to the frontend directory
```bash
cd managementinvoices-frontend
```

#### Install dependencies
```bash
npm install
```

#### Start the development server
```bash
npm start
```

The React application will be available at:
- **Frontend**: http://localhost:3000

## 🔧 Configuration

### Database Connection

The application uses MySQL with the following default configuration:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;port=3306;database=ManagementInvoicesDb;user=root;password=root"
  }
}
```

**For local MySQL**, update the connection string in `ManagementInvoices.API/appsettings.json` with your local MySQL credentials if different from the defaults.

### CORS Configuration

The API is configured to allow requests from the React frontend running on `http://localhost:3000`. If you change the frontend port, update the CORS policy in `Program.cs`.

## 🧪 Testing

### Run Backend Tests
```bash
cd ManagementInvoices.UnitTests
dotnet test
```

### Run Frontend Tests
```bash
cd managementinvoices-frontend
npm test
```

## 📁 Project Structure

```
ManagementInvoices/
├── ManagementInvoices.Domain/           # Domain entities and business logic
├── ManagementInvoices.Application/      # Application services and CQRS
├── ManagementInvoices.Infrastructure/   # Data access and external services
├── ManagementInvoices.API/             # Web API layer
├── ManagementInvoices.UnitTests/       # Unit tests
└── managementinvoices-frontend/        # React frontend application
```

## 🔄 API Endpoints

### Products
- `GET /api/products` - Get all products
- `GET /api/products/{id}` - Get product by ID
- `POST /api/products` - Create new product
- `PUT /api/products/{id}` - Update product
- `DELETE /api/products/{id}` - Delete product


