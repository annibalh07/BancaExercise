namespace BpInterface.Core.Models.Dto
{
    public class CuentaRequestForCreate
    {
        public string NumeroCuenta { get; set; } = null!;
        public string TipoCuenta { get; set; } = null!;
        public decimal Saldo { get; set; }
        public bool? Estado { get; set; }
        public int LimiteDiario { get; set; }
        public string IdentificacionCliente { get; set; } = null!;
    }
}
