namespace BpInterface.Core.Models.Dto
{
    public class CuentaResponse
    {
        public string NumeroCuenta { get; set; } = null!;
        public string? TipoCuenta { get; set; }
        public decimal? SaldoInicial { get; set; }
        public bool? Estado { get; set; }
        public int LimiteDiario { get; set; }
        public string ClienteNombres { get; set; } = null!;
    }
}
