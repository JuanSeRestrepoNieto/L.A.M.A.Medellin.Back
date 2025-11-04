# L.A.M.A. Medellín Backend - Setup Guide

## Prerequisites

- .NET 9.0 SDK
- SQL Server (Local or Azure SQL Database)
- Visual Studio 2022, VS Code, or JetBrains Rider (optional)

## Initial Setup

### 1. Clone the Repository

```bash
git clone https://github.com/JuanSeRestrepoNieto/L.A.M.A.Medellin.Back.git
cd L.A.M.A.Medellin.Back
```

### 2. Configure Database Connection

Edit `src/LAMAMedellin.API/appsettings.json` and update the connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=LAMAMedellinDB;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
  }
}
```

For Azure SQL Database:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=tcp:your-server.database.windows.net,1433;Database=LAMAMedellinDB;User ID=your-username;Password=your-password;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }
}
```

### 3. Create Database and Apply Migrations

```bash
# Navigate to the API project
cd src/LAMAMedellin.API

# Install EF Core tools (if not already installed)
dotnet tool install --global dotnet-ef

# Create initial migration
dotnet ef migrations add InitialCreate --project ../LAMAMedellin.Infrastructure --startup-project .

# Apply migration to create the database
dotnet ef database update --project ../LAMAMedellin.Infrastructure --startup-project .
```

### 4. Build and Run

```bash
# Build the solution
dotnet build

# Run the API
cd src/LAMAMedellin.API
dotnet run
```

The API will start on:
- HTTP: http://localhost:5044
- HTTPS: https://localhost:7147

### 5. Access Swagger UI

Open your browser and navigate to:
- https://localhost:7147/swagger

## Project Structure

```
LAMAMedellin/
├── src/
│   ├── LAMAMedellin.Domain/          # Business entities
│   ├── LAMAMedellin.Application/     # Business logic interfaces
│   ├── LAMAMedellin.Infrastructure/  # Data access implementation
│   └── LAMAMedellin.API/             # API endpoints
└── LAMAMedellin.sln
```

## Available Endpoints

| HTTP Method | Endpoint | Description |
|------------|----------|-------------|
| GET | `/api/miembros` | Get all members |
| GET | `/api/miembros/{id}` | Get member by ID |
| POST | `/api/miembros` | Create new member |
| PUT | `/api/miembros/{id}` | Update member |
| DELETE | `/api/miembros/{id}` | Delete member |

## Example API Calls

### Get All Members
```bash
curl -X GET "https://localhost:7147/api/miembros" -H "accept: application/json"
```

### Create New Member
```bash
curl -X POST "https://localhost:7147/api/miembros" \
  -H "accept: application/json" \
  -H "Content-Type: application/json" \
  -d '{
    "nombre": "Juan",
    "apellido": "Pérez",
    "celular": "3001234567",
    "correoElectronico": "juan.perez@example.com",
    "cargo": "Miembro",
    "rango": "Full Color"
  }'
```

### Update Member
```bash
curl -X PUT "https://localhost:7147/api/miembros/1" \
  -H "accept: application/json" \
  -H "Content-Type: application/json" \
  -d '{
    "id": 1,
    "nombre": "Juan",
    "apellido": "Pérez García",
    "celular": "3001234567",
    "correoElectronico": "juan.perez@example.com",
    "cargo": "Miembro",
    "rango": "Full Color"
  }'
```

### Delete Member
```bash
curl -X DELETE "https://localhost:7147/api/miembros/1" -H "accept: application/json"
```

## Troubleshooting

### Database Connection Issues

If you encounter database connection errors:

1. Verify SQL Server is running
2. Check firewall rules allow connection to SQL Server
3. Verify connection string is correct
4. For Azure SQL, ensure your IP is allowed in firewall rules

### Migration Issues

If migrations fail:

```bash
# Drop the database and recreate
dotnet ef database drop --project ../LAMAMedellin.Infrastructure --startup-project .
dotnet ef database update --project ../LAMAMedellin.Infrastructure --startup-project .
```

## Next Steps

1. Configure Azure Active Directory B2C for authentication
2. Implement file upload for member photos
3. Add search and filtering capabilities
4. Create data export/import functionality
5. Deploy to Azure App Service

## Support

For issues or questions, please create an issue in the GitHub repository.
