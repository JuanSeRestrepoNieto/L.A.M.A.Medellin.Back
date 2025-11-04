# L.A.M.A.Medellin.WebApp
Aplicación web centralizada que permita almacenar, gestionar y acceder a la información de los miembros de forma segura y fácil.

## Problema
  - El capítulo L.A.M.A. Medellín necesita una forma eficiente y segura de gestionar la información de sus miembros.
  - La información actual está dispersa y difícil de acceder, lo que dificulta la comunicación y la toma de decisiones.

## Solución
  -	Desarrollar una aplicación web centralizada que permita almacenar, gestionar y acceder a la información de los miembros de forma segura y fácil.
  -	Implementar funcionalidades de búsqueda, filtrado, exportación e importación de datos.
  -	Integrar autenticación de redes sociales para facilitar el acceso a la aplicación.https://github.com/usuario-dev/reporte-horas-extra)

## Beneficios
  -	Mejora la eficiencia y la productividad del capítulo.
  -	Centraliza la información y facilita el acceso a ella.
  -	Mejora la comunicación entre los miembros.
  -	Fortalece la seguridad de la información.
  -	Permite generar informes y estadísticas sobre los miembros.

## Usuarios
  -	Miembros del capítulo L.A.M.A. Medellín.
  -	Administradores del capítulo.

## Arquitectura de la Aplicación

### Backend ASP.NET Core Web API
  - C# y .NET para construir API RESTful.
  - Entity Framework Core para la interacción con la base de datos.

### Autenticación
  - Azure Active Directory B2C: Para la autenticación de usuarios y la integración con redes sociales (Facebook, Google, etc.).

### Desarrollo del Backend
  - Crear un nuevo proyecto de ASP.NET Core Web API o Azure Functions en Visual Studio.
  - Definir los modelos de datos y los controladores para las operaciones CRUD.
  - Configurar Entity Framework Core para la conexión a Azure SQL Database.
  - Implementar la seguridad con Azure Active Directory.
  - Crear los metodos para recibir los tokens de autentificación de redes sociales.

### Integración y Pruebas
  - Integrar el frontend con el backend y probar todas las funcionalidades.
  - Realizar pruebas unitarias y de integración.
  - Probar la autentificación de redes sociales.

### Crear la Base de Datos Azure SQL Database
  - En el portal de Azure, crea un nuevo recurso de Azure SQL Database.
  - Configura el servidor y la base de datos.
  - Abre el firewall del servidor para permitir conexiones desde tu dirección IP.

Diseñar la tabla "Miembros": 
o	Utilizando SSMS o el editor de consultas del portal de Azure, crea la tabla "Miembros" con las siguientes columnas: 

CREATE TABLE Miembros (
Id INT PRIMARY KEY IDENTITY,
Nombre VARCHAR(100),
Apellido VARCHAR(100),
Celular VARCHAR(20),
CorreoElectronico VARCHAR(100),
FechaIngreso DATE,
Direccion VARCHAR(255),
Member INT,
Cargo VARCHAR(100),
Rango VARCHAR(100),
Estatus VARCHAR(50),
FechaNacimiento DATE,
Cedula VARCHAR(20),
RH VARCHAR(5),
EPS VARCHAR(100),
Padrino VARCHAR(100),
Foto VARCHAR(255),
ContactoEmergencia VARCHAR(255),
Ciudad VARCHAR(100)
Moto VARCHAR(100),
AnoModelo INT,
Marca VARCHAR(100),
CilindrajeCC INT,
PlacaMatricula VARCHAR(20),
FechaExpedicionLicenciaConduccion DATE,
FechaExpedicionSOAT DATE;
);
-	Id (INT, clave principal, identidad)
-	Nombre (VARCHAR(100))
-	Apellido (VARCHAR(100))
-	Celular (VARCHAR(20))
-	CorreoElectronico (VARCHAR(100))
-	FechaIngreso (DATE)
-	Direccion (VARCHAR(255))
-	Member (INT) (Número de miembro)
-	Cargo (VARCHAR(100))
-	Rango (VARCHAR(100)) (Full Color, Rockets o Prospect)
-	Estatus (VARCHAR(50))
-	FechaNacimiento (DATE)
-	Cedula (VARCHAR(20))
-	RH (VARCHAR(5))
-	EPS (VARCHAR(100))
-	Padrino (VARCHAR(100)) (Es alguno de los miembros siempre)
-	Foto (VARCHAR(255))
-	ContactoEmergencia (VARCHAR(255)) (Nombre, Relación y Teléfono de contacto)
-	Ciudad (VARCHAR(100))

Migraciones de Entity Framework Core

Crear los modelos de datos (Models/Miembro.cs): 
o	Crea una clase para representar la entidad "Miembro".
public class Miembro
{
    public int Id { get; set; }
    public string Nombre { get; set; }
        // ... otras propiedades existentes
    public string Moto { get; set; }
    public int? AnoModelo { get; set; } // Usar int? para permitir valores nulos
    public string Marca { get; set; }
    public int? CilindrajeCC { get; set; }
    public string PlacaMatricula { get; set; }
    public DateTime? FechaExpedicionLicenciaConduccion { get; set; }
    public DateTime? FechaExpedicionSOAT { get; set; }
}

6.	Crear el DbContext (Data/ApplicationDbContext.cs):
o	Crea una clase que herede de DbContext para interactuar con la base de datos.
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Miembro> Miembros { get; set; }
}

7.	Configurar DbContext (Program.cs):
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

8.	Crear los controladores (Controllers/MiembrosController.cs): 
o	Crea controladores para manejar las operaciones CRUD sobre la tabla "Miembros".
o	Implementa la lógica de autenticación y autorización.
