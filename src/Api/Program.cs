using Aplicacion.Interfaces.Repositorios;
using Aplicacion.Interfaces.Servicios;
using Aplicacion.Servicios;
using Api.Middleware;
using Infraestructura.Contexto;
using Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddControllers(options =>
{
    // Agregar filtro de validación del modelo
    options.Filters.Add<Api.Filters.ModelValidationFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var scopes = builder.Configuration
        .GetSection("Authentication:Scopes")
        .Get<Dictionary<string, string>>();
    if (scopes == null || scopes.Count == 0)
    {
        throw new InvalidOperationException("Authentication:Scopes configuration is missing or empty.");
    }
    var authorizationUrl = builder.Configuration.GetValue<string>("Authentication:AuthorizationUrl")
        ?? throw new ArgumentNullException("Authentication:AuthorizationUrl");
    var tokenUrl = builder.Configuration.GetValue<string>("Authentication:TokenUrl")
        ?? throw new ArgumentNullException("Authentication:TokenUrl");

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "L.A.M.A API",
        Description = "API created to handle the backend tasks of the L.A.M.A "
    });
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "OAuth2.0 Auth Code with PKCE",
        Name = "oauth2",
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri(authorizationUrl),
                TokenUrl = new Uri(tokenUrl),
                Scopes = scopes
            }
        }
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
            },
            scopes.Keys.ToArray()
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Configurar Entity Framework
builder.Services.AddDbContext<MiembrosContexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar repositorios
builder.Services.AddScoped<IRepositorioMiembro, RepositoryMiembro>();

// Registrar servicios de aplicación
builder.Services.AddScoped<IMiembroService, MiembroService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddMicrosoftIdentityWebApi(op =>
  {
      builder.Configuration.Bind("AzureAdB2C", op);
      op.TokenValidationParameters.NameClaimType = "name";
  },
  op =>
  {
      op.Instance = builder.Configuration.GetValue<string>("AzureAdB2C:Instance") ?? throw new ArgumentNullException("AzureAdB2C:Instance");
      op.TenantId = builder.Configuration.GetValue<string>("AzureAdB2C:TenantId") ?? throw new ArgumentNullException("AzureAdB2C:TenantId");
      op.ClientId = builder.Configuration.GetValue<string>("AzureAdB2C:ClientId") ?? throw new ArgumentNullException("AzureAdB2C:ClientId");
  });

var app = builder.Build();

// Configure the HTTP request pipeline.
// IMPORTANTE: El middleware de manejo de errores debe ir al principio del pipeline
app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "L.A.M.A API v1");
        var clientId = builder.Configuration.GetValue<string>("AzureAdB2C:ClientId")
            ?? throw new ArgumentNullException("AzureAdB2C:ClientId");
        var scopes = builder.Configuration
            .GetSection("Authentication:Scopes")
            .Get<Dictionary<string, string>>();
        if (scopes == null || scopes.Count == 0)
        {
            throw new InvalidOperationException("Authentication:Scopes configuration is missing or empty.");
        }
        options.OAuthClientId(clientId);
        options.OAuthUsePkce();
        options.OAuthScopes(scopes.Keys.ToArray());
    });
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
