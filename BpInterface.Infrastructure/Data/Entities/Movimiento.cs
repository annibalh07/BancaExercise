namespace BpInterface.Infrastructure.Data.Entities
{
    public class Movimiento
    {
        public int NumeroComprobante { get; set; }
        public string NumeroCuenta { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; } = null!;
        public decimal Valor { get; set; }
        public decimal SaldoInicial { get; set; }

        public virtual Cuenta? Cuenta { get; set; }
    }
}
