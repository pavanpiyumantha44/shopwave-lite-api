# ShopWaveLite API

A RESTful e-commerce API built with ASP.NET Core (.NET 10), PostgreSQL (Neon), and JWT authentication.

## Live API
https://shopwave-lite-api.onrender.com

## Tech Stack
- ASP.NET Core Web API (.NET 10)
- Entity Framework Core + PostgreSQL (Neon)
- JWT Authentication + Refresh Tokens
- BCrypt password hashing
- Scalar API docs
- Docker + Render (deployment)

## Roles
| Role | Permissions |
|---|---|
| Admin | Full access |
| Vendor | Create, update, delete own products |
| Customer | Browse products, place and cancel orders |

## Endpoints

### Auth
| Method | Route | Access |
|---|---|---|
| POST | /api/auth/register | Public |
| POST | /api/auth/login | Public |
| POST | /api/auth/refresh | Public |
| POST | /api/auth/logout | Authenticated |

### Products
| Method | Route | Access |
|---|---|---|
| GET | /api/products | Public |
| GET | /api/products/{id} | Public |
| POST | /api/products | Vendor only |
| PUT | /api/products/{id} | Vendor only |
| DELETE | /api/products/{id} | Vendor only |

### Orders
| Method | Route | Access |
|---|---|---|
| POST | /api/orders | Customer only |
| GET | /api/orders | Customer only |
| GET | /api/orders/{id} | Authenticated |
| POST | /api/orders/{id}/cancel | Customer only |

## Running Locally

### Prerequisites
- .NET 10 SDK
- PostgreSQL (or Neon account)

### Setup
```bash
git clone https://github.com/pavanpiyumantha44/shopwave-lite-api.git
cd shopwave-lite-api

# Create local secrets file
cp ShopWaveLite.Api/appsettings.json ShopWaveLite.Api/appsettings.Development.json
# Edit appsettings.Development.json with your connection string and JWT secret

cd ShopWaveLite.Api
dotnet ef database update
dotnet run
```

### API Docs (local)