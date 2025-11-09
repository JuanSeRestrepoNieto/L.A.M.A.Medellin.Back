using Infraestructura.DataEntities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Contexto;

public class MiembrosContexto : DbContext
{
    public MiembrosContexto(DbContextOptions<MiembrosContexto> options) : base(options)
    {
    }

    public DbSet<DataModelMiembro> Miembros { get; set; }
}