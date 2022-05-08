
using Microsoft.EntityFrameworkCore;
using Sistema_TESIS.Models.DB.Configurations;

namespace Sistema_TESIS.Models.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<CuadroClinico> CuadrosClinicos { get; set; }
        public DbSet<Receta> Recetas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Configuracion de tablas
            modelBuilder.ApplyConfiguration(new PersonaConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new TipoConfiguration());
            modelBuilder.ApplyConfiguration(new CuadroClinicoConfiguration());
            modelBuilder.ApplyConfiguration(new RecetaConfiguration());
        }
    }
}
