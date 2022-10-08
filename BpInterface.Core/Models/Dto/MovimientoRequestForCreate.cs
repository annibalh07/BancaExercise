namespace BpInterface.Core.Models.Dto
{
    public class MovimientoRequestForCreate
    {
        public string NumeroCuenta { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; } = null!;
        public decimal Valor { get; set; }
    }
}
