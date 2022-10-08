using BpInterface.Core;
using BpInterface.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BpInterface.Infrastructure.Data
{
    public class BpContextSeed
    {
        public static async Task SeedAsync(BpContext dbContext,
        ILogger? logger = null,
        int retry = 0)
        {
            var retryForAvailability = retry;
            try
            {
                if (dbContext.Database.IsSqlServer())
                {
                    await dbContext.Database.EnsureDeletedAsync();
                    await dbContext.Database.EnsureCreatedAsync();
                }

                if (!await dbContext.Clientes.AnyAsync())
                {
                    await dbContext.Clientes.AddRangeAsync(
                        GetPreconfiguredClientes());

                    await dbContext.SaveChangesAsync();
                }

                if (!await dbContext.Cuentas.AnyAsync())
                {
                    await dbContext.Cuentas.AddRangeAsync(
                        GetPreconfiguredCuentas());

                    await dbContext.SaveChangesAsync();
                }

                if (!await dbContext.Movimientos.AnyAsync())
                {
                    await dbContext.Movimientos.AddRangeAsync(
                        GetPreconfiguredMovimientos());

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability >= 10) throw;

                retryForAvailability++;

                logger!.LogError(ex.Message);
                await SeedAsync(dbContext, logger, retryForAvailability);
                throw;
            }
        }

        private static IEnumerable<Movimiento> GetPreconfiguredMovimientos()
        {
            return new List<Movimiento>
            {
               new Movimiento{ NumeroCuenta = "2200568049", Fecha = DateTime.Now, Valor=500, SaldoInicial = 0, TipoMovimiento = TipoMovimiento.Deposito }
            };
        }

        private static IEnumerable<Cuenta> GetPreconfiguredCuentas()
        {
            return new List<Cuenta>
            {
                new Cuenta{  ClienteId = 1, LimiteDiario = 1000, Saldo = 500, TipoCuenta = TipoCuentas.Ahorros, NumeroCuenta = "2200568049" }
            };
        }

        private static IEnumerable<Cliente> GetPreconfiguredClientes()
        {
            return new List<Cliente>
            {
                new Cliente{ Contrasenia = "pericles567", Direccion = "Av. Brasil", Edad = 33, Genero = GeneroPersona.Masculino, Identificacion = "1206121587", Nombres = "Fernando Salazar", Telefono = "+5930997202316" }
            };
        }
    }
}
