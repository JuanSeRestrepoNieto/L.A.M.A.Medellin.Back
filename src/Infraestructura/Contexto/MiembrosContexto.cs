

public class MiembrosContexto : DbContext
{
    public MiembrosContexto(DbContextOptions<MiembrosContexto> options) : base(options)
    {
    }

    public DbSet<DataModelMiembro> Miembros { get; set; }
}