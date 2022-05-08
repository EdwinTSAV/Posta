using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sistema_TESIS.Models.DB.Configurations
{
    public class CuadroClinicoConfiguration : IEntityTypeConfiguration<CuadroClinico>
    {
        public void Configure(EntityTypeBuilder<CuadroClinico> builder)
        {
            builder.ToTable("CuadroClinico");
            builder.HasKey(o => o.CuadroClinicoId);

            builder.HasOne(o => o.Usuario)
                .WithMany()
                .HasForeignKey(o => o.UsuarioId);

            builder.HasMany(o => o.Recetas)
                .WithOne()
                .HasForeignKey(o => o.CuadroClinicoId);
        }
    }
}
