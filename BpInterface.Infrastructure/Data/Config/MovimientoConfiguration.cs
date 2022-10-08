using BpInterface.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BpInterface.Infrastructure.Data.Config
{
    public class MovimientoConfiguration : IEntityTypeConfiguration<Movimiento>
    {
        public void Configure(EntityTypeBuilder<Movimiento> builder)
        {
            builder.ToTable("Movimiento");

            builder.HasKey(e => e.NumeroComprobante);

            builder.Property(e => e.Fecha).HasColumnType("date");

            builder.Property(e => e.NumeroCuenta)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.SaldoInicial).HasColumnType("decimal(18, 2)");

            builder.Property(e => e.TipoMovimiento)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.Valor).HasColumnType("decimal(18, 2)");

            builder.HasOne(d => d.Cuenta)
                .WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.NumeroCuenta)
                .HasConstraintName("FK_Movimientos_Cuenta");
        }
    }
}
