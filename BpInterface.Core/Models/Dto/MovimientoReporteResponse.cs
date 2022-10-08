namespace BpInterface.Core.Models.Dto
{
    public class MovimientoReporteResponse
    {
        public DateTime Fecha { get; set; }
        public string NombreCliente { get; set; } = null!;
        public int NumeroComprobante { get; set; }
        public string NumeroCuenta { get; set; } = null!;
        public string TipoCuenta { get; set; } = null!;
        public decimal SaldoInicial { get; set; }
        public decimal Movimiento { get; set; }        
        public decimal SaldoDisponible { get; set; }
    }
}
