namespace BpInterface.Infrastructure.Data.Entities
{
    public class Cuenta
    {
        public Cuenta()
        {
            Movimientos = new HashSet<Movimiento>();
        }

        public string NumeroCuenta { get; set; } = null!;
        public int? ClienteId { get; set; }
        public string TipoCuenta { get; set; } = null!;
        public decimal Saldo { get; set; }
        public bool? Estado { get; set; }
        public int LimiteDiario { get; set; }
        public virtual Cliente? Cliente { get; set; }
        public virtual ICollection<Movimiento> Movimientos { get; set; }
    }
}
