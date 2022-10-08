namespace BpInterface.Core.Models.Dto
{
    public class CuentaRequestForUpdate
    {
        public string NumeroCuenta { get; set; } = null!;
        public string TipoCuenta { get; set; } = null!;
        public decimal SaldoInicial { get; set; }
        public bool? Estado { get; set; }
        public int LimiteDiario { get; set; }
    }
}
