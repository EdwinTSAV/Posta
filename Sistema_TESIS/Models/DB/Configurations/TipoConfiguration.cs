using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_TESIS.Models.DB.Configurations
{
    public class TipoConfiguration : IEntityTypeConfiguration<Tipo>
    {
        public void Configure(EntityTypeBuilder<Tipo> builder)
        {
            builder.ToTable("Tipo");
            builder.HasKey(o => o.TipoId);
            builder.HasMany(o => o.Usuario).WithOne(o => o.Tipo).HasForeignKey(o => o.TipoId);
        }
    }
}
