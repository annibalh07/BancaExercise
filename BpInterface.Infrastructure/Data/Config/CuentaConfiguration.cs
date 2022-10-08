using BpInterface.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BpInterface.Infrastructure.Data.Config
{
    public class CuentaConfiguration : IEntityTypeConfiguration<Cuenta>
    {
        public void Configure(EntityTypeBuilder<Cuenta> builder)
        {
            builder.ToTable("Cuenta");

            builder.HasKey(e => e.NumeroCuenta);

            builder.Property(e => e.Estado)
                    .HasDefaultValueSql("((1))");

            builder.Property(e => e.NumeroCuenta)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.Saldo).HasColumnType("decimal(18, 2)");

            builder.Property(e => e.TipoCuenta)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.HasOne(d => d.Cliente)
                .WithMany(p => p.CuentasBancarias)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK_Cuenta_Cliente");
        }
    }
}
