# L.A.M.A. Medellín Backend - Clean Architecture

## Project Structure

This project follows Clean Architecture principles with the following layers:

```
LAMAMedellin/
├── src/
│   ├── LAMAMedellin.Domain/          # Domain Layer (Entities)
│   │   └── Entities/
│   │       └── Miembro.cs
│   │
│   ├── LAMAMedellin.Application/     # Application Layer (Interfaces, DTOs)
│   │   ├── Interfaces/
│   │   │   └── IMiembroRepository.cs
│   │   └── DTOs/
│   │       ├── MiembroDto.cs
│   │       └── CreateMiembroDto.cs
│   │
│   ├── LAMAMedellin.Infrastructure/  # Infrastructure Layer (DbContext, Repositories)
│   │   ├── Data/
│   │   │   └── ApplicationDbContext.cs
│   │   └── Repositories/
│   │       └── MiembroRepository.cs
│   │
│   └── LAMAMedellin.API/             # API Layer (Controllers, Configuration)
│       ├── Controllers/
│       │   └── MiembrosController.cs
│       ├── Program.cs
│       └── appsettings.json
│
└── LAMAMedellin.sln
```

## Clean Architecture Layers

### 1. Domain Layer (`LAMAMedellin.Domain`)
Contains the core business entities and domain logic. This layer has no dependencies on other layers.

- **Entities**: Business objects (e.g., `Miembro`)

### 2. Application Layer (`LAMAMedellin.Application`)
Contains application business logic and interfaces. Depends only on the Domain layer.

- **Interfaces**: Repository contracts (e.g., `IMiembroRepository`)
- **DTOs**: Data Transfer Objects for API communication

### 3. Infrastructure Layer (`LAMAMedellin.Infrastructure`)
Contains implementations of interfaces defined in the Application layer. Handles data access and external services.

- **Data**: Entity Framework DbContext
- **Repositories**: Repository implementations

### 4. API Layer (`LAMAMedellin.API`)
The presentation layer that exposes the API endpoints. Depends on Application and Infrastructure layers.

- **Controllers**: REST API controllers
- **Configuration**: Dependency injection and app configuration

## Getting Started

### Prerequisites
- .NET 9.0 SDK
- SQL Server (local or Azure SQL Database)

### Configuration

1. Update the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=LAMAMedellinDB;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
  }
}
```

2. Create the database and apply migrations:
```bash
cd src/LAMAMedellin.API
dotnet ef migrations add InitialCreate --project ../LAMAMedellin.Infrastructure
dotnet ef database update
```

### Build and Run

```bash
# Build the solution
dotnet build

# Run the API
cd src/LAMAMedellin.API
dotnet run
```

The API will be available at:
- HTTPS: https://localhost:5001
- HTTP: http://localhost:5000
- Swagger UI: https://localhost:5001/swagger

## API Endpoints

### Miembros Controller

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/miembros` | Get all members |
| GET | `/api/miembros/{id}` | Get member by ID |
| POST | `/api/miembros` | Create new member |
| PUT | `/api/miembros/{id}` | Update existing member |
| DELETE | `/api/miembros/{id}` | Delete member |

## Database Schema

The `Miembros` table contains the following fields:
- Personal Information: Id, Nombre, Apellido, Cedula, FechaNacimiento, RH
- Contact: Celular, CorreoElectronico, Direccion, Ciudad
- Member Information: Member, Cargo, Rango, Estatus, FechaIngreso
- Emergency: ContactoEmergencia, Padrino
- Health: EPS
- Motorcycle: Moto, Marca, AnoModelo, CilindrajeCC, PlacaMatricula
- Documents: FechaExpedicionLicenciaConduccion, FechaExpedicionSOAT, Foto

## Dependencies

- **Microsoft.EntityFrameworkCore** (9.0.10): ORM for data access
- **Microsoft.EntityFrameworkCore.SqlServer** (9.0.10): SQL Server provider
- **Microsoft.EntityFrameworkCore.Design** (9.0.10): Design-time support for EF Core
- **Swashbuckle.AspNetCore** (9.0.6): Swagger/OpenAPI support

## Future Enhancements

- Azure Active Directory B2C integration for authentication
- Social media authentication (Facebook, Google)
- File upload for photos
- Advanced search and filtering
- Data export/import functionality
